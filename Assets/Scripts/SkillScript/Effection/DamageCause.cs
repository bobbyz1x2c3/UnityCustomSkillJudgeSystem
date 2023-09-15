using SkillScript.Enums;
using Unity.Netcode;
using UnityEngine;

namespace SkillScript.Effection
{
    [CreateAssetMenu(menuName = "CustomEffect/DamageCause")]
    public class DamageCause : EffectBase
    {
        private EntityProps from;
        private const float DEF_BASE = 10000f;
        [Header("params")]
        public EDamageType _DamageType;
        public float BaseValue;
        public float CoeffValue;
        
        public float PierceCoeffValue;
        public float PierceBaseValue;
        public EAttriTypes _AttriType;

        
        /*public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref _DamageType);
            serializer.SerializeValue(ref netFromId);
            serializer.SerializeValue(ref BaseValue);
            serializer.SerializeValue(ref CoeffValue);
            serializer.SerializeValue(ref PierceCoeffValue);
            serializer.SerializeValue(ref PierceBaseValue);
            serializer.SerializeValue(ref _AttriType);            
            netFrom = TotalManager.GetEntityByID(netFromId);
        }*/
        public override void NetworkSerialize<T>(BufferSerializer<T> serializer)
        {
            NetworkSerializeImpl(serializer);
        }
        protected override void NetworkSerializeImpl<T>(BufferSerializer<T> serializer)
        {
            base.NetworkSerializeImpl(serializer);
            serializer.SerializeValue(ref _DamageType);
            serializer.SerializeValue(ref BaseValue);
            serializer.SerializeValue(ref CoeffValue);
            serializer.SerializeValue(ref PierceCoeffValue);
            serializer.SerializeValue(ref PierceBaseValue);
            serializer.SerializeValue(ref _AttriType);            
            
        }

        public DamageCause()
        {
            BaseValue = 0;
            CoeffValue = 1;
            PierceCoeffValue = 1;
            PierceBaseValue = 0;
            _DamageType = EDamageType.Physics;
            _AttriType = EAttriTypes.atk;
            
        }
        
        public override EEffectType GetEffectType()
        {
            return EEffectType.DamageCause;
        }

        public DamageCause(EntityProps _entity,float _BaseValue = 0, float _CoeffValue = 1, float _PierceCoeffValue = 1, float _PierceBaseValue = 0,
            EDamageType _damageType = EDamageType.Physics, EAttriTypes _attriType = EAttriTypes.atk)
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
            EDamageType _damageType = EDamageType.Physics, EAttriTypes _attriType = EAttriTypes.atk)
        {
            netFrom = _entity;
            BaseValue = _BaseValue;
            CoeffValue = _CoeffValue;
            PierceCoeffValue = _PierceCoeffValue;
            PierceBaseValue = _PierceBaseValue;
            _AttriType = _attriType;
            _DamageType = _damageType;
            
        }
        
        /*public override int Execute(EntityProps to)
        {
            float value = 0;
            switch (_AttriType)
            {
                
                //switch all type in AttriTypes enum
                case EAttriTypes.atk:
                    value = CoeffValue* from.C_ATK + BaseValue;
                    break;
                case EAttriTypes.arm:
                    value = CoeffValue* from.C_ARM + BaseValue;
                    break;
                case EAttriTypes.mag:
                    value = CoeffValue* from.prop.mag + BaseValue;
                    break;
                case EAttriTypes.maxMP:
                    value = CoeffValue* from.prop.maxMP + BaseValue;
                    break;
                case EAttriTypes.maxHP:
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
            
        }*/
/// <summary>
/// 
/// [伤害结算流程]
/// 1. 计算伤害值（或许以后会有暴击呢）
/// coefficience * 标定类型 + fix
/// 2. 根据防御力计算减伤系数
/// 减防公式 1 - x/sqrt(x^2+DEF_BASE)
/// 3. 计算伤害值
/// 4. 特殊buff结算
/// 5. 格挡结算
/// 6. 伤害结算
/// </summary>
/// <param name="to"></param>
/// <returns></returns>
        public override int Execute(EntityNetWorkProps to)
        {
            float value = 0;
            
            switch (_AttriType)
            {
                
                //switch all type in AttriTypes enum
                case EAttriTypes.atk:
                    value = CoeffValue* netFrom.C_ATK + BaseValue;
                    break;
                case EAttriTypes.arm:
                    value = CoeffValue* netFrom.C_ARM + BaseValue;
                    break;
                case EAttriTypes.mag:
                    value = CoeffValue* netFrom.prop.Value.mag + BaseValue;
                    break;
                case EAttriTypes.maxMP:
                    value = CoeffValue* netFrom.prop.Value.maxMP + BaseValue;
                    break;
                case EAttriTypes.maxHP:
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
            
            //特殊buff带来的效果
            float coeff = 1;
            float fix = 0;
            float block_coeff = 1;
            foreach (var _buff in netFrom.buffs)
            {
                switch (_buff.type)
                {
                    case EBuffTypes.Damage_reduce:
                        fix -= _buff.value;
                        break;
                    case EBuffTypes.Damage_ratio:
                        coeff *= _buff.value;
                        break;
                    case EBuffTypes.Damage_amp:
                        fix += _buff.value;
                        break;

                }
            }
            foreach (var _buff in to.buffs)
            {
                switch (_buff.type)
                {
                    case EBuffTypes.Blocking:
                        block_coeff = 0.1f;
                        break;
                }
            }
            value = block_coeff * (value * coeff + fix);
            //to.GetDamageServerRPC(value);
            to.GetDamage(value > 0?value : 0);
            return 0;
        }
    }
}