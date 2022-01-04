using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class updateDistanceView : MonoBehaviour
{

    public Text texty;


    // Update is called once per frame
    void Update()
    {
        texty.text = Mathf.Round(StaticDataHolder.MainLoop.LevelHelperComponent.distanceWalked).ToString();
    }
}
