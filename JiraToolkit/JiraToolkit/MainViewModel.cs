using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using JiraToolkit.Dtos;
using JiraToolkit.Properties;
using JiraToolkit.ViewModels;
using log4net;
using Newtonsoft.Json;
using Environment = System.Environment;

namespace JiraToolkit
{
    internal sealed class MainViewModel
    {
        static readonly ILog logger = LogManager.GetLogger(typeof(Application));

        readonly string configurationFolderPath;

        readonly string configurationFilePath;

        public MainViewModel()
        {
            configurationFolderPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                Settings.Default.ConfigurationFolderName);

            configurationFilePath = Path.Combine(configurationFolderPath, Settings.Default.ConfigurationFile);
        }

        public EnvironmentViewModel[] Environments { get; private set; }

        public bool HasEnvironments => Environments?.Any() ?? false;

        public QueryViewModel[] Queries { get; private set; }

        public bool HasQueries => Queries?.Any() ?? false;

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
                    logger.Fatal("Could not read from configuration file.", e);
                    MessageBox.Show(
                        "Could not read from configuration file. Ensure your configuration file exists and is available for reading.",
                        "Could not read configuration file");
                    Environment.Exit(-1);
                }
            }
            else
            {
                MessageBox.Show("The configuration file does not exist. Please check your configuration folder", "Error");
                Process.Start(configurationFolderPath);
                Environment.Exit(-1);
            }

            return json;
        }

        void UpdateConfiguration(string json)
        {
            try
            {
                var options = new OptionsViewModel(); // TODO: Load from some files.

                var dto = JsonConvert.DeserializeObject<Configuration>(json);
                if (dto == null)
                {
                    MessageBox.Show("Your configuration file is invalid, please check your configuration file for errors.", "Error");
                    Environment.Exit(-1);
                }

                Environments = dto.Environments?.Where(x => x != null).Select((x) => new EnvironmentViewModel(x, options)).ToArray() ?? Array.Empty<EnvironmentViewModel>();
                Queries = dto.Queries?.Where(x => x != null).Select(x => new QueryViewModel(options) { Name = x.Name, Url = x.Url }).ToArray() ?? Array.Empty<QueryViewModel>();
                StayOnTop = options.StayOnTop;
            }
            catch (Exception e)
            {
                MessageBox.Show("Your configuration file is invalid, please check your configuration file for errors.", "Corrupt Configuration");
                logger.Fatal("Invalid configuration file.", e);
                MessageBox.Show("Your configuration file is invalid, please check your configuration file for errors.", "Error");
                Environment.Exit(-1);
            }
        }

        async Task CreateConfiguration()
        {
            if (!Directory.Exists(configurationFolderPath))
            {
                var result = MessageBox.Show("Should a default config file be created?", "Question", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    CreateConfigurationFolder();
                    await CreateConfigurationFile();
                    MessageBox.Show(
                        "A default configuration file has been created. Adjust the configuration file to your needs and restart your application.", "Information");
                    Process.Start(configurationFilePath);
                }

                Environment.Exit(-1);
            }
        }

        void CreateConfigurationFolder()
        {
            try
            {
                var directoryInfo = Directory.CreateDirectory(configurationFolderPath);
                directoryInfo.SetAccessControl(new DirectorySecurity(configurationFolderPath, AccessControlSections.Owner));
            }
            catch (Exception e)
            {
                logger.Fatal("Could not create configuration folder.", e);
                MessageBox.Show("It was not possible to create a configuration folder. Please take a look at the logs.", "Error");
                Environment.Exit(-1);
            }
        }

        async Task CreateConfigurationFile()
        {
            try
            {
                using (var file = new StreamWriter(configurationFilePath, false, Encoding.UTF8))
                {
                    await file.WriteAsync(Settings.Default.Configuration);
                    file.Close();
                }
            }
            catch (Exception e)
            {
                logger.Fatal("Could not create configuration file.", e);
                MessageBox.Show("It was not possible to create a configuration file. Please take a look at the logs.", "Error");
                Environment.Exit(-1);
            }
        }
    }
}
