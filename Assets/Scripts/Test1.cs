using FishNet.Object;
using UnityEngine;

public class Test1 : NetworkBehaviour
{
    public override void OnStartClient()
    {
        Debug.Log("Je suis un client");
    }
}
