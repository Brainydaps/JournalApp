using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace JournalApp.Models
{
    public class JournalEntry : INotifyPropertyChanged
    {
        public DateTime Date { get; set; }

        private string text;
        public string Text
        {
            get => text;
            set
            {
                if (text != value)
                {
                    text = value;
                    OnPropertyChanged(nameof(Text));
                }
            }
        }

        private ObservableCollection<string> pictures;
        public ObservableCollection<string> Pictures
        {
            get => pictures;
            set
            {
                if (pictures != value)
                {
                    pictures = value;
                    OnPropertyChanged(nameof(Pictures));
                }
            }
        }

        public ICommand ImageTappedCommand { get; private set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public JournalEntry()
        {
            Date = DateTime.Now;
            Text = string.Empty;
            Pictures = new ObservableCollection<string>();
            ImageTappedCommand = new Command<string>(OnImageTapped);
        }

        private async void OnImageTapped(string imagePath)
        {
            if (Application.Current?.MainPage is NavigationPage navigationPage)
            {
                await navigationPage.Navigation.PushAsync(new FullscreenImagePage(imagePath));
            }
        }
    }
}
