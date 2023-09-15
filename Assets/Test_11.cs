using System.Collections;
using System.Collections.Generic;
using DataClass.Structs;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class Test_11 : NetworkBehaviour
{
    /*public EntityProps ent;
    public NetworkVariable<ulong> ID;
    //private Dropdown _dropdown;
    public NetworkVariable<ulong> TargetID;
    public static NetworkVariable<List<ulong>> registed = new();

    public static EntityProps[] all_entities;
    // Start is called before the first frame update
    void Start()
    {
        
        
        //_dropdown = GetComponentInChildren<Dropdown>();
        if (IsClient && IsOwner)
        {
            GetIDServerRPC();
        }
        
        
            
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //function name must ends up with "ServerRPC"

    
    [ServerRpc]
    public void UpdateTestServerRPC()
    {
        Debug.Log("RPC called");        
    }

    [ServerRpc]
    public void UpdateTargetServerRPC()
    {
        Targ.Value = Ents.Value[Test_2.instance.dropdown.value];
    }
    [ServerRpc]
    public void GetDamageServerRPC(float dam)
    {
        Targ.Value.GetDamage(dam);
    }

    [ServerRpc]
    public void GetBuffServerRPC(BuffTypes type, float lastTime)
    {
        var buff = new BuffCause(ent,lastTime, type);
        
       // buff.Execute();
    }
    

    [ServerRpc]
    public void GetIDServerRPC()
    {
        ent = GetComponent<EntityProps>();
        ent.prop.ID = NetworkManager.LocalClientId;
        if (!registed.Value.Contains(ent.prop.ID))
        {
            registed.Value.Add(ent.prop.ID);
        }
    }
    public void ChangeValue(int value)
    {
        
    }

    public void refreshAllEntities()
    {
        var b = GameObject.FindGameObjectsWithTag("Player");
        all_entities = new EntityProps[b.Length];
        for (int i = 0;i < b.Length; i++)
        {
            b[i].TryGetComponent<>(out all_entities[i]);
        }
    }
    */
    
}
