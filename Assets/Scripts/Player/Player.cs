using System;
using Managers;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        public float speed = 15f;
        private Rigidbody2D _rb;

        public bool disableMovement;
        public bool disableColliders;
        

        private Vector2 _startTouchPos, _endTouchPos;
        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            GameOverSettingPlayer();
        }

        private void GameOverSettingPlayer()
        {
            //making colliders work with try again btn(bool)
            Physics2D.IgnoreLayerCollision(3, 6, disableColliders);
            GetComponent<Animator>().SetBool($"isTryAgain", disableColliders);
        }

        private void FixedUpdate()
        {
            if(disableMovement) return;
            //PcMovement();
            TouchMethod();
        }

        private void TouchMethod()
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                _startTouchPos = Input.GetTouch(0).position;
            }

            if (Input.touchCount > 00 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                _endTouchPos = Input.GetTouch(0).position;
            }

            if (_endTouchPos.x < _startTouchPos.x)
            {
                MoveRight();
            }

            if (_endTouchPos.x > _startTouchPos.x)
            {
                MoveLeft();
            }
        }

        private void MoveLeft()
        {
            var transform1 = transform;
            var position = transform1.position;
            position = new Vector3(position.x + speed, position.y);
            transform1.position = position;
        }
        
        private void MoveRight()
        {
            var transform1 = transform;
            var position = transform1.position;
            position = new Vector3(position.x - speed, position.y);
            transform1.position = position;
        }

        public void ResetControl()
        {
            var transform1 = transform;
            var position = transform1.position;
            position = new Vector3(0, position.y);
            transform1.position = position;
        }

        private void PcMovement()
        {
            var x = Input.GetAxisRaw("Horizontal") * Time.fixedDeltaTime * speed;
            var newPos = _rb.position + Vector2.right * x;
            _rb.MovePosition(newPos);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (!col.collider.CompareTag("block")) return;
            GameManager.Instance.GameOver();
        }
    }
}
