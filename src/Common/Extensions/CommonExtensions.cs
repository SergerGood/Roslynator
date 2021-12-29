// Copyright (c) Josef Pihrt and Contributors. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Globalization;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Roslynator.Configuration;

namespace Roslynator
{
    internal static class CommonExtensions
    {
        public static bool IsEnabled(
            this AnalyzerOptionDescriptor analyzerOption,
            SyntaxNodeAnalysisContext context)
        {
            return IsEnabled(
                analyzerOption,
                context.Node.SyntaxTree,
                context.Options);
        }

        public static bool? IsEnabled(
            this AnalyzerOptionDescriptor analyzerOption,
            SyntaxNodeAnalysisContext context,
            bool checkParent)
        {
            return IsEnabled(
                analyzerOption,
                context.Node.SyntaxTree,
                context.Compilation.Options,
                context.Options,
                checkParent);
        }

        public static bool IsEnabled(
            this AnalyzerOptionDescriptor analyzerOption,
            SymbolAnalysisContext context)
        {
            return IsEnabled(
                analyzerOption,
                context.Symbol.Locations[0].SourceTree,
                context.Options);
        }

        public static bool? IsEnabled(
            this AnalyzerOptionDescriptor analyzerOption,
            SyntaxTree syntaxTree,
            CompilationOptions compilationOptions,
            AnalyzerOptions analyzerOptions,
            bool checkParent)
        {
            if (checkParent && !analyzerOption.Descriptor.IsEffective(syntaxTree, compilationOptions))
                return null;

            return IsEnabled(analyzerOption, syntaxTree, analyzerOptions);
        }

        public static bool IsEnabled(
            this AnalyzerOptionDescriptor analyzerOption,
            SyntaxTree syntaxTree,
            AnalyzerOptions analyzerOptions)
        {
            if (analyzerOptions
                .AnalyzerConfigOptionsProvider
                .GetOptions(syntaxTree)
                .TryGetValue(analyzerOption.OptionKey, out string value)
                && bool.TryParse(value, out bool result))
            {
                return result;
            }

            return false;
        }

        internal static bool? GetOptionAsBoolOrDefault(
            this SyntaxNodeAnalysisContext context,
            ConfigOptionDescriptor option,
            ConfigOptionDescriptor oldOption,
            DiagnosticDescriptor descriptor,
            bool invertOldValue = false)
        {
            if (context.TryGetOptionAsBool(option, out bool value))
                return value;

            if (context.TryGetOptionAsBool(oldOption, out bool oldValue))
                return (invertOldValue) ? !oldValue : oldValue;

            context.ReportRequiredOptionNotSet(descriptor);

            return null;
        }

        public static bool TryGetOptionAsBool(
            this AnalyzerOptions analyzerOptions,
            AnalyzerOptionDescriptor option,
            SyntaxTree syntaxTree,
            out bool result)
        {
            if (analyzerOptions
                .AnalyzerConfigOptionsProvider
                .GetOptions(syntaxTree)
                .TryGetValue(option.OptionKey, out string rawValue)
                && bool.TryParse(rawValue, out bool value))
            {
                result = value;
                return true;
            }

            result = default;
            return false;
        }

        public static bool IsEnabled(
            this SyntaxNodeAnalysisContext context,
            ConfigOptionDescriptor option,
            bool? defaultValue = null)
        {
            return IsEnabled(context.Options, option, context.Node.SyntaxTree, defaultValue);
        }

        public static bool IsEnabled(
            this AnalyzerOptions analyzerOptions,
            ConfigOptionDescriptor option,
            SyntaxTree syntaxTree,
            bool? defaultValue = null)
        {
            if (analyzerOptions
                .AnalyzerConfigOptionsProvider
                .GetOptions(syntaxTree)
                .TryGetValue(option.Key, out string value)
                && bool.TryParse(value, out bool result))
            {
                return result;
            }

            return defaultValue
                ?? CodeAnalysisConfig.Instance.GetOptionAsBool(option.Key)
                ?? option.DefaultValueAsBool
                ?? false;
        }

        public static bool TryGetOptionAsBool(
            this SyntaxNodeAnalysisContext context,
            ConfigOptionDescriptor option,
            out bool result)
        {
            return TryGetOptionAsBool(context.Options, option, context.Node.SyntaxTree, out result);
        }

        public static bool TryGetOptionAsBool(
            this AnalyzerOptions analyzerOptions,
            ConfigOptionDescriptor option,
            SyntaxTree syntaxTree,
            out bool result)
        {
            if (analyzerOptions
                .AnalyzerConfigOptionsProvider
                .GetOptions(syntaxTree)
                .TryGetValue(option.Key, out string rawValue)
                && bool.TryParse(rawValue, out bool value))
            {
                result = value;
                return true;
            }

            result = default;
            return false;
        }

        public static bool TryGetOptionAsInt(
            this AnalyzerOptions analyzerOptions,
            ConfigOptionDescriptor option,
            SyntaxTree syntaxTree,
            out int result)
        {
            if (analyzerOptions
                .AnalyzerConfigOptionsProvider
                .GetOptions(syntaxTree)
                .TryGetValue(option.Key, out string rawValue)
                && int.TryParse(rawValue, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, CultureInfo.CurrentCulture, out int value))
            {
                result = value;
                return true;
            }

            result = default;
            return false;
        }

        public static int GetOptionAsInt(
            this AnalyzerOptions analyzerOptions,
            ConfigOptionDescriptor option,
            SyntaxTree syntaxTree,
            int defaultValue)
        {
            return (TryGetOptionAsInt(analyzerOptions, option, syntaxTree, out int result))
                ? result
                : defaultValue;
        }

        internal static bool IsEnabled(this AnalyzerConfigOptions analyzerConfigOptions, ConfigOptionDescriptor option)
        {
            return analyzerConfigOptions.TryGetValue(option.Key, out string rawValue)
                && bool.TryParse(rawValue, out bool value)
                && value;
        }

        internal static bool TryGetValueAsBool(this AnalyzerConfigOptions analyzerConfigOptions, ConfigOptionDescriptor option, out bool value)
        {
            value = false;

            return analyzerConfigOptions.TryGetValue(option.Key, out string rawValue)
                && bool.TryParse(rawValue, out value);
        }

        internal static AnalyzerConfigOptions GetConfigOptions(this SyntaxNodeAnalysisContext context)
        {
            return context.Options.AnalyzerConfigOptionsProvider.GetOptions(context.Node.SyntaxTree);
        }

        public static EmptyStringStyle GetEmptyStringStyle(this AnalyzerConfigOptions configOptions)
        {
            if (configOptions.TryGetValue(ConfigOptions.EmptyStringStyle.Key, out string rawValue))
            {
                if (string.Equals(rawValue, "field", StringComparison.OrdinalIgnoreCase))
                {
                    return EmptyStringStyle.Field;
                }
                else if (string.Equals(rawValue, "literal", StringComparison.OrdinalIgnoreCase))
                {
                    return EmptyStringStyle.Literal;
                }
            }

            if (configOptions.IsEnabled(LegacyConfigOptions.UseStringEmptyInsteadOfEmptyStringLiteral))
            {
                return EmptyStringStyle.Field;
            }

            return EmptyStringStyle.None;
        }
    }
}
