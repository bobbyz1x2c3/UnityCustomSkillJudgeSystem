using System.Collections.Generic;
using SkillScript.Effection;
using UnityEngine;

namespace SkillScript.Skills
{
    
    public abstract class SkillEffectionsBase : ScriptableObject
    {
        
        public List<EffectBase> effects;
        /*public int Execute(EntityProps to)
        {
            foreach (var vaEffect in effects)
            {
                vaEffect.Execute(to);
                Debug.Log(vaEffect.ToString());
            }
            return 0;
        }*/

        public void SetEffectOrigin(EntityNetWorkProps origin)
        {
            foreach (var vaEffect in effects)
            {
                vaEffect.netFrom = origin;
            }
            
        }

    }
}