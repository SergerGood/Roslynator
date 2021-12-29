// Copyright (c) Josef Pihrt and Contributors. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Roslynator.Formatting
{
    internal static class FormattingExtensions
    {
        public static bool TryGetNewLinePosition(
            this AnalyzerConfigOptions analyzerConfigOptions,
            ConfigOptionDescriptor option,
            out NewLinePosition newLinePosition)
        {
            if (analyzerConfigOptions.TryGetValue(option.Key, out string rawValue))
            {
                if (string.Equals(rawValue, "before", StringComparison.OrdinalIgnoreCase))
                {
                    newLinePosition = NewLinePosition.Before;
                    return true;
                }
                else if (string.Equals(rawValue, "after", StringComparison.OrdinalIgnoreCase))
                {
                    newLinePosition = NewLinePosition.After;
                    return true;
                }
            }

            newLinePosition = NewLinePosition.None;
            return false;
        }

        public static NewLinePosition GetNewLinePosition(
            this AnalyzerConfigOptions analyzerConfigOptions,
            ConfigOptionDescriptor option,
            out NewLinePosition newLinePosition)
        {
            return (TryGetNewLinePosition(analyzerConfigOptions, option, out newLinePosition))
                ? newLinePosition
                : NewLinePosition.None;
        }

        public static NewLinePosition GetBinaryExpressionNewLinePosition(this AnalyzerConfigOptions configOptions)
        {
            if (configOptions.TryGetNewLinePosition(ConfigOptions.BinaryOperatorNewLine, out NewLinePosition newLinePosition))
                return newLinePosition;

            if (configOptions.IsEnabled(LegacyConfigOptions.AddNewLineAfterBinaryOperatorInsteadOfBeforeIt))
                return NewLinePosition.After;

            return NewLinePosition.None;
        }

        public static NewLinePosition GetConditionalExpressionNewLinePosition(this AnalyzerConfigOptions configOptions)
        {
            if (configOptions.TryGetNewLinePosition(ConfigOptions.ConditionalExpressionNewLine, out NewLinePosition newLinePosition))
                return newLinePosition;

            if (configOptions.IsEnabled(LegacyConfigOptions.AddNewLineAfterConditionalOperatorInsteadOfBeforeIt))
                return NewLinePosition.After;

            return NewLinePosition.None;
        }

        public static NewLinePosition GetArrowTokenNewLinePosition(this AnalyzerConfigOptions configOptions)
        {
            if (configOptions.TryGetNewLinePosition(ConfigOptions.ArrowTokenNewLine, out NewLinePosition newLinePosition))
                return newLinePosition;

            if (configOptions.IsEnabled(LegacyConfigOptions.AddNewLineAfterExpressionBodyArrowInsteadOfBeforeIt))
                return NewLinePosition.After;

            return NewLinePosition.None;
        }

        public static NewLinePosition GetEqualsSignNewLinePosition(this AnalyzerConfigOptions configOptions)
        {
            if (configOptions.TryGetNewLinePosition(ConfigOptions.EqualsSignNewLine, out NewLinePosition newLinePosition))
                return newLinePosition;

            if (configOptions.IsEnabled(LegacyConfigOptions.AddNewLineAfterEqualsSignInsteadOfBeforeIt))
                return NewLinePosition.After;

            return NewLinePosition.None;
        }
    }
}
