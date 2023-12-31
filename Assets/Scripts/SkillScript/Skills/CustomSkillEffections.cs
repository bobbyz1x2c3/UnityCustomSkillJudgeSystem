﻿using System.Collections.Generic;
using SkillScript.Effection;
using SkillScript.Enums;
using UnityEngine;

namespace SkillScript.Skills
{
    [CreateAssetMenu(menuName = "CustomSingleSkill")]
    public class CustomSkillEffections : SkillEffectionsBase
    {
        
        public CustomSkillEffections()
        {
            effects = new List<EffectBase>();
        }
        private void AddDamage(EntityProps entity,float _baseValue, float _coeffValue , float _pierceCoeff = 1,float _pierceBase = 0,EDamageType _damageType = EDamageType.Physics,EAttriTypes _attriType = EAttriTypes.atk)
        {
            var d = new DamageCause(entity, _baseValue, _coeffValue, _pierceCoeff, _pierceBase, _damageType, _attriType);
            effects.Add(d);
        }

        private void AddBuff(EntityProps entity, EBuffTypes _type, float _lastTime)
        {
            effects.Add(new BuffCause(entity,_lastTime, _type));
        }

        private void AddPostureReduce(float _coeff)
        {
            effects.Add(new PostureReduce(_coeff));
        }
    }
}