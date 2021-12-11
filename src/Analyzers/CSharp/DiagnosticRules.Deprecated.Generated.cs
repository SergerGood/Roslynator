﻿// Copyright (c) Josef Pihrt and Contributors. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

// <auto-generated>

using System;
using Microsoft.CodeAnalysis;

namespace Roslynator.CSharp
{
    public static partial class DiagnosticRules
    {
        [Obsolete("", error: true)]
        internal static readonly DiagnosticDescriptor AvoidMultilineExpressionBody = DiagnosticDescriptorFactory.Create(
            id:                 DiagnosticIdentifiers.AvoidMultilineExpressionBody, 
            title:              "[deprecated] Avoid multiline expression body.", 
            messageFormat:      "[deprecated] Use analyzer RCS1016a instead.", 
            category:           DiagnosticCategories.Roslynator, 
            defaultSeverity:    DiagnosticSeverity.Info, 
            isEnabledByDefault: false, 
            description:        null, 
            helpLinkUri:        DiagnosticIdentifiers.AvoidMultilineExpressionBody, 
            customTags:         Array.Empty<string>());

        [Obsolete("", error: true)]
        internal static readonly DiagnosticDescriptor SimplifyLambdaExpressionParameterList = DiagnosticDescriptorFactory.Create(
            id:                 DiagnosticIdentifiers.SimplifyLambdaExpressionParameterList, 
            title:              "Simplify lambda expression parameter list.", 
            messageFormat:      "Simplify lambda expression parameter list.", 
            category:           DiagnosticCategories.Roslynator, 
            defaultSeverity:    DiagnosticSeverity.Hidden, 
            isEnabledByDefault: false, 
            description:        null, 
            helpLinkUri:        DiagnosticIdentifiers.SimplifyLambdaExpressionParameterList, 
            customTags:         Array.Empty<string>());

        [Obsolete("", error: true)]
        public static readonly DiagnosticDescriptor SimplifyLambdaExpressionParameterListFadeOut = DiagnosticDescriptorFactory.CreateFadeOut(SimplifyLambdaExpressionParameterList);

        [Obsolete("", error: true)]
        internal static readonly DiagnosticDescriptor FormatEmptyBlock = DiagnosticDescriptorFactory.Create(
            id:                 DiagnosticIdentifiers.FormatEmptyBlock, 
            title:              "Format empty block.", 
            messageFormat:      "Format empty block.", 
            category:           DiagnosticCategories.Roslynator, 
            defaultSeverity:    DiagnosticSeverity.Hidden, 
            isEnabledByDefault: true, 
            description:        null, 
            helpLinkUri:        DiagnosticIdentifiers.FormatEmptyBlock, 
            customTags:         Array.Empty<string>());

        [Obsolete("", error: true)]
        internal static readonly DiagnosticDescriptor FormatAccessorList = DiagnosticDescriptorFactory.Create(
            id:                 DiagnosticIdentifiers.FormatAccessorList, 
            title:              "Format accessor list.", 
            messageFormat:      "Format accessor list.", 
            category:           DiagnosticCategories.Roslynator, 
            defaultSeverity:    DiagnosticSeverity.Info, 
            isEnabledByDefault: false, 
            description:        null, 
            helpLinkUri:        DiagnosticIdentifiers.FormatAccessorList, 
            customTags:         Array.Empty<string>());

        [Obsolete("", error: true)]
        internal static readonly DiagnosticDescriptor AddNewLineBeforeEnumMember = DiagnosticDescriptorFactory.Create(
            id:                 DiagnosticIdentifiers.AddNewLineBeforeEnumMember, 
            title:              "Add new line before enum member.", 
            messageFormat:      "Add new line before enum member.", 
            category:           DiagnosticCategories.Roslynator, 
            defaultSeverity:    DiagnosticSeverity.Info, 
            isEnabledByDefault: false, 
            description:        null, 
            helpLinkUri:        DiagnosticIdentifiers.AddNewLineBeforeEnumMember, 
            customTags:         Array.Empty<string>());

        [Obsolete("", error: true)]
        internal static readonly DiagnosticDescriptor AddNewLineBeforeStatement = DiagnosticDescriptorFactory.Create(
            id:                 DiagnosticIdentifiers.AddNewLineBeforeStatement, 
            title:              "Add new line before statement.", 
            messageFormat:      "Add new line before statement.", 
            category:           DiagnosticCategories.Roslynator, 
            defaultSeverity:    DiagnosticSeverity.Info, 
            isEnabledByDefault: false, 
            description:        null, 
            helpLinkUri:        DiagnosticIdentifiers.AddNewLineBeforeStatement, 
            customTags:         Array.Empty<string>());

