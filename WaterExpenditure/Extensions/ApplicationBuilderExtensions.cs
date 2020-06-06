using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaterExpenditure.Handlers;
using WaterExpenditure.Models.Events;

namespace WaterExpenditure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder ConfigureEventBus(this IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetService<IEventBus>();
            var applicationLifeTime = app.ApplicationServices.GetService<IHostApplicationLifetime>();
            eventBus.Subscribe<Measurement, MeasurementReceivedEventHandler>("water");
            applicationLifeTime.ApplicationStopping.Register(() => OnStopping(eventBus));
            return app;
        }

        private static void OnStopping(IEventBus eventBus)
        {
            eventBus.Deregister();
        }
    }
}
