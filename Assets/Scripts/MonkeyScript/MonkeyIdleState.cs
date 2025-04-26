using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonkeyIdleState : StateMachineBehaviour
{
    //我需要切換到走路跟追兩個狀態的判斷

    public float idleTime = 4f;

    float timer;

    public float detectArea = 10f;

    Transform player;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       timer = 0f;
       player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       timer += Time.deltaTime;
       if(timer > idleTime)
       {
        animator.SetBool("isWalking", true);
       }
       float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);
       if(distanceFromPlayer < detectArea)
       {
        animator.SetBool("isChasing", true);
       }
    }

}
