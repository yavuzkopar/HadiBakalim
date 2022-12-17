using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SittingBehav : StateMachineBehaviour
{
    float lerpValue;
    public float lerpSpeed;
    Transform targetPos;
    InputHandler InputHandler;
    Transform fPos;
    
    float timer;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        fPos = animator.transform;
        targetPos = TPoint.singleton.activeObject.transform;
        InputHandler = animator.GetComponent<InputHandler>();
        animator.GetComponent<CapsuleCollider>().isTrigger = true;
        animator.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        lerpValue = 0;
        timer = 0f;
        animator.ResetTrigger("idle");
        
       
    }
    

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.position = Vector3.Lerp(fPos.position,targetPos.position,lerpValue);
        animator.transform.rotation = Quaternion.Lerp(fPos.rotation,targetPos.rotation,lerpValue);
        
        
        lerpValue += Time.deltaTime * lerpSpeed;
     //  animator.transform.position = targetPos.position;
     //   animator.transform.rotation = targetPos.rotation;
        timer += Time.deltaTime;
        if (InputHandler.moveAmount >= 1 && timer >= 2f)
        {
            animator.SetTrigger("idle");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        InputHandler.Invoked();
        timer = 0;
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
