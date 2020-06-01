using ElectricityConsumptionSubmission.Handlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricityConsumptionSubmission.Extensions
{
    public class ApplicationBuilderExtension
    {
        public static IApplicationBuilder ConfigureEventBus(this IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetService<IEventBus>(); //Missing from RabbitMq
            var applicationLifeTime = app.ApplicationServices.GetService<IApplicationLifetime>();
            eventBus.Subscribe<Measurement, MeasurementReceivedEventHandler>("electricity");
            applicationLifeTime.ApplicationStopping.Register(() => OnStopping(eventBus));
            return app;
        }

        private static void OnStopping(IEventBus eventBus)
        {
            eventBus.Deregister();
        }
    }
}
