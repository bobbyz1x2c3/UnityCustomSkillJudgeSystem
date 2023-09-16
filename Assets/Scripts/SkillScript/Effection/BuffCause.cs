using DataClass;
using SkillScript.Enums;
using Unity.Netcode;
using UnityEngine;

namespace SkillScript.Effection
{
    [CreateAssetMenu(menuName = "CustomEffect/BuffCause")]
    public class BuffCause : EffectBase 
    {
        private EntityProps from { get; set; }

        public Buff buff;
        public override void NetworkSerialize<T>(BufferSerializer<T> serializer)
        {
            NetworkSerializeImpl(serializer);
        }
        protected override void NetworkSerializeImpl<T>(BufferSerializer<T> serializer)
        {
            base.NetworkSerializeImpl(serializer);
            serializer.SerializeValue(ref buff);
            
        }

        public BuffCause(float duration, EBuffTypes _type)
        {
            buff.type = _type;
            buff.duration = duration;
        }
        public BuffCause(EntityProps from,float duration, EBuffTypes _type)
        {
            buff.type = _type;
            buff.duration = duration;
        }
        public BuffCause()
        {
            buff.type = EBuffTypes.atk_up_ratio;
            buff.duration = 1;
        }
        /*public override int Execute(EntityProps to)
        {
            Debug.Log("对"+to.ToString()+"造成buff："+buff.Type);
            /*to.buffs.Add(buff);#1#
            to.GetBuff(buff);
            return 0;
        }*/

        public override int Execute(EntityNetWorkProps to)
        {
            to.GetBuff(buff);
            return 0;
        }
        

        public override EEffectType GetEffectType()
        {
            return EEffectType.BuffCause;
            
        }
    }
}