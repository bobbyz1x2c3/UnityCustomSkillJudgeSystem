using DataClass.Enums;
using Unity.Netcode;
using UnityEngine;

namespace DataClass.Effection
{
    [CreateAssetMenu(menuName = "CustomEffect/BuffCause")]
    public class BuffCause : EffectBase
    {
        private EntityProps from { get; set; }
        private EntityNetWorkProps netFrom;
        public Buff buff;
        
        public BuffCause(float _lastTime, BuffTypes _type)
        {
            buff.Type = _type;
            buff.lastTime = _lastTime;
        }
        public BuffCause(EntityProps from,float _lastTime, BuffTypes _type)
        {
            buff.Type = _type;
            buff.lastTime = _lastTime;
        }
        public BuffCause()
        {
            buff.Type = BuffTypes.atk_up_10p;
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
    }
}