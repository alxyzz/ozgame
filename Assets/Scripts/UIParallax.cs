using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIParallax : MonoBehaviour
{
    [HideInInspector]
    public bool ParallaxSetting = true;//for setting it off if the player is tired of it
    [HideInInspector]
    public bool ParallaxOn = true;//for setting it off while travelling, through code

    private Vector3 pz;
    private Vector3 StartPos;

    public float modifier;

    void Start()
    {
        StartPos = transform.position;
    }

    void Update()
    {
        if (ParallaxOn && ParallaxSetting)
        {
            pz = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            //pz.z = 0;
            gameObject.transform.position = pz;
            transform.position = new Vector3(StartPos.x - (pz.x * modifier), StartPos.y - (pz.y * modifier), StartPos.z);
        }
        
    }
}
