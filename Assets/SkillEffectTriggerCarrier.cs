using System;
using System.Collections;
using System.Collections.Generic;
using SkillScript.Skills;
using UnityEngine;

public class SkillEffectTriggerCarrier : MonoBehaviour
{
    // Start is called before the first frame update
    EntityNetWorkProps selfProps;
    [SerializeField]
    private SkillBase _skill;
    private void Start()
    {
        selfProps = GetComponentInParent<EntityNetWorkProps>();
        
    }

    private bool isValidTarget(EntityNetWorkProps prop)
    {
        //FIXME 记得改回来
        //return prop != selfProps && !prop.IsDead && prop.IsFriend(selfProps);
        
        //fixme 在测试的时候用这个，只要是活人就能打
        return prop != selfProps && !prop.IsDead ;
    }
    private void OnTriggerEnter(Collider other)
    {
        EntityNetWorkProps prop;
        //Debug.Log(other);
        //var check = other.TryGetComponent<EntityNetWorkProps>(out prop);
        prop = other.GetComponentInParent<EntityNetWorkProps>();
        
        if (selfProps.IsOwner && prop != null && isValidTarget(prop))
        {
            
            Debug.Log(selfProps.id.Value+" :attack ->" +prop.id.Value);
            selfProps.SkillRequest(_skill, prop.id.Value);

        }
    }
}
