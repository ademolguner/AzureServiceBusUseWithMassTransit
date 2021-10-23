using GreenPipes;
using MassTransit;
using MassTransit.Azure.ServiceBus.Core;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBusExample.Application.Common.Providers;
using ServiceBusExample.Domain.Common.Attributes;
using ServiceBusExample.Domain.Enums;
using ServiceBusExample.Infrastructure.Persistance.Configurations;
using ServiceBusExample.Infrastructure.Providers;
using System;
using System.Linq;
using System.Reflection;

namespace ServiceBusExample.Infrastructure
{
    public static class ServiceBusProvierInjection
    {
        private static string _connectionString;
        private static (Type Class, MessageConsumerAttribute Attribute)[] _consumerTypes;

        public static IServiceCollection AddServiceBusInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Reflection ile tüm assembly içerisinde inject edilen tüm türleri alıyoruz.
            var allTypes = Assembly.GetEntryAssembly()
               .GetTypes()
               .GroupBy(t => t.FullName)
               .Select(t => t.First());

            // ServiceBus işlemleri için hangi provider kullanılacak ise ona göre injection belirleniyor.
            switch (configuration.GetValue<ServiceBusProverders?>("ServiceBus:Provider"))
            {
                case ServiceBusProverders.Masstransit:
                    services.AddScoped<IMessageBrokerProvider, MasstransitProvider>();
                    break;

                case ServiceBusProverders.AzureServiceBus:
                    services.AddScoped<IMessageBrokerProvider, AzureServiceBusProvider>();
                    break;

                default:
                    throw new ConfigurationException("Wrong ServiceBug Provider value!");
            }

            // ServiceBus app için kullanılacak CloudProvider connection bilgisi (appsettings.json) dosyasından okuyacağız.
            _connectionString = configuration.GetValue<string>("ServiceBus:ConnectionString");

            /* _consumerTypes   değişkeni ile  Inject edilen türler içerisinden Consumer olanları ayıklamak için attribute altyapısını kullanacağız.
               Bu bize nasıl bir kolaylık sağlayacak? İlerde değineceğiz ancak kısaca tip güvenliği ve kod tekrarından kaçınmış olacağız
               Olusturulan her consumer için  tek tek yazmamıs olacağız.
                    x.Consumer<CategoryConsumer>();
                    x.Consumer<ArticleConsumer>();
                    x.Consumer<TagConsumer>();
                    x.Consumer<CommentConsumer>();
                    x.Consumer<LikeConsumer>();
                    x.Consumer<ArticleDeleteConsumer>();
                    x.Consumer<CategoryUpdateConsumer>();

            */
            _consumerTypes = allTypes
               .Select(t => (Class: t, Attribute: t.GetCustomAttribute<MessageConsumerAttribute>(false)))
               .Where(t => t.Attribute?.GetType() == typeof(MessageConsumerAttribute))
               .ToArray();

            services.AddMassTransit(x =>
            {
                x.AddConsumers(_consumerTypes.Select(t => t.Class).ToArray());
                if (string.IsNullOrEmpty(_connectionString))
                {
                    x.UsingInMemory(InMemoryConfig);
                }
                else
                {
                    x.UsingAzureServiceBus(ServiceBusConfiguration);
                }
            });

            services.AddMassTransitHostedService();
            return services;
        }

        private static void InMemoryConfig(IBusRegistrationContext ctx, IInMemoryBusFactoryConfigurator cfg)
        {
            //var loggerStore = new MasstransitAuditStore(ctx.GetService<ILogger<MasstransitAuditStore>>());
            //cfg.ConnectConsumeAuditObserver(loggerStore);
            //cfg.ConnectSendAuditObservers(loggerStore);

            cfg.MessageTopology.SetEntityNameFormatter(new MessageNameFormatter(cfg.MessageTopology.EntityNameFormatter));

            foreach (var item in _consumerTypes)
            {
                cfg.ReceiveEndpoint(item.Attribute.Name, cfgQue =>
                {
                    SetMasstransitConfig(cfgQue);

                    cfgQue.ConfigureConsumeTopology = false;

                    cfgQue.ConfigureConsumer(ctx, item.Class);

                    //Circuit Breaker Kullanımı
                    cfgQue.UseCircuitBreaker(cb =>
                    {
                        cb.TripThreshold = 15;
                        cb.ActiveThreshold = 10;
                        cb.ResetInterval = TimeSpan.FromMinutes(5);
                    });
                });
            }
        }

