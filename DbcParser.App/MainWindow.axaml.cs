using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using System.IO;
using System.Threading.Tasks;

namespace DbcParser.App;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private async void OnLoadDbcClicked(object? sender, RoutedEventArgs e)
    {
        var options = new FilePickerOpenOptions
        {
            Title = "Open DBC file",
            AllowMultiple = false,
            FileTypeFilter =
            [
                new FilePickerFileType("DBC files")
                {
                    Patterns = ["*.dbc"]
                }
            ]
        };

        var files = await this.StorageProvider.OpenFilePickerAsync(options);

        if (files.Count > 0)
        {
            await using var stream = await files[0].OpenReadAsync();
            using var reader = new StreamReader(stream);

            DbcContentTextBox.Text = await reader.ReadToEndAsync();
        }
    }
}