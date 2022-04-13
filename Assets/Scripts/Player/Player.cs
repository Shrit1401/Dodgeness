using System;
using System.Collections;
using Managers;
using Shop;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        public float speed = 15f;
        private Rigidbody2D _rb;

        private float _moveTime, _moveDuration = 0.1f;
        
        public bool disableMovement;
        public bool disableColliders;
        
        private Vector2 _startTouchPosition, _endTouchPosition;
        private Vector3 _startTouchPositionSmooth, _endTouchPositionSmooth;
        
        public Skin skin;
        public int skinSelectedOption;
        private SpriteRenderer _spriteRenderer;
        
        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            
            _spriteRenderer = GetComponent<SpriteRenderer>();
            
            if (!PlayerPrefs.HasKey("backgroundOption"))
            {
                skinSelectedOption = 0;
            }

            else
            {
                Load(); 
            }
            
            UpdateSkin(skinSelectedOption);
        }

        private void Update()
        {
            if (!PlayerPrefs.HasKey("skinOptions"))
            {
                skinSelectedOption = 0;
            }

            else
            {
                Load();
            }
            UpdateSkin(skinSelectedOption);
            GameOverSettingPlayer();
        }

        private void GameOverSettingPlayer()
        {
            //making colliders work with try again btn(bool)
            Physics2D.IgnoreLayerCollision(3, 6, disableColliders);
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
                _startTouchPosition = Input.GetTouch(0).position;

            if (Input.touchCount <= 0 || Input.GetTouch(0).phase != TouchPhase.Ended) return;
            _endTouchPosition = Input.GetTouch(0).position;

            if ((_endTouchPosition.x < _startTouchPosition.x) && transform.position.x > -speed)
                StartCoroutine(TouchSmooth("left"));

            if ((_endTouchPosition.x > _startTouchPosition.x) && transform.position.x < speed)
                StartCoroutine(TouchSmooth("right"));
        }
        
        private void UpdateSkin(int selectedOptions)
        {

            var storedSkin = skin.GetSkin(selectedOptions);
            
            _spriteRenderer.sprite = storedSkin.playerSprite;
        }

        private IEnumerator TouchSmooth(string location)
        {
            GetComponent<Animator>().SetFloat($"moveSpeed", _moveTime);
            switch (location)
            {
                case "left":
                    _moveTime = 0;
                    var position = transform.position;
                    _startTouchPositionSmooth = position;
                    _endTouchPositionSmooth = new Vector3(_startTouchPositionSmooth.x - speed, position.y, position.z);

                    while (_moveTime < _moveDuration)
                    {
                        _moveTime += Time.deltaTime;
                        transform.position = Vector2.Lerp(_startTouchPositionSmooth, _endTouchPositionSmooth,
                            _moveTime / _moveDuration);
                        yield return null;
                    }

                    break;

                case "right":
                    _moveTime = 0;
                    var positionRight = transform.position;
                    _startTouchPositionSmooth = positionRight;
                    _endTouchPositionSmooth = new Vector3(_startTouchPositionSmooth.x + speed, positionRight.y,
                        positionRight.z);

                    while (_moveTime < _moveDuration)
                    {
                        _moveTime += Time.deltaTime;
                        transform.position = Vector2.Lerp(_startTouchPositionSmooth, _endTouchPositionSmooth,
                            _moveTime / _moveDuration);
                        yield return null;
                    }

                    break;
            }
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
        
        private void Load()
        {
            skinSelectedOption = PlayerPrefs.GetInt("skinOptions");
        }
    }
}
