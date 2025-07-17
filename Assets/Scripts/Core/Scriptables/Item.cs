using FishNet.Object;
using UnityEngine;

namespace Arc.Core.Scriptables
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Item")]
    public class Item : ScriptableObject
    {
        [Header("Identification")]
        public int Id;
    
        [Header("UI")]
        public Sprite Icon;
        public string DisplayName;
    
        [TextArea]
        public string Description;
    
        [Header("Gameplay")]
        public ItemCategory Category;
        public ItemSubCategory SubCategory;
    
        [Min(1)]
        public int MaxStack = 1;
    
        [Header("World Representation")]
        public NetworkObject WorldModelPrefab;
    }
}
