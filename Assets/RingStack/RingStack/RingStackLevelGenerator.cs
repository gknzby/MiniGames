using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gknzby.Managers;
using Gknzby.Components;

namespace Gknzby.RingStack
{
    public class RingStackLevelGenerator : MonoBehaviour, ILevelGenerator, IEventListener<ILevelData>
    {
        private RingStackLevelData levelinData;
        private ILevelData LevelinData { get { return levelinData; } set { levelinData = (RingStackLevelData)value; } }

        [Header("Holder Transforms")]
        [SerializeField] private RingHolder holder1;
        [SerializeField] private RingHolder holder2;
        [SerializeField] private RingHolder holder3;


        [Header("Ring Prefab")]
        [SerializeField] private GameObject ringPrefab;

        private Dictionary<RingType, Material> materialCollection;

        private void OnEnable()
        {
            ManagerProvider.GetManager<IEventManager>().AddEventListener(Gknzby.EventName.LevelChange, this);
        }

        private void OnDisable()
        {
            ManagerProvider.GetManager<IEventManager>()?.RemoveEventListener(Gknzby.EventName.LevelChange, this);
        }

        public void GenerateLevel()
        {
            materialCollection = levelinData.MaterialCollection.GetMaterialDictionary();

            GenerateRingHolder(holder1, levelinData.holder1);
            GenerateRingHolder(holder2, levelinData.holder2);
            GenerateRingHolder(holder3, levelinData.holder3);
        }

        private void GenerateRingHolder(RingHolder holder, RingHolderData holderData)
        {
            holder.ClearStack();

            foreach(RingData ringData in holderData.ringDataList)
            {
                Ring ring = GameObject.Instantiate(ringPrefab).GetComponent<Ring>();
                ring.SetTypeAndMaterial(ringData.ringType, materialCollection[ringData.ringType]);
                holder.PushRingToStack(ring);
            }            
        }

        public Dictionary<RingType, Material> GetMaterialCollection()
        {
            return materialCollection;
        }

        public void SetLevelData(ILevelData levelData)
        {
            if(levelData.WhatIsThis != SubGame.RingStack)
            {
                return;
            }

            LevelinData = levelData;
        }

        public void HandleEvent()
        {
            return;
        }

        public void HandleEvent(ILevelData eventArg)
        {
            SetLevelData(eventArg);
            GenerateLevel();
        }
    }
}
