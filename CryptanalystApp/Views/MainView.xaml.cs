using CryptanalystApp.Core.Frameworck.Models;
using System.Windows.Controls;
using System.Windows.Data;
using UIControl.CustomControls;

namespace CryptanalystApp.Views
{
    public partial class MainView : UserControl
    {
        ListBox _assetsList;
        SwitchControl _findSwitch;

        public MainView()
        {
            InitializeComponent();

            _assetsList = FindName("AssetsList") as ListBox;
            _findSwitch = FindName("FindSwitch") as SwitchControl;
        }

        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e) => Filter();

        private void Filter()
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(_assetsList.ItemsSource);
            view.Filter = null;
            view.Filter = ExceptionFilter;
        }

        private bool ExceptionFilter(object obj)
        {
            bool resault = true;
            var exception = obj as Asset;
            var filterText = FindName("FilterTextBox") as TextBox;
            string filterType = "";

            switch (_findSwitch.Index)
            {
                case 0:
                    filterType = exception.Name;
                    break;
                case 1:
                    filterType = exception.Symbol;
                    break;
            }

            if (!string.IsNullOrWhiteSpace(filterText.Text) && !filterType.Contains(filterText.Text))
            {
                resault = false;
            }

            return resault;
        }

        private void FindSwitch_IndexChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<int> e) => Filter();
    }
}
