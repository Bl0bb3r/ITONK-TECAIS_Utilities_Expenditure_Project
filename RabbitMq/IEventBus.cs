using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMq
{
    public interface IEventBus
    {
        void Deregister();
        void Dispose();
        void Subscribe<TEvent, THandler>(string routingKey = null)
            where TEvent : EventBase
            where THandler : IEventHandler<TEvent>;

        void Publish(EventBase @event);
    }
}
