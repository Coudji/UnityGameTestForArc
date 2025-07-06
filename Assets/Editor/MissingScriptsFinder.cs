using UnityEditor;
using UnityEngine;

public class MissingScriptsFinder : EditorWindow
{
    [MenuItem("Tools/Find Missing Scripts")]
    public static void FindMissingScripts()
    {
        GameObject[] allGOs = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        int missingCount = 0;

        foreach (GameObject go in allGOs)
        {
            Component[] components = go.GetComponents<Component>();

            for (int i = 0; i < components.Length; i++)
            {
                if (components[i] == null)
                {
                    Debug.LogWarning($"[Missing Script] {go.name} in scene: {go.scene.name}", go);
                    missingCount++;
                }
            }
        }

        Debug.Log($"Recherche terminée. {missingCount} composants manquants trouvés.");
    }
}
