using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gknzby.RingStack
{
    [System.Serializable]
    public class RingHolderData
    {
        [SerializeField] public List<RingData> ringDataList = new();
    }
}
