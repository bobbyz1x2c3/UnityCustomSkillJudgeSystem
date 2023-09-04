using System;
using System.Collections;
using System.Collections.Generic;
using DataClass;
using DataClass.Enums;
using DataClass.Skills;
using DataClass.Structs;
using Unity.Netcode;
using UnityEngine;

public class EntityProps : MonoBehaviour
{
    
    private static ulong allocatable_ID = 0;
    private static LinkedList<ulong> Allocatables = new LinkedList<ulong>();
    // Start is called before the first frame update
    public Props prop;

    
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
            return Fix + Coeff * prop.atk;
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
            return Fix + Coeff * prop.arm;
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
            return Fix + Coeff * prop.arm;
        }
    }
    

    public List<CustomSkill> SkillBases;


    public EntityProps SkillTarget;

    public List<Buff> buffs;

    static EntityProps()
    {
        allocatable_ID = 1;
    }

    public EntityProps(Props _prop)
    {
        prop = _prop;
    }

    private void OnEnable()
    {
        prop.maxHP = 100;
        prop.maxMP = 100;
        prop.maxPosture = 100;
        prop.HP = 100;
        prop.MaxStamina = 100;
        prop.stamina = 100;
        prop.atk = 10;
        prop.arm = 10;
        prop.spd = 10;
        prop.mag = 10;
        prop.Posture = 100;
        buffs = new List<Buff>();
        SkillBases = new List<CustomSkill>();
        
    }

    void Start()
    {
        if (Allocatables.Count == 0)
        {
            prop.ID = allocatable_ID++;
        }
        else
        {
            var temp = Allocatables.First;
            prop.ID = temp.Value;
            Allocatables.Remove(temp);
        }
        //SkillBases.Add(Resources.Load<CustomSkill>());
        
        // SkillBases.Add(new CustomSkill());
        // SkillBases.Add(new CustomSkill());
        //
        // SkillBases[0].AddDamage(this,10,1);
        //
        // SkillBases[1].AddBuff(this,BuffTypes.arm_down_10p,5f);
        
    }

    // Update is called once per frame
    void Update()
    {

        
    }
    
    public int GetDamage(float value)
    {
        prop.HP -= value;
        return prop.HP <= 0? 0 : 1;
        OnPropUpdateCallback();
    }

    public void TestSkill(int index)
    {
        SkillBases[index].Execute(SkillTarget);
    }

    // public void TestSkill_ATK_UP()
    // {
    //     var a = new CustomSkill();
    //     a.AddBuff(this,BuffTypes.atk_up_10,5f); 
    //     a.Execute(this);
    // }
    //
    // public void TestSkill_ARM_DOWN()
    // {
    //     var a = new CustomSkill();
    //     a.AddBuff(this,BuffTypes.arm_down_10,5f); 
    //     a.Execute(SkillTarget);
    // }
    // public void TestSkill_ATK_WITH_PIERCE()
    // {
    //     var a = new CustomSkill();
    //     a.AddDamage(this,0,1,0.5f); 
    //     a.Execute(SkillTarget);
    // }

    public int GetBuff(Buff buff)
    {
        buffs.Add(buff);
        IEnumerator buffExist()
        {
            yield return new WaitForSeconds(buff.lastTime);
            Debug.Log(buff.Type + "removed");
            
            buffs.Remove(buff);
        }
        OnPropUpdateCallback();
        StartCoroutine(buffExist());
        
        return 0;
    }

    public delegate void PropUpdated();
    public event PropUpdated OnPropUpdateCallback;
}

