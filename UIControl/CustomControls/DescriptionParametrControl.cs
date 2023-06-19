using System.Windows;
using System.Windows.Controls;

namespace UIControl.CustomControls
{
    public class DescriptionParametrControl : TextBox
    {
        public static readonly DependencyProperty ParametrNamePropperty = DependencyProperty.Register(nameof(ParametrName), typeof(string), typeof(DescriptionParametrControl));

        public string ParametrName
        {
            get => (string)GetValue(ParametrNamePropperty);
            set => SetValue(ParametrNamePropperty, value);
        }

        static DescriptionParametrControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DescriptionParametrControl), new FrameworkPropertyMetadata(typeof(DescriptionParametrControl)));
        }
    }
}
