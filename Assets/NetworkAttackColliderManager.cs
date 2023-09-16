using System.Collections;
using System.Collections.Generic;
using NetworkCharacter;
using UnityEngine;

public class NetworkAttackColliderManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> AttackColliders;
    NetworkCharacterSkillHandler  m_skillHandler;
    private int m_currentCollider = -1;
    internal protected int SwitchCurrentCollider
    {
        get
        {
            return m_currentCollider;
        }
        set
        {
            for(int i=0;i<AttackColliders.Count;i++)
            {
                AttackColliders[i].SetActive(i==value);
            }
            
            m_currentCollider = value;            
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        m_skillHandler = GetComponentInParent<NetworkCharacterSkillHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NormalCombo(int a)
    {
        SwitchCurrentCollider = a;
    }

    public void SkillStart(int SkillID)
    {        
        
        
    }
}
