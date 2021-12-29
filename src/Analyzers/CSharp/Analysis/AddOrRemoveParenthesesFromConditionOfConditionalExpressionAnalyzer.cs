// Copyright (c) Josef Pihrt and Contributors. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Roslynator.CSharp.Analysis
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public sealed class AddOrRemoveParenthesesFromConditionOfConditionalExpressionAnalyzer : BaseDiagnosticAnalyzer
    {
        private static ImmutableArray<DiagnosticDescriptor> _supportedDiagnostics;

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
        {
            get
            {
                if (_supportedDiagnostics.IsDefault)
                    Immutable.InterlockedInitialize(ref _supportedDiagnostics, DiagnosticRules.AddOrRemoveParenthesesFromConditionOfConditionalExpression);

                return _supportedDiagnostics;
            }
        }

        public override void Initialize(AnalysisContext context)
        {
            base.Initialize(context);

            context.RegisterSyntaxNodeAction(f => AnalyzeConditionalExpression(f), SyntaxKind.ConditionalExpression);
        }

        private static void AnalyzeConditionalExpression(SyntaxNodeAnalysisContext context)
        {
            var conditionalExpression = (ConditionalExpressionSyntax)context.Node;

            if (conditionalExpression.ContainsDiagnostics)
                return;

            ExpressionSyntax condition = conditionalExpression.Condition;

            if (condition == null)
                return;

            ParenthesesStyle style = GetParenthesesStyle(context);

            if (style == ParenthesesStyle.None)
                return;

            SyntaxKind kind = condition.Kind();

            if (kind == SyntaxKind.ParenthesizedExpression)
            {
                var parenthesizedExpression = (ParenthesizedExpressionSyntax)condition;

                ExpressionSyntax expression = parenthesizedExpression.Expression;

                if (!expression.IsMissing)
                {
                    if (style == ParenthesesStyle.Remove
                        || (style == ParenthesesStyle.RemoveWhenSingleToken
                            && CSharpFacts.IsSingleTokenExpression(expression.Kind())))
                    {
                        DiagnosticHelpers.ReportDiagnostic(context, DiagnosticRules.AddOrRemoveParenthesesFromConditionOfConditionalExpression, condition, "Remove", "from");
                    }
                }
            }
            else if (style == ParenthesesStyle.Add
                || (style == ParenthesesStyle.RemoveWhenSingleToken
                    && !CSharpFacts.IsSingleTokenExpression(kind)))
            {
                DiagnosticHelpers.ReportDiagnostic(context, DiagnosticRules.AddOrRemoveParenthesesFromConditionOfConditionalExpression, condition, "Add", "to");
            }
        }

        private static ParenthesesStyle GetParenthesesStyle(SyntaxNodeAnalysisContext context)
        {
            AnalyzerConfigOptions configOptions = context.GetConfigOptions();

            var style = ParenthesesStyle.None;

            if (configOptions.TryGetValueAsBool(ConfigOptions.ParenthesizeConditionOfConditionalExpression, out bool result))
            {
                style = (result)
                    ? ParenthesesStyle.Add
                    : ParenthesesStyle.Remove;
            }

            if (configOptions.IsEnabled(ConfigOptions.ParenthesizeConditionOfConditionalExpressionWhenItContainsSingleToken))
                style = ParenthesesStyle.RemoveWhenSingleToken;

            if (configOptions.IsEnabled(LegacyConfigOptions.RemoveParenthesesFromConditionOfConditionalExpressionWhenExpressionIsSingleToken))
                style = ParenthesesStyle.Remove;

            return style;
        }

        private enum ParenthesesStyle
        {
            None,
            Add,
            Remove,
            RemoveWhenSingleToken,

        }
    }
}
