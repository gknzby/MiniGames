using Gknzby.Components;
using Gknzby.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gknzby.VictoryPoser
{
    public class VictoryPoserLevelGenerator : MonoBehaviour, ILevelGenerator, IEventListener<ILevelData>
    {
        #region LevelGenerator
        private VictoryPoserLevelData currentLevelData;
        public ILevelData CurrentLevelData { get { return currentLevelData; } set { currentLevelData = (VictoryPoserLevelData)value; } }


        [SerializeField] private ArrowFlower arrowFlower;
        [SerializeField] private GameObject arrowPrefab;
        private List<Arrow> arrowCollection;
        public void GenerateLevel()
        {
            StopAllCoroutines();
            arrowFlower.StopFlow();
            ClearLevel();

            foreach(ArrowData arrowData in currentLevelData.arrowDataCollection)
            {
                Arrow arrow = GameObject.Instantiate(arrowPrefab, arrowFlower.transform).GetComponent<Arrow>();
                arrow.SetArrowData(arrowData);
                arrowCollection.Add(arrow);
            }

            arrowFlower.ArrowCollection = arrowCollection;
            arrowFlower.StartFlow(out float finishTime);
            StartCoroutine(FinishLevel(finishTime));
        }

        private IEnumerator FinishLevel(float finishTime)
        {
            finishTime += 1f;
            yield return new WaitForSeconds(finishTime);

            ManagerProvider.GetManager<IGameManager>().SendGameAction(GameAction.Win);
        }

        private void ClearLevel()
        {
            if(arrowCollection != null)
            {
                foreach (Arrow arrow in arrowCollection)
                {
                    GameObject.Destroy(arrow.gameObject);
                }
            }
            arrowCollection = new();
        }

        public void SetLevelData(ILevelData levelData)
        {
            if (levelData.subGame != SubGame.VictoryPoser)
            {
                return;
            }

            CurrentLevelData = levelData;
        }
        #endregion


        #region Events => LevelChange
        public void HandleEvent()
        {
            return;
        }

        public void HandleEvent(ILevelData eventArg)
        {
            SetLevelData(eventArg);
            GenerateLevel();
        }
        #endregion

        #region UnityFunctions =>  OnEnable, OnDisable
        private void OnEnable()
        {
            ManagerProvider.GetManager<IEventManager>().AddEventListener(Gknzby.EventName.LevelChange, this);
        }

        private void OnDisable()
        {
            ManagerProvider.GetManager<IEventManager>()?.RemoveEventListener(Gknzby.EventName.LevelChange, this);
        }
        #endregion
    }
}