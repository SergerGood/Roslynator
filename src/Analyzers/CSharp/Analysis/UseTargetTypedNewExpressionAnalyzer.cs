// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Roslynator.CSharp.Analysis
{
    // prefer_target_typed_new_expression_when_type_is_obvious
    //  throw
    //  property initialization
    //  field initialization
    //  local initialization
    //  return (single statement)
    //  expression body
    //  array initializer
    //
    // return
    // yield return
    // assignment
    // coalesce ?
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public sealed class UseTargetTypedNewExpressionAnalyzer : BaseDiagnosticAnalyzer
    {
        private static ImmutableArray<DiagnosticDescriptor> _supportedDiagnostics;

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
        {
            get
            {
                if (_supportedDiagnostics.IsDefault)
                    Immutable.InterlockedInitialize(ref _supportedDiagnostics, DiagnosticRules.UseTargetTypedNewExpression);

                return _supportedDiagnostics;
            }
        }

        public override void Initialize(AnalysisContext context)
        {
            base.Initialize(context);

            context.RegisterSyntaxNodeAction(f => AnalyzeObjectCreationExpression(f), SyntaxKind.ObjectCreationExpression);
        }

        private static void AnalyzeObjectCreationExpression(SyntaxNodeAnalysisContext context)
        {
            var objectCreation = (ObjectCreationExpressionSyntax)context.Node;

            SyntaxNode parent = objectCreation.Parent;

            InitializerExpressionSyntax initializer = objectCreation.Initializer;

            if (initializer != null)
            {
                foreach (ExpressionSyntax expression in initializer.Expressions)
                {
                    if (expression.IsKind(SyntaxKind.ObjectCreationExpression))
                        return;
                }
            }

            switch (parent.Kind())
            {
                case SyntaxKind.ThrowExpression:
                case SyntaxKind.ThrowStatement:
                    {
                        if (context.SemanticModel.GetTypeSymbol(objectCreation, context.CancellationToken)?
                            .HasMetadataName(MetadataNames.System_Exception) == true)
                        {
                            ReportDiagnostic(context, objectCreation);
                        }

                        break;
                    }
                case SyntaxKind.EqualsValueClause:
                    {
                        parent = parent.Parent;

                        SyntaxDebug.Assert(parent.IsKind(SyntaxKind.VariableDeclarator, SyntaxKind.PropertyDeclaration), parent);

                        if (parent.IsKind(SyntaxKind.VariableDeclarator))
                        {
                            parent = parent.Parent;

                            if (parent.IsKind(SyntaxKind.VariableDeclaration))
                            {
                                AnalyzeType(context, objectCreation, ((VariableDeclarationSyntax)parent).Type);
                                return;
                            }
                        }
                        else if (parent.IsKind(SyntaxKind.PropertyDeclaration))
                        {
                            AnalyzeType(context, objectCreation, ((PropertyDeclarationSyntax)parent).Type);
                            return;
                        }

                        break;
                    }
                case SyntaxKind.ReturnStatement:
                case SyntaxKind.YieldReturnStatement:
                    {
                        for (SyntaxNode node = parent.Parent; node != null; node = node.Parent)
                        {
                            if (CSharpFacts.IsAnonymousFunctionExpression(node.Kind()))
                                return;

                            TypeSyntax type = DetermineReturnType(node);

                            if (type != null)
                            {
                                if (parent.IsKind(SyntaxKind.YieldReturnStatement))
                                {
                                    ITypeSymbol typeSymbol = context.SemanticModel.GetTypeSymbol(type, context.CancellationToken);

                                    if (typeSymbol?.OriginalDefinition.SpecialType == SpecialType.System_Collections_Generic_IEnumerable_T)
                                    {
                                        var ienumerableOfT = (INamedTypeSymbol)typeSymbol;

                                        ITypeSymbol typeSymbol2 = ienumerableOfT.TypeArguments.Single();

                                        AnalyzeTypeSymbol(context, objectCreation, typeSymbol2);
                                    }

                                    return;
                                }

                                AnalyzeType(context, objectCreation, type);
                                return;
                            }
                        }

                        SyntaxDebug.Fail(parent);
                        break;
                    }
                case SyntaxKind.ArrowExpressionClause:
                    {
                        TypeSyntax type = DetermineReturnType(parent.Parent);

                        SyntaxDebug.Assert(type != null, parent);

                        if (type != null)
                            AnalyzeType(context, objectCreation, type);

                        break;
                    }
                case SyntaxKind.ArrayInitializerExpression:
                    {
                        if (parent.IsParentKind(SyntaxKind.ArrayCreationExpression))
                        {
                            var arrayCreationExpression = (ArrayCreationExpressionSyntax)parent.Parent;

                            AnalyzeType(context, objectCreation, arrayCreationExpression.Type.ElementType);
                            return;
                        }

                        SyntaxDebug.Assert(parent.IsParentKind(SyntaxKind.ImplicitArrayCreationExpression), parent.Parent);
                        break;
                    }
                case SyntaxKind.CollectionInitializerExpression:
                    {
                        SyntaxDebug.Assert(parent.IsParentKind(SyntaxKind.ObjectCreationExpression, SyntaxKind.SimpleAssignmentExpression), parent.Parent);
                        break;
                    }
                case SyntaxKind.SimpleAssignmentExpression:
                case SyntaxKind.CoalesceAssignmentExpression:
                case SyntaxKind.AddAssignmentExpression:
                case SyntaxKind.SubtractAssignmentExpression:
                    {
                        var assignment = (AssignmentExpressionSyntax)parent;

                        AnalyzeExpression(context, objectCreation, assignment.Left);
                        break;
                    }
                case SyntaxKind.CoalesceExpression:
                    {
                        var coalesceExpression = (BinaryExpressionSyntax)parent;

                        AnalyzeExpression(context, objectCreation, coalesceExpression.Left);
                        break;
                    }
            }
        }

        private static void AnalyzeType(
            SyntaxNodeAnalysisContext context,
            ObjectCreationExpressionSyntax objectCreation,
            TypeSyntax type)
        {
            if (!type.IsVar)
                AnalyzeExpression(context, objectCreation, type);
        }

        private static void AnalyzeExpression(
            SyntaxNodeAnalysisContext context,
            ObjectCreationExpressionSyntax objectCreation,
            ExpressionSyntax expression)
        {
            ITypeSymbol typeSymbol1 = context.SemanticModel.GetTypeSymbol(expression);

            AnalyzeTypeSymbol(context, objectCreation, typeSymbol1);
        }

        private static void AnalyzeTypeSymbol(
            SyntaxNodeAnalysisContext context,
            ObjectCreationExpressionSyntax objectCreation,
            ITypeSymbol typeSymbol1)
        {
            if (typeSymbol1?.IsErrorType() == false)
            {
                ITypeSymbol typeSymbol2 = context.SemanticModel.GetTypeSymbol(objectCreation);

                if (SymbolEqualityComparer.Default.Equals(typeSymbol1, typeSymbol2))
                    ReportDiagnostic(context, objectCreation);
            }
        }

        private static TypeSyntax DetermineReturnType(SyntaxNode node)
        {
            switch (node.Kind())
            {
                case SyntaxKind.LocalFunctionStatement:
                    {
                        return ((LocalFunctionStatementSyntax)node).ReturnType;
                    }
                case SyntaxKind.MethodDeclaration:
                    {
                        return ((MethodDeclarationSyntax)node).ReturnType;
                    }
                case SyntaxKind.OperatorDeclaration:
                    {
                        return ((OperatorDeclarationSyntax)node).ReturnType;
                    }
                case SyntaxKind.ConversionOperatorDeclaration:
                    {
                        return ((ConversionOperatorDeclarationSyntax)node).Type;
                    }
                case SyntaxKind.PropertyDeclaration:
                    {
                        return ((PropertyDeclarationSyntax)node).Type;
                    }
                case SyntaxKind.IndexerDeclaration:
                    {
                        return ((IndexerDeclarationSyntax)node).Type;
                    }
                case SyntaxKind.GetAccessorDeclaration:
                case SyntaxKind.SetAccessorDeclaration:
                case SyntaxKind.AddAccessorDeclaration:
                case SyntaxKind.RemoveAccessorDeclaration:
                case SyntaxKind.InitAccessorDeclaration:
                case SyntaxKind.UnknownAccessorDeclaration:
                    {
                        SyntaxDebug.Assert(node.IsParentKind(SyntaxKind.AccessorList), node.Parent);

                        if (node.IsParentKind(SyntaxKind.AccessorList))
                            return DetermineReturnType(node.Parent.Parent);

                        return null;
                    }
            }

            return null;
        }

        private static void ReportDiagnostic(SyntaxNodeAnalysisContext context, ObjectCreationExpressionSyntax objectCreation)
        {
            DiagnosticHelpers.ReportDiagnostic(context, DiagnosticRules.UseTargetTypedNewExpression, objectCreation.Type);
        }
    }
}
