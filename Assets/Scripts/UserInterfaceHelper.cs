using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    public GameObject GameUI;
    public GameObject MainMenuBack;
    public GameObject MainMenuStart;
    public GameObject MainMenuExit;
    public GameObject MainMenuExitYes;
    public GameObject MainMenuExitSure;
    public GameObject MainMenuExitNo;





    public void ClickTesting()
    {
        Debug.Log("I HAVE BEEN CLICKED. WHO DARES?");
    }



    public void ClickSendPause()
    {
        MainMenuBack.SetActive(true);
        MainMenuStart.SetActive(true);
        MainMenuExit.SetActive(true);
        GameUI.SetActive(false);
        Storagestuff.SoundManagerRef.PlayClickSound();
        
    }

    public void ClickStartGame()
    {

        Storagestuff.SoundManagerRef.PlayClickSound();

        //MainMenuStart.GetComponent<TextMeshProUGUI>().text = "Continue";
        MainMenuStart.SetActive(false);
        MainMenuExit.SetActive(false);
        MainMenuBack.SetActive(false);
        GameUI.SetActive(true);
    }

    public void ClickExitGame()
    {
        Storagestuff.SoundManagerRef.PlayClickSound();
        MainMenuStart.SetActive(false);
        MainMenuExit.SetActive(false);
        MainMenuExitYes.SetActive(true);
        MainMenuExitSure.SetActive(true);
        MainMenuExitNo.SetActive(true);
}

    public void ClickExitYes()
    {
        Storagestuff.SoundManagerRef.PlayClickSound();
        Application.Quit();
    }

    public void ClickExitNo()
    {
        Storagestuff.SoundManagerRef.PlayClickSound();
        MainMenuStart.SetActive(true);
        MainMenuExit.SetActive(true);
        MainMenuExitYes.SetActive(false);
        MainMenuExitSure.SetActive(false);
        MainMenuExitNo.SetActive(false);
    }

    //if we decide wether we should allow characters to have more than one trait, rework this to display the trait icons in a list/row

    // Start is called before the first frame update
    void Start()
    {
        Storagestuff.uiMan = this;
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
