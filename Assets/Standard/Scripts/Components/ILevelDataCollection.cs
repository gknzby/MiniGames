using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gknzby.Components
{
    public interface ILevelDataCollection
    {
        int LevelCount { get; }
        bool GetLevelData(int index, out ILevelData levelData);
    }
}

