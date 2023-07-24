using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskServices.Domain.Common.Interfaces
{
    public interface IIssueEventPublisher
    {
        void SendMessage(BaseEvent @event);
        void Dispose();
    }
}
