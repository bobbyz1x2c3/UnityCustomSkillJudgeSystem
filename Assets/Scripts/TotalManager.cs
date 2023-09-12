using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class TotalManager : MonoBehaviour
{
    
    public static EntityNetWorkProps[] all_entities;
    // Start is called before the first frame update
    void Start()
    {
        
        DontDestroyOnLoad(this);
        NetworkManager.Singleton.OnServerStarted += () =>
        {
            Debug.Log("server started");
            refreshAllEntities();
        };
        NetworkManager.Singleton.OnClientConnectedCallback += (id) =>
        {
            Debug.Log("ClientConnected:" + id);
            refreshAllEntities();
        };
        NetworkManager.Singleton.OnClientDisconnectCallback += (id) =>
        {
            Debug.Log("ClientDisconnected:" + id);
            refreshAllEntities();
        };
    }
    public void refreshAllEntities()
    {
        var b = GameObject.FindGameObjectsWithTag("Player");
        all_entities = new EntityNetWorkProps[b.Length];
        for (int i = 0;i < b.Length; i++)
        {
            b[i].TryGetComponent<EntityNetWorkProps>(out all_entities[i]);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartServer()
    {
        if (NetworkManager.Singleton.StartServer())
        {
            Debug.Log("server started succeessfully");
        }
        else
        {
            Debug.Log("server start failed");
        }        
    }

    public void StartClient()
    {
        if (NetworkManager.Singleton.StartClient())
        {
            Debug.Log("client started succeessfully");
        }
        else
        {
            Debug.Log("client start failed");
        }      
    }

    public void StartHost()
    {
        if (NetworkManager.Singleton.StartHost())
        {
            Debug.Log("Host started succeessfully");
        }
        else
        {
            Debug.Log("Host start failed");
        }      
    }
    public void ShutDownServer()
    {
        NetworkManager.Singleton.Shutdown();
    }

    public void ShowAllEntities()
    {
        Debug.Log("the num of all entities:"+all_entities.Length);
        foreach (var VARIABLE in all_entities)
        {
            Debug.Log(VARIABLE.id.Value + "剩余HP："+VARIABLE.prop.Value.HP);
            Test_2.instance.updateEnt();
        }
    }

    public static EntityNetWorkProps GetEntityByID(ulong ID)
    {
        foreach (var VARIABLE in all_entities)
        {
            if (VARIABLE.id.Value == ID)
            {
                return VARIABLE;
            }
        }
        return null;
    }
    
}
