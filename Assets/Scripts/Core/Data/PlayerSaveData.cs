using System;

[Serializable]
public class PlayerSaveData
{
    public string saveId;
    public string characterName;
    public int level;
    public string classType;
    public string[] equippedItems;
    public long timestampLastLoad;

    public string GetDisplayName() => $"{characterName} (Lvl {level})";

    public static PlayerSaveData CreateDefault()
    {
        return new PlayerSaveData
        {
            saveId = Guid.NewGuid().ToString(),
            characterName = "NewPlayer_" + UnityEngine.Random.Range(1000, 9999),
            level = 1,
            classType = "Warrior",
            equippedItems = Array.Empty<string>(),
            timestampLastLoad = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
        };
    }
}
