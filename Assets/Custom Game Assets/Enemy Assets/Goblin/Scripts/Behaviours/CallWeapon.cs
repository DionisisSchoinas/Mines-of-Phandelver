using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallWeapon : StateMachineBehaviour
{

    private GameObject CurrentGameObject;
    private EnemyAi_V2 aiScript;
    private Transform target;
    private BossFightAi bossFightAi;
    public float apearDelayTimer;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CurrentGameObject = animator.gameObject;
        aiScript = CurrentGameObject.GetComponent<EnemyAi_V2>();
        bossFightAi = CurrentGameObject.GetComponent<BossFightAi>();
        target = aiScript.target.transform;
        bossFightAi.WeaponApear(apearDelayTimer);
        animator.SetBool("HasWeapon", true);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        aiScript.rotateTowards(target.position);
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
