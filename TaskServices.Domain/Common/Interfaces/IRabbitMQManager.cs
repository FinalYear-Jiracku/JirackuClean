using RabbitMQ.Client;

namespace TaskServices.Domain.Common.Interfaces
{
    public interface IRabbitMQManager
    {
        IModel CreateModel();
        void CloseConnection();
        void CheckConnection();
    }
}
