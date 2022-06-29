using Gknzby.Components;

namespace Gknzby.Managers
{
    public interface IEventManager : IManager
    {
        void AddEventListener(EventName eventName, IEventListener eventListener);
        void RemoveEventListener(EventName eventName, IEventListener eventListener);
        void InvokeEvent<T>(EventName eventName, T eventArgs = default) where T : IData;

        public void InvokeEvent(EventName eventName);
    }
}
