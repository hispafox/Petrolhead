using System;
using Windows.UI.Xaml;
using System.Threading.Tasks;
using Petrolhead2016XT.Services.SettingsServices;
using Windows.ApplicationModel.Activation;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights;
using System.Diagnostics;
using Windows.UI.Popups;
using Template10.Common;
using System.IO;
using NotificationsExtensions.Toasts;

namespace Petrolhead2016XT
{
    /// Documentation on APIs used in this page:
    /// https://github.com/Windows-XAML/Template10/wiki

    sealed partial class App : Template10.Common.BootStrapper
    {
       
        public App()
        {
            TelemetryConfiguration.Active.InstrumentationKey = "405f34a8-6d13-4341-b282-fc01e8e45311";
            Microsoft.ApplicationInsights.WindowsAppInitializer.InitializeAsync(
                Microsoft.ApplicationInsights.WindowsCollectors.Metadata |
                Microsoft.ApplicationInsights.WindowsCollectors.Session |
                Microsoft.ApplicationInsights.WindowsCollectors.PageView |
                Microsoft.ApplicationInsights.WindowsCollectors.UnhandledException);
            Telemetry = new TelemetryClient();
            InitializeComponent();
            SplashFactory = (e) => new Views.Splash(e);
            UnhandledException += App_UnhandledException;
            #region App settings

            var _settings = SettingsService.Instance;
            RequestedTheme = _settings.AppTheme;
            CacheMaxDuration = _settings.CacheMaxDuration;
            ShowShellBackButton = _settings.UseShellBackButton;

            #endregion
        }

        private async void App_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                e.Handled = true;
                await DispatcherWrapper.Current().DispatchAsync(async () =>
                {
                    try
                    {
                        MessageDialog dialog = new MessageDialog("Sorry, but an error occurred that Petrolhead wasn't built to handle.", "Fatal Error");
                        if (e.Exception is FileNotFoundException)
                        {
                            Telemetry.TrackEvent("Exit-Dialog-Display-Canceled");
                            return;
                        }

                        dialog.Commands.Add(new UICommand("Exit", (command) =>
                        {
                            Exit();
                        }));
                        await dialog.ShowAsync();
                    }
                    catch (Exception ex)
                    {
                        ToastContent content = new ToastContent()
                        {
                            Visual = new ToastVisual()
                            {
                                TitleText = new ToastText() { }
                            }
                        };
                    }
                });
            }
            catch (Exception ex)
            {
                Toas
            }
        }

        // runs even if restored from state
        public override Task OnInitializeAsync(IActivatedEventArgs args)
        {
            // content may already be shell when resuming
            try
            {
                if ((Window.Current.Content as Views.Shell) == null)
                {
                    // setup hamburger shell
                    var nav = NavigationServiceFactory(BackButton.Attach, ExistingContent.Include);
                    Window.Current.Content = new Views.Shell(nav);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("OnInitializeAsync() : Exception occurred");
                Telemetry.TrackException(ex);
                throw new Exception("Unhandled exception in OnInitializeAsync()", ex);
            }
            return Task.CompletedTask;
        }

        public static TelemetryClient Telemetry { get; set; }
        // runs only when not restored from state
        public override Task OnStartAsync(StartKind startKind, IActivatedEventArgs args)
        {
            NavigationService.Navigate(typeof(Views.MainPage));
            return Task.CompletedTask;
        }
    }
}
