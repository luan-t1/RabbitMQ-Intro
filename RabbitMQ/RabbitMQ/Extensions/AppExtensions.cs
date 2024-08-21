using MassTransit;
using RabbitMQ.Bus;

namespace RabbitMQ.Extensions;

internal static class AppExtensions
{
    public static void AddRabbitMQService(this IServiceCollection services)
    {
        services.AddMassTransit(busConfigurator =>
        {
            services.AddTransient<IPublishBus, PublishBus>();

            busConfigurator.AddConsumer<RelatorioSolicitadoEventConsumer>();

            busConfigurator.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(new Uri("amqp://localhost:5672"), host =>
                {
                    host.Username("guest");
                    host.Password("guest");
                });

                cfg.ConfigureEndpoints(ctx);
            });
        });
    }
}
