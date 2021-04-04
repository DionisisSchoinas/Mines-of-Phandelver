using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRanged : StateMachineBehaviour
{
    private EnemyAi_V2 aiScript;

    float timer;
    public float weaponDissapearDelay;
    //Seconds until the animation locks an no longer folows the target
    public float attackLockDelay;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        aiScript = animator.gameObject.GetComponent<EnemyAi_V2>();
        aiScript.AttackRanged();
        animator.SetBool("HasWeapon", false);
        timer = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        if (timer < attackLockDelay)
        {
            aiScript.rotateTowards(aiScript.target.position);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
