using DataClass.Enums;
using Unity.Mathematics;
using Unity.Netcode;
using UnityEngine;

namespace DataClass.Effection
{
    [CreateAssetMenu(menuName = "CustomEffect/DamageCause")]
    public class DamageCause : EffectBase
    {
        private EntityProps from;
        public EntityNetWorkProps netFrom;
        private const float DEF_BASE = 10000f;
        [Header("params")]
        public DamageType _DamageType;
        public float BaseValue;
        public float CoeffValue;
        
        public float PierceCoeffValue;
        public float PierceBaseValue;
        public AttriTypes _AttriType;

        

        
        public DamageCause()
        {
            BaseValue = 0;
            CoeffValue = 1;
            PierceCoeffValue = 1;
            PierceBaseValue = 0;
            _DamageType = DamageType.Physics;
            _AttriType = AttriTypes.atk;
            
        }

        public DamageCause(EntityProps _entity,float _BaseValue = 0, float _CoeffValue = 1, float _PierceCoeffValue = 1, float _PierceBaseValue = 0,
            DamageType _damageType = DamageType.Physics, AttriTypes _attriType = AttriTypes.atk)
        {
            from = _entity;
            BaseValue = _BaseValue;
            CoeffValue = _CoeffValue;
            PierceCoeffValue = _PierceCoeffValue;
            PierceBaseValue = _PierceBaseValue;
            _AttriType = _attriType;
            _DamageType = _damageType;
            
        }
        public DamageCause(EntityNetWorkProps _entity,float _BaseValue = 0, float _CoeffValue = 1, float _PierceCoeffValue = 1, float _PierceBaseValue = 0,
            DamageType _damageType = DamageType.Physics, AttriTypes _attriType = AttriTypes.atk)
        {
            netFrom = _entity;
            BaseValue = _BaseValue;
            CoeffValue = _CoeffValue;
            PierceCoeffValue = _PierceCoeffValue;
            PierceBaseValue = _PierceBaseValue;
            _AttriType = _attriType;
            _DamageType = _damageType;
            
        }
        
        public override int Execute(EntityProps to)
        {
            float value = 0;
            switch (_AttriType)
            {
                
                //switch all type in AttriTypes enum
                case AttriTypes.atk:
                    value = CoeffValue* from.C_ATK + BaseValue;
                    break;
                case AttriTypes.arm:
                    value = CoeffValue* from.C_ARM + BaseValue;
                    break;
                case AttriTypes.mag:
                    value = CoeffValue* from.prop.mag + BaseValue;
                    break;
                case AttriTypes.maxMP:
                    value = CoeffValue* from.prop.maxMP + BaseValue;
                    break;
                case AttriTypes.maxHP:
                    value = CoeffValue* from.prop.maxHP + BaseValue;
                    break;
                default:
                    value = 0;
                    break;
            }
            
            float def = (PierceCoeffValue * to.C_ARM)-PierceBaseValue;
            Debug.Log("防御"+def+"，攻击值"+value);
            //减防公式 1 - x/sqrt(x^2+DEF_BASE)
            value *= 1-def/Mathf.Sqrt(Mathf.Pow(def,2) + DEF_BASE);
            Debug.Log("减伤系数"+(1-def/Mathf.Sqrt(Mathf.Pow(def,2) + DEF_BASE))+"，折后攻击值"+value);
            
            //todo
            to.GetDamage(value);
            return 0;
            
        }

        public override int Execute(EntityNetWorkProps to)
        {
            float value = 0;
            switch (_AttriType)
            {
                
                //switch all type in AttriTypes enum
                case AttriTypes.atk:
                    value = CoeffValue* netFrom.C_ATK + BaseValue;
                    break;
                case AttriTypes.arm:
                    value = CoeffValue* netFrom.C_ARM + BaseValue;
                    break;
                case AttriTypes.mag:
                    value = CoeffValue* netFrom.prop.Value.mag + BaseValue;
                    break;
                case AttriTypes.maxMP:
                    value = CoeffValue* netFrom.prop.Value.maxMP + BaseValue;
                    break;
                case AttriTypes.maxHP:
                    value = CoeffValue* netFrom.prop.Value.maxHP + BaseValue;
                    break;
                default:
                    value = 0;
                    break;
            }
            
            float def = (PierceCoeffValue * to.C_ARM)-PierceBaseValue;
            Debug.Log("防御"+def+"，攻击值"+value);
            //减防公式 1 - x/sqrt(x^2+DEF_BASE)
            value *= 1-def/Mathf.Sqrt(Mathf.Pow(def,2) + DEF_BASE);
            Debug.Log("减伤系数"+(1-def/Mathf.Sqrt(Mathf.Pow(def,2) + DEF_BASE))+"，折后攻击值"+value);
            
            //to.GetDamageServerRPC(value);
            to.GetDamage(value);
            return 0;
        }
    }
}