using System;
using System.Collections;
using Player;
using Coins;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    [System.Serializable]
    public class GameOver
    {
        [Header("Texts")]
        public bool isGameOver;
        public Text score;
        public Text highScore;
        public Text coins;
        
        
        [Header("values")]
        public int secToWaitToShowBtns = 5;
        public float increaseRate = 5f;
        public float increaseRateMultiplayerAfterReward = 0.5f;
        public int vulnerablePeriod = 4;
        public int timesDestroyRewardButton = 5;
        
        [Header("References")]
        public GameObject gameOverScene;
        public GameObject[] visibleAfterSometime;
        public GameObject[] objectToDestroyAfterGameOver;
        public GameObject rewardBtn;
    }

    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public GameOver gameOver;


        private float _realIncreaseRate;
        private int _timesRewardButtonClicked;
        private Player.Player _player;
        
        private void Awake()
        { 
            if (Instance == null)
                Instance = this;
            else
            {
                Destroy(gameObject);
                return;
            }
            //game over
            gameOver.isGameOver = false;
            _realIncreaseRate = gameOver.increaseRate;
        }
        
        private void Start()
        {
            _player = FindObjectOfType<Player.Player>();
        }
        
        private void Update()
        {
            if (_timesRewardButtonClicked == gameOver.timesDestroyRewardButton)
            {
                Destroy(gameOver.rewardBtn);
            }
        }

        
        public void GameOver()
        {
            _timesRewardButtonClicked++;
            gameOver.isGameOver = true;
            gameOver.gameOverScene.SetActive(true);
            _player.ResetControl();
            var scoreCounter = FindObjectOfType<ScoreCounter>();
            gameOver.score.text = "Score: " + scoreCounter.score;
            gameOver.coins.text = "Coins: " + PlayerPrefs.GetInt("coinNumber", 0);
            StartCoroutine(WaitAfterVisible());

            gameOver.highScore.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0);
            
            foreach (var perObject in gameOver.objectToDestroyAfterGameOver)
            {
                perObject.SetActive(false);
                _player.disableMovement = true;
            }
        }

        public void RewardBtn()
        {
            gameOver.isGameOver = false;
            gameOver.gameOverScene.SetActive(false);
            gameOver.increaseRate = _realIncreaseRate + gameOver.increaseRateMultiplayerAfterReward;
            StartCoroutine(NotVulnerable());
            foreach (var perObject in gameOver.objectToDestroyAfterGameOver)
            {
                perObject.SetActive(true);
                _player.disableMovement = false;
            }
        }
        
        private IEnumerator NotVulnerable()
        {
            _player.disableMovement = true;
            _player.disableColliders = true;
            yield return new WaitForSeconds(gameOver.vulnerablePeriod);
            _player.disableMovement = false;
            _player.disableColliders = false;
        }
        
        private IEnumerator WaitAfterVisible()
        {
            yield return new WaitForSeconds(gameOver.secToWaitToShowBtns);
            foreach (var btn in gameOver.visibleAfterSometime)
            {
                btn.SetActive(true);
            }
        }
    }
}