        [Obsolete("", error: true)]
        internal static readonly DiagnosticDescriptor AddNewLineBeforeEmbeddedStatement = DiagnosticDescriptorFactory.Create(
            id:                 DiagnosticIdentifiers.AddNewLineBeforeEmbeddedStatement, 
            title:              "Add new line before embedded statement.", 
            messageFormat:      "Add new line before embedded statement.", 
            category:           DiagnosticCategories.Roslynator, 
            defaultSeverity:    DiagnosticSeverity.Info, 
            isEnabledByDefault: false, 
            description:        null, 
            helpLinkUri:        DiagnosticIdentifiers.AddNewLineBeforeEmbeddedStatement, 
            customTags:         Array.Empty<string>());

        [Obsolete("", error: true)]
        internal static readonly DiagnosticDescriptor AddNewLineAfterSwitchLabel = DiagnosticDescriptorFactory.Create(
            id:                 DiagnosticIdentifiers.AddNewLineAfterSwitchLabel, 
            title:              "Add new line after switch label.", 
            messageFormat:      "Add new line after switch label.", 
            category:           DiagnosticCategories.Roslynator, 
            defaultSeverity:    DiagnosticSeverity.Info, 
            isEnabledByDefault: false, 
            description:        null, 
            helpLinkUri:        DiagnosticIdentifiers.AddNewLineAfterSwitchLabel, 
            customTags:         Array.Empty<string>());

        [Obsolete("", error: true)]
        internal static readonly DiagnosticDescriptor FormatBinaryOperatorOnNextLine = DiagnosticDescriptorFactory.Create(
            id:                 DiagnosticIdentifiers.FormatBinaryOperatorOnNextLine, 
            title:              "Format binary operator on next line.", 
            messageFormat:      "Format binary operator on next line.", 
            category:           DiagnosticCategories.Roslynator, 
            defaultSeverity:    DiagnosticSeverity.Info, 
            isEnabledByDefault: false, 
            description:        null, 
            helpLinkUri:        DiagnosticIdentifiers.FormatBinaryOperatorOnNextLine, 
            customTags:         Array.Empty<string>());

        [Obsolete("", error: true)]
        internal static readonly DiagnosticDescriptor AddEmptyLineAfterEmbeddedStatement = DiagnosticDescriptorFactory.Create(
            id:                 DiagnosticIdentifiers.AddEmptyLineAfterEmbeddedStatement, 
            title:              "Add empty line after embedded statement.", 
            messageFormat:      "Add empty line after embedded statement.", 
            category:           DiagnosticCategories.Roslynator, 
            defaultSeverity:    DiagnosticSeverity.Info, 
            isEnabledByDefault: false, 
            description:        null, 
            helpLinkUri:        DiagnosticIdentifiers.AddEmptyLineAfterEmbeddedStatement, 
            customTags:         Array.Empty<string>());

        [Obsolete("", error: true)]
        internal static readonly DiagnosticDescriptor ReplaceForEachWithFor = DiagnosticDescriptorFactory.Create(
            id:                 DiagnosticIdentifiers.ReplaceForEachWithFor, 
            title:              "Replace foreach statement with for statement.", 
            messageFormat:      "Replace foreach statement with for statement.", 
            category:           DiagnosticCategories.Roslynator, 
            defaultSeverity:    DiagnosticSeverity.Hidden, 
            isEnabledByDefault: false, 
            description:        null, 
            helpLinkUri:        DiagnosticIdentifiers.ReplaceForEachWithFor, 
            customTags:         Array.Empty<string>());

        [Obsolete("", error: true)]
        public static readonly DiagnosticDescriptor ReplaceForEachWithForFadeOut = DiagnosticDescriptorFactory.CreateFadeOut(ReplaceForEachWithFor);

        [Obsolete("", error: true)]
        internal static readonly DiagnosticDescriptor MergeLocalDeclarationWithReturnStatement = DiagnosticDescriptorFactory.Create(
            id:                 DiagnosticIdentifiers.MergeLocalDeclarationWithReturnStatement, 
            title:              "Merge local declaration with return statement.", 
            messageFormat:      "Merge local declaration with return statement.", 
            category:           DiagnosticCategories.Roslynator, 
            defaultSeverity:    DiagnosticSeverity.Hidden, 
            isEnabledByDefault: true, 
            description:        null, 
            helpLinkUri:        DiagnosticIdentifiers.MergeLocalDeclarationWithReturnStatement, 
            customTags:         Array.Empty<string>());

