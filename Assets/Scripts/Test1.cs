using System.Collections;
using FishNet.Object;
using UnityEngine;

namespace ParentingProblems
{
    public class SpawnAsChild : NetworkBehaviour
    {
        // An empty prefab with only a NetworkObject.
        [SerializeField]
        NetworkObject prefabWithNOB;

        // A prefab with only a NetworkObject and NetworkTransform set to "SyncParents"
        [SerializeField]
        NetworkObject prefabWithNT;

        public override void OnStartServer()
        {
            StartCoroutine(nameof(SpawnChildren));
        }

        IEnumerator SpawnChildren()
        {
            yield return new WaitUntil(() => this.IsSpawned);
            SpawnAndParentObjWithNT();
        }

        private void SpawnAndParentObjWithNT()
        {
            var obj = Instantiate(prefabWithNT);
            Spawn(obj);
            obj.SetParent(this.GetComponent<NetworkObject>());
        }
    }
}
