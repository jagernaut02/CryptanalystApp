using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace UIControl.CustomControls
{
    public class RankControl : TextBox
    {
        public static readonly DependencyProperty RankLevelProperty = DependencyProperty.Register(nameof(RankLevel), typeof(int), typeof(RankControl));

        public int RankLevel
        {
            get => (int)GetValue(RankLevelProperty); 
            set => SetValue(RankLevelProperty, value); 
        }

        static RankControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RankControl), new FrameworkPropertyMetadata(typeof(RankControl)));
        }
    }
}
