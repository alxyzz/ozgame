using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EventLogging : MonoBehaviour
{
    public TextMeshProUGUI TMPComponent;

    public void Log(string log)
    {
        TMPComponent.text += log + "\n";
        RefreshTextField();
    }


    public void LogDanger(string log)
    {
        TMPComponent.text += "<color=#ff7575>" + log + "</color>\n";
        RefreshTextField();
    }
    public void LogGray(string log)
    {//used for background info, events, descriptions, etc
        TMPComponent.text += "<color=#BFBFBF>" + log + "</color>\n";
        RefreshTextField();
    }


    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            if (TMPComponent.pageToDisplay > 1)
            {
                TMPComponent.pageToDisplay--;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {


            if (TMPComponent.pageToDisplay < TMPComponent.textInfo.pageCount)
            {
                TMPComponent.pageToDisplay++;
            }
        }
    }


    private void RefreshTextField()
    {
       TMPComponent.pageToDisplay = TMPComponent.textInfo.pageCount;
       
    }


}
