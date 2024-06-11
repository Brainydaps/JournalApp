using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Media;
using Microsoft.Maui.Devices.Sensors;
using JournalApp.Models;

namespace JournalApp
{
    public partial class JournalEntryPage : ContentPage
    {
        public JournalEntryPage()
        {
            InitializeComponent();
            BindingContext = new JournalEntry();
        }

        private async void AddPictureButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                var photo = await MediaPicker.PickPhotoAsync();
                if (photo != null)
                {
                    var stream = await photo.OpenReadAsync();
                    var filePath = Path.Combine(FileSystem.AppDataDirectory, photo.FileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    {
                        await stream.CopyToAsync(fileStream);
                    }

                    if (BindingContext is JournalEntry entry)
                    {
                        entry.Pictures.Add(filePath);
                        OnPropertyChanged(nameof(entry.Pictures));
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Unable to pick photo: {ex.Message}", "OK");
            }
        }

        private async void TakePictureButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();
                if (photo != null)
                {
                    var stream = await photo.OpenReadAsync();
                    var filePath = Path.Combine(FileSystem.AppDataDirectory, photo.FileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    {
                        await stream.CopyToAsync(fileStream);
                    }

                    if (BindingContext is JournalEntry entry)
                    {
                        entry.Pictures.Add(filePath);
                        OnPropertyChanged(nameof(entry.Pictures));
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Unable to take photo: {ex.Message}", "OK");
            }
        }

        private async void RecordAudioButton_Clicked(object sender, EventArgs e)
        {
#if WINDOWS
            // Logic to record audio
            await Task.CompletedTask;
#else
            await Task.CompletedTask;
#endif
        }

        private async void AddLocationButton_Clicked(object sender, EventArgs e)
        {
            try
            {
#if WINDOWS
                var location = await Geolocation.GetLastKnownLocationAsync();
                if (location != null)
                {
                    // Handle the location
                }
#else
                await Task.CompletedTask;
#endif
            }
            catch (Exception)
            {
                // Handle exceptions
            }
        }

        private async void BookmarkButton_Clicked(object sender, EventArgs e)
        {
#if WINDOWS
            // Logic to bookmark the journal entry
            await Task.CompletedTask;
#else
            await Task.CompletedTask;
#endif
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            //Brainydaps editing
            var entry = BindingContext as JournalEntry;
            
            if (entry != null)
            {
                if (!App.JournalEntries.Contains(entry))
                {
                    App.JournalEntries.Add(entry);
                }
                else
                {
                    var existingEntry = App.JournalEntries.FirstOrDefault(e => e.Date == entry.Date && e.Text == entry.Text);
                    if (existingEntry != null)
                    {
                        existingEntry.Date = entry.Date;
                        existingEntry.Text = entry.Text;
                        existingEntry.Pictures = entry.Pictures;
                    }
                }
                if (Application.Current.MainPage?.BindingContext is MainPageViewModel mainPageViewModel)
                {
                    mainPageViewModel.RefreshEntries();
                }
            }
            await Navigation.PopAsync();
        }
    }
}
