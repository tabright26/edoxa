// Filename: NotificationDescription.cs
// Date Created: 2019-04-06
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;

namespace eDoxa.Notifications.Domain.AggregateModels.NotificationAggregate
{
    internal sealed class NotificationDescription
    {
        public NotificationDescription(string name, string title, string template)
        {
            Name = NotificationNames.IsValid(name) ? name.Trim() : throw new ArgumentException(name, nameof(name));
            Title = !string.IsNullOrWhiteSpace(title) ? title.Trim() : throw new ArgumentNullException(nameof(title));
            Template = !string.IsNullOrWhiteSpace(template) ? template.Trim() : throw new ArgumentNullException(nameof(template));
        }

        public string Name { get; }

        public string Title { get; }

        public int ArgumentCount
        {
            get
            {
                return Template.Count(c => c == '{');
            }
        }

        private string Template { get; }

        public string FormatMessage(INotificationMetadata metadata)
        {
            return metadata?.Any() ?? false ? string.Format(Template, metadata.ToArray()) : Template;
        }

        public void Validate(INotificationMetadata metadata)
        {
            if (Template.Any(c => c == '{' || c == '}'))
            {
                if (!metadata?.Any() ?? true)
                {
                    throw new ArgumentException(metadata?.ToString(), nameof(metadata));
                }

                string.Format(Template, metadata.ToArray());
            }
        }
    }
}