using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gknzby.RingStack
{
    public class RingStackLevelManager : MonoBehaviour
    {

        [Header("Level")]
        [SerializeField] private List<RingStackLevelData> LevelList = new();
        [SerializeField] private int currentLevel = 0;

        [Header("Holder Transforms")]
        [SerializeField] private RingHolder holder1;
        [SerializeField] private RingHolder holder2;
        [SerializeField] private RingHolder holder3;


        [Header("Ring Prefab")]
        [SerializeField] private GameObject ringPrefab;

        private Dictionary<RingType, Material> materialDictionary;

        private void Start()
        {
            GenerateLevel();
        }

        public void GenerateLevel()
        {
            RingStackLevelData levelData = LevelList[currentLevel];

            materialDictionary = levelData.materialDictionary.GetMaterialDictionary();

            GenerateRingHolder(holder1, levelData.holder1);
            GenerateRingHolder(holder2, levelData.holder2);
            GenerateRingHolder(holder3, levelData.holder3);
        }

        private void GenerateRingHolder(RingHolder holder, RingHolderData holderData)
        {
            holder.ClearStack();

            foreach(RingData ringData in holderData.ringDataList)
            {
                Ring ring = GameObject.Instantiate(ringPrefab).GetComponent<Ring>();
                ring.SetTypeAndMaterial(ringData.ringType, materialDictionary[ringData.ringType]);
                holder.PushRingToStack(ring);
            }            
        }
    }
}
