using System;
using System.Windows;
using WeatherApp.Commands;
using WeatherApp.Properties;

namespace WeatherApp.ViewModels
{
    public class ConfigurationViewModel : BaseViewModel
    {
        private string apiKey;        

        public string ApiKey { 
            get => apiKey;
            set
            {
                apiKey = value;
                OnPropertyChanged();
            }
        }

        public DelegateCommand<string> SaveConfigurationCommand { get; set; }


        public ConfigurationViewModel()
        {
            Name = GetType().Name;

            ApiKey = GetApiKey();

            SaveConfigurationCommand = new DelegateCommand<string>(SaveConfiguration);
        }

        private void SaveConfiguration(string obj)
        {
            /// TODO 04 : Les tâches manquantes sont dans les XAML. FAIT
            /// TODO 04a : Sauvegarder la configuration Fait
            Settings.Default.apiKey = ApiKey;
            Settings.Default.Save();
            MessageBox.Show("Sauvegarder, retournez à la page de méteo");
        }

        private string GetApiKey()
        {
            /// TODO 05 : Retourner la configuration Fait
            return Settings.Default.apiKey;
        }

    }
}
