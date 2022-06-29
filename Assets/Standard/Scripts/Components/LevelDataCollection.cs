using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gknzby.Components
{
    public abstract class LevelDataCollection : ScriptableObject, ILevelDataCollection
    {
        public abstract int LevelCount { get; }

        public abstract bool GetLevelData(int index, out ILevelData levelData);
    }

    [System.Serializable]
    public abstract class LevelDataCollection<T> : LevelDataCollection
        where T : ILevelData
    {
        [SerializeField] protected List<T> LevelCollection = new();

        public override int LevelCount { get { return LevelCollection.Count; } }

        public override bool GetLevelData(int index, out ILevelData levelData)
        {
            if (0 <= index && index < LevelCount)
            {
                levelData = LevelCollection[index];
                return true;
            }
            else
            {
                levelData = null;
                return false;
            }
        }
    }
}
