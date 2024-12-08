using System.IO;
using System.Text;
using App.ViewModels;
using App.ViewModels.Pages;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;

namespace App.Views.Pages.Logs;

public partial class LogsIndex : UserControl
{
    public LogsIndex(MainWindowViewModel mainWindowViewModel)
    {
        this.DataContext = new LogViewModel(mainWindowViewModel);
        InitializeComponent();
    }
    
    public FilePickerFileType CsvType = new(".csv")
    {
        Patterns = new[] { "*.csv" },
        AppleUniformTypeIdentifiers = new[] { "public.comma-separated-values-text" },
        MimeTypes = new[] { "text/csv" },
    };

    private async void exportToCsv(object? sender, RoutedEventArgs e)
    {
        var topLevel = TopLevel.GetTopLevel(this);

        // Start async operation to open the dialog.
        var file = await topLevel.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
        {
            Title = "Save data to CSV",
            SuggestedFileName = "logs export",
            FileTypeChoices = new [] { CsvType },
            
        });

        if (file is not null)
        {
            await using var stream = await file.OpenWriteAsync();
            using var streamWriter = new StreamWriter(stream);
            
            
            var csvSeparator = ",";
            var csvBuilder = new StringBuilder();
            
            csvBuilder.AppendLine($"Id{csvSeparator}UserName{csvSeparator}Message{csvSeparator}Action{csvSeparator}CreatedAt");
            
            foreach (var log in ((LogViewModel)DataContext).Logs)
            {
                var userName = log.user?.name ?? "Unknown";
                csvBuilder.AppendLine($"{log.id}{csvSeparator}{userName}{csvSeparator}{log.message}{csvSeparator}{log.action}{csvSeparator}{log.created_at:yyyy-MM-dd HH:mm:ss}");
            }
            
            await streamWriter.WriteAsync(csvBuilder.ToString());
        }
    }
    
    
}