using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;

namespace Coins
{
    public class ScoreCounter : MonoBehaviour
    {
        public Text scoreText;
        [HideInInspector]
        public int score;
        
        [HideInInspector]
        public int coinNumber;

        private void Awake()
        {
            coinNumber = PlayerPrefs.GetInt("coinNumber");
        }

        private void Update()
        {
            if (score < 10)
            {
                scoreText.text = "0" + score;
            }
            else
            {
                scoreText.text = score.ToString();
            }

            if (score > PlayerPrefs.GetInt("HighScore", 0))
            {
                PlayerPrefs.SetInt("HighScore", score); 
            }
            
            PlayerPrefs.SetInt("coinNumber", coinNumber);

            if (coinNumber < 0)
            {
                coinNumber = 0;
            }
        }

        public void AddScore()
        {
            StartCoroutine(ScorePopup());
            scoreText.text = score.ToString();
            coinNumber++;
        }

        private IEnumerator ScorePopup()
        {
            for (var i = 1f; i <= 1.2f; i += 0.05f)
            {
                scoreText.rectTransform.localScale = new Vector3(i, i, i);
                yield return new WaitForEndOfFrame();
            }

            scoreText.rectTransform.localScale += new Vector3(1.2f, 1.2f, 1.2f);
            score++;

            for (var i = 1.2f; i >= 1f; i -= 0.05f)
            {
                scoreText.rectTransform.localScale = new Vector3(i, i, i);
                yield return new WaitForEndOfFrame();
            }
            scoreText.rectTransform.localScale = new Vector3(1, 1, 1);
        }
    }
}
