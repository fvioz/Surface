using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace Surface
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void ImageOnManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            ((Image)sender).Opacity = 0.4;
        }

        private void ImageOnManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            var image = (Image)sender;
            var transform = (CompositeTransform)image.RenderTransform;

            var imageWidth = image.ActualWidth;
            var imageHeight = image.ActualHeight;
            var deltaScale = e.Delta.Scale;
            var deltaTranslationX = e.Delta.Translation.X;
            var deltaTranslationY = e.Delta.Translation.Y;


            if (imageHeight * (transform.ScaleY * deltaScale) < DrawingArea.ActualHeight && imageWidth * (transform.ScaleX * deltaScale) < DrawingArea.ActualWidth)
            {
                transform.ScaleX *= deltaScale;
                transform.ScaleY *= deltaScale;
            }

            if (deltaTranslationX < 0) // Izquierda
            {
                if (DrawingArea.ActualWidth / 2 + (transform.TranslateX + deltaTranslationX) - imageWidth / 2 > 0)
                {
                    transform.TranslateX += deltaTranslationX;
                }
                else
                {
                    transform.TranslateX = imageWidth / 2 - DrawingArea.ActualWidth / 2;
                }
            }
            else // Derecha
            {
                if (DrawingArea.ActualWidth / 2 - (transform.TranslateX + deltaTranslationX) +
                    imageWidth * (0.5 - transform.ScaleX) > 0)
                {
                    transform.TranslateX += deltaTranslationX;
                }
                else
                {
                    transform.TranslateX = imageWidth * (0.5 - transform.ScaleX) + DrawingArea.ActualWidth / 2;
                }
            }
            
        
            if (deltaTranslationY < 0) // Arriba
            {
                if (DrawingArea.ActualHeight / 2 + (transform.TranslateY + deltaTranslationY) - imageHeight / 2 > 0)
                {
                    transform.TranslateY += deltaTranslationY;
                }
                else
                {
                    transform.TranslateY = imageHeight / 2 - DrawingArea.ActualHeight / 2;
                }
            }
            else // Abajo
            {
                if (DrawingArea.ActualHeight / 2 - (transform.TranslateY + deltaTranslationY) +
                    imageHeight * (0.5 - transform.ScaleY) > 0)
                {
                    transform.TranslateY += deltaTranslationY;
                }
                else
                {
                    transform.TranslateY = imageHeight * (0.5 - transform.ScaleY) + DrawingArea.ActualHeight / 2;
                }
            }
        }

        private void ImageOnManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            ((Image)sender).Opacity = 1;
        }

    }
}
