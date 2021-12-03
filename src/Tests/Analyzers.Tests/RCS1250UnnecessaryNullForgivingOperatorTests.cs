﻿// Copyright (c) Josef Pihrt and Contributors. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Roslynator.CSharp.CodeFixes;
using Roslynator.Testing.CSharp;
using Xunit;

namespace Roslynator.CSharp.Analysis.Tests
{
    public class RCS1250UnnecessaryNullForgivingOperatorTests : AbstractCSharpDiagnosticVerifier<UnncessaryNullForgivingOperatorAnalyzer, TokenCodeFixProvider>
    {
        public override DiagnosticDescriptor Descriptor { get; } = DiagnosticRules.UnnecessaryNullForgivingOperator;

        [Fact, Trait(Traits.Analyzer, DiagnosticIdentifiers.UnnecessaryNullForgivingOperator)]
        public async Task Test_Property()
        {
            await VerifyDiagnosticAndFixAsync(@"
#nullable enable

class C
{
    string? P { get; set; } [|= null!|]; //x
}
", @"
#nullable enable

class C
{
    string? P { get; set; } //x
}
");
        }

        [Fact, Trait(Traits.Analyzer, DiagnosticIdentifiers.UnnecessaryNullForgivingOperator)]
        public async Task Test_Property_DefaultLiteral()
        {
            await VerifyDiagnosticAndFixAsync(@"
#nullable enable

class C
{
    string? P { get; set; } [|= default!|];
}
", @"
#nullable enable

class C
{
    string? P { get; set; }
}
");
        }

        [Fact, Trait(Traits.Analyzer, DiagnosticIdentifiers.UnnecessaryNullForgivingOperator)]
        public async Task Test_Property_DefaultExpression()
        {
            await VerifyDiagnosticAndFixAsync(@"
#nullable enable

class C
{
    string? P { get; set; } [|= default(string)!|];
}
", @"
#nullable enable

class C
{
    string? P { get; set; }
}
");
        }

        [Fact, Trait(Traits.Analyzer, DiagnosticIdentifiers.UnnecessaryNullForgivingOperator)]
        public async Task Test_Field()
        {
            await VerifyDiagnosticAndFixAsync(@"
#nullable enable

class C
{
    private string? F [|= null!|]; //x
}
", @"
#nullable enable

class C
{
    private string? F; //x
}
");
        }

        [Fact, Trait(Traits.Analyzer, DiagnosticIdentifiers.UnnecessaryNullForgivingOperator)]
        public async Task Test_LocalVariable()
        {
            await VerifyDiagnosticAndFixAsync(@"
#nullable enable

class C
{
    void M()
    {
        string? s = null[|!|]; //x
    }
}
", @"
#nullable enable

class C
{
    void M()
    {
        string? s = null; //x
    }
}
");
        }

        [Fact, Trait(Traits.Analyzer, DiagnosticIdentifiers.UnnecessaryNullForgivingOperator)]
        public async Task Test_Argument()
        {
            await VerifyDiagnosticAndFixAsync(@"
#nullable enable

class C
{
    void M(string? p)
    {
        string? s = null;

        M(s[|!|]);
    }
}
", @"
#nullable enable

class C
{
    void M(string? p)
    {
        string? s = null;

        M(s);
    }
}
");
        }
    }
}
