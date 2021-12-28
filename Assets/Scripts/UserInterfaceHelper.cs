using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static LevelHelper;

public class UserInterfaceHelper : MonoBehaviour
{
    [Header("references to the objects used for displaying info about current PLAYER character")]
    public GameObject selectedCharAvatar; //we will replace this object's image with the currently selected character's avatar
    public GameObject selectedCharName;//the name of the current player char
    public GameObject selectedCharTraitDesc;//"The Wrathful/Kind/etc", the trait based title shown after the name
    public GameObject selectedCharTraitIcon; //the trait's icon - the object/image where the texture is applied
    public GameObject selectedCharDescription; //the description of the character
    [Space(10)]
    [Header("references to the objects used for displaying info about currently TARGETED ENEMY character")]
    public GameObject selectedEnemyCharAvatar; //we will replace this object's image with the currently selected character's avatar
    public GameObject selectedEnemyCharName;//the name of the targeted enemy char
    public GameObject selectedEnemyCharEnemyType;//"The Wrathful/Kind/etc", the trait based title shown after the name
    //public GameObject selectedEnemyCharTraitIcon;//the trait's icon - the object/image where the texture is applied -- doubtful we're using it for enemies 
    public GameObject selectedEnemyCharDescription;//the description of the character
    [Space(10)]
    [Header("the stuff related to PLAYER party members in the lower part of the UI - the little images + name + health bar")]
   
    public GameObject firstCharAvatar; //we will replace this object's image with the currently selected character's avatar
    public GameObject firstCharName;//the name of the current player char
    public GameObject firstHealthBar;//the name of the current player char
    public GameObject firstselectionRectangle; // A REFERENCE TO THE RECTANGLE THAT HOLDS A REFERENCE TO THE CHARACTER'S SCRIPT, WHICH WILL THEN GET CLICKED IF YOU CLICK THE ICON. FOR QUALITY OF LIFE
    [Space(5)]
    public GameObject secondCharAvatar; //we will replace this object's image with the currently selected character's avatar
    public GameObject secondCharName;//the name of the current player char
    public GameObject secondHealthBar;//HEALTH BAR REF
    public GameObject secondselectionRectangle;//the name of the current player char
    [Space(5)]
    public GameObject thirdCharAvatar; //we will replace this object's image with the currently selected character's avatar
    public GameObject thirdCharName;//the name of the current player char
    public GameObject thirdHealthBar;//HEALTH BAR REF
    public GameObject thirdselectionRectangle;//the name of the current player char
    [Space(5)]
    public GameObject fourthCharAvatar; //we will replace this object's image with the currently selected character's avatar
    public GameObject fourthCharName;//the name of the current player char
    public GameObject fourthHealthBar;//HEALTH BAR REF
    public GameObject fourthselectionRectangle;//the name of the current player char
    [Space(5)]
    [Header("the stuff related to ENEMY party members in the lower part of the UI - the little images + name + health bar")]
    [Header("NOTE - there are >4, so refresh based on lowest health. so you can just click the buttons to target them")]
    public GameObject firstEnemyCharAvatar; //we will replace this object's image with the currently selected character's avatar
    public GameObject firstEnemyCharName;//the name of the current player char
    public GameObject firstEnemyHealthBar;//HEALTH BAR REF
    public GameObject firstEnemyselectionRectangle;//the name of the current player char
    [Space(10)]
    public GameObject secondEnemyCharAvatar; //we will replace this object's image with the currently selected character's avatar
    public GameObject secondEnemyCharName;//the name of the current player char
    public GameObject secondEnemyHealthBar;//HEALTH BAR REF
    public GameObject secondEnemyselectionRectangle;//the name of the current player char
    [Space(10)]
    public GameObject thirdEnemyCharAvatar; //we will replace this object's image with the currently selected character's avatar
    public GameObject thirdEnemyCharName;//the name of the current player char
    public GameObject thirdEnemyHealthBar;//HEALTH BAR REF
    public GameObject thirdEnemyselectionRectangle;//the name of the current player char
    [Space(10)]
    public GameObject fourthEnemyCharAvatar; //we will replace this object's image with the currently selected character's avatar
    public GameObject fourthEnemyCharName;//the name of the current player char
    public GameObject fourthEnemyHealthBar;//HEALTH BAR REF
    public GameObject fourthEnemyselectionRectangle;//the name of the current player char
    [Space(10)]
    [Header("references to consumable slots - this is just so you can change their sprite based on what item is in that slot")]
    public GameObject ConsumableSlot1;
    public GameObject ConsumableSlot2;
    public GameObject ConsumableSlot3;
    [Space(10)]
    public GameObject worldmapDescription;
    public GameObject worldmapName;
    [Space(10)]
    [Header("loading stuff - unused for now")]
    public GameObject darkText;
    public float travelMicroDelay;
    public float transparencyIncrement;
    [Header("Canvas of the entire game activity area")]
    public GameObject GameUI; 
    [Space(15)]
    [Header("various menu canvases")]
    public GameObject MainMenuBack;
    public GameObject ExitConfirmationCanvas;
    public GameObject SettingsCanvas;
    public GameObject MenuCanvas;
    public GameObject SettingsParallaxButton; //just so we can change the text
    public GameObject MainMenuStart;//for changing the text for subsequent menu opening from Start to Continue
    [Space(10)]
    [Header("ingame buttons, dealt with in UserInterfaceHelper usually by calling a method there on click, for some in CombatHelper")]
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
