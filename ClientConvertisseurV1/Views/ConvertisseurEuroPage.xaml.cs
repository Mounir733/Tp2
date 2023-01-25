// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using ClientConvertisseurV1.Models;
using ClientConvertisseurV1.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ClientConvertisseurV1.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ConvertisseurEuroPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private double montant;
        private ObservableCollection<Devise> devises;
        private Devise deviseSelected;
        private double montantResult;
        public ConvertisseurEuroPage()
        {
            this.InitializeComponent();
            this.DataContext = this;
            GetDataOnLoadAsync();
        }

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

        private async void GetDataOnLoadAsync()
        {
            WSService service = new WSService("https://localhost:7032/api/"); 
            List<Devise> result = await service.GetDevisesAsync("devises"); 
            if (result == null)
                MessageAsync("API non disponible !", "Erreur");
            else   
                Devises = new ObservableCollection<Devise>(result);

            
        }

        private void MessageAsync(string v1, string v2)
        {
            throw new NotImplementedException();
        }
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        private void BtnConvertir_Click(object sender, RoutedEventArgs e)
        {
            WSService service = new WSService("https://localhost:7032/api/");
            MontantResult = service.CalculMontant(DeviseSelected, Montant);
        }
    }
}
