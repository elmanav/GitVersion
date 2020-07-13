using System;
using System.Collections.Generic;
using System.Linq;
using GitVersion.VersionCalculation;
using JetBrains.Annotations;
using YamlDotNet.Serialization;

namespace GitVersion.Model.Configuration
{
    public class IgnoreConfig
    {
        public IgnoreConfig()
        {
            ShAs = Enumerable.Empty<string>();
        }

        [YamlMember(Alias = "commits-before")]
        public DateTimeOffset? Before { get; set; }

        [YamlMember(Alias = "sha")]
        [NotNull]
        public IEnumerable<string> ShAs { get; [UsedImplicitly] private set; }

        [YamlIgnore]
        public virtual bool IsEmpty => Before == null && !ShAs.Any();

        public virtual IEnumerable<IVersionFilter> ToFilters()
        {
            if (ShAs.Any()) yield return new ShaVersionFilter(ShAs);
            if (Before.HasValue) yield return new MinDateVersionFilter(Before.Value);
        }
    }
}
