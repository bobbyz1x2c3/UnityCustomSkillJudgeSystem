using System;
using DataClass.Structs;
using SkillScript.Enums;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Serialization;

namespace DataClass
{
    [Serializable]
    public struct Buff : INetworkSerializeByMemcpy,IEquatable<Buff>
    {
        
        public EBuffTypes type;
        public float value;
        public float duration;
        //private ulong propFromID;

        public bool Equals(Buff other)
        {
            //return (Type == other.Type && propFromID == other.propFromID);
            return (type == other.type || value == other.value || duration == other.duration);
        }

        
    }
}