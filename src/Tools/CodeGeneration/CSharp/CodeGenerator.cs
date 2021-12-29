﻿// Copyright (c) Josef Pihrt and Contributors. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Roslynator.CSharp;
using Roslynator.Metadata;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Roslynator.CSharp.CSharpFactory;

namespace Roslynator.CodeGeneration.CSharp
{
    public static class CodeGenerator
    {
        public static CompilationUnitSyntax GenerateConfigOptions(IEnumerable<ConfigOptionMetadata> options, IEnumerable<AnalyzerMetadata> analyzers)
        {
            return CompilationUnit(
                UsingDirectives("System.Collections.Generic"),
                NamespaceDeclaration(
                    "Roslynator",
                    ClassDeclaration(
                        Modifiers.Public_Static_Partial(),
                        "ConfigOptions",
                        options
                            .OrderBy(f => f.Id)
                            .Select(f =>
                            {
                                return FieldDeclaration(
                                    Modifiers.Public_Static_ReadOnly(),
                                    IdentifierName("ConfigOptionDescriptor"),
                                    f.Id,
                                    ImplicitObjectCreationExpression(
                                        ArgumentList(
                                            Argument(NameColon("key"), ParseExpression($"ConfigOptionKeys.{f.Id}")),
                                            Argument(NameColon("defaultValue"), (f.DefaultValue != null) ? StringLiteralExpression(f.DefaultValue) : NullLiteralExpression()),
                                            Argument(NameColon("defaultValuePlaceholder"), StringLiteralExpression(f.DefaultValuePlaceholder)),
                                            Argument(NameColon("description"), StringLiteralExpression(f.Description))),
                                        default(InitializerExpressionSyntax)));
                            })
                            //TODO: x
                            //.Concat(new MemberDeclarationSyntax[]
                            //    {
                            //        MethodDeclaration(
                            //            Modifiers.Private_Static(),
                            //            ParseTypeName("IEnumerable<KeyValuePair<string, string>>"),
                            //            Identifier("GetRequiredOptions"),
                            //            ParameterList(),
                            //            Block(
                            //                analyzers
                            //                    .Where(f => f.ConfigOptions.Any(f => f.IsRequired))
                            //                    .Select(f => (id: f.Id, keys: f.ConfigOptions.Where(f => f.IsRequired)))
                            //                    .Select(f =>
                            //                    {
                            //                        IEnumerable<string> optionIdentifiers = f.keys
                            //                            .Select(f => options.Single(o => o.Key == f.Key))
                            //                            .Select(f => $"ConfigOptionKeys.{f.Id}");

                            //                        return YieldReturnStatement(
                            //                            ParseExpression($"new KeyValuePair<string, string>(\"{f.id}\", JoinOptionKeys({string.Join(", ", optionIdentifiers)}))"));
                            //                    })))
                            //    })
                            .ToSyntaxList<MemberDeclarationSyntax>())));
        }

        public static CompilationUnitSyntax GenerateLegacyConfigOptions(IEnumerable<AnalyzerMetadata> analyzers)
        {
            return CompilationUnit(
                UsingDirectives(),
                NamespaceDeclaration(
                    "Roslynator",
                    ClassDeclaration(
                        Modifiers.Public_Static_Partial(),
                        "LegacyConfigOptions",
                        analyzers
                            .SelectMany(f => f.Options)
                            .OrderBy(f => f.Identifier)
                            .Select(f =>
                            {
                                return FieldDeclaration(
                                    Modifiers.Public_Static_ReadOnly(),
                                    IdentifierName("ConfigOptionDescriptor"),
                                    f.Identifier,
                                    ImplicitObjectCreationExpression(
                                        ArgumentList(
                                            Argument(NameColon("key"), StringLiteralExpression($"roslynator.{f.ParentId}.{f.OptionKey}")),
                                            Argument(NameColon("defaultValue"), StringLiteralExpression("false")),
                                            Argument(NameColon("defaultValuePlaceholder"), StringLiteralExpression("true|false")),
                                            Argument(NameColon("description"), StringLiteralExpression(""))),
                                        default(InitializerExpressionSyntax)));
                            })
                            .ToSyntaxList<MemberDeclarationSyntax>())));
        }

        public static CompilationUnitSyntax GenerateConfigOptionKeys(IEnumerable<ConfigOptionMetadata> options)
        {
            return CompilationUnit(
                UsingDirectives(),
                NamespaceDeclaration(
                    "Roslynator",
                    ClassDeclaration(
                        Modifiers.Internal_Static_Partial(),
                        "ConfigOptionKeys",
                        options
                            .OrderBy(f => f.Id)
                            .Select(f =>
                            {
                                return FieldDeclaration(
                                    Modifiers.Public_Const(),
                                    PredefinedStringType(),
                                    f.Id,
                                    StringLiteralExpression(f.Key));
                            })
                            .ToSyntaxList<MemberDeclarationSyntax>())));
        }
    }
}
