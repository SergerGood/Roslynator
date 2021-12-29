// Copyright (c) Josef Pihrt and Contributors. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Roslynator.CSharp.CSharpFactory;

namespace Roslynator.CodeGeneration.CSharp
{
    internal class WrapArgumentsRewriter : CSharpSyntaxRewriter
    {
        private int _classDeclarationDepth;
        private int _maxArgumentNameLength;

        public static WrapArgumentsRewriter Instance { get; } = new();

        public override SyntaxNode VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            _classDeclarationDepth++;
            SyntaxNode result = base.VisitClassDeclaration(node);
            _classDeclarationDepth--;

            return result;
        }

        public override SyntaxNode VisitFieldDeclaration(FieldDeclarationSyntax node)
        {
            node = (FieldDeclarationSyntax)base.VisitFieldDeclaration(node);

            return node.AppendToTrailingTrivia(NewLine());
        }

        public override SyntaxNode VisitArgument(ArgumentSyntax node)
        {
            if (node.NameColon != null)
            {
                return node
                    .WithNameColon(node.NameColon.AppendToLeadingTrivia(TriviaList(NewLine(), Whitespace(new string(' ', 4 * (2 + _classDeclarationDepth))))))
                    .WithExpression(node.Expression.PrependToLeadingTrivia(Whitespace(new string(' ', _maxArgumentNameLength - node.NameColon.Name.Identifier.ValueText.Length))));
            }

            return node;
        }

        public override SyntaxNode VisitArgumentList(ArgumentListSyntax node)
        {
            _maxArgumentNameLength = node.Arguments.Max(f => f.NameColon?.Name.Identifier.ValueText.Length ?? 0);
            SyntaxNode result = base.VisitArgumentList(node);
            _maxArgumentNameLength = 0;

            return result;
        }

        public override SyntaxNode VisitAttribute(AttributeSyntax node)
        {
            return node;
        }
    }
}
