namespace TaskServices.Domain.Common.Interfaces
{
    public interface IUserEventSubscriber
    {
        Task ReceiveMessage();
        void Dispose();
        void StopConsume();
    }
}
