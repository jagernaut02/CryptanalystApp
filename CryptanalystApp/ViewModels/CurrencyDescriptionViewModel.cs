using Caliburn.Micro;
using CryptanalystApp.Core.Frameworck.Models;
using CryptanalystApp.Core.Frameworck.StaticElements;
using CryptanalystApp.Core.RelayCommand.Command;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Windows;
using System.Windows.Input;

namespace CryptanalystApp.ViewModels
{
    public class CurrencyDescriptionViewModel : Screen
    {
        private string _currentCoinId;
        private string _title = "Detailed description of ";
        private Coin _currentCoin;

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                NotifyOfPropertyChange(() => Title);
            }
        }

        public Coin CurrentCoin
        {
            get { return _currentCoin; }
            set 
            {
                _currentCoin = value; 
                NotifyOfPropertyChange(() => CurrentCoin);
            }
        }

        public ICommand GoToMarketCommand { get; set; }

        public CurrencyDescriptionViewModel(string id)
        {
            DisplayName = "Details";
            _currentCoinId = id;
            Title += _currentCoinId + " currency";

            GoToMarketCommand = new RelayCommand(GoToMarket);

            GetCurrentCoin();
        }

        private async void GetCurrentCoin()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    HttpResponseMessage response = await httpClient.GetAsync(StaticElements.ApiUrl + StaticElements.AssetsEndpoint + "/" + _currentCoinId);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        CurrentCoin = JsonConvert.DeserializeObject<Response<Coin>>(responseBody).Data;
                    }
                    else
                    {
                        MessageBox.Show($"Ошибка при выполнении запроса: {response.StatusCode}", "Error", MessageBoxButton.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Error", MessageBoxButton.OK);
            }
        } 

        private void GoToMarket() => Process.Start(StaticElements.MarketLink);
        
    }
}
