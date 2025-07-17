using UnityEngine;

[CreateAssetMenu(fileName = "InventoryConfig", menuName = "Inventory/Config")]
public class InventoryConfig : ScriptableObject
{
    public int columns = 5;
    public int rows = 4;

    [Header("UI")]
    public int cellSize = 100;
    public int cellMargin = 10;

    public int MaxSlots => columns * rows;
}
