using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gknzby.Managers;
using Gknzby.Components;

namespace Gknzby.RingStack
{
    public class RingStackLevelGenerator : MonoBehaviour, ILevelGenerator, IEventListener<ILevelData>
    {
        private RingStackLevelData currentLevelData;
        public ILevelData CurrentLevelData { get { return currentLevelData; } set { currentLevelData = (RingStackLevelData)value; } }

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
            materialCollection = currentLevelData.MaterialCollection.GetMaterialDictionary();

            GenerateRingHolder(holder1, currentLevelData.holder1);
            GenerateRingHolder(holder2, currentLevelData.holder2);
            GenerateRingHolder(holder3, currentLevelData.holder3);
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
            if (levelData.subGame != SubGame.RingStack)
            {
                return;
            }

            CurrentLevelData = levelData;
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
