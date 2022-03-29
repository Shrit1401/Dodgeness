using System;
using Managers;
using UnityEngine;

namespace Blocks
{
    public class Block : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<Rigidbody2D>().gravityScale += Time.timeSinceLevelLoad / FindObjectOfType<GameManager>().gameOver.increaseRate;
        }

        private void Update()
        {
            if (transform.position.y < -13f)
            {
                Destroy(gameObject);
            }
        }
    }
}
