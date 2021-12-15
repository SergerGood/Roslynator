﻿// Copyright (c) Josef Pihrt and Contributors. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using Microsoft.VisualStudio.Shell;

namespace Roslynator.VisualStudio
{
    public abstract class BaseOptionsPage : UIElementDialogPage
    {
        protected override UIElement Child => Control;

        [Browsable(false)]
        public string LastMaxId { get; set; }

        protected abstract string MaxId { get; }

        protected Dictionary<string, bool> Items { get; } = new Dictionary<string, bool>();

        internal BaseOptionsPageControl Control { get; } = new BaseOptionsPageControl();

        public bool IsLoaded { get; private set; }

        protected abstract void Fill(ICollection<BaseModel> items);

        public void Load()
        {
            if (!IsLoaded)
            {
                Fill(Control.Items);
                IsLoaded = true;
            }
        }

        internal IEnumerable<KeyValuePair<string, bool>> GetItems()
        {
            foreach (KeyValuePair<string, bool> kvp in Items)
                yield return kvp;
        }

        public void CheckNewItemsDisabledByDefault(IEnumerable<string> itemsDisabledByDefault)
        {
            var shouldSave = false;

            if (string.IsNullOrEmpty(LastMaxId))
            {
                if (Items.Count == 0)
                {
                    foreach (string id in itemsDisabledByDefault)
                        Items.Add(id, false);
                }

                shouldSave = true;
            }
            else if (string.CompareOrdinal(LastMaxId, MaxId) < 0)
            {
                foreach (string id in itemsDisabledByDefault)
                {
                    if (string.CompareOrdinal(LastMaxId, id) < 0)
                        Items[id] = false;
                }

                shouldSave = true;
            }

            if (shouldSave)
            {
                LastMaxId = MaxId;
                SaveSettingsToStorage();
            }
        }

        protected override void OnActivate(CancelEventArgs e)
        {
            base.OnActivate(e);

            Load();
        }

        protected override void OnClosed(EventArgs e)
        {
            IsLoaded = false;
        }

        protected override void OnApply(PageApplyEventArgs e)
        {
            if (e.ApplyBehavior == ApplyKind.Apply)
            {
                foreach (BaseModel model in Control.Items)
                    SetIsEnabled(model.Id, model.Enabled);
            }

            base.OnApply(e);
        }

        protected void SetIsEnabled(string id, bool? isEnabled)
        {
            if (isEnabled.HasValue)
            {
                Items[id] = isEnabled.Value;
            }
            else
            {
                Items.Remove(id);
            }
        }

        protected bool? IsEnabled(string id)
        {
            if (Items.TryGetValue(id, out bool enabled))
                return enabled;

            return null;
        }
    }
}
