using System;
using DataClass.Enums;
using DataClass.Structs;
using Unity.Netcode;

namespace DataClass
{
    [Serializable]
    public struct Buff : INetworkSerializeByMemcpy,IEquatable<Buff>
    {
        public BuffTypes Type;
        public float lastTime;
        private ulong propFromID;

        public bool Equals(Buff other)
        {
            return (Type == other.Type && propFromID == other.propFromID);
        }
    }
}