using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using JiraToolkit.Dtos;
using JiraToolkit.Properties;
using JiraToolkit.ViewModels;
using Newtonsoft.Json;
using Shuriken;
using Environment = System.Environment;

namespace JiraToolkit
{
    internal sealed class MainViewModel : ObservableObject
    {
        readonly string configurationFolderPath;

        readonly string configurationFilePath;

        public MainViewModel()
        {
            configurationFolderPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                Settings.Default.ConfigurationFolderName);
            configurationFilePath = Path.Combine(configurationFolderPath, Settings.Default.ConfigurationFile);
        }

        [Observable]
        public EnvironmentViewModel[] Environments { get; private set; }

        [Observable]
        public QueryViewModel[] Queries { get; private set; }

        public bool HasQueries => Queries?.Any() ?? false;

        [Observable]
        public bool StayOnTop { get; private set; }

        public async Task Initialize()
        {
            await CreateConfiguration();
            var json = ReadConfiguration();
            UpdateConfiguration(json);
        }

        public void UpdateConfiguration() => UpdateConfiguration(ReadConfiguration());

        string ReadConfiguration()
        {
            var json = string.Empty;
            if (File.Exists(configurationFilePath))
            {
                try
                {
                    json = File.ReadAllText(configurationFilePath);
                }
                catch (Exception e)
                {
                    MessageBox.Show(
                        "Could not read from configuration file. Ensure your configuration file exists and is available for reading.",
                        "Could not read configuration file");
                    Environment.Exit(-1);
                }
            }
            else
            {
                MessageBox.Show("The configuration file does not exist. Please check your configuration folder", "No configuration available");
                Process.Start(configurationFolderPath);
                Environment.Exit(-1);
            }

            return json;
        }

        void UpdateConfiguration(string json)
        {
            try
            {
                var dto = JsonConvert.DeserializeObject<Configuration>(json);
                if (dto == null)
                {
                    MessageBox.Show("Your configuration file is invalid, please check your configuration file for errors.", "Corrupt Configuration");
                    Environment.Exit(-1);
                }
                Environments = dto.Environments.Where(x => x != null).Select((x) => new EnvironmentViewModel(x)).ToArray();
                Queries = dto.Queries.Where(x => x != null).Select(x => new QueryViewModel() { Name = x.Name, Url = x.Url }).ToArray();
                StayOnTop = dto.StayOnTop ?? false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Your configuration file is invalid, please check your configuration file for errors.", "Corrupt Configuration");
                Environment.Exit(-1);
            }
        }

        async Task CreateConfiguration()
        {
            if (!Directory.Exists(configurationFolderPath))
            {
                var result = MessageBox.Show("Should a default config file be created?", "Could not read config file", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    CreateConfigurationFolder();
                   await CreateConfigurationFile();
                    MessageBox.Show(
                        "A default configuration file has been created. Adjust the configuration file to your needs and restart your application.");
                    Process.Start(configurationFilePath);
                }

                Environment.Exit(-1);
            }
        }

        void CreateConfigurationFolder()
        {
            var directoryInfo = Directory.CreateDirectory(configurationFolderPath);
            directoryInfo.SetAccessControl(new DirectorySecurity(configurationFolderPath, AccessControlSections.Owner));
        }

        async Task CreateConfigurationFile()
        {
            var file = File.Create(configurationFilePath);
            byte[] bytes = Encoding.UTF8.GetBytes(Settings.Default.Configuration);
            await file.WriteAsync(bytes,0,bytes.Length,CancellationToken.None);
            file.Close();
        }
    }
}
