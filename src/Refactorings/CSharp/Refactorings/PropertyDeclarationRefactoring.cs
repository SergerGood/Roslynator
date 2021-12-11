﻿// Copyright (c) Josef Pihrt and Contributors. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Options;
using Microsoft.CodeAnalysis.Rename;
using Roslynator.CSharp.Refactorings.MakeMemberAbstract;
using Roslynator.CSharp.Refactorings.MakeMemberVirtual;
using Roslynator.CSharp.Refactorings.ReplacePropertyWithMethod;

namespace Roslynator.CSharp.Refactorings
{
    internal static class PropertyDeclarationRefactoring
    {
        public static async Task ComputeRefactoringsAsync(RefactoringContext context, PropertyDeclarationSyntax propertyDeclaration)
        {
            if (context.IsRefactoringEnabled(RefactoringIdentifiers.ReplacePropertyWithMethod)
                && propertyDeclaration.HeaderSpan().Contains(context.Span))
            {
                ReplacePropertyWithMethodRefactoring.ComputeRefactoring(context, propertyDeclaration);
            }

            if (context.IsRefactoringEnabled(RefactoringIdentifiers.RemovePropertyInitializer)
                && RemovePropertyInitializerRefactoring.CanRefactor(context, propertyDeclaration))
            {
                context.RegisterRefactoring(
                    "Remove property initializer",
                    ct => RemovePropertyInitializerRefactoring.RefactorAsync(context.Document, propertyDeclaration, ct),
                    RefactoringIdentifiers.RemovePropertyInitializer);
            }

            if (context.IsAnyRefactoringEnabled(
                RefactoringIdentifiers.ConvertAutoPropertyToFullProperty,
                RefactoringIdentifiers.ConvertAutoPropertyToFullPropertyWithoutBackingField)
                && propertyDeclaration.Span.Contains(context.Span)
                && ConvertAutoPropertyToFullPropertyWithoutBackingFieldRefactoring.CanRefactor(propertyDeclaration))
            {
                if (context.IsRefactoringEnabled(RefactoringIdentifiers.ConvertAutoPropertyToFullProperty))
                {
                    context.RegisterRefactoring(
                        "Convert to full property",
                        ct => ConvertAutoPropertyToFullPropertyRefactoring.RefactorAsync(context.Document, propertyDeclaration, context.Settings.PrefixFieldIdentifierWithUnderscore, ct),
                        RefactoringIdentifiers.ConvertAutoPropertyToFullProperty);
                }

                if (context.IsRefactoringEnabled(RefactoringIdentifiers.ConvertAutoPropertyToFullPropertyWithoutBackingField))
                {
                    context.RegisterRefactoring(
                        "Convert to full property (without backing field)",
                        ct => ConvertAutoPropertyToFullPropertyWithoutBackingFieldRefactoring.RefactorAsync(context.Document, propertyDeclaration, ct),
                        RefactoringIdentifiers.ConvertAutoPropertyToFullPropertyWithoutBackingField);
                }
            }

            if (context.IsRefactoringEnabled(RefactoringIdentifiers.ConvertBlockBodyToExpressionBody)
                && context.SupportsCSharp6
                && ConvertBlockBodyToExpressionBodyRefactoring.CanRefactor(propertyDeclaration, context.Span))
            {
                context.RegisterRefactoring(
                    ConvertBlockBodyToExpressionBodyRefactoring.Title,
                    ct => ConvertBlockBodyToExpressionBodyRefactoring.RefactorAsync(context.Document, propertyDeclaration, ct),
                    RefactoringIdentifiers.ConvertBlockBodyToExpressionBody);
            }

            if (context.IsRefactoringEnabled(RefactoringIdentifiers.NotifyWhenPropertyChanges))
                await NotifyWhenPropertyChangesRefactoring.ComputeRefactoringAsync(context, propertyDeclaration).ConfigureAwait(false);

            if (context.IsRefactoringEnabled(RefactoringIdentifiers.MakeMemberAbstract)
                && propertyDeclaration.HeaderSpan().Contains(context.Span))
            {
                MakePropertyAbstractRefactoring.ComputeRefactoring(context, propertyDeclaration);
            }

            if (context.IsRefactoringEnabled(RefactoringIdentifiers.MakeMemberVirtual)
                && context.Span.IsEmptyAndContainedInSpan(propertyDeclaration.Identifier))
            {
                MakePropertyVirtualRefactoring.ComputeRefactoring(context, propertyDeclaration);
            }

            if (context.IsRefactoringEnabled(RefactoringIdentifiers.CopyDocumentationCommentFromBaseMember)
                && propertyDeclaration.HeaderSpan().Contains(context.Span)
                && !propertyDeclaration.HasDocumentationComment())
            {
                SemanticModel semanticModel = await context.GetSemanticModelAsync().ConfigureAwait(false);
                CopyDocumentationCommentFromBaseMemberRefactoring.ComputeRefactoring(context, propertyDeclaration, semanticModel);
            }

            if (context.IsRefactoringEnabled(RefactoringIdentifiers.RenamePropertyAccordingToTypeName))
                await RenamePropertyAccordingToTypeName(context, propertyDeclaration).ConfigureAwait(false);

            if (context.IsRefactoringEnabled(RefactoringIdentifiers.AddMemberToInterface)
                && context.Span.IsEmptyAndContainedInSpanOrBetweenSpans(propertyDeclaration.Identifier))
            {
                SemanticModel semanticModel = await context.GetSemanticModelAsync().ConfigureAwait(false);

                AddMemberToInterfaceRefactoring.ComputeRefactoring(context, propertyDeclaration, semanticModel);
            }
        }

        private static async Task RenamePropertyAccordingToTypeName(RefactoringContext context, PropertyDeclarationSyntax propertyDeclaration)
        {
            TypeSyntax type = propertyDeclaration.Type;

            if (type == null)
                return;

            SyntaxToken identifier = propertyDeclaration.Identifier;

            if (!context.Span.IsEmptyAndContainedInSpanOrBetweenSpans(identifier))
                return;

            SemanticModel semanticModel = await context.GetSemanticModelAsync().ConfigureAwait(false);

            ITypeSymbol typeSymbol = semanticModel.GetTypeSymbol(type, context.CancellationToken);

            if (typeSymbol?.IsErrorType() != false)
                return;

            string newName = NameGenerator.CreateName(typeSymbol);

            if (string.IsNullOrEmpty(newName))
                return;

            string oldName = identifier.ValueText;

            newName = StringUtility.FirstCharToUpper(newName);

            if (string.Equals(oldName, newName, StringComparison.Ordinal))
                return;

            ISymbol symbol = semanticModel.GetDeclaredSymbol(propertyDeclaration, context.CancellationToken);

            if (!await MemberNameGenerator.IsUniqueMemberNameAsync(
                newName,
                symbol,
                context.Solution,
                cancellationToken: context.CancellationToken)
                .ConfigureAwait(false))
            {
                return;
            }

            context.RegisterRefactoring(
                $"Rename '{oldName}' to '{newName}'",
                ct => Renamer.RenameSymbolAsync(context.Solution, symbol, newName, default(OptionSet), ct),
                RefactoringIdentifiers.RenamePropertyAccordingToTypeName);
        }
    }
}
