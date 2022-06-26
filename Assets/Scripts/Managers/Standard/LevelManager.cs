using System.Collections.Generic;
using UnityEngine;

namespace Gknzby.Managers
{
    public class LevelManager : MonoBehaviour, ILevelManager
    {
        public int LevelCount { get { return LevelList.Count; } }
        private List<int> LevelList = new List<int>();

        public bool LoadLevel(int index)
        {
            if (index < 0 || index >= LevelCount)
            {
                Debug.Log(index + " level cannot be loaded.");
                return false;
            }
            return true;
        }

        //[SerializeField] private List<LevelData> LevelList;

        //[SerializeField] private Transform GridParentTransform;
        //[SerializeField] private GameObject GridPrefab;
        //private GridMain gridMain;
        //private List<INewLevelListener> levelListeners = new List<INewLevelListener>();

        //public bool LoadLevel(int index)
        //{
        //    if (index < 0 || index >= LevelCount)
        //    {
        //        Debug.Log(index + " level cannot be loaded.");
        //        return false;
        //    }
        //    return true;

        //    gridMain = GetNewGrid();
        //    LevelData levelData = LevelList[index];

        //    gridMain.SetGridDimensions(levelData.GridDimensions);
        //    gridMain.SetCellSize(levelData.CellSize, levelData.CellGap);
        //    gridMain.SetMaterialDict(levelData.GetMaterialDict());
        //    gridMain.SetCellTypeArray(levelData.cellTypeArray);
        //    gridMain.GenerateGrid();

        //    NotifyNewLevelListeners(levelData);
        //}

        //private GridMain GetNewGrid()
        //{
        //    if (gridMain != null)
        //    {
        //        GameObject.Destroy(gridMain.gameObject);
        //    }
        //    //while (GridParentTransform.childCount > 0)
        //    //{
        //    //    GameObject.Destroy(GridParentTransform.GetChild(0).gameObject);
        //    //}

        //    return GameObject.Instantiate(GridPrefab, GridParentTransform).GetComponent<GridMain>();
        //}

        //public void AddNewLevelListener(INewLevelListener listener)
        //{
        //    levelListeners.Add(listener);
        //}
        //public void RemoveNewLevelListener(INewLevelListener listener)
        //{
        //    levelListeners.Remove(listener);
        //}
        //private void NotifyNewLevelListeners(LevelData levelData)
        //{
        //    foreach (INewLevelListener inll in levelListeners)
        //    {
        //        inll.NewLevelData(levelData);
        //    }
        //}

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
