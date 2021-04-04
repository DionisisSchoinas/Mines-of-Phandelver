using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EngagedBehaviour : StateMachineBehaviour
{
    private EnemyAi_V2 aiScript;
  
    float timer;
 
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        aiScript = animator.gameObject.GetComponent<EnemyAi_V2>();
        timer = 0;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        aiScript.RotateTowards(aiScript.target.position);
        timer += Time.deltaTime;
        if (Vector3.Distance(aiScript.target.transform.position, animator.transform.position) > aiScript.attackRadius + 3)//+1 is to offset the rapid Changes
        {
            animator.SetBool("Engage", false);
        }
        else
        {
            if (timer > Random.Range(1f,2f))
            {
                int behaviourIndex = Random.Range(1, 5);
                if (behaviourIndex < 3) {
                    animator.SetBool("Relocate", true);
                }
                else
                {
                    animator.SetBool("GetInRange", true);
                }
            }           
        }     
        // aiScript.Relocate();
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
