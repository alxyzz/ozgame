using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceHelper : MonoBehaviour
{

    public GameObject selectedCharAvatar; //we will replace this object's image with the currently selected character's avatar
    public GameObject selectedCharName;
    public GameObject selectedCharTraitDesc;
    public GameObject selectedCharTraitIcon;
    public GameObject selectedCharDescription;
    [Space(10)]
    public GameObject worldmapDescription;
    public GameObject worldmapName;
    [Space(10)]
    public GameObject darkText;
    public float travelMicroDelay;
    public float transparencyIncrement;
    //if we decide wether we should allow characters to have more than one trait, rework this to display the trait icons in a list/row

    // Start is called before the first frame update
    void Start()
    {
        GameManager.uiMan = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator darksequence()
    {
        float transparency = 0f;
        while (transparency <= 100f)//quickly fade the loading stuff into view
        {
            transparency += transparencyIncrement/100;
            Vector4 b = new Vector4(0, 0, 0, transparency);
            darkText.GetComponent<Image>().color = b;
            yield return new WaitForSecondsRealtime(travelMicroDelay);
        }
        yield return new WaitForSecondsRealtime(2f);
        while (transparency >= 0f)
        {
            transparency -= transparencyIncrement / 100;
            Vector4 b = new Vector4(0, 0, 0, transparency);
            darkText.GetComponent<Image>().color = b;
            yield return new WaitForSecondsRealtime(travelMicroDelay);
        }


    }

    public void TravelLoadingSequence()
    {


        StartCoroutine(darksequence());

    }
}
