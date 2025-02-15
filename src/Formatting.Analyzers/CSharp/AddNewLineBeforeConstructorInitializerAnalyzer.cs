﻿// Copyright (c) Josef Pihrt and Contributors. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Roslynator.CSharp;

namespace Roslynator.Formatting.CSharp
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public sealed class AddNewLineBeforeConstructorInitializerAnalyzer : BaseDiagnosticAnalyzer
    {
        private static ImmutableArray<DiagnosticDescriptor> _supportedDiagnostics;

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
        {
            get
            {
                if (_supportedDiagnostics.IsDefault)
                    Immutable.InterlockedInitialize(ref _supportedDiagnostics, DiagnosticRules.AddNewLineBeforeConstructorInitializer);

                return _supportedDiagnostics;
            }
        }

        public override void Initialize(AnalysisContext context)
        {
            base.Initialize(context);

            context.RegisterSyntaxNodeAction(f => AnalyzeConstructorInitializer(f), SyntaxKind.ThisConstructorInitializer);
            context.RegisterSyntaxNodeAction(f => AnalyzeConstructorInitializer(f), SyntaxKind.BaseConstructorInitializer);
        }

        private static void AnalyzeConstructorInitializer(SyntaxNodeAnalysisContext context)
        {
            var constructorInitializer = (ConstructorInitializerSyntax)context.Node;

            SyntaxToken colonToken = constructorInitializer.ColonToken;

            if (colonToken.LeadingTrivia.Any())
                return;

            var constructorDeclaration = (ConstructorDeclarationSyntax)constructorInitializer.Parent;

            if (!constructorDeclaration.ParameterList.GetTrailingTrivia().SingleOrDefault(shouldThrow: false).IsWhitespaceTrivia())
                return;

            DiagnosticHelpers.ReportDiagnostic(
                context,
                DiagnosticRules.AddNewLineBeforeConstructorInitializer,
                Location.Create(colonToken.SyntaxTree, colonToken.Span.WithLength(0)));
        }
    }
}
