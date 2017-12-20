using Core.Settings;
using Lykke.SettingsReader;
using Microsoft.Extensions.Configuration;

namespace ModernMoney
{
    public static class ApplicationSettings
    {
        public static IConfigurationRoot Configuration { get; set; }
        public static AppSettings AppSettings { get; set; }

        static ApplicationSettings()
        {
            var builder = new ConfigurationBuilder()
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            AppSettings = (Configuration.LoadSettings<AppSettings>()).CurrentValue;
        }
    }
}
