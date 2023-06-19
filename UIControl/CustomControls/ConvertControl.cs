using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UIControl.CustomControls
{
    public class ConvertControl : Control
    {
        TextBox _partText;

        public static readonly DependencyProperty FromItemsSourceProperty = DependencyProperty.Register(nameof(FromItemsSource), typeof(IEnumerable<IAsset>), typeof(ConvertControl));
        public static readonly DependencyProperty DecimalPlacesProperty = DependencyProperty.Register(nameof(DecimalPlaces), typeof(int), typeof(ConvertControl), new PropertyMetadata(2));
        public static readonly DependencyProperty SelectedFromItemProperty = DependencyProperty.Register(nameof(SelectedFromItem), typeof(IAsset), typeof(ConvertControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnSelectedItemPropertyChanged)));
        public static readonly DependencyProperty InputValueProperty = DependencyProperty.Register(nameof(InputValue), typeof(decimal), typeof(ConvertControl), new FrameworkPropertyMetadata((decimal)0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public static readonly DependencyProperty ToItemsSourceProperty = DependencyProperty.Register(nameof(ToItemsSource), typeof(List<IAsset>), typeof(ConvertControl));
        public static readonly DependencyProperty SelectedToItemProperty = DependencyProperty.Register(nameof(SelectedToItem), typeof(IAsset), typeof(ConvertControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnSelectedItemPropertyChanged)));
        private static readonly DependencyProperty ResultValueProperty = DependencyProperty.Register(nameof(ResultValue), typeof(string), typeof(ConvertControl));

        public IEnumerable<IAsset> FromItemsSource
        {
            get { return (IEnumerable<IAsset>)GetValue(FromItemsSourceProperty); }
            set { SetValue(FromItemsSourceProperty, value); }
        }

        public int DecimalPlaces
        {
            get => (int)GetValue(DecimalPlacesProperty);
            set => SetValue(DecimalPlacesProperty, value);
        }

        public IAsset SelectedFromItem
        {
            get => (IAsset)GetValue(SelectedFromItemProperty);
            set => SetValue(SelectedFromItemProperty, value);
        }

        public decimal InputValue
        {
            get => (decimal)GetValue(InputValueProperty);
            set => SetValue(InputValueProperty, value);
        }

        public List<IAsset> ToItemsSource
        {
            get => (List<IAsset>)GetValue(ToItemsSourceProperty);
            set => SetValue(ToItemsSourceProperty, value);
        }

        public IAsset SelectedToItem
        {
            get => (IAsset)GetValue(SelectedToItemProperty);
            set => SetValue(SelectedToItemProperty, value);
        }

        public string ResultValue
        {
            get => (string)GetValue(ResultValueProperty);
            set => SetValue(ResultValueProperty, value);
        }

        static ConvertControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ConvertControl), new FrameworkPropertyMetadata(typeof(ConvertControl)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (_partText != null)
            {
                _partText.PreviewTextInput -= InputValidation;
                _partText.TextChanged -= OnInputValueChange;
            }

            _partText = Template.FindName("PART_TEXT", this) as TextBox;
            if (_partText == null)
                return;

            _partText.PreviewTextInput += InputValidation;
            _partText.TextChanged += OnInputValueChange;
        }

        private void InputValidation(object sender, TextCompositionEventArgs e) => e.Handled = !e.Text.Any(x => Char.IsDigit(x) || '.'.Equals(x));
        private void OnInputValueChange(object sender, TextChangedEventArgs e)
        {
            CoerceValue(this, (sender as TextBox).Text);
        }

        private static void OnSelectedItemPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ConvertControl control = obj as ConvertControl;
            if (control == null)
                return;

            control.UpdateReultValueValue();
        }

        protected void UpdateReultValueValue()
        {
            string str = "";
            SetValue(ResultValueProperty, str);

            if(SelectedToItem == null || SelectedFromItem == null) return;
            
            var convertedAmount = Math.Round((InputValue / SelectedToItem.PriceUsd) * SelectedFromItem.PriceUsd, DecimalPlaces);

            SetValue(ResultValueProperty, convertedAmount.ToString());
        }

        private void CoerceValue(DependencyObject obj, object value)
        {
            decimal newValue;
            ConvertControl control = obj as ConvertControl;
            if (control == null)
                return;

            try
            {
                newValue = Convert.ToDecimal(value);
            }
            catch 
            {
                newValue = 0; 
            }

            control.InputValue = newValue;
            control.UpdateReultValueValue();
        }
    }
}
