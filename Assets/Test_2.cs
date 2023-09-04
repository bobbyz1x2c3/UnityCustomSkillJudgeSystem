using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Netcode;

public class Test_2 : MonoBehaviour
{
    public static Test_2 instance;
    
    public TMP_Dropdown  dropdown;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        dropdown = GetComponentInChildren<TMP_Dropdown>();
    }

    public void updateEnt()
    {
        dropdown.options.Clear();
        foreach (var VARIABLE in TotalManager.all_entities)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData(VARIABLE.id.Value.ToString()));
        }
    }

    public void AttackBtnCallback()
    {
        
        Debug.Log("selected id:"+dropdown.value);
        if (dropdown.value < 0) return;
        var z = TotalManager.GetEntityByID(NetworkManager.Singleton.LocalClientId);
        var a = TotalManager.GetEntityByID((ulong)dropdown.value);
        if(z != null && a != null)
        {
            z.AttackRequestServerRPC((ulong)dropdown.value);
        }
    }
    
}
