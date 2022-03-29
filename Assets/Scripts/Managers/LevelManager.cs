using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        public Animator transition;
        public float transitionTime = 1f;

        public void LoadLevel(string levelName)
        {
            StartCoroutine(LoadLevelString(levelName));
        }

        private IEnumerator LoadLevelString(string levelName)
        {
            transition.SetTrigger($"start");

            yield return new WaitForSeconds(transitionTime);

            SceneManager.LoadScene(levelName);
        }

        public void LoadSameLevel()
        {
            StartCoroutine(LoadLevelAgain());
        }
        
        private IEnumerator LoadLevelAgain()
        {
            transition.SetTrigger($"start");

            yield return new WaitForSeconds(transitionTime);

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
