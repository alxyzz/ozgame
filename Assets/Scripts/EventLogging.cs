using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EventLogging : MonoBehaviour
{
    public TextMeshProUGUI TMPComponent;
    [Header("Cool Text Display")]
    public GameObject GradualTextDisplayObject;
    public TextMeshProUGUI GradualTextDisplayText;
    public float textDisplayDelayPerLetter;


    //private bool isDisplayingAlready = false;
    //public void LogDisplayGradualText(string text)
    //{
    //    if (isDisplayingAlready)
    //    {
    //        return;
    //    }
    //    GradualTextDisplayText.text = "";
    //    isDisplayingAlready = true;
    //    StartCoroutine(GradualDisplay(text));
    //}



    //IEnumerator GradualDisplay(string text)
    //{
    //    GradualTextDisplayObject.SetActive(true);
    //    for (int i = 0; i < text.Length-1; i++)
    //    {
    //        GradualTextDisplayText.text += text[i]; 
    //        yield return new WaitForSecondsRealtime(textDisplayDelayPerLetter);
    //    }
    //    yield return new WaitForSecondsRealtime(1f);
    //    GradualTextDisplayObject.SetActive(false);
    //    isDisplayingAlready = false;

    //}


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
