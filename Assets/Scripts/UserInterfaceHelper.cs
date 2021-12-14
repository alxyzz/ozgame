using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static LevelHelper;

public class UserInterfaceHelper : MonoBehaviour
{
    public GameObject selectedCharAvatar; //we will replace this object's image with the currently selected character's avatar
    public GameObject selectedCharName;
    public GameObject selectedCharTraitDesc;
    public GameObject selectedCharTraitIcon;
    public GameObject selectedCharDescription;
    [Space(10)]
    public GameObject ConsumableSlot1;
    public GameObject ConsumableSlot2;
    public GameObject ConsumableSlot3;
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

    [Space(15)]
    public GameObject WorldMapCanvas; //for activating it on click
    public GameObject WorldCanvasLevelPrefab; //prefab of a singular icon  on the overmap
    public GameObject SelectedChar;


    public void ClickTesting()
    {
        Debug.Log("I HAVE BEEN CLICKED. WHO DARES?");
    }


    public void ClickPassTurn()
    {
        MainData.MainLoop.PassTurn();
        //add a click sound here


    }

    public void ClickOvermapLevel(MapLevel clickyyy)
    {
        if (clickyyy != MainData.currentLevel)
        {//we go there if possible
            if (MainData.currentLevel.nextLevels.Contains(clickyyy))
            {//oh yeeeeee

                MainData.SoundManagerRef.PlayClickSound();
            }
            else
            {
                //failure
                MainData.SoundManagerRef.PlayFailureSound();
            }
        }

    }
    public void ClickMapClose()
    {

        WorldMapCanvas.SetActive(false);
        Debug.Log("CLICKED closeOVERMAP BUTTON.");
        MainData.SoundManagerRef.PlayClickSound();
    }
    public void ClickMapOpen()
    {

        WorldMapCanvas.SetActive(true);
        Debug.Log("CLICKED OVERMAP-open BUTTON.");
        MainData.SoundManagerRef.PlayClickSound();
    }
    public void ClickSendPause()
    {
        MainMenuBack.SetActive(true);
        MainMenuStart.SetActive(true);
        MainMenuExit.SetActive(true);
        GameUI.SetActive(false);
        MainData.SoundManagerRef.PlayClickSound();
        
    }

    public void ClickStartGame()
    {
        MainData.SoundManagerRef.PlayClickSound();
        if (!MainData.MainLoop.gameStarted)
        {
            
            MainData.SoundManagerRef.ChangeSoundtrack(MainData.SoundManagerRef.MainTheme);
        }
        MainMenuStart.GetComponentInChildren<TextMeshProUGUI>().text = "Continue";
        MainMenuStart.SetActive(false);
        MainMenuExit.SetActive(false);
        MainMenuBack.SetActive(false);
        GameUI.SetActive(true);
        MainData.MainLoop.gameStarted = true;
    }

    public void ClickExitGame()
    {
        MainData.SoundManagerRef.PlayClickSound();
        MainMenuStart.SetActive(false);
        MainMenuExit.SetActive(false);
        MainMenuExitYes.SetActive(true);
        MainMenuExitSure.SetActive(true);
        MainMenuExitNo.SetActive(true);
}




    public void ClickExitYes()
    {
        MainData.SoundManagerRef.PlayClickSound();
        Application.Quit();
    }

    public void ClickExitNo()
    {
        MainData.SoundManagerRef.PlayClickSound();
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
        MainData.uiMan = this;
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
