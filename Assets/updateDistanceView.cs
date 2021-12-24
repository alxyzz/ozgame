using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class updateDistanceView : MonoBehaviour
{

    public TextMeshProUGUI texty;


    // Update is called once per frame
    void Update()
    {
        texty.text = MainData.MainLoop.LevelHelperComponent.distanceWalked.ToString();
    }
}
