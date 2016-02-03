using System;
using Windows.UI.Xaml;

namespace Petrolhead2016XT.Services.SettingsServices
{
    public interface ISettingsService
    {
        bool UseShellBackButton { get; set; }
        ApplicationTheme AppTheme { get; set; }
        TimeSpan CacheMaxDuration { get; set; }
        bool IsEncryptionEnabled { get; set; }
        
    }
}
