using System;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    [Serializable]
    public class PanelSettings
    {
        public GameObject  greenPanel;
        public Text greenPanelTxt;

        public string[] greenPanelText;
    }
    
    [Serializable]
    public class Settings
    {
        [Header("Delete local data")]
        public GameObject deleteAssure;
    }

    public class SettingsManager : MonoBehaviour
    {
        public PanelSettings panelSettings;
        public Settings settings;
        
        public void DeleteLocalData()
        {
            settings.deleteAssure.SetActive(true);
            RandomText();
        }

        public void DeleteLocalDataAssured()
        {
            settings.deleteAssure.SetActive(false);
            panelSettings.greenPanel.GetComponent<Animator>().SetTrigger($"open");
            PlayerPrefs.DeleteAll();
        }

        public void DeleteLocalDataNotAssured()
        {
            settings.deleteAssure.SetActive(false);
        }

        private void RandomText()
        {
            var rand = UnityEngine.Random.Range(0, panelSettings.greenPanelText.Length);
            panelSettings.greenPanelTxt.text = panelSettings.greenPanelText[rand];
        }
    }
}