        [Obsolete("", error: true)]
        public static readonly DiagnosticDescriptor MergeLocalDeclarationWithReturnStatementFadeOut = DiagnosticDescriptorFactory.CreateFadeOut(MergeLocalDeclarationWithReturnStatement);

        [Obsolete("", error: true)]
        internal static readonly DiagnosticDescriptor AddEmptyLineBetweenDeclarations = DiagnosticDescriptorFactory.Create(
            id:                 DiagnosticIdentifiers.AddEmptyLineBetweenDeclarations, 
            title:              "Add empty line between declarations.", 
            messageFormat:      "Add empty line between declarations.", 
            category:           DiagnosticCategories.Roslynator, 
            defaultSeverity:    DiagnosticSeverity.Info, 
            isEnabledByDefault: false, 
            description:        null, 
            helpLinkUri:        DiagnosticIdentifiers.AddEmptyLineBetweenDeclarations, 
            customTags:         Array.Empty<string>());

        [Obsolete("", error: true)]
        internal static readonly DiagnosticDescriptor AvoidInterpolatedStringWithNoInterpolation = DiagnosticDescriptorFactory.Create(
            id:                 DiagnosticIdentifiers.AvoidInterpolatedStringWithNoInterpolation, 
            title:              "Avoid interpolated string with no interpolation.", 
            messageFormat:      "Remove '$' from interpolated string with no interpolation.", 
            category:           DiagnosticCategories.Roslynator, 
            defaultSeverity:    DiagnosticSeverity.Hidden, 
            isEnabledByDefault: true, 
            description:        null, 
            helpLinkUri:        DiagnosticIdentifiers.AvoidInterpolatedStringWithNoInterpolation, 
            customTags:         WellKnownDiagnosticTags.Unnecessary);

        [Obsolete("", error: true)]
        internal static readonly DiagnosticDescriptor RemoveArgumentListFromObjectCreation2 = DiagnosticDescriptorFactory.Create(
            id:                 DiagnosticIdentifiers.RemoveArgumentListFromObjectCreation2, 
            title:              "Remove argument list from object creation expression.", 
            messageFormat:      "Remove argument list from object creation expression.", 
            category:           DiagnosticCategories.Roslynator, 
            defaultSeverity:    DiagnosticSeverity.Info, 
            isEnabledByDefault: false, 
            description:        null, 
            helpLinkUri:        DiagnosticIdentifiers.RemoveArgumentListFromObjectCreation2, 
            customTags:         WellKnownDiagnosticTags.Unnecessary);

        [Obsolete("", error: true)]
        internal static readonly DiagnosticDescriptor FormatDeclarationBraces = DiagnosticDescriptorFactory.Create(
            id:                 DiagnosticIdentifiers.FormatDeclarationBraces, 
            title:              "Format declaration braces.", 
            messageFormat:      "Format declaration braces.", 
            category:           DiagnosticCategories.Roslynator, 
            defaultSeverity:    DiagnosticSeverity.Hidden, 
            isEnabledByDefault: true, 
            description:        null, 
            helpLinkUri:        DiagnosticIdentifiers.FormatDeclarationBraces, 
            customTags:         Array.Empty<string>());

        [Obsolete("", error: true)]
        internal static readonly DiagnosticDescriptor UseCountOrLengthPropertyInsteadOfCountMethod = DiagnosticDescriptorFactory.Create(
            id:                 DiagnosticIdentifiers.UseCountOrLengthPropertyInsteadOfCountMethod, 
            title:              "Use 'Count/Length' property instead of 'Count' method.", 
            messageFormat:      "Use '{0}' property instead of 'Count' method.", 
            category:           DiagnosticCategories.Roslynator, 
            defaultSeverity:    DiagnosticSeverity.Warning, 
            isEnabledByDefault: true, 
            description:        null, 
            helpLinkUri:        DiagnosticIdentifiers.UseCountOrLengthPropertyInsteadOfCountMethod, 
            customTags:         Array.Empty<string>());

        [Obsolete("", error: true)]
        internal static readonly DiagnosticDescriptor CallAnyInsteadOfCount = DiagnosticDescriptorFactory.Create(
            id:                 DiagnosticIdentifiers.CallAnyInsteadOfCount, 
            title:              "Call 'Enumerable.Any' instead of 'Enumerable.Count'.", 
            messageFormat:      "Call 'Enumerable.Any' instead of 'Enumerable.Count'.", 
            category:           DiagnosticCategories.Roslynator, 
            defaultSeverity:    DiagnosticSeverity.Warning, 
            isEnabledByDefault: true, 
            description:        null, 
            helpLinkUri:        DiagnosticIdentifiers.CallAnyInsteadOfCount, 
            customTags:         Array.Empty<string>());

