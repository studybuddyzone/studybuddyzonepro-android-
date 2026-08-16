using Microsoft.UI.Xaml;

namespace StudyBuddyZoneWindows;

public sealed partial class MainWindow : Window
{
    // Same production URL the Android build loads via capacitor.config.json
    // "server.url". Kept identical on purpose so both platforms show the
    // exact same live app (see repo requirement: do not change this URL).
    private const string StartUrl = "https://studybuddypro-psi.vercel.app/";

    public MainWindow()
    {
        InitializeComponent();
        Title = "StudyBuddyZone";
        InitializeWebViewAsync();
    }

    private async void InitializeWebViewAsync()
    {
        await AppWebView.EnsureCoreWebView2Async();

        AppWebView.CoreWebView2.NavigationCompleted += (sender, args) =>
        {
            LoadingPanel.Visibility = Visibility.Collapsed;
            AppWebView.Visibility = Visibility.Visible;
        };

        AppWebView.Source = new Uri(StartUrl);
    }
}
