namespace Gknzby.Components
{
    public interface IEventListener
    {
        void HandleEvent();
    }

    public interface IEventListener<T> : IEventListener 
        where T : IData
    {
        void HandleEvent(T eventArg);
    }
}
