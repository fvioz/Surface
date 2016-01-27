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


            // Only allow scaling when both dimensions are smaller than the drawingarea
            if (image.ActualHeight * (transform.ScaleY * e.Delta.Scale) < DrawingArea.ActualHeight &&
                image.ActualWidth * (transform.ScaleX * e.Delta.Scale) < DrawingArea.ActualWidth)
            {
                transform.ScaleX *= e.Delta.Scale;
                transform.ScaleY *= e.Delta.Scale;
            }

            // LEFT-RIGHT bounds
            if (e.Delta.Translation.X < 0) // Going left
            {
                if (DrawingArea.ActualWidth / 2 + (transform.TranslateX + e.Delta.Translation.X) - image.ActualWidth / 2 > 0)
                {
                    // Staying inside, apply translation
                    transform.TranslateX += e.Delta.Translation.X;
                }
                else
                {
                    // Trying to go outside, because scale sucks to work with, move image back inside
                    transform.TranslateX = image.ActualWidth / 2 - DrawingArea.ActualWidth / 2;
                }
            }
            else // Going right
            {
                if (DrawingArea.ActualWidth / 2 - (transform.TranslateX + e.Delta.Translation.X) +
                    image.ActualWidth * (0.5 - transform.ScaleX) > 0)
                {
                    // Staying inside, apply translation
                    transform.TranslateX += e.Delta.Translation.X;
                }
                else
                {
                    // Trying to go outside, move image back inside
                    transform.TranslateX = image.ActualWidth * (0.5 - transform.ScaleX) + DrawingArea.ActualWidth / 2;
                }
            }

            // UP-DOWN bounds
            if (e.Delta.Translation.Y < 0) // Going up
            {
                if (DrawingArea.ActualHeight / 2 + (transform.TranslateY + e.Delta.Translation.Y) - image.ActualHeight / 2 > 0)
                {
                    // Staying inside, apply translation
                    transform.TranslateY += e.Delta.Translation.Y;
                }
                else
                {
                    // Trying to go outside, move image back inside
                    transform.TranslateY = image.ActualHeight / 2 - DrawingArea.ActualHeight / 2;
                }
            }
            else // Going down
            {
                if (DrawingArea.ActualHeight / 2 - (transform.TranslateY + e.Delta.Translation.Y) +
                    image.ActualHeight * (0.5 - transform.ScaleY) > 0)
                {
                    // Staying inside, apply translation
                    transform.TranslateY += e.Delta.Translation.Y;
                }
                else
                {
                    // Trying to go outside, move image back inside
                    transform.TranslateY = image.ActualHeight * (0.5 - transform.ScaleY) + DrawingArea.ActualHeight / 2;
                }
            }
        }

        private void ImageOnManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            ((Image)sender).Opacity = 1;
        }

    }
}
