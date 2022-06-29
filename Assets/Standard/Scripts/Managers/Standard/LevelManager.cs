using System.Collections.Generic;
using UnityEngine;
using Gknzby.Components;

namespace Gknzby.Managers
{
    public class LevelManager : MonoBehaviour, ILevelManager
    {
        /// <summary>
        /// 
        [System.Serializable]
        private class SubGameLevelCollection
        {
            public SubGame subGame;
            public LevelDataCollection levelCollection;
        }
        [SerializeField] private List<SubGameLevelCollection> subGameLevelCollections = new List<SubGameLevelCollection>();
        /// 
        /// </summary>

        [SerializeField] private List<LevelDataCollection> lvevelCol = new();

        

        public int LevelCount { get { return LevelList.Count; } }
        private List<int> LevelList = new List<int>();

        public bool LoadLevel(int index)
        {
            //Debug.Log("level index" + index);
            //if (index < 0 || index >= LevelCount)
            //{
            //    Debug.Log(index + " level cannot be loaded.");
            //    return false;
            //}
            //return true;

            ILevelData levelData;

            _ = subGameLevelCollections[0].levelCollection.GetLevelData(index, out levelData);

            ManagerProvider.GetManager<IEventManager>().InvokeEvent(EventName.LevelChange, levelData);

            return true;

        }

        #region Unity Functions => Awake, OnDestroy
        private void Awake()
        {
            ManagerProvider.AddManager<ILevelManager>(this);
        }

        private void OnDestroy()
        {
            ManagerProvider.RemoveManager<ILevelManager>();
        }
        #endregion
    }
}
