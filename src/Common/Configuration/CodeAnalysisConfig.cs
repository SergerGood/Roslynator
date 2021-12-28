﻿// Copyright (c) Josef Pihrt and Contributors. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Roslynator.Configuration
{
    public sealed class CodeAnalysisConfig
    {
        private ImmutableDictionary<string, bool> _editorConfigBoolOptions;

        public static CodeAnalysisConfig Instance { get; private set; } = new();

        private CodeAnalysisConfig(
            XmlCodeAnalysisConfig xmlConfig = null,
            EditorConfigCodeAnalysisConfig editorConfig = null,
            VisualStudioCodeAnalysisConfig visualStudioConfig = null)
        {
            if (xmlConfig != null)
            {
                XmlConfig = xmlConfig;
            }
            else
            {
                string xmlConfigPath = XmlCodeAnalysisConfig.GetDefaultConfigFilePath();

                XmlConfig = (File.Exists(xmlConfigPath))
                    ? XmlCodeAnalysisConfigLoader.Load(xmlConfigPath)
                    : XmlCodeAnalysisConfig.Empty;
            }

            if (editorConfig != null)
            {
                EditorConfig = editorConfig;
            }
            else
            {
                IEnumerable<string> editorConfigPaths = XmlConfig.Includes
                    .Where(path => Path.GetFileName(path) == EditorConfigCodeAnalysisConfig.FileName);

                string defaultEditorConfigPath = EditorConfigCodeAnalysisConfig.GetDefaultConfigFilePath();

                if (!string.IsNullOrEmpty(defaultEditorConfigPath))
                    editorConfigPaths = (new string[] { defaultEditorConfigPath }).Concat(editorConfigPaths);

                EditorConfig = EditorConfigCodeAnalysisConfigLoader.Load(editorConfigPaths);
            }

            _editorConfigBoolOptions = EditorConfig.Options.Select(f =>
            {
                if (bool.TryParse(f.Value, out bool value))
                {
                    return (key: f.Key, value);
                }

                return default;
            })
            .Where(f => f.key != null)
            .ToImmutableDictionary(f => f.key, f => f.value);

            VisualStudioConfig = visualStudioConfig ?? VisualStudioCodeAnalysisConfig.Empty;

            bool? prefixFieldIdentifierWithUnderscore = XmlConfig.PrefixFieldIdentifierWithUnderscore;

            if (EditorConfig.PrefixFieldIdentifierWithUnderscore != null)
                prefixFieldIdentifierWithUnderscore = EditorConfig.PrefixFieldIdentifierWithUnderscore;

            if (VisualStudioConfig.PrefixFieldIdentifierWithUnderscore != ConfigOptionDefaultValues.PrefixFieldIdentifierWithUnderscore)
                prefixFieldIdentifierWithUnderscore = VisualStudioConfig.PrefixFieldIdentifierWithUnderscore;

            PrefixFieldIdentifierWithUnderscore = prefixFieldIdentifierWithUnderscore ?? ConfigOptionDefaultValues.PrefixFieldIdentifierWithUnderscore;

            int? maxLineLength = XmlConfig.MaxLineLength;

            if (EditorConfig.MaxLineLength != null)
                maxLineLength = EditorConfig.MaxLineLength;

            MaxLineLength = maxLineLength ?? ConfigOptionDefaultValues.MaxLineLength;

            var refactorings = new Dictionary<string, bool>();
            SetRefactorings(refactorings, XmlConfig.Refactorings);
            SetRefactorings(refactorings, EditorConfig.Refactorings);
            SetRefactorings(refactorings, VisualStudioConfig.Refactorings);
            Refactorings = refactorings.ToImmutableDictionary();

            var codeFixes = new Dictionary<string, bool>();
            SetCodeFixes(codeFixes, XmlConfig.CodeFixes);
            SetCodeFixes(codeFixes, EditorConfig.CodeFixes);
            SetCodeFixes(codeFixes, VisualStudioConfig.CodeFixes);
            CodeFixes = codeFixes.ToImmutableDictionary();

            void SetRefactorings(Dictionary<string, bool> options, ImmutableDictionary<string, bool> options2)
            {
                foreach (KeyValuePair<string, bool> option in options2)
                    options[option.Key] = option.Value;
            }

            void SetCodeFixes(Dictionary<string, bool> options, ImmutableDictionary<string, bool> options2)
            {
                foreach (KeyValuePair<string, bool> option in options2)
                    options[option.Key] = option.Value;
            }
        }

        internal EditorConfigCodeAnalysisConfig EditorConfig { get; }

        internal XmlCodeAnalysisConfig XmlConfig { get; }

        internal VisualStudioCodeAnalysisConfig VisualStudioConfig { get; }

        public int MaxLineLength { get; }

        public bool PrefixFieldIdentifierWithUnderscore { get; }

        public ImmutableDictionary<string, bool> Refactorings { get; }

        public ImmutableDictionary<string, bool> CodeFixes { get; }

        public static event EventHandler Updated;

        public bool? GetOptionAsBool(string key)
        {
            if (_editorConfigBoolOptions.TryGetValue(key, out bool value))
                return value;

            return null;
        }

        internal static void UpdateVisualStudioConfig(Func<VisualStudioCodeAnalysisConfig, VisualStudioCodeAnalysisConfig> config)
        {
            VisualStudioCodeAnalysisConfig newConfig = config(Instance.VisualStudioConfig);
            Instance = new CodeAnalysisConfig(Instance.XmlConfig, Instance.EditorConfig, newConfig);
            Updated?.Invoke(null, EventArgs.Empty);
        }

        public bool IsRefactoringEnabled(string id)
        {
            return (!Refactorings.TryGetValue(id, out bool enabled)) || enabled;
        }

        public DiagnosticSeverity? GetDiagnosticSeverity(string id, string category)
        {
            return EditorConfig.GetDiagnosticSeverity(id, category)
                ?? XmlConfig.GetDiagnosticSeverity(id);
        }

        public bool? IsDiagnosticEnabled(string id, string category)
        {
            return EditorConfig.IsDiagnosticEnabled(id, category)
                ?? XmlConfig.IsDiagnosticEnabled(id);
        }
    }
}