        [Obsolete("", error: true)]
        internal static readonly DiagnosticDescriptor UseLinefeedAsNewLine = DiagnosticDescriptorFactory.Create(
            id:                 DiagnosticIdentifiers.UseLinefeedAsNewLine, 
            title:              "Use linefeed as newline.", 
            messageFormat:      "Use linefeed as newline.", 
            category:           DiagnosticCategories.Roslynator, 
            defaultSeverity:    DiagnosticSeverity.Info, 
            isEnabledByDefault: false, 
            description:        null, 
            helpLinkUri:        DiagnosticIdentifiers.UseLinefeedAsNewLine, 
            customTags:         Array.Empty<string>());

        [Obsolete("", error: true)]
        internal static readonly DiagnosticDescriptor UseCarriageReturnAndLinefeedAsNewLine = DiagnosticDescriptorFactory.Create(
            id:                 DiagnosticIdentifiers.UseCarriageReturnAndLinefeedAsNewLine, 
            title:              "Use carriage return + linefeed as newline.", 
            messageFormat:      "Use carriage return + linefeed as newline.", 
            category:           DiagnosticCategories.Roslynator, 
            defaultSeverity:    DiagnosticSeverity.Info, 
            isEnabledByDefault: false, 
            description:        null, 
            helpLinkUri:        DiagnosticIdentifiers.UseCarriageReturnAndLinefeedAsNewLine, 
            customTags:         Array.Empty<string>());

        [Obsolete("", error: true)]
        internal static readonly DiagnosticDescriptor UseSpacesInsteadOfTab = DiagnosticDescriptorFactory.Create(
            id:                 DiagnosticIdentifiers.UseSpacesInsteadOfTab, 
            title:              "Use space(s) instead of tab.", 
            messageFormat:      "Use space(s) instead of tab.", 
            category:           DiagnosticCategories.Roslynator, 
            defaultSeverity:    DiagnosticSeverity.Info, 
            isEnabledByDefault: false, 
            description:        null, 
            helpLinkUri:        DiagnosticIdentifiers.UseSpacesInsteadOfTab, 
            customTags:         Array.Empty<string>());

        [Obsolete("", error: true)]
        internal static readonly DiagnosticDescriptor AddEmptyLineBeforeWhileInDoStatement = DiagnosticDescriptorFactory.Create(
            id:                 DiagnosticIdentifiers.AddEmptyLineBeforeWhileInDoStatement, 
            title:              "Add empty line before 'while' keyword in 'do' statement.", 
            messageFormat:      "Add empty line before 'while' keyword in 'do' statement.", 
            category:           DiagnosticCategories.Roslynator, 
            defaultSeverity:    DiagnosticSeverity.Info, 
            isEnabledByDefault: false, 
            description:        null, 
            helpLinkUri:        DiagnosticIdentifiers.AddEmptyLineBeforeWhileInDoStatement, 
            customTags:         Array.Empty<string>());

        [Obsolete("", error: true)]
        internal static readonly DiagnosticDescriptor UseCSharp6DictionaryInitializer = DiagnosticDescriptorFactory.Create(
            id:                 DiagnosticIdentifiers.UseCSharp6DictionaryInitializer, 
            title:              "Use C# 6.0 dictionary initializer.", 
            messageFormat:      "Use C# 6.0 dictionary initializer.", 
            category:           DiagnosticCategories.Roslynator, 
            defaultSeverity:    DiagnosticSeverity.Info, 
            isEnabledByDefault: true, 
            description:        null, 
            helpLinkUri:        DiagnosticIdentifiers.UseCSharp6DictionaryInitializer, 
            customTags:         Array.Empty<string>());

        [Obsolete("", error: true)]
        internal static readonly DiagnosticDescriptor CallCastInsteadOfSelect = DiagnosticDescriptorFactory.Create(
            id:                 DiagnosticIdentifiers.CallCastInsteadOfSelect, 
            title:              "Call 'Enumerable.Cast' instead of 'Enumerable.Select'.", 
            messageFormat:      "Call 'Enumerable.Cast' instead of 'Enumerable.Select'.", 
            category:           DiagnosticCategories.Roslynator, 
            defaultSeverity:    DiagnosticSeverity.Info, 
            isEnabledByDefault: true, 
            description:        null, 
            helpLinkUri:        DiagnosticIdentifiers.CallCastInsteadOfSelect, 
            customTags:         Array.Empty<string>());

