using SkillScript.Enums;
using Unity.Netcode;
using UnityEngine;

namespace SkillScript.Effection
{
    [CreateAssetMenu(menuName = "CustomEffect/HealCause")]
    public class HealCause: EffectBase
    {
        public float healAmount;
        public override void NetworkSerialize<T>(BufferSerializer<T> serializer)
        {
            NetworkSerializeImpl(serializer);
        }
        protected override void NetworkSerializeImpl<T>(BufferSerializer<T> serializer)
        {
            base.NetworkSerializeImpl(serializer);
            serializer.SerializeValue(ref healAmount);                
            
        }

        public override EEffectType GetEffectType()
        {
            return EEffectType.HealCause;
        }

        public override int Execute(EntityNetWorkProps to)
        {
            to.GetDamage(-healAmount);
            return 1;
        }
    }
}