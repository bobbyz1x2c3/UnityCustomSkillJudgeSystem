
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
    //public NetworkVariable<List<Buff>> buffs;qqq
    public NetworkVariable<Props> prop;
    public NetworkList<Buff> buffs ;
    public NetworkVariable<ulong> id;
    
    public float C_ATK
    {
        get
        {
            float Coeff = 1;
            float Fix = 0;
            foreach (var buff in buffs)
            {
                if (buff.Type == BuffTypes.atk_down_10p)
                {
                    Coeff -= 0.1f;
                }
                else if (buff.Type == BuffTypes.atk_up_10p)
                {
                    Coeff += 0.1f;
                }
                else if(buff.Type == BuffTypes.atk_down_10)
                {
                    Fix -= 10;
                }
                else if(buff.Type == BuffTypes.atk_up_10)
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
                if (buff.Type == BuffTypes.mag_down_10p)
                {
                    Coeff -= 0.1f;
                }
                else if (buff.Type == BuffTypes.mag_up_10p)
                {
                    Coeff += 0.1f;
                }
                else if(buff.Type == BuffTypes.mag_down_10)
                {
                    Fix -= 10;
                }
                else if(buff.Type == BuffTypes.mag_up_10)
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
                if (buff.Type == BuffTypes.arm_down_10p)
                {
                    Coeff -= 0.1f;
                }
                else if (buff.Type == BuffTypes.arm_up_10p)
                {
                    Coeff += 0.1f;
                }
                else if(buff.Type == BuffTypes.arm_down_10)
                {
                    Fix -= 10;
                }
                else if(buff.Type == BuffTypes.arm_up_10)
                {
                    Fix += 10;
                }

            }
            return Fix + Coeff * prop.Value.arm;
        }
    }

    private void OnEnable()
    {
        buffs = new NetworkList<Buff>();
    }

    private void Start()
    {
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
        Debug.Log(b);
        b.Execute(TotalManager.GetEntityByID(ID));
    }

    public void GetDamage(float damage)
    {
        var a = prop.Value;
        a.HP -= damage;
        prop.Value = a;
    }

    public void GetBuff(Buff buff)
    {
        buffs.Add(buff);
    }

    public void GetPostureReduce(float value)
    {
        var a = prop.Value;
        a.Posture -= value;
        prop.Value = a;
    }
    
    

    


    
}