        [Obsolete("", error: true)]
        internal static readonly DiagnosticDescriptor ReplaceReturnStatementWithExpressionStatement = DiagnosticDescriptorFactory.Create(
            id:                 DiagnosticIdentifiers.ReplaceReturnStatementWithExpressionStatement, 
            title:              "Replace yield/return statement with expression statement.", 
            messageFormat:      "Replace {0} statement with expression statement.", 
            category:           DiagnosticCategories.Roslynator, 
            defaultSeverity:    DiagnosticSeverity.Hidden, 
            isEnabledByDefault: true, 
            description:        null, 
            helpLinkUri:        DiagnosticIdentifiers.ReplaceReturnStatementWithExpressionStatement, 
            customTags:         WellKnownDiagnosticTags.Unnecessary);

        [Obsolete("", error: true)]
        internal static readonly DiagnosticDescriptor AddBreakStatementToSwitchSection = DiagnosticDescriptorFactory.Create(
            id:                 DiagnosticIdentifiers.AddBreakStatementToSwitchSection, 
            title:              "Add break statement to switch section.", 
            messageFormat:      "Add break statement to switch section.", 
            category:           DiagnosticCategories.Roslynator, 
            defaultSeverity:    DiagnosticSeverity.Hidden, 
            isEnabledByDefault: true, 
            description:        null, 
            helpLinkUri:        DiagnosticIdentifiers.AddBreakStatementToSwitchSection, 
            customTags:         Array.Empty<string>());

        [Obsolete("", error: true)]
        internal static readonly DiagnosticDescriptor AddReturnStatementThatReturnsDefaultValue = DiagnosticDescriptorFactory.Create(
            id:                 DiagnosticIdentifiers.AddReturnStatementThatReturnsDefaultValue, 
            title:              "Add return statement that returns default value.", 
            messageFormat:      "Add return statement that returns default value.", 
            category:           DiagnosticCategories.Roslynator, 
            defaultSeverity:    DiagnosticSeverity.Hidden, 
            isEnabledByDefault: true, 
            description:        null, 
            helpLinkUri:        DiagnosticIdentifiers.AddReturnStatementThatReturnsDefaultValue, 
            customTags:         Array.Empty<string>());

        [Obsolete("", error: true)]
        internal static readonly DiagnosticDescriptor CallFindInsteadOfFirstOrDefault = DiagnosticDescriptorFactory.Create(
            id:                 DiagnosticIdentifiers.CallFindInsteadOfFirstOrDefault, 
            title:              "Call 'Find' instead of 'FirstOrDefault'.", 
            messageFormat:      "Call 'Find' instead of 'FirstOrDefault'.", 
            category:           DiagnosticCategories.Roslynator, 
            defaultSeverity:    DiagnosticSeverity.Info, 
            isEnabledByDefault: true, 
            description:        null, 
            helpLinkUri:        DiagnosticIdentifiers.CallFindInsteadOfFirstOrDefault, 
            customTags:         Array.Empty<string>());

        [Obsolete("", error: true)]
        internal static readonly DiagnosticDescriptor UseElementAccessInsteadOfElementAt = DiagnosticDescriptorFactory.Create(
            id:                 DiagnosticIdentifiers.UseElementAccessInsteadOfElementAt, 
            title:              "Use [] instead of calling 'ElementAt'.", 
            messageFormat:      "Use [] instead of calling 'ElementAt'.", 
            category:           DiagnosticCategories.Roslynator, 
            defaultSeverity:    DiagnosticSeverity.Info, 
            isEnabledByDefault: true, 
            description:        null, 
            helpLinkUri:        DiagnosticIdentifiers.UseElementAccessInsteadOfElementAt, 
            customTags:         Array.Empty<string>());

        [Obsolete("", error: true)]
        internal static readonly DiagnosticDescriptor UseElementAccessInsteadOfFirst = DiagnosticDescriptorFactory.Create(
            id:                 DiagnosticIdentifiers.UseElementAccessInsteadOfFirst, 
            title:              "Use [] instead of calling 'First'.", 
            messageFormat:      "Use [] instead of calling 'First'.", 
            category:           DiagnosticCategories.Roslynator, 
            defaultSeverity:    DiagnosticSeverity.Info, 
            isEnabledByDefault: true, 
            description:        null, 
            helpLinkUri:        DiagnosticIdentifiers.UseElementAccessInsteadOfFirst, 
            customTags:         Array.Empty<string>());

