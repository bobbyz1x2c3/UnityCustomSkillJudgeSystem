
using System;
using System.Collections;
using System.Collections.Generic;
using DataClass;
using DataClass.Structs;
using SkillScript.Effection;
using SkillScript.Enums;
using SkillScript.Skills;
using Unity.Netcode;
using UnityEngine;

#region Utils



#endregion

public class EntityNetWorkProps : NetworkBehaviour , INetworkUpdateSystem
{
    public static EntityNetWorkProps LocalEntityNetWorkProp;
    //public NetworkVariable<List<Buff>> buffss;
    public NetworkVariable<Props> prop;
    public NetworkList<Buff> buffs;

    public NetworkVariable<ulong> id;
    public NetworkVariable<EEntityTypes> entityType;    


    public float C_ATK
    {
        get
        {
            float Coeff = 1;
            float Fix = 0;
            foreach (var buff in buffs)
            {
                if (buff.type == EBuffTypes.atk_down_ratio)
                {
                    Coeff -= buff.value;
                }
                else if (buff.type == EBuffTypes.atk_up_ratio)
                {
                    Coeff += buff.value;
                }
                else if(buff.type == EBuffTypes.atk_down)
                {
                    Fix -= buff.value;
                }
                else if(buff.type == EBuffTypes.atk_up)
                {
                    Fix += buff.value;
                }
                
            }
            return Fix + Coeff * prop.Value.atk;
        }
    }
    public float C_MAG
    {
        get
        {
            float Coeff = 1;
            float Fix = 0;
            foreach (var buff in buffs)
            {
                if (buff.type == EBuffTypes.mag_down_ratio)
                {
                    Coeff -= buff.value;
                }
                else if (buff.type == EBuffTypes.mag_up_ratio)
                {
                    Coeff += buff.value;
                }
                else if(buff.type == EBuffTypes.mag_down)
                {
                    Fix -= buff.value;
                }
                else if(buff.type == EBuffTypes.mag_up)
                {
                    Fix += buff.value;
                }

            }
            return Fix + Coeff * prop.Value.arm;
        }
    }
    public float C_ARM
    {
        get
        {
            float Coeff = 1;
            float Fix = 0;
            foreach (var buff in buffs)
            {
                if (buff.type == EBuffTypes.arm_down_ratio)
                {
                    Coeff -= buff.value;
                }
                else if (buff.type == EBuffTypes.arm_up_ratio)
                {
                    Coeff += buff.value;
                }
                else if(buff.type == EBuffTypes.arm_down)
                {
                    Fix -= buff.value;
                }
                else if(buff.type == EBuffTypes.arm_up)
                {
                    Fix += buff.value;
                }

            }
            return Fix + Coeff * prop.Value.arm;
        }
    }

    public bool IsDead => prop.Value.HP <= 0;

    public bool IsPlayer => !IsNPC;

    public bool IsNPC => entityType.Value.IsNPC();


    public delegate void OnNetworkUpdateDelegate();
    public event OnNetworkUpdateDelegate OnNetworkUpdateEvent;
    public void NetworkUpdate(NetworkUpdateStage updateStage)
    {
        OnNetworkUpdateEvent?.Invoke();
        
    }

    private void OnEnable()
    {
        buffs = new NetworkList<Buff>();
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        this.RegisterNetworkUpdate(NetworkUpdateStage.FixedUpdate);
        //下面的写法也一样的
        //NetworkUpdateLoop.RegisterNetworkUpdate(this,NetworkUpdateStage.FixedUpdate);
        if(IsOwner && IsClient)
        {
            LocalEntityNetWorkProp = this;
            //todo
            GenerateServerRPC(NetworkManager.Singleton.LocalClientId);
            Debug.Log("client id:"+id.Value);
        }
    }

    
    [ServerRpc]
    public void GenerateServerRPC(ulong ID)
    {
        id.Value = ID;
        prop.Value = new Props
        {
            arm = 100,
            atk = 100,
            HP = 1000,
            maxHP = 1000,
            maxMP = 1000,
            ID = id.Value,
            mag = 100,
            maxPosture = 100,
            MaxStamina = 100,
            MP = 1000,
            Posture = 100,
            stamina = 100,
            spd = 100,
            postureRecoverRate = 10
        };
        buffs = new NetworkList<Buff>();
        buffs.Initialize(this);
        entityType.Value = EEntityTypes.PlayerCharacter;
    }
    [ServerRpc]
    public void GetDamageServerRPC(float damage)
    {
        var a = prop.Value;
        a.HP -= damage;
        prop.Value = a;
    }


    [ServerRpc]
    public void GetBuffServerRPC(Buff buff)
    {
        buffs.Add(buff);
    }

