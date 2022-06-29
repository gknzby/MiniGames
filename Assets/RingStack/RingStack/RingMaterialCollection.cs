using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gknzby.RingStack
{

    [CreateAssetMenu(fileName = "RingMaterials", menuName = "Gknzby/Ring Stack/Material Dictionary", order = 2)]
    public class RingMaterialCollection : ScriptableObject
    {
        [System.Serializable]
        public class RingMat
        {
            public RingType ringType;
            public Material material;
        }
        [SerializeField] private List<RingMat> MaterialList = new();

        public Dictionary<RingType, Material> GetMaterialDictionary()
        {
            Dictionary<RingType, Material> materialDictionary = new();

            foreach(RingMat ringMat in MaterialList)
            {
                materialDictionary.Add(ringMat.ringType, ringMat.material);
            }

            return materialDictionary;
        }
    }
}
