using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq
{
    public interface IEventHandler<TEvent>
    {
        Task Handle(TEvent @event);
    }
}
