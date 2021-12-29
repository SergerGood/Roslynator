﻿// Copyright (c) Josef Pihrt and Contributors. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.CodeAnalysis;

#pragma warning disable RS2008 // Enable analyzer release tracking

namespace Roslynator
{
    internal static class CommonDiagnosticRules
    {
        public static readonly DiagnosticDescriptor AnalyzerIsObsolete = new(
            id: CommonDiagnosticIdentifiers.AnalyzerIsObsolete,
            title: "Analyzer is obsolete",
            messageFormat: "Analyzer {0} is obsolete.{1}",
            category: DiagnosticCategories.Roslynator,
            defaultSeverity: DiagnosticSeverity.Warning,
            isEnabledByDefault: true,
            description: null,
            helpLinkUri: DiagnosticDescriptorUtility.GetHelpLinkUri(CommonDiagnosticIdentifiers.AnalyzerIsObsolete),
            customTags: Array.Empty<string>());

        public static readonly DiagnosticDescriptor RequiredOptionNotSetForAnalyzer = new(
            id: CommonDiagnosticIdentifiers.RequiredOptionNotSetForAnalyzer,
            title: "Required option not set for an analyzer",
            messageFormat: "Required option not set for analyzer {0}, allowed values: {1}",
            category: DiagnosticCategories.Roslynator,
            defaultSeverity: DiagnosticSeverity.Warning,
            isEnabledByDefault: true,
            description: null,
            helpLinkUri: DiagnosticDescriptorUtility.GetHelpLinkUri(CommonDiagnosticIdentifiers.RequiredOptionNotSetForAnalyzer),
            customTags: Array.Empty<string>());
    }
}
