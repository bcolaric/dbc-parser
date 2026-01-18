using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using System.Linq;
using System.IO;
using DbcParser.Parsers;
using DbcParser.Models;

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

            var content = await reader.ReadToEndAsync();
            
            // Parsing dbc file
            var parser = new Parser();
            var network = parser.Parse(content);
            
            // What is loaded
            FileNameText.Text = files[0].Name;
            MessagesDataGrid.ItemsSource = network.Messages;
        }
    }

    private void OnMessageSelected(object? sender, SelectionChangedEventArgs e)
    {
        if (MessagesDataGrid.SelectedItem is Message selectedMessage)
        {
            var signalViewModels = selectedMessage.Signals.Select(s => new SignalViewModel(s)).ToList();
            SignalsDataGrid.ItemsSource = signalViewModels;
        }
    }
}