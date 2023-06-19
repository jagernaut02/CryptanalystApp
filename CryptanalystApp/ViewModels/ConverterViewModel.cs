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
        private List<IAsset> _allAssets = new List<IAsset>() { new Asset() { Symbol = "x", Id = "x" } };
        private IAsset _fromSelectedItem;
        private IAsset _toSelectedItem;

        public List<IAsset> AllAssets
        {
            get => _allAssets;
            set
            {
                _allAssets = value;
                NotifyOfPropertyChange(() => _allAssets);
            }
        }

        public IAsset FromSelectedItem
        {
            get { return _fromSelectedItem; }
            set 
            { 
                _fromSelectedItem = value; 
                NotifyOfPropertyChange(() => FromSelectedItem);
            }
        }

        public IAsset ToSelectedItem
        {
            get { return _toSelectedItem; }
            set 
            {
                _toSelectedItem = value;
                NotifyOfPropertyChange(() => ToSelectedItem); 
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
            FromSelectedItem = AllAssets.First();
            ToSelectedItem = AllAssets.First();
        }
    }
}
