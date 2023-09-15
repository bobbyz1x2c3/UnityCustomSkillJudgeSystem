using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CharacterUIPanel : MonoBehaviour
{
    private const int HP_SLIDER_INDEX = 0;
    private const int POSTURE_SLIDER_INDEX = 1;    
    // Start is called before the first frame updat
    [SerializeField]
    private EntityNetWorkProps m_prop;
    [SerializeField]
    private List<Slider> m_Sliders;
    
    void Start()
    {
        m_prop = GetComponentInParent<EntityNetWorkProps>();
        m_Sliders = new(GetComponentsInChildren<Slider>());
        
        m_prop.prop.OnValueChanged += (prev,val) =>
        {
            m_Sliders[HP_SLIDER_INDEX].value = val.HP>0? val.HP / val.maxHP : 0;
            m_Sliders[POSTURE_SLIDER_INDEX].value = val.Posture>0? val.Posture / val.maxPosture : 0;

        };
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        //transform.position = owner.position + Vector3.up * height;
        transform.forward = Camera.main.transform.forward;
        
        //test
    }

    private void OnDestroy()
    {
        /*m_prop.prop.OnValueChanged -= (prev,val) =>
        {
            m_hpSlider[0].value = val.HP / val.maxHP;
        };*/
    }
}
