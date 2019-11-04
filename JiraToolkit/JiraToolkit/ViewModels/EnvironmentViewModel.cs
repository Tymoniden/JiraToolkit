using System.Collections.Generic;
using JiraToolkit.Dtos;

namespace JiraToolkit.ViewModels
{
    internal class EnvironmentViewModel
    {
        private readonly OptionsViewModel _options;

        public EnvironmentViewModel(Environment environment, OptionsViewModel options)
        {
            _options = options ?? throw new System.ArgumentNullException(nameof(options));

            Name = environment.Name;

            foreach (var prefix in environment.Prefixes)
            {
                Entries.Add(new EnvironmentEntryViewModel(_options)
                {
                    Root = environment.RootUrl,
                    Prefix = prefix
                });
            }
        }

        public string Name { get; }

        public List<EnvironmentEntryViewModel> Entries { get; } = new List<EnvironmentEntryViewModel>();
    }
}
