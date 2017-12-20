using Autofac;
using Autofac.Extensions.DependencyInjection;
using AzureRepositories;
using Common.Log;
using Core.Conversation;
using Core.Services;
using Core.Settings;
using Lykke.ModernMoney.Services;
using Lykke.SettingsReader;
using Microsoft.Extensions.DependencyInjection;

namespace ModernMoney.Modules
{
    public class ServiceModule : Module
    {
        private readonly IReloadingManager<AppSettings> _settings;
        private readonly ILog _log;
        private readonly IServiceCollection _services;

        public ServiceModule(IReloadingManager<AppSettings> settings, ILog log)
        {
            _settings = settings;
            _log = log;
            _services = new ServiceCollection();
        }

        protected override void Load(ContainerBuilder builder)
        {
            RegisterLocalTypes(builder);
            RegisterLocalServices(builder);

            builder.RegisterInstance(_settings.CurrentValue.ModernMoneyWebsite.Email);

            builder.RegisterInstance<IConversationRepository>(
                  AzureRepoBinder.CreateConversationInformationRepository(_settings.ConnectionString(x => x.ModernMoneyWebsite.AzureStorage.ClientPersonalInfoConnString), _log)).
              SingleInstance();

            builder.Populate(_services);
        }

        private static void RegisterLocalServices(ContainerBuilder builder)
        {
            builder.RegisterType<HealthService>()
                .As<IHealthService>()
                .SingleInstance();
        }

        private void RegisterLocalTypes(ContainerBuilder builder)
        {
            builder.RegisterInstance(_log).As<ILog>().SingleInstance();
        }
    }
}
