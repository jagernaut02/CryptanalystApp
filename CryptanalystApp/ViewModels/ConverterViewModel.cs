using Caliburn.Micro;
using CryptanalystApp.Core.Frameworck.Models;
using CryptanalystApp.Core.Frameworck.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UIControl;

namespace CryptanalystApp.ViewModels
{
    public class ConverterViewModel : Screen
    {
        private IShell _shell;
        private List<IAsset> _allAssets;
        private List<IAsset> _baseCurrencylist;
        private IAsset _fromCoinConverterSelectedItem;
        private IAsset _toCoinConverterSelectedItem;
        private IAsset _fromCurrencyConverterSelectedItem;
        private IAsset _toCurrencyConverterSelectedItem;

        public List<IAsset> AllAssets
        {
            get => _allAssets;
            set
            {
                _allAssets = value;
                NotifyOfPropertyChange(() => _allAssets);
            }
        }

        public List<IAsset> BaseCurrencyList
        {
            get => _baseCurrencylist;
            set
            {
                _baseCurrencylist = value;
                NotifyOfPropertyChange(() => _baseCurrencylist);
            }
        }

        public IAsset FromCoinConverterSelectedItem
        {
            get { return _fromCoinConverterSelectedItem; }
            set 
            {
                _fromCoinConverterSelectedItem = value; 
                NotifyOfPropertyChange(() => FromCoinConverterSelectedItem);
            }
        }

        public IAsset ToCoinConverterSelectedItem
        {
            get { return _toCoinConverterSelectedItem; }
            set 
            {
                _toCoinConverterSelectedItem = value;
                NotifyOfPropertyChange(() => ToCoinConverterSelectedItem); 
            }
        }

        public IAsset FromCurrencyConverterSelectedItem
        {
            get { return _fromCurrencyConverterSelectedItem; }
            set
            {
                _fromCurrencyConverterSelectedItem = value;
                NotifyOfPropertyChange(() => FromCurrencyConverterSelectedItem);
            }
        }

        public IAsset ToCurrencyConverterSelectedItem
        {
            get { return _toCurrencyConverterSelectedItem; }
            set
            {
                _toCurrencyConverterSelectedItem = value;
                NotifyOfPropertyChange(() => ToCurrencyConverterSelectedItem);
            }
        }

        public ConverterViewModel()
        {
            DisplayName = "Converter";
            Initialize();
        }

        private void Initialize()
        {
            _shell = IoC.Get<IShell>();
            AllAssets = _shell.AllAssets.ToList();
            BaseCurrencyList = GetBaseCurrency();

            FromCoinConverterSelectedItem = AllAssets.First();
            ToCoinConverterSelectedItem = AllAssets.First();

            FromCurrencyConverterSelectedItem = AllAssets.First();
            ToCurrencyConverterSelectedItem = BaseCurrencyList.First();
        }

        private List<IAsset> GetBaseCurrency()
        {
            var list = new List<IAsset>();
            list.Add(new Asset("hryvnia", "UAH", "Hryvnia", (decimal)0.027));
            list.Add(new Asset("dollar", "USD", "Dollar", 1));
            list.Add(new Asset("euro", "EUR", "Euro", (decimal)1.09));
            list.Add(new Asset("ruble", "RUB", "Ruble", (decimal)0.012));
            list.Add(new Asset("zloty", "PLN", "Zloty", (decimal)0.048));

            return list;
        }
    }
}
