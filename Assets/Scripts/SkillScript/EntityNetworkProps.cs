
using System.Collections;
using System.Collections.Generic;
using DataClass;
using DataClass.Effection;
using DataClass.Enums;
using DataClass.Skills;
using DataClass.Structs;
using Unity.Netcode;
using UnityEngine;

public class EntityNetWorkProps : NetworkBehaviour
{
    public static EntityNetWorkProps LocalEntityNetWorkProp;
    //public NetworkVariable<List<Buff>> buffss;
    public NetworkVariable<Props> prop;
    public NetworkList<Buff> buffs;

    public NetworkVariable<ulong> id;


    public float C_ATK
    {
        get
        {
            float Coeff = 1;
            float Fix = 0;
            foreach (var buff in buffs)
            {
                if (buff.Type == EBuffTypes.atk_down_10p)
                {
                    Coeff -= 0.1f;
                }
                else if (buff.Type == EBuffTypes.atk_up_10p)
                {
                    Coeff += 0.1f;
                }
                else if(buff.Type == EBuffTypes.atk_down_10)
                {
                    Fix -= 10;
                }
                else if(buff.Type == EBuffTypes.atk_up_10)
                {
                    Fix += 10;
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
                if (buff.Type == EBuffTypes.mag_down_10p)
                {
                    Coeff -= 0.1f;
                }
                else if (buff.Type == EBuffTypes.mag_up_10p)
                {
                    Coeff += 0.1f;
                }
                else if(buff.Type == EBuffTypes.mag_down_10)
                {
                    Fix -= 10;
                }
                else if(buff.Type == EBuffTypes.mag_up_10)
                {
                    Fix += 10;
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
                if (buff.Type == EBuffTypes.arm_down_10p)
                {
                    Coeff -= 0.1f;
                }
                else if (buff.Type == EBuffTypes.arm_up_10p)
                {
                    Coeff += 0.1f;
                }
                else if(buff.Type == EBuffTypes.arm_down_10)
                {
                    Fix -= 10;
                }
                else if(buff.Type == EBuffTypes.arm_up_10)
                {
                    Fix += 10;
                }

            }
            return Fix + Coeff * prop.Value.arm;
        }
    }

    public bool IsDead
    {
        get
        {
            return prop.Value.HP <= 0;
        }
    }
    private void OnEnable()
    {
        buffs = new NetworkList<Buff>();
        
    }

    private void Start()
    {
        

    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        
        if(IsOwner && IsClient)
        {
            LocalEntityNetWorkProp = this;
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
            spd = 100
        };
        buffs = new NetworkList<Buff>();
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
       ShowPropInfo(TotalManager.GetEntityByID(ID));
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
        ShowPropInfo(TotalManager.GetEntityByID(ID));
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
        }
    }

    public void SkillRequest(SkillBase paramSkill, ulong ID)
    {
        foreach (var VARIABLE in paramSkill.effects)
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
        buffs.Initialize(this);
        buffs.Add(buff);
        yield return new WaitForSeconds(buff.lastTime);
        buffs.Remove(buff);
        Debug.Log("buff end"+buff.Type);    
    }
    public void GetBuff(Buff buff)
    {
        //FIXME
        StartCoroutine(BuffTimer(buff));       
        
    }

    public void GetPostureReduce(float value)
    {
        var a = prop.Value;
        a.Posture -= value;
        prop.Value = a;
    }



    #region Utils

    public static void ShowPropInfo(EntityNetWorkProps _param)
    {
        Debug.Log(_param.id.Value + "HP:" + _param.prop.Value.HP + "Posture:" + _param.prop.Value.Posture + "Speed:" + _param.prop.Value.spd + "Attack:" + _param.prop.Value.atk + "Defence:" + _param.prop.Value.arm);
        Debug.Log("buffs:" + _param.buffs.Count);
        foreach (var VARIABLE in _param.buffs)
        {
            Debug.Log(VARIABLE.Type.ToString() + ":" + VARIABLE.lastTime);
        }
    }

    #endregion

  


    
}

