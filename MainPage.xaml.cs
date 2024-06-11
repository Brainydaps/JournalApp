using JournalApp.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Microsoft.Maui.Controls;

namespace JournalApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
            // Check if the view model is being instantiated correctly
            System.Diagnostics.Debug.WriteLine("MainPageViewModel instantiated");
        }

        private async void AddEntryButton_Clicked(object sender, EventArgs e)
        {
            //Brainydaps editing
            var newEntry = new JournalEntry { Date = DateTime.Now };
            System.Diagnostics.Debug.WriteLine("AddEntryButtonclicked method was called and var new entry was made");
            await Navigation.PushAsync(new JournalEntryPage
            {
                BindingContext = new JournalEntry { Date = DateTime.Now }
            });
        }

        private async void EditEntryButton_Clicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is JournalEntry entry)
            {
                await Navigation.PushAsync(new JournalEntryPage
                {
                    BindingContext = entry
                });
            }
        }

        private void DeleteEntryButton_Clicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is JournalEntry entry)
            {
                App.JournalEntries.Remove(entry);
                (BindingContext as MainPageViewModel)?.RefreshEntries();
            }
        }
        protected override void OnAppearing()
        {
            //Brainydaps editing
            System.Diagnostics.Debug.WriteLine("OnAppearing method of MainPage was called");
            base.OnAppearing();
            (BindingContext as MainPageViewModel)?.RefreshEntries();
        }
    }

    public class MainPageViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<JournalEntry> journalEntries = new();
        public ObservableCollection<JournalEntry> JournalEntries
        {
            get => journalEntries;
            set
            {
                journalEntries = value;
                OnPropertyChanged(nameof(JournalEntries));
                // Check if the JournalEntries property is being updated correctly
                System.Diagnostics.Debug.WriteLine("JournalEntries updated");
            }
        }

        public MainPageViewModel()
        {
            RefreshEntries();
            System.Diagnostics.Debug.WriteLine("RefreshEntries method called in MainPageViewModel");
            JournalEntries = new ObservableCollection<JournalEntry>(App.JournalEntries.OrderByDescending(e => e.Date));
            // Check if the JournalEntries collection is being populated correctly
            System.Diagnostics.Debug.WriteLine("JournalEntries populated");

        }

        public void RefreshEntries()
        {
            JournalEntries = new ObservableCollection<JournalEntry>(App.JournalEntries.OrderByDescending(e => e.Date));
            // Check if the RefreshEntries method is being called correctly
            System.Diagnostics.Debug.WriteLine("RefreshEntries called");
            //Brainydaps Editing
            OnPropertyChanged(nameof(JournalEntries));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
