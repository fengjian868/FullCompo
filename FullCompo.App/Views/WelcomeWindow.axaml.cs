using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using FullCompo.Core.Abstractions.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FullCompo.App.Views;

public partial class WelcomeWindow : Window
{
    private readonly IConfigService _configService;

    public WelcomeWindow()
    {
        _configService = null!;
        InitializeComponent();
    }

    public WelcomeWindow(IServiceProvider services)
    {
        _configService = services.GetRequiredService<IConfigService>();
        InitializeComponent();
        BindControls();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void BindControls()
    {
        var startupCheckBox = this.FindControl<CheckBox>("StartupCheckBox");
        var startButton = this.FindControl<Button>("StartButton");

        if (startupCheckBox != null)
        {
            startupCheckBox.IsChecked = _configService.AppSettings.RunOnStartup;
        }

        if (startButton != null)
        {
            startButton.Click += (_, _) =>
            {
                if (startupCheckBox != null)
                {
                    _configService.AppSettings.RunOnStartup = startupCheckBox.IsChecked ?? false;
                }

                _configService.AppSettings.IsFirstRun = false;
                _configService.Save();
                Close();
            };
        }
    }
}
