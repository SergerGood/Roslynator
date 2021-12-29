// Copyright (c) Josef Pihrt and Contributors. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Text;

namespace Roslynator.CSharp.Analysis
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public sealed class UseExplicitlyOrImplicitlyTypedArrayAnalyzer : BaseDiagnosticAnalyzer
    {
        private static ImmutableArray<DiagnosticDescriptor> _supportedDiagnostics;

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
        {
            get
            {
                if (_supportedDiagnostics.IsDefault)
                    Immutable.InterlockedInitialize(ref _supportedDiagnostics, DiagnosticRules.UseExplicitlyOrImplicitlyTypedArray, CommonDiagnosticRules.RequiredOptionNotSetForAnalyzer);

                return _supportedDiagnostics;
            }
        }

        public override void Initialize(AnalysisContext context)
        {
            base.Initialize(context);

            context.RegisterSyntaxNodeAction(
                c =>
                {
                    ArrayCreationKind kind = GetArrayCreationKind(c);

                    if (kind == ArrayCreationKind.Explicit
                        || kind == ArrayCreationKind.ImplicitWhenTypeIsObvious)
                    {
                        AnalyzeImplicitArrayCreationExpression(c, kind);
                    }
                },
                SyntaxKind.ImplicitArrayCreationExpression);

            context.RegisterSyntaxNodeAction(
                c =>
                {
                    ArrayCreationKind kind = GetArrayCreationKind(c);

                    if (kind == ArrayCreationKind.Implicit
                        || kind == ArrayCreationKind.ImplicitWhenTypeIsObvious)
                    {
                        AnalyzeArrayCreationExpression(c, kind);
                    }
                },
                SyntaxKind.ArrayCreationExpression);
        }

        private ArrayCreationKind GetArrayCreationKind(SyntaxNodeAnalysisContext context)
        {
            if (context.IsEnabled(ConfigOptions.PreferExplicitlyTypedArray))
                return ArrayCreationKind.Explicit;

            if (context.IsEnabled(ConfigOptions.PreferImplicitlyTypedArrayWhenTypeIsObvious))
                return ArrayCreationKind.ImplicitWhenTypeIsObvious;

            if (context.IsEnabled(ConfigOptions.PreferImplicitlyTypedArray))
                return ArrayCreationKind.Implicit;

            if (context.IsEnabled(LegacyConfigOptions.UseImplicitlyTypedArrayWhenTypeIsObvious))
                return ArrayCreationKind.ImplicitWhenTypeIsObvious;

            if (context.IsEnabled(LegacyConfigOptions.UseImplicitlyTypedArray))
                return ArrayCreationKind.Implicit;

            context.ReportRequiredOptionNotSet(DiagnosticRules.UseExplicitlyOrImplicitlyTypedArray);

            return ArrayCreationKind.None;
        }

        private static void AnalyzeImplicitArrayCreationExpression(SyntaxNodeAnalysisContext context, ArrayCreationKind kind)
        {
            var expression = (ImplicitArrayCreationExpressionSyntax)context.Node;

            if (expression.ContainsDiagnostics)
                return;

            if (expression.NewKeyword.ContainsDirectives)
                return;

            if (expression.OpenBracketToken.ContainsDirectives)
                return;

            if (expression.CloseBracketToken.ContainsDirectives)
                return;

            if (kind == ArrayCreationKind.ImplicitWhenTypeIsObvious)
            {
                InitializerExpressionSyntax initializer = expression.Initializer;

                if (initializer != null)
                {
                    var isObvious = false;

                    foreach (ExpressionSyntax expression2 in initializer.Expressions)
                    {
                        isObvious = CSharpTypeAnalysis.IsTypeObvious(expression2, null, context.SemanticModel, context.CancellationToken);

                        if (!isObvious)
                            break;
                    }

                    if (isObvious)
                        return;
                }
            }

            if (context.SemanticModel.GetTypeSymbol(expression, context.CancellationToken) is not IArrayTypeSymbol arrayTypeSymbol)
                return;

            if (!arrayTypeSymbol.ElementType.SupportsExplicitDeclaration())
                return;

            Location location = Location.Create(expression.SyntaxTree, TextSpan.FromBounds(expression.NewKeyword.SpanStart, expression.CloseBracketToken.Span.End));

            DiagnosticHelpers.ReportDiagnostic(
                context,
                DiagnosticRules.UseExplicitlyOrImplicitlyTypedArray,
                location,
                "explicitly");
        }

        private static void AnalyzeArrayCreationExpression(SyntaxNodeAnalysisContext context, ArrayCreationKind kind)
        {
            var arrayCreation = (ArrayCreationExpressionSyntax)context.Node;

            if (arrayCreation.ContainsDiagnostics)
                return;

            ArrayTypeSyntax arrayType = arrayCreation.Type;

            if (arrayType.ContainsDirectives)
                return;

            SeparatedSyntaxList<ExpressionSyntax> expressions = arrayCreation.Initializer?.Expressions ?? default;

            if (!expressions.Any())
                return;

            if (kind == ArrayCreationKind.ImplicitWhenTypeIsObvious)
            {
                foreach (ExpressionSyntax expression in expressions)
                {
                    if (!CSharpTypeAnalysis.IsTypeObvious(expression, null, context.SemanticModel, context.CancellationToken))
                        return;
                }
            }

            TypeSyntax elementType = arrayType.ElementType;
            SyntaxList<ArrayRankSpecifierSyntax> rankSpecifiers = arrayType.RankSpecifiers;

            TextSpan textSpan = TextSpan.FromBounds(
                elementType.SpanStart,
                ((rankSpecifiers.Count > 1) ? rankSpecifiers.LastButOne() : (SyntaxNode)elementType).Span.End);

            Location location = Location.Create(arrayCreation.SyntaxTree, textSpan);

            DiagnosticHelpers.ReportDiagnostic(
                context,
                DiagnosticRules.UseExplicitlyOrImplicitlyTypedArray,
                location,
                "implicitly");
        }

        private enum ArrayCreationKind
        {
            None,
            Explicit,
            Implicit,
            ImplicitWhenTypeIsObvious,
        }
    }
}
