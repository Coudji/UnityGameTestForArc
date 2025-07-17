using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Arc.Gameplay.Units.Enemy
{
    public class EnemyAI : MonoBehaviour
    {
        public string playerTag = "Player";
        public float updateRate = 0.3f;
        public float minDistanceChange = 0.5f;
        public float chaseRange = 15f;
        public float stopChaseBuffer = 2f;
    
        private NavMeshAgent agent;
        private Transform currentTarget;
        private Vector3 lastTargetPosition;
        private Coroutine followRoutine;
        private bool isChasing = false;
    
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            followRoutine = StartCoroutine(ChaseClosestPlayerLoop());
        }
    
        IEnumerator ChaseClosestPlayerLoop()
        {
            while (true)
            {
                // Rechercher tous les joueurs dans la scène
                GameObject[] players = GameObject.FindGameObjectsWithTag(playerTag);
    
                Transform closest = null;
                float minDistance = Mathf.Infinity;
    
                foreach (GameObject player in players)
                {
                    float dist = Vector3.Distance(transform.position, player.transform.position);
                    if (dist < minDistance)
                    {
                        minDistance = dist;
                        closest = player.transform;
                    }
                }
    
                // Mettre à jour la cible si un joueur est dans la portée
                if (closest != null && minDistance <= chaseRange)
                {
                    if (currentTarget != closest)
                    {
                        currentTarget = closest;
                        lastTargetPosition = currentTarget.position;
                        isChasing = true;
                        agent.SetDestination(lastTargetPosition);
                    }
                }
                else if (
                    isChasing
                    && (
                        currentTarget == null
                        || Vector3.Distance(transform.position, currentTarget.position)
                            > chaseRange + stopChaseBuffer
                    )
                )
                {
                    // Arrêter la poursuite
                    isChasing = false;
                    currentTarget = null;
                    agent.ResetPath();
                }
    
                if (isChasing && currentTarget != null)
                {
                    float delta = Vector3.Distance(lastTargetPosition, currentTarget.position);
                    if (delta >= minDistanceChange)
                    {
                        lastTargetPosition = currentTarget.position;
                        agent.SetDestination(lastTargetPosition);
                    }
                }
    
                yield return new WaitForSeconds(updateRate);
            }
        }
    
        void OnDisable()
        {
            if (followRoutine != null)
                StopCoroutine(followRoutine);
        }
    }
}
