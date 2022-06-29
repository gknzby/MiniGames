using UnityEngine;
using Gknzby.Managers;

namespace Gknzby.UI
{
    public class UIMenu : MonoBehaviour, IUIMenu
    {
        #region Variables
        [SerializeField] protected GameObject MenuObj;
        [SerializeField] protected string menuName;
        public string MenuName { get { return menuName; } }
        #endregion

        #region IUIMenu
        public virtual void ShowMenu()
        {
            this.MenuObj.SetActive(true);
        }
        public virtual void HideMenu()
        {
            this.MenuObj.SetActive(false);
        }
        public void RegisterToUIManager()
        {
            if (menuName.Length < 1)
            {
                menuName = this.gameObject.name;
            }

            ManagerProvider.GetManager<IUIManager>().RegisterMenu(this);
        }
        #endregion

        #region Unity Functions => Start
        private void Start()
        {
            RegisterToUIManager();
        }
        #endregion
    }
}
