using UnityEngine;

namespace Gknzby.Components
{
    public interface IInputReceiver
    {
        void Click(Vector2 screenPos);
        void Release(Vector2 screenPos);
        void PositionUpdate(Vector2 screenPos);
        void Cancel();
    }
}