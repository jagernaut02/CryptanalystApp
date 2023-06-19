using Caliburn.Micro;
using CryptanalystApp.Core.Frameworck.Services;
using CryptanalystApp.Core.RelayCommand.Command;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using UIControl;

namespace CryptanalystApp.ViewModels
{
    public class MainViewModel : Screen
    {
        private IShell _shell;
        private IAsset _selectedAssetItem;
        private List<IAsset> _allAssets;
        private List<IAsset> _popularityAssets;
        private int _findSwitchIndex;

        public List<IAsset> AllAssets
        {
            get { return _allAssets; }
            set
            {
                _allAssets = value;
                NotifyOfPropertyChange(() => AllAssets);
            }
        }

        public List<IAsset> PopularityAssets
        {
            get { return _popularityAssets; }
            set 
            {
                _popularityAssets = value;
                NotifyOfPropertyChange(() => PopularityAssets);
            }
        }

        public IAsset SelectedAssetItem
        {
            get { return _selectedAssetItem; }
            set 
            { 
                _selectedAssetItem = value;
                NotifyOfPropertyChange(() => SelectedAssetItem);
            }
        }

        public int FindSwitchIndex
        {
            get { return _findSwitchIndex; }
            set
            {
                _findSwitchIndex = value;
                NotifyOfPropertyChange<int>(() => FindSwitchIndex);
            }
        }



        public List<string> FindSwitchState { get { return new List<string>() { "name", "symbol" }; } }
        public ICommand GoToDescriptionViewCommand { get; private set; }

        public MainViewModel()
        {
            DisplayName = "Home";
            GoToDescriptionViewCommand = new RelayCommand(GoToDescriptionView);
        }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

            Initialization();
        }

        private void Initialization()
        {
            _shell = IoC.Get<IShell>();
            AllAssets = _shell.AllAssets.ToList();
            FillPopularityAssetsList(10);
        }

        private void FillPopularityAssetsList(int coutn) => PopularityAssets = AllAssets.TakeWhile(a => a.Rank <= coutn).OrderBy(a => a.Rank).ToList();
        
        private void GoToDescriptionView()
        {
            if (SelectedAssetItem != null)
            {
                _shell.OpenDescriptionView(SelectedAssetItem.Id);
            }
        }
    }

    
}

