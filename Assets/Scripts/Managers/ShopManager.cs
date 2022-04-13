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

        public GameObject redPanel, greenPanel;
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
    
    [Serializable]
    public class SkinShop
    {
        public Skin skin;
        public Image image;
        public Text amount;
        public Text name;
        public Text currentMoney;
        public int skinSelectedOption;
        public int amountValue;
        public Button select;
        public bool canBuy;
        public bool isUnlocked;
    }
    
    public class ShopManager : MonoBehaviour    
    {
        public ShopUISetting shopUISetting;
        public BackgroundShop background;
        public SkinShop skin;


        private void Start()
        {
            if (!PlayerPrefs.HasKey("backgroundOption"))
            {
                background.backgroundSelectedOptions = 0;
            }

            else
            {
                BackgroundLoad();
            }
            
            UpdateBackground(background.backgroundSelectedOptions);
            
            if (!PlayerPrefs.HasKey("skinOptions"))
            {
                skin.skinSelectedOption = 0;
            }

            else
            {
                SkinLoad();
            }
            
            UpdateSkin(skin.skinSelectedOption);
        }

        private void Update()
        {
            background.currentMoney.text = "$" + PlayerPrefs.GetInt("coinNumber");
            skin.currentMoney.text = "$" + PlayerPrefs.GetInt("coinNumber");

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
        
        public void BackgroundBtnClicked()
        {
            if (!shopUISetting.isSkinActive) return;
            shopUISetting.skinWay.SetActive(false);
            shopUISetting.backgroundWay.SetActive(true);
            shopUISetting.isSkinActive = false;
            shopUISetting.isBackgroundActive = true;
        }
        
        
        //shop Background

        public void BackgroundNextOption()
        {
            background.backgroundSelectedOptions++;

            if (background.backgroundSelectedOptions >= background.background.BackgroundCount)
            {
                background.backgroundSelectedOptions = 0;
            }
            UpdateBackground(background.backgroundSelectedOptions);
        }
        
        public void BackgroundBackOption()
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

        }

        public void BackgroundSelectOption()
        {
            BackgroundSave();
        }
        
        private void BackgroundLoad()
        {
            background.backgroundSelectedOptions = PlayerPrefs.GetInt("backgroundOption");
        }

        private void BackgroundSave()
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
                    shopUISetting.greenPanel.GetComponent<Animator>().SetTrigger($"open");
                }

                else
                {
                    shopUISetting.redPanel.GetComponent<Animator>().SetTrigger($"open");
                }
            }

            else
            {
                shopUISetting.greenPanel.GetComponent<Animator>().SetTrigger($"open");
                PlayerPrefs.SetInt("backgroundOption", background.backgroundSelectedOptions);   
            }
        }
        
        // shop Skin
        
        public void SkinNextOption()
        {
            skin.skinSelectedOption++;

            if (skin.skinSelectedOption >= skin.skin.SkinCount)
            {
                skin.skinSelectedOption = 0;
            }
            UpdateSkin(skin.skinSelectedOption);
        }
        
        public void SkinBackOption()
        {
            skin.skinSelectedOption--;

            if (skin.skinSelectedOption < 0)
            {
                skin.skinSelectedOption = skin.skin.SkinCount - 1;
            }
            UpdateSkin(skin.skinSelectedOption);
        }
        
        private void UpdateSkin(int selectedOptions)
        {

            var storedSkin =
                skin.skin.GetSkin(selectedOptions);
            
            skin.image.sprite = storedSkin.playerSprite;

            skin.amountValue = storedSkin.amount;
            if (storedSkin.amount == 0)
            {
                skin.amount.text = "free";
            }

            else
            {
                skin.amount.text = "$" + storedSkin.amount;

            }
            skin.name.text = storedSkin.name;
            skin.amountValue = storedSkin.amount;

            skin.canBuy = PlayerPrefs.GetInt("coinNumber") >= storedSkin.amount;
            skin.isUnlocked = storedSkin.isUnlocked;

        }
        
        
        public void SkinSelectOption()
        {
            SkinSave();
        }
        
        private void SkinLoad()
        {
            skin.skinSelectedOption = PlayerPrefs.GetInt("skinOptions");
        }
        
        private void SkinSave()
        {
            if (!skin.isUnlocked)
            {
                if (skin.canBuy)
                {
                    var rnCoin = PlayerPrefs.GetInt("coinNumber");
                    var coinMoney = rnCoin - skin.amountValue;
                    PlayerPrefs.SetInt("coinNumber", coinMoney);
                    var storedSkin =
                        skin.skin.GetSkin(skin.skinSelectedOption);
                    skin.isUnlocked = true;
                    storedSkin.isUnlocked = skin.isUnlocked;
                    PlayerPrefs.SetInt("skinOptions", skin.skinSelectedOption);
                    shopUISetting.greenPanel.GetComponent<Animator>().SetTrigger($"open");
                }

                else
                {
                    shopUISetting.redPanel.GetComponent<Animator>().SetTrigger($"open");
                }
            }

            else
            {
                shopUISetting.greenPanel.GetComponent<Animator>().SetTrigger($"open");
                PlayerPrefs.SetInt("skinOptions", skin.skinSelectedOption);   
            }
        }
        
    }
}
