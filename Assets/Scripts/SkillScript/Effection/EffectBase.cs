using UnityEngine;

namespace DataClass.Effection
{
    public abstract class EffectBase : ScriptableObject
    {
        private EntityProps from {get; set; }
        public EntityNetWorkProps network_from;
        public abstract int Execute( EntityProps to);
        public abstract int Execute(EntityNetWorkProps to);
    }
}