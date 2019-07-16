using System.Collections.Generic;
using JiraToolkit.ViewModels;

namespace JiraToolkit
{
    internal sealed class EnvironmentViewModel
    {
        public EnvironmentViewModel(Environment environment)
        {
            Name = environment.Name;
            Entries = new List<EnvironmentEntry>();
            foreach (var prefix in environment.Prefixes)
            {
                Entries.Add(new EnvironmentEntry
                {
                    Root = environment.RootUrl,
                    Prefix = prefix
                });
            }
        }

        public string Name { get; }

        public List<EnvironmentEntry> Entries { get;}
    }
}
