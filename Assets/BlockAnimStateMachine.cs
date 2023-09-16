using System;
using System.Collections;
using System.Collections.Generic;
using NetworkCharacter;
using UnityEngine;
using UnityEngine.Serialization;

public class BlockAnimStateMachine : StateMachineBehaviour
{
    [FormerlySerializedAs("networkCharacterPropManager")] [SerializeField]
    NetworkCharacterPropHandler networkCharacterPropHandler = null;
    private void OnEnable()
    {

    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (networkCharacterPropHandler == null)
        {
            networkCharacterPropHandler = animator.GetComponentInParent<NetworkCharacterPropHandler>();
        }
        networkCharacterPropHandler.StartBlock();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        networkCharacterPropHandler.EndBlock();
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
