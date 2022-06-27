namespace Gknzby.UI
{
    public interface IUIMenu
    {
        string MenuName { get; }
        void ShowMenu();
        void HideMenu();
        void RegisterToUIManager();
    }
}