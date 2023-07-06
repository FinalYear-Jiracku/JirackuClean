using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserServices.Domain.Common.Interfaces
{
    public interface IUserEventPublisher
    {
        void SendMessage(BaseEvent @event);
        void Dispose();
    }
}
