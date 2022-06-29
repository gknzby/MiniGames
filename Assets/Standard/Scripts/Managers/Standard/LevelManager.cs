using System.Collections.Generic;
using UnityEngine;
using Gknzby.Components;

namespace Gknzby.Managers
{
    public class LevelManager : MonoBehaviour, ILevelManager, IEventListener<SubGameData>
    {
        [System.Serializable]
        private class SubGameLevelCollection
        {
            public SubGame subGame;
            public int LevelCount { get { return levelCollection.LevelCount; } }
            public LevelDataCollection levelCollection;
        }
        [SerializeField] private List<SubGameLevelCollection> subGameLevelCollections = new();
        
        public int LevelCount { 
            get {
                if (activeSubGame == null)
                    return 0;

                foreach(SubGameLevelCollection slc in subGameLevelCollections)
                {
                    if (slc.subGame == activeSubGame.subGame)
                    {
                        return slc.LevelCount;
                    }
                }
                return 0;
            } 
        }
        private SubGameData activeSubGame;

        public bool LoadLevel(int index)
        {
            if(TryFindSubGame(out SubGameLevelCollection collection)
                && !IsOutOfIndex(index)
                && collection.levelCollection.GetLevelData(index, out ILevelData levelData))
            {
                ManagerProvider.GetManager<IEventManager>().InvokeEvent(EventName.LevelChange, levelData);

                return true;
            }

            return false;
        }

        private bool IsOutOfIndex(int index)
        {
            if (index < 0 || index >= LevelCount)
            {
                Debug.LogWarning(index + " level cannot be loaded.");
                return true;
            }

            return false;
        }

        private bool TryFindSubGame(out SubGameLevelCollection subGameLevelCollection)
        {
            if (activeSubGame != null)
            {
                foreach (SubGameLevelCollection slc in subGameLevelCollections)
                {
                    if (slc.subGame == activeSubGame.subGame)
                    {
                        subGameLevelCollection = slc;
                        return true;
                    }
                }
            }

            Debug.LogWarning("SubGame couldn't found");
            subGameLevelCollection = null;
            return false;
        }

        public int SubGameLevelCount(SubGame subGame)
        {
            return TryFindSubGame(out SubGameLevelCollection levelDataCollection) ? levelDataCollection.LevelCount : 0;
        }
        public void HandleEvent(SubGameData eventArg)
        {
            activeSubGame = eventArg;
        }

        public void HandleEvent()
        {
            activeSubGame = ManagerProvider.GetManager<GameSelector>().ActiveSubGameData;
        }

        #region Unity Functions => Awake, OnDestroy
        private void Awake()
        {
            ManagerProvider.AddManager<ILevelManager>(this);
        }

        private void Start()
        {
            ManagerProvider.GetManager<IEventManager>().AddEventListener(EventName.SubGameChange, this);
        }

        private void OnDestroy()
        {
            ManagerProvider.GetManager<IEventManager>()?.RemoveEventListener(EventName.SubGameChange, this);
            ManagerProvider.RemoveManager<ILevelManager>();
        }
        #endregion
    }
}
