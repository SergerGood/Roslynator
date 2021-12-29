﻿// Copyright (c) Josef Pihrt and Contributors. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Roslynator.Formatting.CodeFixes.CSharp;
using Roslynator.Testing.CSharp;
using Xunit;

namespace Roslynator.Formatting.CSharp.Tests
{
    public class RCS0015AddOrRemoveEmptyLineBetweenUsingDirectiveWithDifferentRootNamespaceTests : AbstractCSharpDiagnosticVerifier<AddOrRemoveEmptyLineBetweenUsingDirectiveAnalyzer, SyntaxTriviaCodeFixProvider>
    {
        public override DiagnosticDescriptor Descriptor { get; } = DiagnosticRules.AddOrRemoveEmptyLineBetweenUsingDirectiveWithDifferentRootNamespace;

        [Fact, Trait(Traits.Analyzer, DiagnosticIdentifiers.AddOrRemoveEmptyLineBetweenUsingDirectiveWithDifferentRootNamespace)]
        public async Task Test_AddEmptyLine_EmptyLine()
        {
            await VerifyDiagnosticAndFixAsync(@"
using System;[||]
using Microsoft.CodeAnalysis;[||]
using System.Threading;

class C
{
}
", @"
using System;

using Microsoft.CodeAnalysis;

using System.Threading;

class C
{
}
", options: Options.AddConfigOption(ConfigOptionKeys.PreferEmptyLineBetweenUsingDirectivesWithDifferentRootNamespace, true));
        }

        [Fact, Trait(Traits.Analyzer, DiagnosticIdentifiers.AddOrRemoveEmptyLineBetweenUsingDirectiveWithDifferentRootNamespace)]
        public async Task Test_RemoveEmptyLine()
        {
            await VerifyDiagnosticAndFixAsync(@"
using System;
[||]
using Microsoft.CodeAnalysis;
[||]
using System.Threading;

class C
{
}
", @"
using System;
using Microsoft.CodeAnalysis;
using System.Threading;

class C
{
}
", options: Options.AddConfigOption(ConfigOptionKeys.PreferEmptyLineBetweenUsingDirectivesWithDifferentRootNamespace, false));
        }

        [Fact, Trait(Traits.Analyzer, DiagnosticIdentifiers.AddOrRemoveEmptyLineBetweenUsingDirectiveWithDifferentRootNamespace)]
        public async Task Test_RemoveEmptyLines()
        {
            await VerifyDiagnosticAndFixAsync(@"
using System;
[||]    

using Microsoft.CodeAnalysis;

class C
{
}
", @"
using System;
using Microsoft.CodeAnalysis;

class C
{
}
", options: Options.AddConfigOption(ConfigOptionKeys.PreferEmptyLineBetweenUsingDirectivesWithDifferentRootNamespace, false));
        }

        [Fact, Trait(Traits.Analyzer, DiagnosticIdentifiers.AddOrRemoveEmptyLineBetweenUsingDirectiveWithDifferentRootNamespace)]
        public async Task TestNoDiagnostic_AddEmptyLine_SameRootNamespace()
        {
            await VerifyNoDiagnosticAsync(@"
using System;
using System.Linq;

class C
{
}
");
        }

        [Fact, Trait(Traits.Analyzer, DiagnosticIdentifiers.AddOrRemoveEmptyLineBetweenUsingDirectiveWithDifferentRootNamespace)]
        public async Task TestNoDiagnostic_AddEmptyLine_UsingStatic()
        {
            await VerifyNoDiagnosticAsync(@"
using Microsoft.CodeAnalysis;
using static System.IO.Path;

class C
{
}
");
        }

        [Fact, Trait(Traits.Analyzer, DiagnosticIdentifiers.AddOrRemoveEmptyLineBetweenUsingDirectiveWithDifferentRootNamespace)]
        public async Task TestNoDiagnostic_AddEmptyLine_Alias()
        {
            await VerifyNoDiagnosticAsync(@"
using Microsoft.CodeAnalysis;
using I = System.Int32;

class C
{
}
");
        }

        [Fact, Trait(Traits.Analyzer, DiagnosticIdentifiers.AddOrRemoveEmptyLineBetweenUsingDirectiveWithDifferentRootNamespace)]
        public async Task TestNoDiagnostic_RemoveEmptyLine_SameRootNamespace()
        {
            await VerifyNoDiagnosticAsync(@"
using System;

using System.Linq;

class C
{
}
");
        }

        [Fact, Trait(Traits.Analyzer, DiagnosticIdentifiers.AddOrRemoveEmptyLineBetweenUsingDirectiveWithDifferentRootNamespace)]
        public async Task TestNoDiagnostic_RemoveEmptyLine_UsingStatic()
        {
            await VerifyNoDiagnosticAsync(@"
using Microsoft.CodeAnalysis;

using static System.IO.Path;

class C
{
}
", options: Options.AddConfigOption(ConfigOptionKeys.PreferEmptyLineBetweenUsingDirectivesWithDifferentRootNamespace, false));
        }

        [Fact, Trait(Traits.Analyzer, DiagnosticIdentifiers.AddOrRemoveEmptyLineBetweenUsingDirectiveWithDifferentRootNamespace)]
        public async Task TestNoDiagnostic_RemoveEmptyLine_Alias()
        {
            await VerifyNoDiagnosticAsync(@"
using Microsoft.CodeAnalysis;

using I = System.Int32;

class C
{
}
", options: Options.AddConfigOption(ConfigOptionKeys.PreferEmptyLineBetweenUsingDirectivesWithDifferentRootNamespace, false));
        }
    }
}
