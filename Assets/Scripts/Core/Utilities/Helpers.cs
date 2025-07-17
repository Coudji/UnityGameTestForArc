using UnityEngine;

namespace Arc.Core.Utilities
{
    public static class Helpers
    {
        public static void DestroyChildren(this Transform t)
        {
            foreach (Transform child in t)
                Object.Destroy(child.gameObject);
        }
    }
}
