using JournalApp.Models;
using System.Collections.ObjectModel;

namespace JournalApp
{
    public partial class App : Application
    {
        public static ObservableCollection<JournalEntry> JournalEntries { get; private set; } = new();

        public App()
        {
            InitializeComponent();
            //Brainydaps Editing
            JournalEntries = new ObservableCollection<JournalEntry>();
            MainPage = new NavigationPage(new MainPage());
        }

        public static void AddJournalEntry(JournalEntry entry)
        {
            JournalEntries.Add(entry);
            // Check if the AddJournalEntry method is being called correctly
            System.Diagnostics.Debug.WriteLine("JournalEntry added");
            if (Current.MainPage?.BindingContext is MainPageViewModel mainPageViewModel)
            {
                mainPageViewModel.RefreshEntries();
            }
        }
    }
}
