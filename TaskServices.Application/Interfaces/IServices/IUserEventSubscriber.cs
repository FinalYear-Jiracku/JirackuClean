namespace TaskServices.Application.Interfaces.IServices
{
    public interface IUserEventSubscriber
    {
        Task ReceiveMessage();
        void Dispose();
        void StopConsume();
    }
}
