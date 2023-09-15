using SkillScript.Enums;
using Unity.Netcode;
using UnityEngine;

namespace SkillScript.Effection
{
    public abstract class EffectBase : ScriptableObject ,INetworkSerializable
    {

        
        private EntityProps from {get; set; }
        public EntityNetWorkProps netFrom;
        public ulong netFromId;

        public abstract void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter;

        protected virtual void NetworkSerializeImpl<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        { 
            //implement this method
            //this can help serialize the origin 
            serializer.SerializeValue(ref netFromId);
            netFrom = TotalManager.GetEntityByID(netFromId);
        }

        //public abstract int Execute( EntityProps to);
        public abstract int Execute(EntityNetWorkProps to);
        
        public abstract EEffectType GetEffectType();
        
    }
}