        [Obsolete("", error: true)]
        internal static readonly DiagnosticDescriptor AddMissingSemicolon = DiagnosticDescriptorFactory.Create(
            id:                 DiagnosticIdentifiers.AddMissingSemicolon, 
            title:              "Add missing semicolon.", 
            messageFormat:      "Add missing semicolon.", 
            category:           DiagnosticCategories.Roslynator, 
            defaultSeverity:    DiagnosticSeverity.Hidden, 
            isEnabledByDefault: false, 
            description:        null, 
            helpLinkUri:        DiagnosticIdentifiers.AddMissingSemicolon, 
            customTags:         Array.Empty<string>());

        [Obsolete("", error: true)]
        internal static readonly DiagnosticDescriptor MarkMemberAsStatic = DiagnosticDescriptorFactory.Create(
            id:                 DiagnosticIdentifiers.MarkMemberAsStatic, 
            title:              "Mark member as static.", 
            messageFormat:      "Mark member as static.", 
            category:           DiagnosticCategories.Roslynator, 
            defaultSeverity:    DiagnosticSeverity.Hidden, 
            isEnabledByDefault: true, 
            description:        null, 
            helpLinkUri:        DiagnosticIdentifiers.MarkMemberAsStatic, 
            customTags:         Array.Empty<string>());

        [Obsolete("", error: true)]
        internal static readonly DiagnosticDescriptor MergeLocalDeclarationWithAssignment = DiagnosticDescriptorFactory.Create(
            id:                 DiagnosticIdentifiers.MergeLocalDeclarationWithAssignment, 
            title:              "Merge local declaration with assignment.", 
            messageFormat:      "Merge local declaration with assignment.", 
            category:           DiagnosticCategories.Roslynator, 
            defaultSeverity:    DiagnosticSeverity.Info, 
            isEnabledByDefault: true, 
            description:        null, 
            helpLinkUri:        DiagnosticIdentifiers.MergeLocalDeclarationWithAssignment, 
            customTags:         Array.Empty<string>());

        [Obsolete("", error: true)]
        public static readonly DiagnosticDescriptor MergeLocalDeclarationWithAssignmentFadeOut = DiagnosticDescriptorFactory.CreateFadeOut(MergeLocalDeclarationWithAssignment);

        [Obsolete("", error: true)]
        internal static readonly DiagnosticDescriptor ReplaceReturnWithYieldReturn = DiagnosticDescriptorFactory.Create(
            id:                 DiagnosticIdentifiers.ReplaceReturnWithYieldReturn, 
            title:              "Replace return with yield return.", 
            messageFormat:      "Replace return with yield return.", 
            category:           DiagnosticCategories.Roslynator, 
            defaultSeverity:    DiagnosticSeverity.Hidden, 
            isEnabledByDefault: true, 
            description:        null, 
            helpLinkUri:        DiagnosticIdentifiers.ReplaceReturnWithYieldReturn, 
            customTags:         Array.Empty<string>());

        [Obsolete("", error: true)]
        internal static readonly DiagnosticDescriptor AddDocumentationComment = DiagnosticDescriptorFactory.Create(
            id:                 DiagnosticIdentifiers.AddDocumentationComment, 
            title:              "Add documentation comment to publicly visible type or member.", 
            messageFormat:      "Add documentation comment to publicly visible type or member.", 
            category:           DiagnosticCategories.Roslynator, 
            defaultSeverity:    DiagnosticSeverity.Hidden, 
            isEnabledByDefault: true, 
            description:        null, 
            helpLinkUri:        DiagnosticIdentifiers.AddDocumentationComment, 
            customTags:         Array.Empty<string>());

        [Obsolete("", error: true)]
        internal static readonly DiagnosticDescriptor MarkContainingClassAsAbstract = DiagnosticDescriptorFactory.Create(
            id:                 DiagnosticIdentifiers.MarkContainingClassAsAbstract, 
            title:              "Mark containing class as abstract.", 
            messageFormat:      "Mark containing class as abstract.", 
            category:           DiagnosticCategories.Roslynator, 
            defaultSeverity:    DiagnosticSeverity.Hidden, 
            isEnabledByDefault: true, 
            description:        null, 
            helpLinkUri:        DiagnosticIdentifiers.MarkContainingClassAsAbstract, 
            customTags:         Array.Empty<string>());

