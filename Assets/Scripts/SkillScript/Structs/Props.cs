using System;
using Unity.Netcode;

namespace DataClass.Structs
{
    [Serializable]
    public struct Props:INetworkSerializeByMemcpy
    {
        public ulong ID;
        public float HP;
        public float maxHP;
        public float Posture;
        public float maxPosture ;
        public float stamina ;
        public float MaxStamina ;
        public float MP ;
        public float maxMP ;

        public float atk ;
        public float arm ;
        public float spd ;
        public float mag ;
        public float postureRecoverRate;

    }
}