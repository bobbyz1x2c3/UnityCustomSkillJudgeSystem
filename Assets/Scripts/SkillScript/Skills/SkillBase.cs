
using System.Collections.Generic;
using DataClass.Effection;
using UnityEngine;

namespace DataClass.Skills
{
    
    public abstract class SkillBase : ScriptableObject
    {
        
        public List<EffectBase> effects;
        public int Execute(EntityProps to)
        {
            foreach (var vaEffect in effects)
            {
                vaEffect.Execute(to);
                Debug.Log(vaEffect.ToString());
            }
            return 0;
        }

    }
}