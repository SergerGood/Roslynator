// Copyright (c) Josef Pihrt and Contributors. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Immutable;
using System.Composition;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Roslynator.CodeFixes;

namespace Roslynator.CSharp.CodeFixes
{
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(TokenCodeFixProvider))]
    [Shared]
    public sealed class TokenCodeFixProvider : BaseCodeFixProvider
    {
        public override ImmutableArray<string> FixableDiagnosticIds
        {
            get { return ImmutableArray.Create(DiagnosticIdentifiers.UnnecessaryNullForgivingOperator); }
        }

        public override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            SyntaxNode root = await context.GetSyntaxRootAsync().ConfigureAwait(false);

            if (!TryFindToken(root, context.Span.Start, out SyntaxToken token))
                return;

            Diagnostic diagnostic = context.Diagnostics[0];
            Document document = context.Document;

            switch (diagnostic.Id)
            {
                case DiagnosticIdentifiers.UnnecessaryNullForgivingOperator:
                    {
                        CodeAction codeAction = CodeAction.Create(
                            "Remove null-forgiving operator",
                            ct =>
                            {
                                var expression = (PostfixUnaryExpressionSyntax)token.Parent;

                                ExpressionSyntax newExpression = expression.Operand.AppendToTrailingTrivia(token.LeadingAndTrailingTrivia());

                                return document.ReplaceNodeAsync(expression, newExpression, ct);
                            },
                            GetEquivalenceKey(diagnostic));

                        context.RegisterCodeFix(codeAction, diagnostic);
                        break;
                    }
            }
        }
    }
}
