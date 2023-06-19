using Caliburn.Micro;
using CryptanalystApp.Core.Frameworck.Models;
using CryptanalystApp.Core.Frameworck.Services;
using CryptanalystApp.Core.Frameworck.StaticElements;
using CryptanalystApp.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net.Http;
using System.Windows;
using UIControl;

namespace CryptanalystApp
{
    [Export(typeof(IShell))]
    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive, IShell
    {
        private IEnumerable<IAsset> _allAssets;
        private object _activeOptions;
        public object ActiveOptions
        {
            get => _activeOptions;
            set
            {
                _activeOptions = value;
                NotifyOfPropertyChange<object>(() => ActiveOptions);
            }
        }

        public IEnumerable<IAsset> AllAssets
        {
            get => _allAssets;
            set
            {
                _allAssets = value;
                NotifyOfPropertyChange<object>(() => AllAssets);
            }
        }

        public ShellViewModel()
        {
            
        }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

            GetAssets();
        }

        private void Initialize()
        {
            ShowView(new MainViewModel());
            ShowView(new ConverterViewModel());
            ActiveOptions = Items[0];
        }

        private async void GetAssets()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    HttpResponseMessage response = await httpClient.GetAsync(StaticElements.ApiUrl + StaticElements.AssetsEndpoint);

                    if (response.IsSuccessStatusCode)
                    {
                        AllAssets = JsonConvert.DeserializeObject<ListResponse<Asset>>(await response.Content.ReadAsStringAsync()).Data;
                        Initialize();
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

        private void ShowView(IScreen view) => ActivateItem(view);

        public IObservableCollection<IScreen> GetDocuments()
        {
            lock (Items) return Items;
        }

        public void ChangeCurrentActiveItem(IScreen view) => ActiveOptions = view;

        public void ChangeCurrentActiveItem(IScreen view, int index)
        {
            ChangeCurrentActiveItem(view);
            (view as IOption).ActiveItemByElementIndex(index);
        }

        public void OpenDescriptionView(string id)
        {
            var view = GetDocuments().FirstOrDefault(x => x.DisplayName == "Details");
            if (view != null)
            {
                DeactivateItem(view, true);
            }

            ShowView(new CurrencyDescriptionViewModel(id));
            ChangeCurrentActiveItem(GetDocuments().FirstOrDefault(x => x.DisplayName == "Details"));
        }
        
    }
}
