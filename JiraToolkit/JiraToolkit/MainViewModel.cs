using System.IO;
using System.Linq;
using JiraToolkit.Dtos;
using JiraToolkit.ViewModels;
using Newtonsoft.Json;
using Shuriken;

namespace JiraToolkit
{
    internal sealed class MainViewModel : ObservableObject
    {
        public MainViewModel() => UpdateConfigurationCommand = new Command(UpdateConfiguration, () => true);

        [Observable]
        public EnvironmentViewModel[] Environments { get; private set; }

        [Observable]
        public QueryViewModel[] Queries { get; private set; }

        public bool HasQueries => Queries.Any();

        [Observable]
        public bool StayOnTop { get; private set; }

        [Observable]
        public Command UpdateConfigurationCommand { get; private set; }

        public void UpdateConfiguration()
        {
            var json = File.ReadAllText("./Configuration.json");
            var dto = JsonConvert.DeserializeObject<Configuration>(json);
            Environments = dto.Environments.Where(x => x != null).Select((x) => new EnvironmentViewModel(x)).ToArray();
            Queries = dto.Queries.Where(x => x != null).Select(x => new QueryViewModel() { Name = x.Name, Url = x.Url }).ToArray();
            StayOnTop = dto.StayOnTop ?? false;
        }
    }
}
