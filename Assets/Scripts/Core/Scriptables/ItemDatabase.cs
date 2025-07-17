using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Database/Item Database")]
public class ItemDatabase : ScriptableObject
{
    public List<Item> items;

    private Dictionary<int, Item> itemLookup;

    public void Init()
    {
        itemLookup = items.ToDictionary(item => item.Id, item => item);
    }

    public Item GetItemById(int id)
    {
        if (itemLookup == null)
            Init();

        itemLookup.TryGetValue(id, out var item);

        return item;
    }
}
