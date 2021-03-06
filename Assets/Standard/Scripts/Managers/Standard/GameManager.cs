using System.Collections;
using UnityEngine;
using Gknzby.Components;
using System;

namespace Gknzby.Managers
{
    public class GameManager : MonoBehaviour, IGameManager
    {
        #region IGameManager
        public void SendGameAction(GameAction gameAction)
        {
            switch (gameAction)
            {
                case GameAction.PauseGame:
                    StopGame();
                    break;
                case GameAction.Lost:
                    GameLost();
                    break;
                case GameAction.Win:
                    GameWin();
                    break;
                case GameAction.Restart:
                    RestartGame();
                    break;
                case GameAction.LoadLevel:
                    LoadLevel();
                    break;
                case GameAction.EndGame:
                    EndGame();
                    break;
                case GameAction.ResumeGame:
                    ResumeGame();
                    break;
                case GameAction.ExitGame:
                    ExitGame();
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Class Functions
        private string GetActiveLevelPhrase()
        {
            string subGamePrefix = ManagerProvider.GetManager<GameSelector>().ActiveSubGameData.Prefix;
            PhraseGenerator phraseGenerator = new PhraseGenerator(subGamePrefix);
            phraseGenerator += PostFix.Level;
            return phraseGenerator.ToString();
        }
        private void EndGame()
        {
            StopGame();
            ManagerProvider.GetManager<IUIManager>().ShowMenu("EndGameMenu");
            PlayerPrefs.SetInt(GetActiveLevelPhrase(), 0);
        }

        private void GameWin()
        {
            StopGame();
            ManagerProvider.GetManager<IUIManager>().ShowMenu("WinMenu");
        }

        private void GameLost()
        {
            StopGame();
            ManagerProvider.GetManager<IUIManager>().ShowMenu("LostMenu");
        }

        private void ResumeGame()
        {
            Time.timeScale = 1f;
            ManagerProvider.GetManager<IUIManager>().ShowMenu("HUD");
            ManagerProvider.GetManager<IInputManager>().StartSendingInputs();
        }

        private void StopGame()
        {
            Time.timeScale = 0f;
            ManagerProvider.GetManager<IUIManager>().HideMenu("HUD");
            ManagerProvider.GetManager<IInputManager>().StopSendingInputs();
        }
        
        private void RestartGame()
        {
            ManagerProvider.GetManager<IUIManager>().ShowMenu("MainMenu");
        }

        private void LoadLevel()
        {
            PlayerPrefs.Save();

            int level = PlayerPrefs.GetInt(GetActiveLevelPhrase());

            ILevelManager levelManager = ManagerProvider.GetManager<ILevelManager>();
            if (levelManager.LoadLevel(level))
            {
                ResumeGame();
            }
            else
            {
                SendGameAction(GameAction.EndGame);
            }
        }
        private void ExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
         Application.OpenURL(webplayerQuitURL);
#else
         Application.Quit();
#endif
        }
        #endregion

        #region Unity Functions => Awake, Start, OnDestroy
        private void Awake()
        {
            ManagerProvider.AddManager<IGameManager>(this);
        }
        private IEnumerator Start()
        {
            yield return null; //Waiting first update functions
            ManagerProvider.GetManager<IUIManager>().ShowMenu("GameSelectionMenu");
        }

        private void OnDestroy()
        {
            ManagerProvider.RemoveManager<IGameManager>();
        }
        #endregion

    }
}