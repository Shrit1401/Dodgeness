using Managers;
using UnityEngine;

namespace Coins
{
   public class Coin : MonoBehaviour
   {

      private void Start()
      {
         GetComponent<Rigidbody2D>().gravityScale += Time.timeSinceLevelLoad / FindObjectOfType<GameManager>().gameOver.increaseRate;
      }
      
      private void OnTriggerEnter2D(Collider2D col)
      {
         if (!col.CompareTag("Player")) return;
         FindObjectOfType<ScoreCounter>().AddScore();
         Destroy(gameObject);
      }
   }
}
