using System.Collections.Generic;
using JiraToolkit.Dtos;
using JiraToolkit.ViewModels;

namespace JiraToolkit
{
    internal sealed class EnvironmentViewModel
    {
        public EnvironmentViewModel(Environment environment)
        {
            Name = environment.Name;
            Entries = new List<EnvironmentEntryViewModel>();
            foreach (var prefix in environment.Prefixes)
            {
                Entries.Add(new EnvironmentEntryViewModel
                {
                    Root = environment.RootUrl,
                    Prefix = prefix
                });
            }
        }

        public string Name { get; }

        public List<EnvironmentEntryViewModel> Entries { get;}
    }
}
