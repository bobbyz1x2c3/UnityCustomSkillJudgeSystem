using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkCameraManager : NetworkBehaviour
{
    public Camera _camera;
    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponentInChildren<Camera>();
        
        if (IsOwner && IsClient)
        {
            _camera?.gameObject.SetActive(true);        
        }
        else
        {
            _camera?.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
