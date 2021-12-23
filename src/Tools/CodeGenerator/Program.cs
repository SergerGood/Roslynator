﻿// Copyright (c) Josef Pihrt and Contributors. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Roslynator.CodeGeneration.CSharp;
using Roslynator.Metadata;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Roslynator.CodeGeneration.EditorConfig;

namespace Roslynator.CodeGeneration
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            if (args == null || args.Length == 0)
            {
#if DEBUG
                args = new[] { @"..\..\..\..\.." };
#else
                args = new string[] { Environment.CurrentDirectory };
#endif
            }

            string rootPath = args[0];

            StringComparer comparer = StringComparer.InvariantCulture;

            var metadata = new RoslynatorMetadata(rootPath);

            ImmutableArray<AnalyzerMetadata> analyzers = metadata.Analyzers;
            ImmutableArray<AnalyzerMetadata> codeAnalysisAnalyzers = metadata.CodeAnalysisAnalyzers;
            ImmutableArray<AnalyzerMetadata> formattingAnalyzers = metadata.FormattingAnalyzers;
            ImmutableArray<RefactoringMetadata> refactorings = metadata.Refactorings;
            ImmutableArray<CodeFixMetadata> codeFixes = metadata.CodeFixes;
            ImmutableArray<CompilerDiagnosticMetadata> compilerDiagnostics = metadata.CompilerDiagnostics;

            WriteCompilationUnit(
                @"Refactorings\CSharp\RefactoringDescriptors.Generated.cs",
                RefactoringDescriptorsGenerator.Generate(refactorings.Where(f => !f.IsObsolete), comparer: comparer));

            WriteCompilationUnit(
                @"Refactorings\CSharp\RefactoringIdentifiers.Generated.cs",
                RefactoringIdentifiersGenerator.Generate(refactorings, obsolete: false, comparer: comparer));

            WriteCompilationUnit(
                @"Refactorings\CSharp\RefactoringIdentifiers.Deprecated.Generated.cs",
                RefactoringIdentifiersGenerator.Generate(refactorings, obsolete: true, comparer: comparer));

            WriteCompilationUnit(
                @"VisualStudio.Common\RefactoringsOptionsPage.Generated.cs",
                RefactoringsOptionsPageGenerator.Generate(refactorings.Where(f => !f.IsObsolete), comparer));

            WriteDiagnostics(@"Analyzers\CSharp", analyzers, @namespace: "Roslynator.CSharp", categoryName: nameof(DiagnosticCategories.Roslynator));

            WriteDiagnostics(@"CodeAnalysis.Analyzers\CSharp", codeAnalysisAnalyzers, @namespace: "Roslynator.CodeAnalysis.CSharp", categoryName: nameof(DiagnosticCategories.Roslynator));

            WriteDiagnostics(@"Formatting.Analyzers\CSharp", formattingAnalyzers, @namespace: "Roslynator.Formatting.CSharp", categoryName: nameof(DiagnosticCategories.Roslynator));

            WriteCompilationUnit(
                @"CodeFixes\CSharp\CompilerDiagnosticRules.Generated.cs",
                CompilerDiagnosticRulesGenerator.Generate(compilerDiagnostics, comparer: comparer, @namespace: "Roslynator.CSharp"),
                normalizeWhitespace: false);

            WriteCompilationUnit(
                @"CodeFixes\CSharp\CodeFixDescriptors.Generated.cs",
                CodeFixDescriptorsGenerator.Generate(codeFixes.Where(f => !f.IsObsolete), comparer: comparer, @namespace: "Roslynator.CSharp"),
                normalizeWhitespace: false);

            WriteCompilationUnit(
                @"CodeFixes\CSharp\CodeFixIdentifiers.Generated.cs",
                CodeFixIdentifiersGenerator.Generate(codeFixes, comparer));

            WriteCompilationUnit(
                @"VisualStudio.Common\CodeFixesOptionsPage.Generated.cs",
                CodeFixesOptionsPageGenerator.Generate());

            WriteCompilationUnit(
                @"CSharp\CSharp\CompilerDiagnosticIdentifiers.Generated.cs",
                CompilerDiagnosticIdentifiersGenerator.Generate(compilerDiagnostics, comparer));

            WriteCompilationUnit(
                @"Tools\CodeGeneration\CSharp\Symbols.Generated.cs",
                SymbolsGetKindsGenerator.Generate());

            WriteCompilationUnit(
                @"CSharp\CSharp\SyntaxWalkers\CSharpSyntaxNodeWalker.cs",
                CSharpSyntaxNodeWalkerGenerator.Generate());

            string configFileContent = EditorConfigGenerator.GenerateEditorConfig(metadata);

            File.WriteAllText(
                Path.Combine(rootPath, "../docs/options.editorconfig"),
                configFileContent);

            File.WriteAllText(
                Path.Combine(rootPath, @"VisualStudioCode\package\src\configurationFiles.generated.ts"),
                @"export const configurationFileContent = {
	roslynatorconfig: `is_global = true
"
                    + configFileContent
                    + @"`
};",
                new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));

            Console.WriteLine($"number of analyzers: {analyzers.Count(f => !f.IsObsolete)}");
            Console.WriteLine($"number of code analysis analyzers: {codeAnalysisAnalyzers.Count(f => !f.IsObsolete)}");
            Console.WriteLine($"number of formatting analyzers: {formattingAnalyzers.Count(f => !f.IsObsolete)}");
            Console.WriteLine($"number of refactorings: {refactorings.Length}");
            Console.WriteLine($"number of code fixes: {codeFixes.Length}");
            Console.WriteLine($"number of fixable compiler diagnostics: {codeFixes.SelectMany(f => f.FixableDiagnosticIds).Distinct().Count()}");

            void WriteDiagnostics(
                string dirPath,
                ImmutableArray<AnalyzerMetadata> analyzers,
                string @namespace,
                string categoryName,
                string descriptorsClassName = "DiagnosticRules",
                string identifiersClassName = "DiagnosticIdentifiers")
            {
                WriteCompilationUnit(
                    Path.Combine(dirPath, $"{descriptorsClassName}.Generated.cs"),
                    DiagnosticRulesGenerators.Default.Generate(analyzers, obsolete: false, comparer: comparer, @namespace: @namespace, className: descriptorsClassName, identifiersClassName: identifiersClassName, categoryName: categoryName),
                    normalizeWhitespace: false);

                WriteCompilationUnit(
                    Path.Combine(dirPath, $"{descriptorsClassName}.Deprecated.Generated.cs"),
                    DiagnosticRulesGenerators.Default.Generate(analyzers, obsolete: true, comparer: comparer, @namespace: @namespace, className: descriptorsClassName, identifiersClassName: identifiersClassName, categoryName: categoryName),
                    normalizeWhitespace: false);

                WriteCompilationUnit(
                    Path.Combine(dirPath, $"{identifiersClassName}.Generated.cs"),
                    DiagnosticIdentifiersGenerator.Generate(analyzers, obsolete: false, comparer: comparer, @namespace: @namespace, className: identifiersClassName));

                WriteCompilationUnit(
                    Path.Combine(dirPath, $"{identifiersClassName}.Deprecated.Generated.cs"),
                    DiagnosticIdentifiersGenerator.Generate(analyzers, obsolete: true, comparer: comparer, @namespace: @namespace, className: identifiersClassName));

                IEnumerable<AnalyzerMetadata> optionAnalyzers = analyzers.SelectMany(f => f.OptionAnalyzers);

                if (optionAnalyzers.Any())
                {
                    WriteCompilationUnit(
                        Path.Combine(dirPath, "AnalyzerOptionDiagnosticIdentifiers.Generated.cs"),
                        DiagnosticIdentifiersGenerator.Generate(optionAnalyzers, obsolete: false, comparer: comparer, @namespace: @namespace, className: "AnalyzerOptionDiagnosticIdentifiers"),
                        fileMustExist: false);

                    WriteCompilationUnit(
                        Path.Combine(dirPath, "AnalyzerOptions.Generated.cs"),
                        AnalyzerOptionDescriptorsGenerator.Generate(analyzers, obsolete: false, comparer: comparer, @namespace: @namespace, className: "AnalyzerOptions"),
                        fileMustExist: false);
                }

                IEnumerable<string> analyzerOptionIdentifiers = analyzers
                    .SelectMany(f => f.OptionAnalyzers)
                    .Where(f => f.Id != null)
                    .Select(f => f.Identifier);
            }

            void WriteCompilationUnit(
                string path,
                CompilationUnitSyntax compilationUnit,
                bool autoGenerated = true,
                bool normalizeWhitespace = true,
                bool fileMustExist = true,
                bool overwrite = true)
            {
                CodeGenerationHelpers.WriteCompilationUnit(
                    path: Path.Combine(rootPath, path),
                    compilationUnit: compilationUnit,
                    banner: "Copyright (c) Josef Pihrt and Contributors. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.",
                    autoGenerated: autoGenerated,
                    normalizeWhitespace: normalizeWhitespace,
                    fileMustExist: fileMustExist,
                    overwrite: overwrite);
            }
        }
    }
}
