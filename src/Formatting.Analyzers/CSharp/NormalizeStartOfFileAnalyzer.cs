﻿// Copyright (c) Josef Pihrt and Contributors. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Text;
using Roslynator.CSharp;

namespace Roslynator.Formatting.CSharp
{
    //TODO: prefer_newline_before_using_directive,  prefer_newline_between_start_of_file_and_using_directive
    //TODO: prefer_newline_before_declaration, prefer_newline_between_start_of_file_and_declaration
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public sealed class NormalizeStartOfFileAnalyzer : BaseDiagnosticAnalyzer
    {
        private static ImmutableArray<DiagnosticDescriptor> _supportedDiagnostics;

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
        {
            get
            {
                if (_supportedDiagnostics.IsDefault)
                    Immutable.InterlockedInitialize(ref _supportedDiagnostics, DiagnosticRules.NormalizeStartOfFile);

                return _supportedDiagnostics;
            }
        }

        public override void Initialize(AnalysisContext context)
        {
            base.Initialize(context);

            context.RegisterSyntaxNodeAction(f => AnalyzeCompilationUnit(f), SyntaxKind.CompilationUnit);
        }

        private static void AnalyzeCompilationUnit(SyntaxNodeAnalysisContext context)
        {
            var compilationUnit = (CompilationUnitSyntax)context.Node;

            if (compilationUnit.Span.Length == 0)
                return;

            SyntaxToken token = compilationUnit.EndOfFileToken;

            if (token.FullSpan.Start > 0)
            {
                token = compilationUnit.GetFirstToken();

                SyntaxDebug.Assert(token.FullSpan.Start == 0, token);

                if (token.FullSpan.Start > 0)
                    return;
            }

            SyntaxTriviaList.Enumerator en = token.LeadingTrivia.GetEnumerator();

            if (en.MoveNext()
                && en.Current.IsWhitespaceOrEndOfLineTrivia())
            {
                ReportDiagnostic(context, token);
            }

            static void ReportDiagnostic(SyntaxNodeAnalysisContext context, SyntaxToken token)
            {
                DiagnosticHelpers.ReportDiagnostic(
                    context,
                    DiagnosticRules.NormalizeStartOfFile,
                    Location.Create(token.SyntaxTree, new TextSpan(0, 0)));
            }
        }
    }
}
