using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.Foundation;

namespace Surface
{
    public sealed partial class MainPage : Page
    {
        Point startTranslation;
        double startRotation;
        double startScale;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void ImageOnManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            var image = (Image)sender;
            image.Opacity = 0.4;

            var transform = (CompositeTransform)image.RenderTransform;
            startTranslation = new Point(transform.TranslateX, transform.TranslateY);
            startRotation = transform.Rotation;
            startScale = transform.ScaleX;
        }

        private void ImageOnManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            var image = (Image)sender;
            var transform = (CompositeTransform)image.RenderTransform;

            transform.ScaleX = startScale * e.Cumulative.Scale;
            transform.ScaleY = startScale * e.Cumulative.Scale;
            transform.TranslateX = startTranslation.X + e.Cumulative.Translation.X;
            transform.TranslateY = startTranslation.Y + e.Cumulative.Translation.Y;
            transform.Rotation = startRotation + e.Cumulative.Rotation;
        }

        private void ImageOnManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            ((Image)sender).Opacity = 1;
        }

    }
}
