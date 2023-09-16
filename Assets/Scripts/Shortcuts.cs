using UnityEngine;

namespace Shortcuts
{
    public static class AnimationKeys
    {
        public static class Layers
        {
            public const int MAIN_LAYER = 0;
            public const int ATTACK_LAYER = 1;
            public const int BLOCK_LAYER = 2;
        }

        public static class AnimationStates
        {
            public const string PRE_BLOCK = "pre_block";
            public const string POST_BLOCK = "post_block";
            public const string BLOCKING = "blocking";
            public const string PRE_ATTACK = "pre_attack";
            public const string ATTACKING = "attacking";
            public const string POST_ATTACK = "post_attack";        
            public const string UPSLASH = "upslash";
            
            public const string COMBO_1 = "combo 1";
            public const string COMBO_2 = "combo 2";
            public const string COMBO_3 = "combo 3";
            public const string COMBO_4 = "combo 4";
            public const string COMBO_5 = "combo 5";
        }
        public static class Params
        {
            public const string IS_DEFEND = "isDefend";
            public const string IS_ATTACK = "isAttack";
            public const string IS_UPSLASH = "isUpslash";

            public const string CURRENT_SPEED = "CurrentSpeed";
            public const string ANIMATION_OCCUPIED = "AnimationOccupied";
        }
        




        
        
    }

    public static class CharacterAnimatorUtils
    {
        
        #region GetAnimationState


        public static bool isAnimationCombo(Animator _animator)
        {

            return (_animator.GetCurrentAnimatorStateInfo(Shortcuts.AnimationKeys.Layers.ATTACK_LAYER)
                        .IsName(Shortcuts.AnimationKeys.AnimationStates.COMBO_1) ||
                    _animator.GetCurrentAnimatorStateInfo(Shortcuts.AnimationKeys.Layers.ATTACK_LAYER)
                        .IsName(Shortcuts.AnimationKeys.AnimationStates.COMBO_2) ||
                    _animator.GetCurrentAnimatorStateInfo(Shortcuts.AnimationKeys.Layers.ATTACK_LAYER)
                        .IsName(Shortcuts.AnimationKeys.AnimationStates.COMBO_3) ||
                    _animator.GetCurrentAnimatorStateInfo(Shortcuts.AnimationKeys.Layers.ATTACK_LAYER)
                        .IsName(Shortcuts.AnimationKeys.AnimationStates.COMBO_4) ||
                    _animator.GetCurrentAnimatorStateInfo(Shortcuts.AnimationKeys.Layers.ATTACK_LAYER)
                        .IsName(Shortcuts.AnimationKeys.AnimationStates.COMBO_5));

        }

        public static bool isAnimationComboWithoutEnd(Animator _animator)
        {
            return (_animator.GetCurrentAnimatorStateInfo(Shortcuts.AnimationKeys.Layers.ATTACK_LAYER)
                        .IsName(Shortcuts.AnimationKeys.AnimationStates.COMBO_1) ||
                    _animator.GetCurrentAnimatorStateInfo(Shortcuts.AnimationKeys.Layers.ATTACK_LAYER)
                        .IsName(Shortcuts.AnimationKeys.AnimationStates.COMBO_2) ||
                    _animator.GetCurrentAnimatorStateInfo(Shortcuts.AnimationKeys.Layers.ATTACK_LAYER)
                        .IsName(Shortcuts.AnimationKeys.AnimationStates.COMBO_3) ||
                    _animator.GetCurrentAnimatorStateInfo(Shortcuts.AnimationKeys.Layers.ATTACK_LAYER)
                        .IsName(Shortcuts.AnimationKeys.AnimationStates.COMBO_4) );
        }

        public static bool isPreDefend(Animator _animator)
        {
            return _animator.GetCurrentAnimatorStateInfo(Shortcuts.AnimationKeys.Layers.BLOCK_LAYER)
                .IsName(Shortcuts.AnimationKeys.AnimationStates.PRE_BLOCK);
        }

        public static bool isAnimationDefend(Animator _animator)
        {
            return (_animator.GetCurrentAnimatorStateInfo(Shortcuts.AnimationKeys.Layers.BLOCK_LAYER)
                        .IsName(Shortcuts.AnimationKeys.AnimationStates.PRE_BLOCK) ||
                    _animator.GetCurrentAnimatorStateInfo(Shortcuts.AnimationKeys.Layers.BLOCK_LAYER)
                        .IsName(Shortcuts.AnimationKeys.AnimationStates.BLOCKING) ||
                    _animator.GetCurrentAnimatorStateInfo(Shortcuts.AnimationKeys.Layers.BLOCK_LAYER)
                        .IsName(Shortcuts.AnimationKeys.AnimationStates.POST_BLOCK));
            
        }

        public static bool isAnimationAttack(Animator _animator)
        {
            return (_animator.GetCurrentAnimatorStateInfo(Shortcuts.AnimationKeys.Layers.ATTACK_LAYER)
                        .IsName(Shortcuts.AnimationKeys.AnimationStates.ATTACKING) ||
                    _animator.GetCurrentAnimatorStateInfo(Shortcuts.AnimationKeys.Layers.ATTACK_LAYER)
                        .IsName(Shortcuts.AnimationKeys.AnimationStates.PRE_ATTACK) ||
                    _animator.GetCurrentAnimatorStateInfo(Shortcuts.AnimationKeys.Layers.ATTACK_LAYER)
                        .IsName(Shortcuts.AnimationKeys.AnimationStates.POST_ATTACK)
                    );
            
        }

        public static bool isAnimationUpslash(Animator _animator)
        {
            return (_animator?.GetCurrentAnimatorStateInfo(AnimationKeys.Layers.ATTACK_LAYER).IsName(AnimationKeys.AnimationStates.UPSLASH)).Value;
        }

        public static bool isAnimationOccupied(Animator _animator)
        {
            return isAnimationAttack(_animator)
                   || isAnimationDefend(_animator)
                   || isAnimationCombo(_animator)
                   || isAnimationUpslash(_animator);

        }

        public static bool isAnimationBlocking(Animator _animator)
        {
            return (_animator?.GetCurrentAnimatorStateInfo(AnimationKeys.Layers.BLOCK_LAYER).IsName(AnimationKeys.AnimationStates.BLOCKING)).Value;
        }

        #endregion

    }
    
    
    
}