using FishNet.Object;
using UnityEngine;

public class Test : NetworkBehaviour
{
    private static int counter = 0;

    private void Awake()
    {
        Debug.Log($"[{++counter}] Awake");
    }

    private void OnEnable()
    {
        Debug.Log($"[{++counter}] OnEnable");
    }

    public override void OnStartNetwork()
    {
        Debug.Log($"[{++counter}] OnStartNetwork");
    }

    public override void OnStartServer()
    {
        Debug.Log($"[{++counter}] OnStartServer");
    }

    public override void OnStartClient()
    {
        Debug.Log($"[{++counter}] OnStartClient");
    }

    private void Start()
    {
        Debug.Log($"[{++counter}] Start");
    }

    private void Update()
    {
        // Just log once
        if (counter < 10)
            Debug.Log($"[{++counter}] Update");
    }

    private void LateUpdate()
    {
        if (counter < 10)
            Debug.Log($"[{++counter}] LateUpdate");
    }

    private void OnDrawGizmos()
    {
        if (counter < 10)
            Debug.Log($"[{++counter}] OnDrawGizmos");
    }
}
