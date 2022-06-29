using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gknzby.Managers;

namespace Gknzby.UI
{
    public class UILevelsMenu : UIMenu
    {
        [SerializeField] private GameObject LevelButtonPrefab;
        [SerializeField] private Transform GridTransform;

        private List<GameObject> generatedLevelButtons;

        #region UIMenu
        public override void ShowMenu()
        {
            base.ShowMenu();

            ClearLevelButtons();
            GenerateLevelButtons();
        }
        public override void HideMenu()
        {
            base.HideMenu();

            ClearLevelButtons();
        }
        #endregion

        #region Class Functions
        private void ClearLevelButtons()
        {
            if (generatedLevelButtons == null)
                return;

            foreach (GameObject levelButton in generatedLevelButtons)
            {
                Destroy(levelButton);
            }

            generatedLevelButtons.Clear();
        }

        private void GenerateLevelButtons()
        {
            int levelCount = ManagerProvider.GetManager<ILevelManager>().LevelCount;

            generatedLevelButtons = new List<GameObject>();

            for (int i = 0; i < levelCount; i++)
            {
                GameObject levelBtn = Instantiate(LevelButtonPrefab, GridTransform.transform);
                levelBtn.GetComponent<UIComponent>().value = i.ToString();
                levelBtn.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = (i + 1).ToString();
                levelBtn.name = (i + 1).ToString();

                generatedLevelButtons.Add(levelBtn);
            }
        }
        #endregion
    }
}
