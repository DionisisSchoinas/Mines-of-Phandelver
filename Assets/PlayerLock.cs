using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLock : StateMachineBehaviour
{
    private PlayerMovementScriptWarrior controls;
    private MeleeController meleeController;

    private void Awake()
    {
        controls = FindObjectOfType<PlayerMovementScriptWarrior>();

        meleeController = controls.gameObject.GetComponent<MeleeController>();
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.IsName("Combo 1"))
            meleeController.Attack(0);
        else if (stateInfo.IsName("Combo 2"))
            meleeController.Attack(1);
        else if (stateInfo.IsName("Combo 3"))
            meleeController.Attack(2);

        controls.LockPlayer(0.5f);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        meleeController.IsSwinging(false);
    }

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