        [Obsolete("", error: true)]
        internal static readonly DiagnosticDescriptor RemoveInapplicableModifier = DiagnosticDescriptorFactory.Create(
            id:                 DiagnosticIdentifiers.RemoveInapplicableModifier, 
            title:              "Remove inapplicable modifier.", 
            messageFormat:      "Remove inapplicable modifier.", 
            category:           DiagnosticCategories.Roslynator, 
            defaultSeverity:    DiagnosticSeverity.Hidden, 
            isEnabledByDefault: true, 
            description:        null, 
            helpLinkUri:        DiagnosticIdentifiers.RemoveInapplicableModifier, 
            customTags:         WellKnownDiagnosticTags.Unnecessary);

        [Obsolete("", error: true)]
        internal static readonly DiagnosticDescriptor RemoveUnreachableCode = DiagnosticDescriptorFactory.Create(
            id:                 DiagnosticIdentifiers.RemoveUnreachableCode, 
            title:              "Remove unreachable code.", 
            messageFormat:      "Remove unreachable code.", 
            category:           DiagnosticCategories.Roslynator, 
            defaultSeverity:    DiagnosticSeverity.Hidden, 
            isEnabledByDefault: true, 
            description:        null, 
            helpLinkUri:        DiagnosticIdentifiers.RemoveUnreachableCode, 
            customTags:         WellKnownDiagnosticTags.Unnecessary);

        [Obsolete("", error: true)]
        internal static readonly DiagnosticDescriptor RemoveImplementationFromAbstractMember = DiagnosticDescriptorFactory.Create(
            id:                 DiagnosticIdentifiers.RemoveImplementationFromAbstractMember, 
            title:              "Remove implementation from abstract member.", 
            messageFormat:      "Remove implementation from {0}.", 
            category:           DiagnosticCategories.Roslynator, 
            defaultSeverity:    DiagnosticSeverity.Hidden, 
            isEnabledByDefault: true, 
            description:        null, 
            helpLinkUri:        DiagnosticIdentifiers.RemoveImplementationFromAbstractMember, 
            customTags:         WellKnownDiagnosticTags.Unnecessary);

        [Obsolete("", error: true)]
        internal static readonly DiagnosticDescriptor CallStringConcatInsteadOfStringJoin = DiagnosticDescriptorFactory.Create(
            id:                 DiagnosticIdentifiers.CallStringConcatInsteadOfStringJoin, 
            title:              "Call string.Concat instead of string.Join.", 
            messageFormat:      "Call string.Concat instead of string.Join.", 
            category:           DiagnosticCategories.Roslynator, 
            defaultSeverity:    DiagnosticSeverity.Info, 
            isEnabledByDefault: true, 
            description:        null, 
            helpLinkUri:        DiagnosticIdentifiers.CallStringConcatInsteadOfStringJoin, 
            customTags:         Array.Empty<string>());

        [Obsolete("", error: true)]
        internal static readonly DiagnosticDescriptor MemberTypeMustMatchOverriddenMemberType = DiagnosticDescriptorFactory.Create(
            id:                 DiagnosticIdentifiers.MemberTypeMustMatchOverriddenMemberType, 
            title:              "Member type must match overridden member type.", 
            messageFormat:      "Member type must match overridden member type.", 
            category:           DiagnosticCategories.Roslynator, 
            defaultSeverity:    DiagnosticSeverity.Hidden, 
            isEnabledByDefault: true, 
            description:        null, 
            helpLinkUri:        DiagnosticIdentifiers.MemberTypeMustMatchOverriddenMemberType, 
            customTags:         Array.Empty<string>());

        [Obsolete("", error: true)]
        internal static readonly DiagnosticDescriptor AddEmptyLineAfterClosingBrace = DiagnosticDescriptorFactory.Create(
            id:                 DiagnosticIdentifiers.AddEmptyLineAfterClosingBrace, 
            title:              "Add empty line after closing brace.", 
            messageFormat:      "Add empty line after closing brace.", 
            category:           DiagnosticCategories.Roslynator, 
            defaultSeverity:    DiagnosticSeverity.Info, 
            isEnabledByDefault: false, 
            description:        null, 
            helpLinkUri:        DiagnosticIdentifiers.AddEmptyLineAfterClosingBrace, 
            customTags:         Array.Empty<string>());

        [Obsolete("", error: true)]
        internal static readonly DiagnosticDescriptor OverridingMemberCannotChangeAccessModifiers = DiagnosticDescriptorFactory.Create(
            id:                 DiagnosticIdentifiers.OverridingMemberCannotChangeAccessModifiers, 
            title:              "Overriding member cannot change access modifiers.", 
            messageFormat:      "Overriding member cannot change access modifiers.", 
            category:           DiagnosticCategories.Roslynator, 
            defaultSeverity:    DiagnosticSeverity.Hidden, 
            isEnabledByDefault: true, 
            description:        null, 
            helpLinkUri:        DiagnosticIdentifiers.OverridingMemberCannotChangeAccessModifiers, 
            customTags:         Array.Empty<string>());

