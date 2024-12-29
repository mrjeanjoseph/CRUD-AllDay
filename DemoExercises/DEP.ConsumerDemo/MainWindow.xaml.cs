using DemoLibrary48;
using System.Threading.Tasks;
using System.Windows;

namespace DEP.ConsumerDemo {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();

            ApiHelper.InitializeClient();
        }

        private async Task LoadImage(int imageNumber = 0) {
            
        }
    }
}
