using System;
using UnityEngine;

namespace Managers
{
    [System.Serializable]
    public class BlockShifting
    {
        public GameObject homeBlock, gameModes;
        public bool isHome, isGameMode;
    }
    
    public class HomeScreenManager : MonoBehaviour
    {
        public BlockShifting blockShifting;

        public void ShowGameMode()
        {
            if (!blockShifting.isHome) return;
            blockShifting.gameModes.SetActive(true);
            blockShifting.homeBlock.SetActive(false);
            blockShifting.homeBlock.GetComponent<Animator>().SetTrigger($"close");
            
            blockShifting.isHome = false;
            blockShifting.isGameMode = true;
        }
        
        public void ShowHome()
        {
            if (!blockShifting.isGameMode) return;
            blockShifting.gameModes.SetActive(false);
            blockShifting.homeBlock.SetActive(true);
            blockShifting.gameModes.GetComponent<Animator>().SetTrigger($"close");

            blockShifting.isHome = true;
            blockShifting.isGameMode = false;
        }
    }
}
