using System.IO;
using System.Linq;
using JiraToolkit.Dtos;
using Newtonsoft.Json;

namespace JiraToolkit
{
    internal sealed class MainViewModel
    {
        public MainViewModel()
        {
            var json = File.ReadAllText("./Configuration.json");
            var dto = JsonConvert.DeserializeObject<Configuration>(json);
            Environments = dto.Environments.Where(x => x != null).Select((x) => new EnvironmentViewModel(x)).ToArray();
            BrowserPath = dto.BrowserPath;
        }

        public EnvironmentViewModel[] Environments { get; }

        public string BrowserPath { get; }
    }
}
