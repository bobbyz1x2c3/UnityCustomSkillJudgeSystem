﻿using SkillScript.Enums;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Serialization;

namespace SkillScript.Effection
{
    [CreateAssetMenu(menuName = "CustomEffect/PostureReduce")]
    public class PostureReduce : EffectBase
    {
        //deprecated
        private EntityProps from { get; set; }

        [FormerlySerializedAs("Coeff")] [Tooltip("the coeffience of Posture damage")]
        public float Value;

        private const float BLOCK_COEFF = 0.2f;

        public override void NetworkSerialize<T>(BufferSerializer<T> serializer)
        {
            NetworkSerializeImpl(serializer);
        }

        protected override void NetworkSerializeImpl<T>(BufferSerializer<T> serializer)
        {
            base.NetworkSerializeImpl(serializer);
            serializer.SerializeValue(ref Value);     
        }

        public override EEffectType GetEffectType()
        {
            return EEffectType.PostureReduce;
        }
        
        public PostureReduce()
        {
            Value = 1;
        }

        public PostureReduce(float value)
        {
            Value = value;
        }
        /*public override int Execute(EntityProps to)
        {
            to.prop.Posture -= Value;
            return 0;
        }*/

        public override int Execute(EntityNetWorkProps to)
        {
            //fixme 不够优雅
            //要判断被击的状态
            if (to.IsPlayer)
            {
                var a = to.GetComponentInChildren<Animator>();
                
                if (a != null && Shortcuts.CharacterAnimatorUtils.isAnimationBlocking(a))
                {
                    to.GetPostureReduce(BLOCK_COEFF * Value);
                }
                else
                {
                    to.GetPostureReduce(Value);
                }
            }
            else
            {
                to.GetPostureReduce(Value);
            }

            return 0;
        }
    }
}