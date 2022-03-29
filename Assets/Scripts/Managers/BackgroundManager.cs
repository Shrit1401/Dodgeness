using System;
using Shop;
using UnityEngine;

namespace Managers
{
    public class BackgroundManager : MonoBehaviour
    {
        public Background background;
        public int backgroundSelectedOptions;
        private SpriteRenderer _spriteRenderer;
        
        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            
            if (!PlayerPrefs.HasKey("backgroundOption"))
            {
                backgroundSelectedOptions = 0;
            }

            else
            {
                Load();
            }
            
            UpdateBackground(backgroundSelectedOptions);
        }

        private void Update()
        {
            if (!PlayerPrefs.HasKey("backgroundOption"))
            {
                backgroundSelectedOptions = 0;
            }

            else
            {
                Load();
            }
            UpdateBackground(backgroundSelectedOptions);
        }

        private void UpdateBackground(int selectedOptions)
        {
            var storedBackground =
                background.GetBackground(selectedOptions);
            _spriteRenderer.sprite = storedBackground.backgroundSprite;
            _spriteRenderer.color = storedBackground.color;
        }
        
        private void Load()
        {
            backgroundSelectedOptions = PlayerPrefs.GetInt("backgroundOption");
        }
    }
}
