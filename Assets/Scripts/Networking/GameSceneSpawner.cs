using System.Collections.Generic;
using FishNet.Connection;
using FishNet.Managing;
using FishNet.Managing.Scened;
using FishNet.Object;
using UnityEngine;

public class GameSceneSpawner : NetworkBehaviour
{
    [SerializeField]
    private GameObject characterPrefab;

    private readonly HashSet<NetworkConnection> spawned = new();

    public override void OnStartServer()
    {
        base.OnStartServer();
        SceneManager.OnClientPresenceChangeEnd += OnClientReadyInScene;
    }

    private void OnClientReadyInScene(ClientPresenceChangeEventArgs args)
    {
        if (!IsServerInitialized || spawned.Contains(args.Connection) || !args.Added)
            return;

        spawned.Add(args.Connection);

        Vector3 spawnPos = GetSpawnPosition();
        GameObject character = Instantiate(characterPrefab, spawnPos, Quaternion.identity);
        Spawn(character, args.Connection);
    }

    private Vector3 GetSpawnPosition()
    {
        return new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
    }

    private void OnDestroy()
    {
        if (SceneManager != null)
            SceneManager.OnClientPresenceChangeEnd -= OnClientReadyInScene;
    }
}
