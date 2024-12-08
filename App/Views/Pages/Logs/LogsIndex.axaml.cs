using App.ViewModels;
using App.ViewModels.Pages;
using Avalonia.Controls;

namespace App.Views.Pages.Logs;

public partial class LogsIndex : UserControl
{
    public LogsIndex(MainWindowViewModel mainWindowViewModel)
    {
        this.DataContext = new LogViewModel(mainWindowViewModel);
        InitializeComponent();
    }
}