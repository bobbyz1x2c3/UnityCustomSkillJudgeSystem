using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixTransformError : MonoBehaviour
{
    private Animator _animator;

    /*public bool isAnimationCombo
    {
        get
        {
            return (_animator.GetCurrentAnimatorStateInfo(Shortcuts.AnimationKeys.ATTACK_LAYER)
                        .IsName(Shortcuts.AnimationKeys.COMBO_1) ||
                    _animator.GetCurrentAnimatorStateInfo(Shortcuts.AnimationKeys.ATTACK_LAYER)
                        .IsName(Shortcuts.AnimationKeys.COMBO_2) ||
                    _animator.GetCurrentAnimatorStateInfo(Shortcuts.AnimationKeys.ATTACK_LAYER)
                        .IsName(Shortcuts.AnimationKeys.COMBO_3) ||
                    _animator.GetCurrentAnimatorStateInfo(Shortcuts.AnimationKeys.ATTACK_LAYER)
                        .IsName(Shortcuts.AnimationKeys.COMBO_4) ||
                    _animator.GetCurrentAnimatorStateInfo(Shortcuts.AnimationKeys.ATTACK_LAYER)
                        .IsName(Shortcuts.AnimationKeys.COMBO_5));
        }
    }
    public bool isPreDefend
    {
        get
        {
            return _animator.GetCurrentAnimatorStateInfo(Shortcuts.AnimationKeys.BLENDABLE_LAYER).IsName(Shortcuts.AnimationKeys.PRE_BLOCK);
        }
    }
    public bool isAnimationDefend
    {
        get
        {
            return (_animator.GetCurrentAnimatorStateInfo(Shortcuts.AnimationKeys.BLENDABLE_LAYER).IsName(Shortcuts.AnimationKeys.PRE_BLOCK) ||
                    _animator.GetCurrentAnimatorStateInfo(Shortcuts.AnimationKeys.BLENDABLE_LAYER).IsName(Shortcuts.AnimationKeys.BLOCKING) ||
                    _animator.GetCurrentAnimatorStateInfo(Shortcuts.AnimationKeys.BLENDABLE_LAYER).IsName(Shortcuts.AnimationKeys.POST_BLOCK));
        }
    }

    public bool isAnimationAttack
    {
        get
        {
            return (_animator.GetCurrentAnimatorStateInfo(Shortcuts.AnimationKeys.ATTACK_LAYER).IsName(Shortcuts.AnimationKeys.ATTACKING) ||
                    _animator.GetCurrentAnimatorStateInfo(Shortcuts.AnimationKeys.ATTACK_LAYER).IsName(Shortcuts.AnimationKeys.PRE_ATTACK) ||
                    _animator.GetCurrentAnimatorStateInfo(Shortcuts.AnimationKeys.ATTACK_LAYER).IsName(Shortcuts.AnimationKeys.POST_ATTACK)
                );
        }
    }

    public bool isAnimationOccupied
    {
        get
        {
            return isAnimationAttack
                   || isAnimationDefend
                   || isAnimationCombo;
        }
    }*/

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void LateUpdate()
    {
        transform.localPosition = Vector3.zero;
        _animator.SetBool(Shortcuts.AnimationKeys.PARAM_ANIMATION_OCCUPIED, Shortcuts.CharacterAnimatorUtils.isAnimationOccupied(_animator));
        /*if(isAnimationAttack)
        {
            _animator.SetLayerWeight(Shortcuts.AnimationKeys.MAIN_LAYER, 0);
            _animator.SetLayerWeight(Shortcuts.AnimationKeys.UPPER_LAYER, 1);
        }
        else
        {
            _animator.SetLayerWeight(Shortcuts.AnimationKeys.MAIN_LAYER, 1);
            _animator.SetLayerWeight(Shortcuts.AnimationKeys.UPPER_LAYER, 0);
        }*/
    }
    
}
