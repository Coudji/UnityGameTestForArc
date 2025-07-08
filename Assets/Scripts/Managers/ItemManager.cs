public class ItemManager : Singleton<ItemManager>
{
    public ItemDatabase ItemDatabase;

    protected override void Awake()
    {
        base.Awake();
        ItemDatabase.Init();
    }

    public static Item GetItem(int id) => Instance.ItemDatabase.GetItemById(id);
}
