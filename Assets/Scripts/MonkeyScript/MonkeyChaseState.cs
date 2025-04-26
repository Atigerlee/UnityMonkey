using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonkeyChaseState : StateMachineBehaviour
{
    public float stopChasingRange = 15f;
    public float chaseSpeed = 6f;

    public float AttackRange = 1.5f;

    NavMeshAgent agent;

    Transform player;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       player = GameObject.FindGameObjectWithTag("Player").transform;
       agent = animator.GetComponent<NavMeshAgent>();
       agent.speed = chaseSpeed;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       agent.SetDestination(player.position);
       float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);
       if(distanceFromPlayer >= stopChasingRange)
       {
        animator.SetBool("isChasing", false);
       }

       if(distanceFromPlayer <= AttackRange){
        animator.SetBool("isAttacking", true);
       }

    }
 
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       agent.SetDestination(animator.transform.position);
    }
}
