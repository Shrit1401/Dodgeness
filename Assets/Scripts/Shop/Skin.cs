using UnityEngine;

namespace Shop
{
        [System.Serializable]
        public class StoredSkin
        {
            public string name;
            public Sprite playerSprite;
            public int amount;
            public bool isUnlocked;
        }
    
        [CreateAssetMenu]
        public class Skin : ScriptableObject
        {
            public StoredSkin[] skins;

            public int SkinCount => skins.Length;
                
            public StoredSkin GetSkin(int index)
            {
                return skins[index];  
            }
        }
}
