using Gknzby.UI;

namespace Gknzby.Managers
{
    public interface IUIManager : IManager
    {
        void SendUIComponent(UIComponent uiComponent);
        void SendUIAction(UIAction uiAction);
        void RegisterMenu(IUIMenu uiMenu);
        void ShowMenu(string menuName);
        void HideMenu(string menuName);
    }
}