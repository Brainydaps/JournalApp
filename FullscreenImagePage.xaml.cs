using Microsoft.Maui.Controls;

namespace JournalApp
{
    public partial class FullscreenImagePage : ContentPage
    {
        public FullscreenImagePage(string imagePath)
        {
            InitializeComponent();
            FullImage.Source = ImageSource.FromFile(imagePath);
            // Handle tap to close
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) => Navigation.PopAsync();
            FullImage.GestureRecognizers.Add(tapGestureRecognizer);
        }
    }
}
