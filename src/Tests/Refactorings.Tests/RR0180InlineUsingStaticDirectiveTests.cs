﻿// Copyright (c) Josef Pihrt and Contributors. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Roslynator.Testing.CSharp;
using Xunit;

namespace Roslynator.CSharp.Refactorings.Tests
{
    public class RR0180InlineUsingStaticDirectiveTests : AbstractCSharpRefactoringVerifier
    {
        public override string RefactoringId { get; } = RefactoringIdentifiers.InlineUsingStaticDirective;

        [Fact, Trait(Traits.Refactoring, RefactoringIdentifiers.InlineUsingStaticDirective)]
        public async Task Test()
        {
            await VerifyRefactoringAsync(@"
using System.Linq;
using [||]static System.Linq.Enumerable;

class C
{
    void M()
    {
        #region
        Empty<object>();
        #endregion

        Empty<object>();
    }
}
", @"
using System.Linq;

class C
{
    void M()
    {
        #region
        Enumerable.Empty<object>();
        #endregion

        Enumerable.Empty<object>();
    }
}
", equivalenceKey: EquivalenceKey.Create(RefactoringId));
        }
    }
}
