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

    public GameObject GameUI; //Canvas of the entire game activity area.
    [Space(15)]
    public GameObject MainMenuBack;
    public GameObject ExitConfirmationCanvas;
    public GameObject SettingsCanvas;
    public GameObject MenuCanvas;
    public GameObject SettingsParallaxButton; //just so we can change the text
    public GameObject MainMenuStart;//for changing the text for subsequent menu opening from Start to Continue
    [Space(10)]
    public GameObject PassTurnButton;
    public GameObject AttackButton;
    public GameObject AbilityButton;

    [Space(15)]
    public GameObject WorldMapCanvas; //for activating it on click
    public GameObject WorldCanvasLevelPrefab; //prefab of a singular icon  on the overmap
    public GameObject SelectedChar;
    [Space(10)]
    public GameObject CombatHighlightObject;

    /// <summary>
    /// Menu Buttons
    /// </summary>

    public void ClickSendPause()
    {
        MainMenuBack.SetActive(true);
        MenuCanvas.SetActive(true);
        GameUI.SetActive(false);
        MainData.SoundManagerRef.PlayClickSound();

    }

    public void ClickStartGame()
    {
        //MainData.MainLoop.SoundManagerComponent.PlayClickSound();
        if (!MainData.MainLoop.gameStarted)
        {

            //MainData.MainLoop.SoundManagerComponent.ChangeSoundtrack(MainData.SoundManagerRef.MainTheme);
        }
        MainMenuStart.GetComponentInChildren<TextMeshProUGUI>().text = "Continue";
        MenuCanvas.SetActive(false);
        MainMenuBack.SetActive(false);
        GameUI.SetActive(true);
        MainData.MainLoop.gameStarted = true;
    }

    public void ClickExitGame()
    {
        MainData.SoundManagerRef.PlayClickSound();
        MenuCanvas.SetActive(false);

        ExitConfirmationCanvas.SetActive(true);
    }




    public void ClickExitYes()
    {
        MainData.SoundManagerRef.PlayClickSound();
        Application.Quit();
    }

    public void ClickExitNo()
    {
        MainData.SoundManagerRef.PlayClickSound();
        ExitConfirmationCanvas.SetActive(false);
        MenuCanvas.SetActive(true);



    }
    public void ClickMenuSettings()
    {
        MenuCanvas.SetActive(false);
        SettingsCanvas.SetActive(true);

    }



    public void ClickSettingsBack()
    {
        MenuCanvas.SetActive(true);
        SettingsCanvas.SetActive(false);



    }

    public void ClickSettingsParallax()
    {
        if (MainData.MainLoop.BackgroundParallaxObject.ParallaxSetting)
        {
            MainData.MainLoop.BackgroundParallaxObject.ParallaxSetting = false;
            SettingsParallaxButton.GetComponentInChildren<TextMeshProUGUI>().text = "Parallax - Off";
        }
        else
        {
            MainData.MainLoop.BackgroundParallaxObject.ParallaxSetting = true;
            SettingsParallaxButton.GetComponentInChildren<TextMeshProUGUI>().text = "Parallax - On";
        }



    }



    /// <summary>
    /// in-game buttons
    /// </summary>


    public void ClickTesting()
    {
        Debug.Log("I HAVE BEEN CLICKED. WHO DARES?");
    }

    public void ClickPassTurn()
    {
        if (MainData.MainLoop.CombatHelperComponent.allHaveActed)
        {
            MainData.MainLoop.PassTurn();
        }
        else
        {
            MainData.MainLoop.EventLoggingComponent.Log("Current turn not finished.");
        }
        
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
                //MainData.SoundManagerRef.PlayFailureSound();
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


    //NOTE - combat buttons are handled in CombatHelper.cs



    IEnumerator darksequence()
    {
        float transparency = 0f;
        while (transparency <= 100f)//quickly fade the loading stuff into view
        {
            transparency += transparencyIncrement / 100;
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
