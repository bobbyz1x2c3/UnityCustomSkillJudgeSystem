using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using TMPro;
using UnityEngine;

public class ClientUIPanel : MonoBehaviour
{
    public static ClientUIPanel Instance;

    private TextMeshProUGUI testTxt;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = Instance == null ? this : Instance;
    }

    void Start()
    {
        testTxt = GameObject.Find("Texttest").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText(string text)
    {
        testTxt.text = text;
    }
    
}
