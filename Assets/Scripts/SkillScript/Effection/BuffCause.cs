using DataClass.Enums;
using Unity.Netcode;
using UnityEngine;

namespace DataClass.Effection
{
    [CreateAssetMenu(menuName = "CustomEffect/BuffCause")]
    public class BuffCause : EffectBase, INetworkSerializable
    {
        private EntityProps from { get; set; }

        public Buff buff;
        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            NetworkSerializeImpl(serializer);
        }
        protected override void NetworkSerializeImpl<T>(BufferSerializer<T> serializer)
        {
            base.NetworkSerializeImpl(serializer);
            serializer.SerializeValue(ref buff);
            
        }

        public BuffCause(float _lastTime, EBuffTypes _type)
        {
            buff.Type = _type;
            buff.lastTime = _lastTime;
        }
        public BuffCause(EntityProps from,float _lastTime, EBuffTypes _type)
        {
            buff.Type = _type;
            buff.lastTime = _lastTime;
        }
        public BuffCause()
        {
            buff.Type = EBuffTypes.atk_up_10p;
            buff.lastTime = 1;
        }
        public override int Execute(EntityProps to)
        {
            Debug.Log("对"+to.ToString()+"造成buff："+buff.Type);
            /*to.buffs.Add(buff);*/
            to.GetBuff(buff);
            return 0;
        }

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