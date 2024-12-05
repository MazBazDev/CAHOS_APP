using System.Threading.Tasks;
using ReactiveUI;

namespace App.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{ 
    private string? _errorMessage;
    private string? _errorColor;
    public string UserName { get; set; }
    
    public string? ErrorMessage
    {
        get => _errorMessage;
        set => this.RaiseAndSetIfChanged(ref _errorMessage, value);
    }
    
    public string? ErrorColor
    {
        get => _errorColor;
        set => this.RaiseAndSetIfChanged(ref _errorColor, value);
    }
    
    public MainWindowViewModel()
    {
        UserName = "MazBaz";
    }
    
    public void SetError(string message, int? delay = null)
    {
        ErrorMessage = message;
        ErrorColor = "red";
        
        if (delay != null)
        {
            Task.Delay((int)delay).ContinueWith(_ => ClearError());
        }
    }
    
    public void setSuccess(string message, int? delay = null)
    {
        ErrorMessage = message;
        ErrorColor = "green";
        
        if (delay != null)
        {
            Task.Delay((int)delay).ContinueWith(_ => ClearError());
        }
    }
    
    public void ClearError()
    {
        ErrorMessage = null;
        ErrorColor = null;
    }
}