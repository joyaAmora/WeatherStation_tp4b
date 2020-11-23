using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WeatherApp.Commands;
using WeatherApp.Properties;
using WeatherApp.Services;

namespace WeatherApp.ViewModels
{
    public class ApplicationViewModel : BaseViewModel
    {
        #region Membres
        private BaseViewModel currentViewModel;
        private List<BaseViewModel> viewModels;
        private OpenWeatherService ows;

        #endregion

        #region Propriétés
        /// <summary>
        /// Model actuellement affiché
        /// </summary>
        public BaseViewModel CurrentViewModel
        {
            get { return currentViewModel; }
            set { 
                currentViewModel = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Commande pour changer la page à afficher
        /// </summary>
        public DelegateCommand<string> ChangePageCommand { get; set; }

        public List<BaseViewModel> ViewModels
        {
            get {
                if (viewModels == null)
                    viewModels = new List<BaseViewModel>();
                return viewModels; 
            }
        }
        #endregion

        public ApplicationViewModel()
        {
            ChangePageCommand = new DelegateCommand<string>(ChangePage);
           
            /// TODO 11 : Commenter cette ligne lorsque la configuration utilisateur fonctionne
            //var apiKey = AppConfiguration.GetValue("OWApiKey");
            //ows = new OpenWeatherService(apiKey);

            initViewModels();
        }

        #region Méthodes
        void initViewModels()
        {

            /// TemperatureViewModel setup
            var tvm = new TemperatureViewModel();
            ConfigurationViewModel configViewModel = new ConfigurationViewModel();
            /// TODO 09 : Indiquer qu'il n'y a aucune clé si le Settings apiKey est vide. Fait
            /// S'il y a une valeur, instancié OpenWeatherService avec la clé Fait
            if (String.IsNullOrEmpty(Settings.Default.apiKey))
            {
                MessageBox.Show("La clé API est obligatoire pour obtenir la météo");
            }
            else
            {
                ows = new OpenWeatherService(Settings.Default.apiKey);
            }
                
            tvm.SetTemperatureService(ows);
            ViewModels.Add(tvm);
            
            ViewModels.Add(configViewModel);
            CurrentViewModel = ViewModels[0];
            /// TODO 01 : ConfigurationViewModel Add Configuration ViewModel Fait


            CurrentViewModel = ViewModels[0];
        }

        private void ChangePage(string pageName)
        {
            /// TODO 10 : Si on a changé la clé, il faudra la mettre dans le service. Fait
            /// 
            /// Algo
            /// Si la vue actuelle est ConfigurationViewModel
            ///   Mettre la nouvelle clé dans le OpenWeatherService
            ///   Rechercher le TemperatureViewModel dans la liste des ViewModels
            ///   Si le service de temperature est null
            ///     Assigner le service de température
            /// 
            
            if (pageName == "ConfigurationViewModel")
            {
                if (ows == null)
                {
                    ows = new OpenWeatherService(Settings.Default.apiKey);
                }
                ows.SetApiKey(Settings.Default.apiKey);

                TemperatureViewModel temperatureViewModel = (TemperatureViewModel)ViewModels.FirstOrDefault(x => x.Name == "TemperatureViewModel");
                if (temperatureViewModel.TemperatureService == null)
                    temperatureViewModel.SetTemperatureService(ows);
            }

            /// Permet de retrouver le ViewModel avec le nom indiqé
            CurrentViewModel = ViewModels.FirstOrDefault(x => x.Name == pageName);  
        }

        #endregion
    }
}
