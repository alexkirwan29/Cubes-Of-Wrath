using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class ProtoMenuCamera : MonoBehaviour
{
    public int size = 25;
    public float dist = 2f;

    Vector3 targetPos;

    NavMeshAgent agent;

    void Start()
    {
        targetPos = new Vector3(Random.Range(-size, size), 1, Random.Range(-size, size));
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(targetPos);
    }

    void Update()
    {
        if(agent.remainingDistance < dist && !agent.pathPending)
        {
            targetPos = new Vector3(Random.Range(-size, size), 1, Random.Range(-size, size));
            agent.SetDestination(targetPos);
        }
    }
}