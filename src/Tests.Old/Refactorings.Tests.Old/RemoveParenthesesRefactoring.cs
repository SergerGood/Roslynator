﻿// Copyright (c) Josef Pihrt and Contributors. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Roslynator.CSharp.Refactorings.Tests
{
    internal class RemoveParenthesesRefactoring
    {
        public void SomeMethod()
        {
            bool condition = false;

            bool result = (condition) ? true : false;
        }
    }
}
