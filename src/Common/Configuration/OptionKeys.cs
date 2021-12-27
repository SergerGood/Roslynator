// Copyright (c) Josef Pihrt and Contributors. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Roslynator.Configuration
{
    internal static class OptionKeys
    {
        public const string PreferImplicitObjectCreation = "roslynator.prefer_implicit_object_creation";
        public const string PreferImplicitObjectCreationWhenTypeIsObvious = "roslynator.prefer_implicit_object_creation_when_type_is_obvious";
        public const string PreferExplicitObjectCreation = "roslynator.prefer_explicit_object_creation";
        public const string PreferVarInsteadOfImplicitObjectCreation = "roslynator.prefer_var_instead_of_implicit_object_creation";

        public const string CompilerDiagnosticFixEnabled = "roslynator.compiler_diagnostic_fix.enabled";
        public const string CompilerDiagnosticFixPrefix = "roslynator.compiler_diagnostic_fix.";
        public const string MaxLineLength = "roslynator.max_line_length";
        public const string PreferNoNewLineAtEndOfFile = "roslynator.prefer_no_new_line_at_end_of_file";
        public const string PrefixFieldIdentifierWithUnderscore = "roslynator.prefix_field_identifier_with_underscore";
        public const string RefactoringEnabled = "roslynator.refactoring.enabled";
        public const string RefactoringPrefix = "roslynator.refactoring.";
    }
}