    [ServerRpc]
    public void GetPostureReduceServerRPC(float coeff)
    {
        var a = prop.Value;
        a.Posture -= coeff;
        prop.Value = a;
    }

    [ServerRpc]
    public void AttackRequestServerRPC(ulong ID)
    {
        //var a = new DamageCause(this, 0, 1);
        var b = Resources.Load<DamageCause>("test/aaa");
        b.netFrom = this;
        b.Execute(TotalManager.GetEntityByID(ID));
    }

    #region 哥们直接判断类型封装，嘻嘻
    [ServerRpc]
    public void BuffCauseServerRPC(BuffCause _eb, ulong ID)
    {
        EffectExecute(_eb, ID);
    }
    [ServerRpc]
    public void DamageCauseServerRPC(DamageCause _eb, ulong ID)
    {
        EffectExecute(_eb, ID);
    }
    [ServerRpc]
    public void PostureReduceServerRPC(PostureReduce _eb, ulong ID)
    {
        EffectExecute(_eb, ID);
    }
    [ServerRpc]
    public void BuffRemoveServerRPC(BuffRemove _eb, ulong ID)
    {
        EffectExecute(_eb, ID);
    }
    [ServerRpc]
    public void HealCauseServerRPC(HealCause _eb, ulong ID){
        EffectExecute(_eb, ID);
    }

    /// <summary>
    /// 在Server执行
    /// </summary>
    /// <param name="_eb"></param>
    /// <param name="ID"></param>
    public void EffectExecute(EffectBase _eb, ulong ID)
    {
        Debug.Log("a"+_eb.GetEffectType());
        _eb.netFrom = this;
        _eb.netFromId = id.Value;
        _eb.Execute(TotalManager.GetEntityByID(ID));
#if UNITY_EDITOR
        //todo hideme
        //TotalManager.GetEntityByID(ID).ShowPropInfo();
#endif 
        
    }
    /// <summary>
    /// 在客户端调用
    /// </summary>
    /// <param name="_eb">执行的effect本身</param>
    /// <param name="ID">执行对象</param>
    public void EffectRequest(EffectBase _eb, ulong ID)
    {
        switch (_eb.GetEffectType())
        {
            case EEffectType.BuffCause:
                BuffCauseServerRPC(_eb as BuffCause, ID);
                break;
            case EEffectType.DamageCause:
                DamageCauseServerRPC(_eb as DamageCause, ID);
                break;
            case EEffectType.PostureReduce:
                PostureReduceServerRPC(_eb as PostureReduce, ID);
                break;
            case EEffectType.BuffRemove:
                BuffRemoveServerRPC(_eb as BuffRemove, ID);
                break;
            case EEffectType.HealCause: 
                HealCauseServerRPC(_eb as HealCause, ID);
                break;                
        }
    }

    public void SkillRequest(SkillEffectionsBase paramSkillEffections, ulong ID)
    {
        Debug.Log(paramSkillEffections);
        foreach (var VARIABLE in paramSkillEffections.effects)
        {
            Debug.Log("a1"+VARIABLE.GetEffectType());
            EffectRequest(VARIABLE, ID);
        }
    }
    

    #endregion
    

    public void GetDamage(float damage)
    {
        var a = prop.Value;
        a.HP -= damage;
        prop.Value = a;
    }

    IEnumerator BuffTimer(Buff buff)
    {
        //todo 不知道会不会出事
        //buffs.Initialize(this);
        buffs.Add(buff);
        if(buff.duration < -0.9f) yield break;
        yield return new WaitForSeconds(buff.duration);
        buffs.Remove(buff);
        //Debug.Log("buff end"+buff.Type);    
    }
    public void GetBuff(Buff buff)
    {
        //FIXME
        StartCoroutine(BuffTimer(buff));       
        
    }
    
    public void RemoveBuff(Buff buff)
    {
        buffs.Remove(buff);
    }

    public void GetPostureReduce(float value)
    {
        var a = prop.Value;
        a.Posture -= value;
        prop.Value = a;
    }


    public bool IsFriend(EntityNetWorkProps prop1)
    {
        if (prop1.entityType.Value == EEntityTypes.FriendNPCCharacter ||
            entityType.Value == EEntityTypes.FriendNPCCharacter || 
            prop1.entityType.Value == entityType.Value)
        {
            return true;
        }
        return false;
    }
    public void ShowPropInfo()
    {
        Debug.Log(id.Value + "HP:" + prop.Value.HP + "Posture:" + prop.Value.Posture + "Speed:" + prop.Value.spd + "Attack:" + prop.Value.atk + "Defence:" + prop.Value.arm);
        Debug.Log("buffs:" + buffs.Count);
        foreach (var VARIABLE in buffs)
        {
            Debug.Log(VARIABLE.type.ToString() + ":" + VARIABLE.duration);
        }
    }


  


    
}

