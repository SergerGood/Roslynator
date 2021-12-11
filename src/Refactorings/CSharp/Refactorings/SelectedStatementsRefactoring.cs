﻿// Copyright (c) Josef Pihrt and Contributors. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Roslynator.CSharp.Analysis.If;
using Roslynator.CSharp.Refactorings.WrapStatements;

namespace Roslynator.CSharp.Refactorings
{
    internal static class SelectedStatementsRefactoring
    {
        public static bool IsAnyRefactoringEnabled(RefactoringContext context)
        {
            return context.IsRefactoringEnabled(RefactoringIdentifiers.WrapStatementsInUsingStatement)
                || context.IsRefactoringEnabled(RefactoringIdentifiers.InitializePropertiesInInitializer)
                || context.IsRefactoringEnabled(RefactoringIdentifiers.MergeIfStatements)
                || context.IsRefactoringEnabled(RefactoringIdentifiers.ConvertStatementsToIfElse)
                || context.IsRefactoringEnabled(RefactoringIdentifiers.MergeLocalDeclarations)
                || context.IsRefactoringEnabled(RefactoringIdentifiers.WrapStatementsInCondition)
                || context.IsRefactoringEnabled(RefactoringIdentifiers.WrapLinesInTryCatch)
                || context.IsRefactoringEnabled(RefactoringIdentifiers.UseCoalesceExpressionInsteadOfIf)
                || context.IsRefactoringEnabled(RefactoringIdentifiers.ConvertIfToConditionalExpression)
                || context.IsRefactoringEnabled(RefactoringIdentifiers.SimplifyIf)
                || context.IsRefactoringEnabled(RefactoringIdentifiers.CheckExpressionForNull)
                || context.IsRefactoringEnabled(RefactoringIdentifiers.ConvertWhileToFor);
        }

        public static async Task ComputeRefactoringAsync(RefactoringContext context, StatementListSelection selectedStatements)
        {
            if (context.IsRefactoringEnabled(RefactoringIdentifiers.WrapStatementsInUsingStatement))
            {
                var refactoring = new WrapStatementsInUsingStatementRefactoring();
                await refactoring.ComputeRefactoringAsync(context, selectedStatements).ConfigureAwait(false);
            }

            if (context.IsRefactoringEnabled(RefactoringIdentifiers.InitializePropertiesInInitializer))
                await InitializePropertiesInInitializerRefactoring.ComputeRefactoringsAsync(context, selectedStatements).ConfigureAwait(false);

            if (context.IsRefactoringEnabled(RefactoringIdentifiers.MergeIfStatements))
                MergeIfStatementsRefactoring.ComputeRefactorings(context, selectedStatements);

            if (context.IsRefactoringEnabled(RefactoringIdentifiers.ConvertStatementsToIfElse))
                ConvertStatementsToIfElseRefactoring.ComputeRefactorings(context, selectedStatements);

            if (context.IsAnyRefactoringEnabled(
                RefactoringIdentifiers.UseCoalesceExpressionInsteadOfIf,
                RefactoringIdentifiers.ConvertIfToConditionalExpression,
                RefactoringIdentifiers.SimplifyIf))
            {
                SemanticModel semanticModel = await context.GetSemanticModelAsync().ConfigureAwait(false);

                IfAnalysisOptions options = IfStatementRefactoring.GetIfAnalysisOptions(context);

                foreach (IfAnalysis analysis in IfAnalysis.Analyze(selectedStatements, options, semanticModel, context.CancellationToken))
                {
                    string refactoringId = IfStatementRefactoring.GetRefactoringIdentifier(analysis);

                    if (context.IsRefactoringEnabled(refactoringId))
                    {
                        context.RegisterRefactoring(
                            analysis.Title,
                            ct => IfRefactoring.RefactorAsync(context.Document, analysis, ct),
                            equivalenceKey: refactoringId);
                    }
                }
            }

            if (context.IsRefactoringEnabled(RefactoringIdentifiers.MergeLocalDeclarations))
                await MergeLocalDeclarationsRefactoring.ComputeRefactoringsAsync(context, selectedStatements).ConfigureAwait(false);

            if (context.IsRefactoringEnabled(RefactoringIdentifiers.RemoveUnnecessaryAssignment))
                RemoveUnnecessaryAssignmentRefactoring.ComputeRefactorings(context, selectedStatements);

            if (context.IsRefactoringEnabled(RefactoringIdentifiers.CheckExpressionForNull))
                await CheckExpressionForNullRefactoring.ComputeRefactoringAsync(context, selectedStatements).ConfigureAwait(false);

            if (context.IsRefactoringEnabled(RefactoringIdentifiers.ConvertWhileToFor))
                await ConvertWhileToForRefactoring.ComputeRefactoringAsync(context, selectedStatements).ConfigureAwait(false);

            if (context.IsRefactoringEnabled(RefactoringIdentifiers.WrapStatementsInCondition))
            {
                context.RegisterRefactoring(
                    WrapInIfStatementRefactoring.Title,
                    ct => WrapInIfStatementRefactoring.Instance.RefactorAsync(context.Document, selectedStatements, ct),
                    RefactoringIdentifiers.WrapStatementsInCondition);
            }

            if (context.IsRefactoringEnabled(RefactoringIdentifiers.WrapLinesInTryCatch))
            {
                context.RegisterRefactoring(
                    WrapLinesInTryCatchRefactoring.Title,
                    ct => WrapLinesInTryCatchRefactoring.Instance.RefactorAsync(context.Document, selectedStatements, ct),
                    RefactoringIdentifiers.WrapLinesInTryCatch);
            }
        }
    }
}
