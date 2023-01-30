using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientConvertisseurV2.Models;
using ClientConvertisseurV2.Services;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;

namespace ClientConvertisseurV2.ViewModels
{
    public class ConvertisseurEuroViewModel : ObservableObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private double montant;
        private ObservableCollection<Devise> devises;
        private Devise deviseSelected;
        private double montantResult;
        //public ConvertisseurEuroPage()
        //{
        //    this.InitializeComponent();
        //    this.DataContext = this;
        //    GetDataOnLoadAsync();
        //}



        public double Montant
        {
            get
            {
                return montant;
            }

            set
            {
                montant = value;
                OnPropertyChanged("Montant");
            }
        }



        public double MontantResult
        {
            get
            {
                return montantResult;
            }

            set
            {
                montantResult = value;
                OnPropertyChanged("MontantResult");
            }
        }

        public Devise DeviseSelected
        {
            get
            {
                return deviseSelected;
            }

            set
            {
                deviseSelected = value;
                OnPropertyChanged("DeviseSelected");
            }
        }

        public ObservableCollection<Devise> Devises
        {
            get
            {
                return devises;
            }

            set
            {
                devises = value;
                OnPropertyChanged("Devises");
            }
        }

        public async void GetDataOnLoadAsync()
        {
            WSService service = new WSService("https://localhost:7032/api/");
            List<Devise> result = await service.GetDevisesAsync("devises");
            if (result == null)
            {
                DisplayNoAPI();
            }

            else
                Devises = new ObservableCollection<Devise>(result);


        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public IRelayCommand BtnSetConversion { get; }
        public ConvertisseurEuroViewModel()
        {
                //Boutons
                BtnSetConversion = new RelayCommand(ActionSetConversion);
            GetDataOnLoadAsync();

        }
        private void ActionSetConversion()
        {
            WSService service = new WSService("https://localhost:7032/api/");

            if (deviseSelected == null)
            { 
                DisplayNoDevise();
            }
            else
            {
                MontantResult = service.CalculMontant(DeviseSelected, Montant);
            }
        }

        public async void DisplayNoDevise()
        {
            ContentDialog noDevise = new ContentDialog
            {
                Title = "Erreur",
                Content = "Vous n'avez pas selectionné de devise.",
                CloseButtonText = "Ok"
            };

            noDevise.XamlRoot = App.MainRoot.XamlRoot;
            await noDevise.ShowAsync();
        }
        public async void DisplayNoAPI()
        {
            ContentDialog noAPI = new ContentDialog
            {
                Title = "Erreur",
                Content = "API non disponible !",
                CloseButtonText = "Ok"
            };

            noAPI.XamlRoot = App.MainRoot.XamlRoot;
            await noAPI.ShowAsync();
        }
    }
}
