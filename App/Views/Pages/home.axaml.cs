using App.ViewModels.Pages;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace App.Views.Pages;

public partial class home : UserControl
{
    public home()
    {
        this.DataContext = new HomeViewModel();
    }
}