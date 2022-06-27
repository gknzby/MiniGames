using Gknzby.Managers;
using UnityEngine;

namespace Gknzby.UI
{
    public class UIComponent : MonoBehaviour
    {
        public UIAction uiAction;
        public string value;

        public void SendToUIManager()
        {
            ManagerProvider.GetManager<IUIManager>().SendUIComponent(this);
        }
    }
}