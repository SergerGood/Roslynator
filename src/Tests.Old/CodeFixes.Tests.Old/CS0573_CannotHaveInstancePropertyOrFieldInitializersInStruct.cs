﻿// Copyright (c) Josef Pihrt and Contributors. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Roslynator.CSharp.CodeFixes.Tests
{
    internal static class CS0573_CannotHaveInstancePropertyOrFieldInitializersInStruct
    {
        private struct Foo
        {
            private string _fieldName = "";

            public string PropertyName { get; set; } = "";
        }
    }
}
