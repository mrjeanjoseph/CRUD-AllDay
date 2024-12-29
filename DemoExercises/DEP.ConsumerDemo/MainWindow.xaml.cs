using DemoLibrary48;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace DEP.ConsumerDemo {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        private int maxNumber = 0;
        private int currentNumber = 0;

        public MainWindow() {
            InitializeComponent();

            ApiHelper.InitializeClient();

            NextImgButton.IsEnabled = false;
        }

        private async Task LoadImage(int imageNumber = 0) {

            var comic = await ComicProcessor.LoadComic(imageNumber);
            if (imageNumber == 0) maxNumber = comic.Num;
            currentNumber = comic.Num;

            var uriSource = new Uri(comic.Img, UriKind.Absolute);
            ComicImage.Source = new BitmapImage(uriSource);
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e) {
            await LoadImage();
        }

        private async void PrvImgButton_Click(object sender, RoutedEventArgs e) {

            if (currentNumber > 1) {
                currentNumber -= 1;
                NextImgButton.IsEnabled = true;

                await LoadImage(currentNumber);

                if (currentNumber == 1) PrvImgButton.IsEnabled = false;
            }
        }

        private async void NextImgButton_Click(object sender, RoutedEventArgs e) {

            if(currentNumber < maxNumber) {
                currentNumber += 1;
                PrvImgButton.IsEnabled = true;

                await LoadImage(currentNumber);

                if (currentNumber == maxNumber) NextImgButton.IsEnabled = false;
            }
        }

        private void SunDataBtn_Click(object sender, RoutedEventArgs e) {
            SunInfo sunData = new SunInfo();

            sunData.Show();
        }
    }
}
