using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class WavePointPatrol : MonoBehaviour
{

    private NavMeshAgent navMeshAgent;

    private int currentWaypointIndex;

    public Transform[] wayPoints;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(wayPoints[currentWaypointIndex].position);
    }

    // Update is called once per frame
    void Update()
    {
        if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % wayPoints.Length;
            navMeshAgent.SetDestination(wayPoints[currentWaypointIndex].position);
        }
    }
}
