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
using Windows.UI.Notifications;

namespace Petrolhead2016XT
{

   /// <summary>
   /// Provides bootstrapper functionality.
   /// </summary>
    sealed partial class App : Template10.Common.BootStrapper
    {
       /// <summary>
       /// Initializes telemetry collection and important variables.
       /// </summary>
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


        /// <summary>
        /// Prevents async operations from crashing into each other
        /// </summary>
        /// <example>
        /// private async Task AsyncMethod()
        /// {
        ///     while (App.Busy)
        ///     {
        ///         await Task.Delay(100);
        ///     }
        ///     App.Busy = true;
        ///     ... the rest of the method ...
        /// }
        /// </example>
        private static bool _Busy = false;
        public static bool Busy
        {
            get
            {
                return _Busy;
            }
            set
            {
                _Busy = value;
                Views.Shell.SetBusy(value, BusyString);
            }
        }

        /// <summary>
        /// Sets the text used in the busy indicator.
        /// </summary>
        private static string _BusyStr = "Please wait...";
        public static string BusyString { get { return _BusyStr; } set
            {
                if (string.IsNullOrWhiteSpace(value))
                    _BusyStr = "Please wait...";
                else
                    _BusyStr = value;
                Views.Shell.SetBusy(Busy, _BusyStr);
            } }

        /// <summary>
        /// Provides error handling code for Petrolhead
        /// </summary>
        /// <param name="sender">Object that threw the unhandled exception</param>
        /// <param name="e">Event args for the instance</param>
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
                        Telemetry.TrackException(ex);
                        ToastContent content = new ToastContent()
                        {
                            Visual = new ToastVisual()
                            {
                                TitleText = new ToastText() { Text = "Fatal Error" },
                                BodyTextLine1 = new ToastText() { Text = "Petrolhead couldn't display a dialog to inform you about the problem, so it has closed." },

                            },
                            Scenario = ToastScenario.Reminder,
                        };
                        ToastNotification toast = new ToastNotification(content.GetXml());
                        ToastNotificationManager.CreateToastNotifier().Show(toast);
                        Exit();
                    }
                });
                
            }
            catch (Exception ex)
            {
                Telemetry.TrackException(ex);
            }
        }

        /// <summary>
        /// Initializes the application.
        /// </summary>
        /// <param name="args">An instance of IActivatedEventArgs</param>
        /// <returns>A completed Task object</returns>
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

        /// <summary>
        /// Provides a global entrypoint into the telemetry client.
        /// </summary>
        public static TelemetryClient Telemetry { get; set; }
        /// <summary>
        /// Performs initialization tasks valid when the state is not restored only.
        /// </summary>
        /// <param name="startKind">Type of start</param>
        /// <param name="args">Instance of IActivatedEventArgs</param>
        /// <returns></returns>
        // runs only when not restored from state
        public override Task OnStartAsync(StartKind startKind, IActivatedEventArgs args)
        {
            NavigationService.Navigate(typeof(Views.MainPage));
            return Task.CompletedTask;
        }
    }
}

