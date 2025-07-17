using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class PlayerSaveSystem
{
    private string SaveFolder => Application.persistentDataPath;

    public void Save(PlayerSaveData data)
    {
        if (string.IsNullOrEmpty(data.saveId))
            data.saveId = Guid.NewGuid().ToString();

        data.timestampLastLoad = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(GetFilePath(data.saveId), json);
    }

    public PlayerSaveData CreateNewSave()
    {
        PlayerSaveData newSave = PlayerSaveData.CreateDefault();
        Save(newSave);
        return newSave;
    }

    public IEnumerable<PlayerSaveData> LoadAll()
    {
        if (!Directory.Exists(SaveFolder))
            Directory.CreateDirectory(SaveFolder);

        foreach (var file in Directory.GetFiles(SaveFolder, "*.json"))
        {
            string json = File.ReadAllText(file);
            yield return JsonUtility.FromJson<PlayerSaveData>(json);
        }
    }

    public IEnumerable<PlayerSaveData> LoadAllOrdered() =>
        LoadAll().OrderByDescending(s => s.timestampLastLoad);

    private string GetFilePath(string saveId) => Path.Combine(SaveFolder, $"{saveId}.json");
}
