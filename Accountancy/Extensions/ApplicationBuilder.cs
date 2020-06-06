using Accountancy.Handlers;
using Accountancy.Models.Events;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using RabbitMq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accountancy.Extensions
{
    public static class ApplicationBuilder
    {
        public static IApplicationBuilder ConfigureEventBus(this IApplicationBuilder myApp)
        {

            // https://stackoverflow.com/questions/54216939/the-non-generic-method-iserviceprovider-getservicetype-cannot-be-used-with-t
            var eventBus = myApp.ApplicationServices.GetService<IEventBus>();

            // IHostApplicationLifetime events or older version IApplicationLifetime?
            var myAppLifetime = myApp.ApplicationServices.GetService<IHostApplicationLifetime>();
            eventBus.Subscribe<AccountancyRelay, MessageReceivedHandler>("Accountancy");
            myAppLifetime.ApplicationStopping.Register(() => OnStopping(eventBus));


            return myApp;
        }

        private static void OnStopping(IEventBus eventBus)
        {
            eventBus.Deregister();
        }
    }
}
