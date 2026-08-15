using Microsoft.UI.Xaml;

namespace StudyBuddyZoneWindows;

/// <summary>
/// StudyBuddyZone Windows host application. Provides the application-specific
/// behavior to supplement the default Application class. Wraps the same
/// production web app the Android build points at (see
/// capacitor.config.json "server.url") in a native WebView2 window so it can
/// be packaged as an MSIX for the Microsoft Store.
/// </summary>
public partial class App : Application
{
    private Window? _window;

    public App()
    {
        InitializeComponent();
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        _window = new MainWindow();
        _window.Activate();
    }
}