        private static void ServiceBusConfiguration(IBusRegistrationContext ctx, IServiceBusBusFactoryConfigurator cfg)
        {
            //var loggerStore = new MasstransitAuditStore(ctx.GetService<ILogger<MasstransitAuditStore>>());
            //cfg.ConnectConsumeAuditObserver(loggerStore);
            //cfg.ConnectSendAuditObservers(loggerStore);

            cfg.Host(_connectionString);
            cfg.MessageTopology.SetEntityNameFormatter(new MessageNameFormatter(cfg.MessageTopology.EntityNameFormatter));

            foreach (var item in _consumerTypes)
            {
                switch (item.Attribute.MessageType)
                {
                    case MessageTypes.Topic:
                        cfg.SubscriptionEndpoint(item.Attribute.SubscriptionName, item.Attribute.Name, cfgSub =>
                        {
                            SetAzureConfig(cfgSub);
                            SetMasstransitConfig(cfgSub);
                            cfgSub.ConfigureConsumer(ctx, item.Class);

                            if (item.Attribute.FilterQuery != null)
                            {
                                cfgSub.Rule = new RuleDescription
                                {
                                    Name = RuleDescription.DefaultRuleName,
                                    Filter = new SqlFilter(item.Attribute.FilterQuery)
                                };
                            }
                        });
                        break;

                    case MessageTypes.Queue:
                        cfg.ReceiveEndpoint(item.Attribute.Name, cfgQue =>
                        {
                            SetAzureConfig(cfgQue);
                            SetMasstransitConfig(cfgQue);

                            cfgQue.ConfigureConsumeTopology = false;

                            cfgQue.ConfigureConsumer(ctx, item.Class);

                            //Circuit Breaker Kullanımı
                            cfgQue.UseCircuitBreaker(cb =>
                              {
                                  cb.TripThreshold = 15;
                                  cb.ActiveThreshold = 10;
                                  cb.ResetInterval = TimeSpan.FromMinutes(5);
                              });

                            //3. Rate Limiter Kullanımı
                            cfgQue.UseRateLimit(20, TimeSpan.FromSeconds(5));
                        });
                        break;
                }
            }
        }

        private static void SetMasstransitConfig(IConsumePipeConfigurator cfgSub)
        {
            cfgSub.UseMessageRetry(r =>
            {
                r.Ignore(
                    typeof(ArgumentException),
                    typeof(ArgumentNullException),
                    typeof(ArgumentOutOfRangeException),
                    typeof(IndexOutOfRangeException),
                    typeof(DivideByZeroException),
                    typeof(InvalidCastException));
                r.Immediate(1).Intervals(new TimeSpan[] { TimeSpan.FromSeconds(20), TimeSpan.FromSeconds(60) });
               
            });
        }

        private static void SetAzureConfig(IServiceBusEndpointConfigurator cfg)
        {
            var concurrent = 1;
            if (cfg is IReceiveEndpointConfigurator recCfg)
            {
                recCfg.PrefetchCount = concurrent;
                recCfg.ConcurrentMessageLimit = concurrent;
            }
            if (cfg is IConsumePipeConfigurator consumeCfg)
            {
                consumeCfg.UseConcurrencyLimit(concurrent);
            }
            if (cfg is IServiceBusQueueEndpointConfigurator queue)
            {
                queue.EnablePartitioning = false;
            }
            cfg.MaxConcurrentCalls = concurrent;
            cfg.MaxDeliveryCount = 10;
            cfg.LockDuration = TimeSpan.FromMinutes(5);
            cfg.MaxAutoRenewDuration = TimeSpan.FromMinutes(10);
            cfg.EnableBatchedOperations = false;
            cfg.EnableDeadLetteringOnMessageExpiration = false;
            cfg.DefaultMessageTimeToLive = TimeSpan.FromDays(7);
            cfg.RequiresSession = false;
        }
    }
}