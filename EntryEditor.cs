using Microsoft.Maui.Controls;

namespace JournalApp
{
    public class EntryEditor : Editor
    {
        public EntryEditor()
        {
            this.TextChanged += (sender, e) =>
            {
                var editor = sender as EntryEditor;
                if (editor != null)
                {
                    System.Diagnostics.Debug.WriteLine($"EntryEditor text changed to: {editor.Text}");
                }
            };
        }
    }
}
