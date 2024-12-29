using DemoLibrary48;
using System.Windows;

namespace DEP.ConsumerDemo {
    /// <summary>
    /// Interaction logic for SunInfo.xaml
    /// </summary>
    public partial class SunInfo : Window {
        public SunInfo() {
            InitializeComponent();
        }

        private async void LoadSunDataBtn_Click(object sender, RoutedEventArgs e) {
            var sunData = await SunProcessor.LoadSunData();

            sunriseText.Text = $"Sunrise is at {sunData.Sunrise.ToLocalTime().ToShortTimeString()}";

            sunsetText.Text = $"Sunrise is at {sunData.Sunset.ToLocalTime().ToShortTimeString()}";
        }
    }
}
