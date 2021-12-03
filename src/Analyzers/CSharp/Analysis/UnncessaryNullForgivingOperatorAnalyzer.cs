// Copyright (c) Josef Pihrt and Contributors. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Roslynator.CSharp.Analysis
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public sealed class UnncessaryNullForgivingOperatorAnalyzer : BaseDiagnosticAnalyzer
    {
        private static ImmutableArray<DiagnosticDescriptor> _supportedDiagnostics;

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
        {
            get
            {
                if (_supportedDiagnostics.IsDefault)
                    Immutable.InterlockedInitialize(ref _supportedDiagnostics, DiagnosticRules.UnnecessaryNullForgivingOperator);

                return _supportedDiagnostics;
            }
        }

        public override void Initialize(AnalysisContext context)
        {
            base.Initialize(context);

            context.RegisterSyntaxNodeAction(f => AnalyzeSuppressNullableWarningExpression(f), SyntaxKind.SuppressNullableWarningExpression);
        }

        private static void AnalyzeSuppressNullableWarningExpression(SyntaxNodeAnalysisContext context)
        {
            var expression = (PostfixUnaryExpressionSyntax)context.Node;

            ExpressionSyntax operand = expression.Operand.WalkDownParentheses();

            if (!operand.IsKind(
                SyntaxKind.NullLiteralExpression,
                SyntaxKind.DefaultLiteralExpression,
                SyntaxKind.DefaultExpression))
            {
                return;
            }

            SyntaxNode parent = expression.Parent;

            SyntaxDebug.Assert(parent.IsKind(SyntaxKind.EqualsValueClause), expression.Parent);

            if (!parent.IsKind(SyntaxKind.EqualsValueClause))
                return;

            parent = parent.Parent;

            if (parent.IsKind(SyntaxKind.PropertyDeclaration))
            {
                var property = (PropertyDeclarationSyntax)expression.Parent.Parent;

                if (IsNullableReferenceType(context, property.Type))
                    ReportDiagnostic(context, expression);
            }
            else
            {
                SyntaxDebug.Assert(
                    parent.IsKind(SyntaxKind.VariableDeclarator)
                        && parent.IsParentKind(SyntaxKind.VariableDeclaration)
                        && parent.Parent.IsParentKind(SyntaxKind.FieldDeclaration, SyntaxKind.LocalDeclarationStatement),
                    parent);

                if (parent.IsKind(SyntaxKind.VariableDeclarator)
                    && parent.IsParentKind(SyntaxKind.VariableDeclaration)
                    && parent.Parent.IsParentKind(SyntaxKind.FieldDeclaration, SyntaxKind.LocalDeclarationStatement))
                {
                    var variableDeclaration = (VariableDeclarationSyntax)parent.Parent;

                    if (IsNullableReferenceType(context, variableDeclaration.Type))
                        ReportDiagnostic(context, expression);
                }
            }

            static bool IsNullableReferenceType(SyntaxNodeAnalysisContext context, TypeSyntax type)
            {
                if (!type.IsKind(SyntaxKind.NullableType))
                    return false;

                ITypeSymbol typeSymbol = context.SemanticModel.GetTypeSymbol(type, context.CancellationToken);

                return !typeSymbol.IsErrorType()
                    && typeSymbol.IsReferenceType;
            }

            static void ReportDiagnostic(SyntaxNodeAnalysisContext context, PostfixUnaryExpressionSyntax expression)
            {
                context.ReportDiagnostic(DiagnosticRules.UnnecessaryNullForgivingOperator, expression.OperatorToken);
            }
        }
    }
}
