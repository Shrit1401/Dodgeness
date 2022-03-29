using System;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Blocks
{
    public class BlockSpawner : MonoBehaviour
    {
        public Transform[] spawnPoints;
        public GameObject redPrefab;
        public GameObject coinPrefab;

        public float timeBetweenSpawn = 1f;
        private float _timeToSpawn = 2f;

        public bool disableSpawn;
        
        private void Update()
        {
            if (!(Time.time >= _timeToSpawn) || GameManager.Instance.gameOver.isGameOver || disableSpawn) return;
            SpawnBlocks();
            _timeToSpawn = Time.time + timeBetweenSpawn;

        }

        private void SpawnBlocks()
        {
            var randomIndex = Random.Range(0, spawnPoints.Length);

            for (var i = 0; i < spawnPoints.Length; i++)
            {
                if (randomIndex != i)
                {
                    Instantiate(redPrefab, spawnPoints[i].position, Quaternion.identity);
                }

                if (randomIndex == i)
                {
                    Instantiate(coinPrefab, spawnPoints[i].position, Quaternion.identity);
 
                }
            }
        }
    }
}
