using UnityEngine;

namespace Shortcuts
{
    public static class AnimationKeys
    {
        public const int MAIN_LAYER = 0;
        public const int ATTACK_LAYER = 1;
        public const int BLENDABLE_LAYER = 2;
        
        public const string PRE_BLOCK = "pre_block";
        public const string POST_BLOCK = "post_block";
        public const string BLOCKING = "blocking";
        public const string PRE_ATTACK = "pre_attack";
        public const string ATTACKING = "attacking";
        public const string POST_ATTACK = "post_attack";        
        public const string UPSLASH = "upslash";

        public const string PARAM_IS_DEFEND = "isDefend";
        public const string PARAM_IS_ATTACK = "isAttack";
        public const string PARAM_IS_UPSLASH = "isUpslash";

        public const string PARAM_CURRENT_SPEED = "CurrentSpeed";
        public const string PARAM_ANIMATION_OCCUPIED = "AnimationOccupied";

        public const string COMBO_1 = "combo 1";
        public const string COMBO_2 = "combo 2";
        public const string COMBO_3 = "combo 3";
        public const string COMBO_4 = "combo 4";
        public const string COMBO_5 = "combo 5";
        
        
    }

    public static class CharacterAnimatorUtils
    {
        
        #region GetAnimationState


        public static bool isAnimationCombo(Animator _animator)
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

        public static bool isPreDefend(Animator _animator)
        {
            return _animator.GetCurrentAnimatorStateInfo(Shortcuts.AnimationKeys.BLENDABLE_LAYER)
                .IsName(Shortcuts.AnimationKeys.PRE_BLOCK);
        }

        public static bool isAnimationDefend(Animator _animator)
        {
            return (_animator.GetCurrentAnimatorStateInfo(Shortcuts.AnimationKeys.BLENDABLE_LAYER)
                        .IsName(Shortcuts.AnimationKeys.PRE_BLOCK) ||
                    _animator.GetCurrentAnimatorStateInfo(Shortcuts.AnimationKeys.BLENDABLE_LAYER)
                        .IsName(Shortcuts.AnimationKeys.BLOCKING) ||
                    _animator.GetCurrentAnimatorStateInfo(Shortcuts.AnimationKeys.BLENDABLE_LAYER)
                        .IsName(Shortcuts.AnimationKeys.POST_BLOCK));
            
        }

        public static bool isAnimationAttack(Animator _animator)
        {
            return (_animator.GetCurrentAnimatorStateInfo(Shortcuts.AnimationKeys.ATTACK_LAYER)
                        .IsName(Shortcuts.AnimationKeys.ATTACKING) ||
                    _animator.GetCurrentAnimatorStateInfo(Shortcuts.AnimationKeys.ATTACK_LAYER)
                        .IsName(Shortcuts.AnimationKeys.PRE_ATTACK) ||
                    _animator.GetCurrentAnimatorStateInfo(Shortcuts.AnimationKeys.ATTACK_LAYER)
                        .IsName(Shortcuts.AnimationKeys.POST_ATTACK)
                    );
            
        }

        public static bool isAnimationUpslash(Animator _animator)
        {
            return _animator.GetCurrentAnimatorStateInfo(AnimationKeys.ATTACK_LAYER).IsName(AnimationKeys.UPSLASH);
        }

        public static bool isAnimationOccupied(Animator _animator)
        {
            return isAnimationAttack(_animator)
                   || isAnimationDefend(_animator)
                   || isAnimationCombo(_animator)
                   || isAnimationUpslash(_animator);

        }

        #endregion

    }
    
    
    
}