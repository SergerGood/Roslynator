// Copyright (c) Josef Pihrt and Contributors. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Roslynator.CSharp.CodeFixes;
using Roslynator.Testing.CSharp;
using Xunit;

namespace Roslynator.CSharp.Analysis.Tests
{
    public class RCS1051AddOrRemoveParenthesesFromConditionOfConditionalExpressionTests : AbstractCSharpDiagnosticVerifier<AddOrRemoveParenthesesFromConditionOfConditionalExpressionAnalyzer, ExpressionCodeFixProvider>
    {
        public override DiagnosticDescriptor Descriptor { get; } = DiagnosticRules.AddOrRemoveParenthesesFromConditionOfConditionalExpression;

        [Fact, Trait(Traits.Analyzer, DiagnosticIdentifiers.AddOrRemoveParenthesesFromConditionOfConditionalExpression)]
        public async Task Test_AddParentheses()
        {
            await VerifyDiagnosticAndFixAsync(@"
class C
{
    void M()
    {
        string s = null;
        s = [|s != null|] ? ""true"" : ""false"";
    }
}
", @"
class C
{
    void M()
    {
        string s = null;
        s = (s != null) ? ""true"" : ""false"";
    }
}
", options: Options.AddConfigOption(ConfigOptionKeys.ParenthesizeConditionOfConditionalExpression, true));
        }

        [Fact, Trait(Traits.Analyzer, DiagnosticIdentifiers.AddOrRemoveParenthesesFromConditionOfConditionalExpression)]
        public async Task Test_AddParentheses_SingleTokenExpression()
        {
            await VerifyDiagnosticAndFixAsync(@"
class C
{
    void M()
    {
        bool b = false;
        string s = [|b|] ? ""true"" : ""false"";
    }
}
", @"
class C
{
    void M()
    {
        bool b = false;
        string s = (b) ? ""true"" : ""false"";
    }
}
", options: Options.AddConfigOption(ConfigOptionKeys.ParenthesizeConditionOfConditionalExpression, true));
        }

        [Fact, Trait(Traits.Analyzer, DiagnosticIdentifiers.AddOrRemoveParenthesesFromConditionOfConditionalExpression)]
        public async Task Test_RemoveParentheses()
        {
            await VerifyDiagnosticAndFixAsync(@"
class C
{
    void M()
    {
        string s = null;
        s = [|(s != null)|] ? ""true"" : ""false"";
    }
}
", @"
class C
{
    void M()
    {
        string s = null;
        s = s != null ? ""true"" : ""false"";
    }
}
", options: Options.AddConfigOption(ConfigOptionKeys.ParenthesizeConditionOfConditionalExpression, false));
        }

        [Fact, Trait(Traits.Analyzer, DiagnosticIdentifiers.AddOrRemoveParenthesesFromConditionOfConditionalExpression)]
        public async Task Test_RemoveParentheses_SingleTokenExpression()
        {
            await VerifyDiagnosticAndFixAsync(@"
class C
{
    void M()
    {
        bool b = false;
        string s = [|(b)|] ? ""true"" : ""false"";
    }
}
", @"
class C
{
    void M()
    {
        bool b = false;
        string s = b ? ""true"" : ""false"";
    }
}
", options: Options.AddConfigOption(ConfigOptionKeys.ParenthesizeConditionOfConditionalExpressionWhenItContainsSingleToken, true));
        }

        [Fact, Trait(Traits.Analyzer, DiagnosticIdentifiers.AddOrRemoveParenthesesFromConditionOfConditionalExpression)]
        public async Task TestNoDiagnostic_SingleTokenExpression()
        {
            await VerifyNoDiagnosticAsync(@"
public class C
{
    void M()
    {
        bool b = false;
        string s = b ? ""true"" : ""false"";
    }
}
", options: Options.AddConfigOption(ConfigOptionKeys.ParenthesizeConditionOfConditionalExpressionWhenItContainsSingleToken, true));
        }
    }
}
