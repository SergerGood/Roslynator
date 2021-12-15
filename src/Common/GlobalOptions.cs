﻿// Copyright (c) Josef Pihrt and Contributors. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Roslynator.Configuration;

namespace Roslynator
{
    public static class GlobalOptions
    {
        public static readonly OptionDescriptor MaxLineLength = new OptionDescriptor(
            OptionKeys.MaxLineLength,
            defaultValue: OptionDefaultValues.MaxLineLength.ToString(),
            description: "Max line length",
            valuePlaceholder: "<MAX_LINE_LENGTH>");

        public static readonly OptionDescriptor PrefixFieldIdentifierWithUnderscore = new OptionDescriptor(
            OptionKeys.PrefixFieldIdentifierWithUnderscore,
            defaultValue: OptionDefaultValues.PrefixFieldIdentifierWithUnderscore.ToString().ToLowerInvariant(),
            description: "Prefix field identifier with underscore",
            valuePlaceholder: "true|false");

        public static readonly OptionDescriptor PreferNoNewlineAtEndOfFile = new OptionDescriptor(
            OptionKeys.PreferNoNewlineAtEndOfFile,
            defaultValue: "false",
            description: "Prefer no newline at end of file",
            valuePlaceholder: "true|false");
    }
}
