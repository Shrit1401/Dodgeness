using System;
using Shop;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    [Serializable]
    public class ShopUISetting
    {
        public GameObject skinBtn, backgroundBtn;
        public GameObject skinWay, backgroundWay;

        public bool isSkinActive, isBackgroundActive;
    }

    [Serializable]
    public class BackgroundShop
    {
        public Background background;
        public Image image;
        public Text amount;
        public Text name;
        public Text currentMoney;
        public int backgroundSelectedOptions;
        public int amountValue;
        public Button select;
        public bool canBuy;
        public bool isUnlocked;
    }
    
    public class ShopManager : MonoBehaviour    
    {
        public ShopUISetting shopUISetting;
        public BackgroundShop background;


        private void Start()
        {
            if (!PlayerPrefs.HasKey("backgroundOption"))
            {
                background.backgroundSelectedOptions = 0;
            }

            else
            {
                Load();
            }
            
            UpdateBackground(background.backgroundSelectedOptions);
        }

        private void Update()
        {
            background.currentMoney.text = "$" + PlayerPrefs.GetInt("coinNumber");
            
            if (shopUISetting.isSkinActive)
            {
                shopUISetting.backgroundBtn.GetComponent<CanvasGroup>().alpha = 0.5f;
                shopUISetting.skinBtn.GetComponent<CanvasGroup>().alpha = 1f;
            }
            
            else if (shopUISetting.isBackgroundActive)
            {
                shopUISetting.skinBtn.GetComponent<CanvasGroup>().alpha = 0.5f;
                shopUISetting.backgroundBtn.GetComponent<CanvasGroup>().alpha = 1f;
            }
        }

        public void SkinBtnClicked()
        {
            if (!shopUISetting.isBackgroundActive) return;
            shopUISetting.skinWay.SetActive(true);
            shopUISetting.backgroundWay.SetActive(false);
            shopUISetting.isBackgroundActive = false;
            shopUISetting.isSkinActive = true;
        }
        
        public void ButtonBtnClicked()
        {
            if (!shopUISetting.isSkinActive) return;
            shopUISetting.skinWay.SetActive(false);
            shopUISetting.backgroundWay.SetActive(true);
            shopUISetting.isSkinActive = false;
            shopUISetting.isBackgroundActive = true;
        }
        
        
        //shop

        public void NextOption()
        {
            background.backgroundSelectedOptions++;

            if (background.backgroundSelectedOptions >= background.background.BackgroundCount)
            {
                background.backgroundSelectedOptions = 0;
            }
            UpdateBackground(background.backgroundSelectedOptions);
        }
        
        public void BackOption()
        {
            background.backgroundSelectedOptions--;

            if (background.backgroundSelectedOptions < 0)
            {
                background.backgroundSelectedOptions = background.background.BackgroundCount - 1;
            }
            UpdateBackground(background.backgroundSelectedOptions);
        }

        private void UpdateBackground(int selectedOptions)
        {

            var storedBackground =
                background.background.GetBackground(selectedOptions);
            
            background.image.sprite = storedBackground.backgroundSprite;

            background.amountValue = storedBackground.amount;
            if (storedBackground.amount == 0)
            {
                background.amount.text = "free";
            }

            else
            {
                background.amount.text = "$" + storedBackground.amount;

            }
            background.name.text = storedBackground.name;
            background.amountValue = storedBackground.amount;

            background.canBuy = PlayerPrefs.GetInt("coinNumber") >= storedBackground.amount;
            background.isUnlocked = storedBackground.isUnlocked;

            background.@select.interactable = background.isUnlocked || background.canBuy;
        }

        public void SelectOption()
        {
            Save();
        }
        
        private void Load()
        {
            background.backgroundSelectedOptions = PlayerPrefs.GetInt("backgroundOption");
        }

        private void Save()
        {
            if (!background.isUnlocked)
            {
                if (background.canBuy)
                {
                    var rnCoin = PlayerPrefs.GetInt("coinNumber");
                    var coinMoney = rnCoin - background.amountValue;
                    PlayerPrefs.SetInt("coinNumber", coinMoney);
                    var storedBackground =
                        background.background.GetBackground(background.backgroundSelectedOptions);
                    background.isUnlocked = true;
                    storedBackground.isUnlocked = background.isUnlocked;
                    PlayerPrefs.SetInt("backgroundOption", background.backgroundSelectedOptions);
                }

                else
                {
                    print("not enough money");
                }
            }

            else
            {
                PlayerPrefs.SetInt("backgroundOption", background.backgroundSelectedOptions);   
            }
        }
    }
}
