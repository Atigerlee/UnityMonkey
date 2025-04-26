using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonkeyWalkState : StateMachineBehaviour
{
    float timer;
    
    public float walkingTime = 10f;
    public float detectArea = 10f;

    public float walkSpeed = 2;

    Transform player;

    NavMeshAgent agent;

    List<Transform> wayPointsList = new List<Transform>();
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0f;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();

        GameObject wayPointsCluster = animator.GetComponent<MonsterWayPoints>().wayPointsCluster;
        foreach(Transform t in wayPointsCluster.transform)
        {
            wayPointsList.Add(t);
        }

        Vector3 firstPoint = wayPointsList[Random.Range(0, wayPointsList.Count)].position;

        agent.speed = walkSpeed;
        agent.SetDestination(firstPoint);
    }

    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //走到定點，換下個點
        if(agent.remainingDistance <= agent.stoppingDistance) //用小於會怪怪的
        {
            Vector3 nextPoint = wayPointsList[Random.Range(0, wayPointsList.Count)].position;
            agent.SetDestination(nextPoint);
        }
        
        //走太久回到idle
       timer += Time.deltaTime;
       if(walkingTime < timer)
       {
        animator.SetBool("isWalking", false);
       }

       //to Chasing state
       float distanceFromPlayer = Vector3.Distance(animator.transform.position, player.position);
       if(distanceFromPlayer < detectArea)
       {
        animator.SetBool("isChasing", true);
       }
    }

    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       agent.SetDestination(animator.transform.position);
    }

}
