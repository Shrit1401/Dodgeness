using UnityEngine;

namespace Shop
{
    [System.Serializable]
    public class StoredBackground
    {
        public string name;
        public Sprite backgroundSprite;
        public int amount;
        public Color color;
        public bool isUnlocked;
    }
    
    [CreateAssetMenu]
    public class Background : ScriptableObject
    {
        public StoredBackground[] backgrounds;

        public int BackgroundCount => backgrounds.Length;

        public StoredBackground GetBackground(int index)
        {
            return backgrounds[index];  
        }
    }
}
