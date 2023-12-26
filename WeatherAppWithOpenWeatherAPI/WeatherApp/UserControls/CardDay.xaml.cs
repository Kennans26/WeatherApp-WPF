using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;

namespace WeatherApp.UserControls
{
    /// <summary>
    /// Interaction logic for ForecastCard.xaml
    /// </summary>
    public partial class CardDay : UserControl
    {
        public CardDay()
        {
            InitializeComponent();
        }


        public string Day
        {
            get { return (string)GetValue(DayProperty); }
            set { SetValue(DayProperty, value); }
        }

        public static readonly DependencyProperty DayProperty = DependencyProperty.Register("Day", typeof(string), typeof(CardDay));


        public ImageSource Source
        {
            get { return (ImageSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(ImageSource), typeof(CardDay));


        public string Detail
        {
            get { return (string)GetValue(DetailProperty); }
            set { SetValue(DetailProperty, value); }
        }

        public static readonly DependencyProperty DetailProperty = DependencyProperty.Register("Detail", typeof(string), typeof(CardDay));

        public string MaxTemp
        {
            get { return (string)GetValue(MaxNumProperty); }
            set { SetValue(MaxNumProperty, value); }
        }

        public static readonly DependencyProperty MaxNumProperty = DependencyProperty.Register("MaxTemp", typeof(string), typeof(CardDay));


        public string MinTemp
        {
            get { return (string)GetValue(MinNumProperty); }
            set { SetValue(MinNumProperty, value); }
        }

        public static readonly DependencyProperty MinNumProperty = DependencyProperty.Register("MinTemp", typeof(string), typeof(CardDay));
    }
}
