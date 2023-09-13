using System;
using DataClass.Enums;
using DataClass.Structs;
using Unity.Netcode;
using UnityEngine;

namespace DataClass
{
    [Serializable]
    public struct Buff : INetworkSerializeByMemcpy,IEquatable<Buff>
    {
        public EBuffTypes Type;
        public float lastTime;
        //private ulong propFromID;

        public bool Equals(Buff other)
        {
            //return (Type == other.Type && propFromID == other.propFromID);
            return Type == other.Type;
        }
    }
}