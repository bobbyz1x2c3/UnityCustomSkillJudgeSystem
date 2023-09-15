using DataClass;
using SkillScript.Enums;
using Unity.Netcode;
using UnityEngine;

namespace SkillScript.Effection
{
    [CreateAssetMenu(menuName = "CustomEffect/BuffReduce")]
    public class BuffRemove : EffectBase
    {
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

        public override int Execute(EntityNetWorkProps to)
        {
            
            to.RemoveBuff(buff);
            return 0;
        }

        public override EEffectType GetEffectType()
        {
            return EEffectType.BuffRemove;
        }
    }
}