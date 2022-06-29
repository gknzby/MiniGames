using System.Collections.Generic;
using Gknzby.UI;
using UnityEngine;
using Gknzby.Components;
using System;

namespace Gknzby.Managers
{
    public class UIManager : MonoBehaviour, IUIManager
    {
        private List<IUIMenu> uiMenuCollection = new();
        private List<IUIMenu> activeUIMenuCollection = new();

        #region IUIManager
        public void SendUIAction(UIAction uiAction)
        {
            switch (uiAction)
            {
                case UIAction.Unassigned:
                    Debug.LogError("Assign UIAction value!");
                    break;
                case UIAction.StartGame:
                    LoadLevel();
                    break;
                case UIAction.RetryLevel:
                    LoadLevel();
                    break;
                case UIAction.NextLevel:
                    NextLevel();
                    break;
                case UIAction.RestartGame:
                    RestartGame();
                    break;
                case UIAction.ResumeGame:
                    ResumeGame();
                    break;
                case UIAction.PauseGame:
                    StopGame();
                    break;
                case UIAction.ExitGame:
                    ExitGame();
                    break;
                default:
                    break;
            }
        }

        private void ExitGame()
        {
            ManagerProvider.GetManager<IGameManager>().SendGameAction(GameAction.ExitGame);
        }

        public void SendUIComponent(UIComponent uiComponent)
        {
            switch (uiComponent.uiAction)
            {
                case UIAction.LoadLevel:
                    int level = int.Parse(uiComponent.value);
                    LoadLevel(level);
                    break;
                case UIAction.ShowMenu:
                    ShowMenu(uiComponent.value);
                    break;
                case UIAction.SelectSubGame:
                    SelectSubGame(uiComponent.value);
                    break;
                case UIAction.HideMenu:
                    HideMenu(uiComponent.value);
                    break;
                default:
                    SendUIAction(uiComponent.uiAction);
                    break;
            }
        }

        public void RegisterMenu(IUIMenu uiMenu)
        {
            if (!uiMenuCollection.Contains(uiMenu))
            {
                uiMenuCollection.Add(uiMenu);
            }
            uiMenu.HideMenu();
        }
        public void ShowMenu(string menuName)
        {
            foreach (IUIMenu uiMenu in uiMenuCollection)
            {
                if (uiMenu.MenuName == menuName)
                {
                    uiMenu.ShowMenu();
                    activeUIMenuCollection.Add(uiMenu);
                    return;
                }
            }

            Debug.LogWarning("UIManager couldn't find " + menuName);
        }
        public void HideMenu(string menuName)
        {
            foreach (IUIMenu uiMenu in uiMenuCollection)
            {
                if (uiMenu.MenuName == menuName)
                {
                    uiMenu.HideMenu();
                    activeUIMenuCollection.Remove(uiMenu);
                    return;
                }
            }

            Debug.LogWarning("UIManager couldn't find " + menuName);
        }

        public IUIMenu GetMenu(string menuName)
        {
            foreach (IUIMenu uiMenu in uiMenuCollection)
            {
                if (uiMenu.MenuName == menuName)
                {
                    return uiMenu;
                }
            }
            return null;
        }

        private void SelectSubGame(string subGameStr)
        {
            if(System.Enum.TryParse(subGameStr, out SubGame subGame))
            {
                ManagerProvider.GetManager<GameSelector>().SelectGame(subGame);
            }
            else
            {
                Debug.LogError(subGameStr + " SubGame couldn't found in SubGame(Enum)");
            }
        }
        #endregion

        #region Class Functions
        private void ResumeGame()
        {
            ManagerProvider.GetManager<IGameManager>().SendGameAction(GameAction.ResumeGame);

            this.HideMenu("PauseMenu");
        }
        private void StopGame()
        {
            ManagerProvider.GetManager<IGameManager>().SendGameAction(GameAction.PauseGame);

            this.ShowMenu("PauseMenu");
        }
        private void RestartGame()
        {
            ManagerProvider.GetManager<IGameManager>().SendGameAction(GameAction.Restart);
        }
        private void LoadLevel()
        {
            if (!PlayerPrefs.HasKey(GetActiveLevelPhrase()))
            {
                PlayerPrefs.SetInt(GetActiveLevelPhrase(), 0);
            }

            ManagerProvider.GetManager<IGameManager>().SendGameAction(GameAction.LoadLevel);
        }
        private void LoadLevel(int index)
        {
            PlayerPrefs.SetInt(GetActiveLevelPhrase(), index);

            LoadLevel();
        }
        private void NextLevel()
        {
            if (!PlayerPrefs.HasKey(GetActiveLevelPhrase()))
            {
                Debug.LogWarning("Next level but there is no 'Level' key in PlayerPrefs");
                PlayerPrefs.SetInt(GetActiveLevelPhrase(), 0);
            }

            LoadLevel(PlayerPrefs.GetInt(GetActiveLevelPhrase()) + 1);
        }
        private string GetActiveLevelPhrase()
        {
            string subGamePrefix = ManagerProvider.GetManager<GameSelector>().ActiveSubGameData.Prefix;
            PhraseGenerator phraseGenerator = new PhraseGenerator(subGamePrefix);
            phraseGenerator += PostFix.Level;
            return phraseGenerator.ToString();
        }

        private void HideAllMenus()
        {
            foreach (IUIMenu menu in activeUIMenuCollection)
            {
                HideMenu(menu.MenuName);
            }
        }
        #endregion

        #region Unity Functions => Awake, OnDestroy
        private void Awake()
        {
            uiMenuCollection = new List<IUIMenu>();
            ManagerProvider.AddManager<IUIManager>(this);
        }

        private void OnDestroy()
        {
            ManagerProvider.RemoveManager<IUIManager>();
        }
        #endregion
    }
}