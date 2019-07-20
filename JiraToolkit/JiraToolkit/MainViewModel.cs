using System.IO;
using System.Linq;
using JiraToolkit.Dtos;
using JiraToolkit.ViewModels;
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
            Queries = dto.Queries.Where(x => x != null).Select(x => new QueryViewModel() { Name = x.Name, Url = x.Url }).ToArray();
        }

        public EnvironmentViewModel[] Environments { get; }

        public QueryViewModel[] Queries { get; }
    }
}
