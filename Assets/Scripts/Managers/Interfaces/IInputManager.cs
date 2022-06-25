using Gknzby.Components;
namespace Gknzby.Managers
{
    public interface IInputManager : IManager
    {
        void SetDefaultReceiver(IInputReceiver inputReceiver);
        void RemoveDefaultReceiver(IInputReceiver inputReceiver);
        void StopSendingInputs();
        void StartSendingInputs();
    }
}