        [Obsolete("", error: true)]
        internal static readonly DiagnosticDescriptor CallDebugFailInsteadOfDebugAssert = DiagnosticDescriptorFactory.Create(
            id:                 DiagnosticIdentifiers.CallDebugFailInsteadOfDebugAssert, 
            title:              "Call Debug.Fail instead of Debug.Assert.", 
            messageFormat:      "Call Debug.Fail instead of Debug.Assert.", 
            category:           DiagnosticCategories.Roslynator, 
            defaultSeverity:    DiagnosticSeverity.Info, 
            isEnabledByDefault: true, 
            description:        null, 
            helpLinkUri:        DiagnosticIdentifiers.CallDebugFailInsteadOfDebugAssert, 
            customTags:         Array.Empty<string>());

        [Obsolete("", error: true)]
        internal static readonly DiagnosticDescriptor FormatInitializerWithSingleExpressionOnSingleLine = DiagnosticDescriptorFactory.Create(
            id:                 DiagnosticIdentifiers.FormatInitializerWithSingleExpressionOnSingleLine, 
            title:              "Format initializer with single expression on single line.", 
            messageFormat:      "Format initializer with single expression on single line.", 
            category:           DiagnosticCategories.Roslynator, 
            defaultSeverity:    DiagnosticSeverity.Hidden, 
            isEnabledByDefault: true, 
            description:        null, 
            helpLinkUri:        DiagnosticIdentifiers.FormatInitializerWithSingleExpressionOnSingleLine, 
            customTags:         Array.Empty<string>());

        [Obsolete("", error: true)]
        internal static readonly DiagnosticDescriptor FormatConditionalExpression = DiagnosticDescriptorFactory.Create(
            id:                 DiagnosticIdentifiers.FormatConditionalExpression, 
            title:              "Format conditional expression (format ? and : on next line).", 
            messageFormat:      "Format conditional expression.", 
            category:           DiagnosticCategories.Roslynator, 
            defaultSeverity:    DiagnosticSeverity.Info, 
            isEnabledByDefault: false, 
            description:        null, 
            helpLinkUri:        DiagnosticIdentifiers.FormatConditionalExpression, 
            customTags:         Array.Empty<string>());

        [Obsolete("", error: true)]
        internal static readonly DiagnosticDescriptor FormatSingleLineBlock = DiagnosticDescriptorFactory.Create(
            id:                 DiagnosticIdentifiers.FormatSingleLineBlock, 
            title:              "Format single-line block.", 
            messageFormat:      "Format single-line block.", 
            category:           DiagnosticCategories.Roslynator, 
            defaultSeverity:    DiagnosticSeverity.Info, 
            isEnabledByDefault: false, 
            description:        null, 
            helpLinkUri:        DiagnosticIdentifiers.FormatSingleLineBlock, 
            customTags:         Array.Empty<string>());

        [Obsolete("", error: true)]
        internal static readonly DiagnosticDescriptor CallSkipAndAnyInsteadOfCount = DiagnosticDescriptorFactory.Create(
            id:                 DiagnosticIdentifiers.CallSkipAndAnyInsteadOfCount, 
            title:              "Call 'Enumerable.Skip' and 'Enumerable.Any' instead of 'Enumerable.Count'.", 
            messageFormat:      "Call 'Enumerable.Skip' and 'Enumerable.Any' instead of 'Enumerable.Count'.", 
            category:           DiagnosticCategories.Roslynator, 
            defaultSeverity:    DiagnosticSeverity.Info, 
            isEnabledByDefault: false, 
            description:        null, 
            helpLinkUri:        DiagnosticIdentifiers.CallSkipAndAnyInsteadOfCount, 
            customTags:         Array.Empty<string>());

        [Obsolete("", error: true)]
        internal static readonly DiagnosticDescriptor SimplifyConditionalExpression2 = DiagnosticDescriptorFactory.Create(
            id:                 DiagnosticIdentifiers.SimplifyConditionalExpression2, 
            title:              "Simplify conditional expression.", 
            messageFormat:      "Simplify conditional expression.", 
            category:           DiagnosticCategories.Roslynator, 
            defaultSeverity:    DiagnosticSeverity.Hidden, 
            isEnabledByDefault: true, 
            description:        null, 
            helpLinkUri:        DiagnosticIdentifiers.SimplifyConditionalExpression2, 
            customTags:         Array.Empty<string>());

    }
}