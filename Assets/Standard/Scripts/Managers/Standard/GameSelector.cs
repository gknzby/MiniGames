using Gknzby.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Gknzby.Managers
{
    public class GameSelector : MonoBehaviour, IManager
    {
        [System.Serializable]
        private class SubGameTransform
        {
            public SubGameData subGameData;
            public Transform subGameTransform;
            public void SetEnableSubGame(bool enable)
            {
                subGameTransform.gameObject.SetActive(enable);
            }
        }
        [SerializeField] private List<SubGameTransform> subGameCollection = new();
        private SubGameTransform activeSubGame;

        public SubGameData ActiveSubGameData
        {
            get
            {
                return activeSubGame == null ? null : activeSubGame.subGameData;
            }
        }

        public bool SelectGame(SubGame subGame)
        {
            Debug.Log("Game selected");
            DisableActiveSubGame();

            if(TryFindSubGame(subGame, out SubGameTransform subGameTransform))
            {
                activeSubGame = subGameTransform;
                activeSubGame.SetEnableSubGame(true);

                ManagerProvider.GetManager<IEventManager>().InvokeEvent(EventName.SubGameChange, activeSubGame.subGameData);
                ManagerProvider.GetManager<IUIManager>().ShowMenu("MainMenu");
                return true;
            }
            else
            {
                ManagerProvider.GetManager<IEventManager>().InvokeEvent<SubGameData>(EventName.SubGameChange, null);
                Debug.LogWarning("SubGame couldn't enabled");
                return false;
            }

        }

        public void DisableActiveSubGame()
        {
            bool anyActiveGame = activeSubGame != null;
            if (anyActiveGame)
            {
                activeSubGame.SetEnableSubGame(false);
                activeSubGame = null;
            }
        }

        private SubGameTransform FindSubGame(SubGame subGame)
        {
            SubGameTransform subGameTransform;
            
            _ = TryFindSubGame(subGame, out subGameTransform);

            return subGameTransform;
        }

        private bool TryFindSubGame(SubGame subGame, out SubGameTransform subGameTransform)
        {
            foreach (SubGameTransform sgt in subGameCollection)
            {
                if(sgt.subGameData.subGame == subGame)
                {
                    subGameTransform = sgt;
                    return true;
                }
            }

            subGameTransform = null;
            return false;
        }

        #region Unity Functions => Awake, OnDestroy
        private void Awake()
        {
            ManagerProvider.AddManager<GameSelector>(this);
        }

        private void OnDestroy()
        {
            ManagerProvider.RemoveManager<GameSelector>();
        }
        #endregion
    }
}
