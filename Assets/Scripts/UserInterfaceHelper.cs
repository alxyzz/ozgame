using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static EntitiesDefinition;
using static LevelHelper;

public class UserInterfaceHelper : MonoBehaviour
{
    [Header("references to the objects used for displaying info about current PLAYER character")]
    public Image selectedCharAvatar; //we will replace this object's image with the currently selected character's avatar
    public GameObject selectedCharName;//the name of the current player char
    public GameObject selectedCharTraitDesc;//"The Wrathful/Kind/etc", the trait based title shown after the name
    public Image selectedCharTraitIcon; //the trait's icon - the object/image where the texture is applied
    public GameObject selectedCharDescription; //the description of the character
    [Space(10)]
    [Header("references to the objects used for displaying info about currently TARGETED ENEMY character")]
    public Image selectedEnemyCharAvatar; //we will replace this object's image with the currently selected character's avatar
    public TextMeshProUGUI selectedEnemyCharName;//the name of the targeted enemy char
    public TextMeshProUGUI selectedEnemyCharEnemyType;//"The Wrathful/Kind/etc", the trait based title shown after the name
    //public GameObject selectedEnemyCharTraitIcon;//the trait's icon - the object/image where the texture is applied -- doubtful we're using it for enemies 
    public TextMeshProUGUI selectedEnemyCharDescription;//the description of the character
    [Space(10)]
    [Header("the stuff related to PLAYER party members in the lower part of the UI - the little images + name + health bar")]
    [HideInInspector]
    public CharacterScript PC1;
    public Image firstCharAvatar; //we will replace this object's image with the currently selected character's avatar
    public TextMeshProUGUI firstCharName;//the name of the current player char
    public TextMeshProUGUI firstCharTrait;//the name of the current player char
    public Slider firstHealthBar;//the name of the current player char
    public GameObject firstselectionRectangle; // A REFERENCE TO THE RECTANGLE THAT HOLDS A REFERENCE TO THE CHARACTER'S SCRIPT, WHICH WILL THEN GET CLICKED IF YOU CLICK THE 
    [Space(10)]
    [HideInInspector]
    public CharacterScript PC2;
    public Image secondCharAvatar; //we will replace this object's image with the currently selected character's avatar
    public TextMeshProUGUI secondCharName;//the name of the current player char
    public TextMeshProUGUI secondCharTrait;//the name of the current player char
    public Slider secondHealthBar;//the name of the current player cha
    public GameObject secondselectionRectangle; // A REFERENCE TO THE RECTANGLE THAT HOLDS A REFERENCE TO THE CHARACTER'S SCRIPT, WHICH WILL THEN GET CLICKED IF YOU CLICK TH
    [Space(10)]
    [HideInInspector]
    public CharacterScript PC3;
    public Image thirdCharAvatar; //we will replace this object's image with the currently selected character's avatar
    public TextMeshProUGUI thirdCharName;//the name of the current player char
    public TextMeshProUGUI thirdCharTrait;//the name of the current player char
    public Slider thirdHealthBar;//the name of the current player char
    public GameObject thirdselectionRectangle; // A REFERENCE TO THE RECTANGLE THAT HOLDS A REFERENCE TO THE CHARACTER'S SCRIPT, WHICH WILL THEN GET CLICKED IF YOU CLICK THE 
    [Space(10)]
    [HideInInspector]
    public CharacterScript PC4;
    public Image fourthCharAvatar; //we will replace this object's image with the currently selected character's avatar
    public TextMeshProUGUI fourthCharName;//the name of the current player char
    public TextMeshProUGUI fourthCharTrait;//the name of the current player char
    public Slider fourthHealthBar;//the name of the current player char
    public GameObject fourthselectionRectangle; // A REFERENCE TO THE RECTANGLE THAT HOLDS A REFERENCE TO THE CHARACTER'S SCRIPT, WHICH WILL THEN GET CLICKED IF YOU CLICK THE
    [Space(5)]
    [Header("the stuff related to ENEMY party members in the lower part of the UI - the little images + name + health bar")]
    [Header("NOTE - there are >4, so refresh based on lowest health. so you can just click the buttons to target them")]
    [Space(10)]
    [HideInInspector]
    public CharacterScript NPC1;// NOTE - THESE ARE DEFINED IN EntityDefinition.cs
    public Image firstEnemyCharAvatar; //we will replace this object's image with the currently selected character's avatar
    public TextMeshProUGUI firstEnemyCharName;//the name of the current player char
    public Slider firstEnemyHealthBar;//HEALTH BAR REF
    public GameObject firstEnemyselectionRectangle;//the name of the current player char
    [Space(10)]
    [HideInInspector]
    public CharacterScript NPC2;
    public Image secondEnemyCharAvatar; //we will replace this object's image with the currently selected character's avatar
    public TextMeshProUGUI secondEnemyCharName;//the name of the current player char
    public Slider secondEnemyHealthBar;//HEALTH BAR REF
    public GameObject secondEnemyselectionRectangle;//the name of the current player char
    [Space(10)]
    [HideInInspector]
    public CharacterScript NPC3;
    public Image thirdEnemyCharAvatar; //we will replace this object's image with the currently selected character's avatar
    public TextMeshProUGUI thirdEnemyCharName;//the name of the current player char
    public Slider thirdEnemyHealthBar;//HEALTH BAR REF
    public GameObject thirdEnemyselectionRectangle;//the name of the current player char
    [Space(10)]
    [HideInInspector]
    public CharacterScript NPC4;
    public Image fourthEnemyCharAvatar; //we will replace this object's image with the currently selected character's avatar
    public TextMeshProUGUI fourthEnemyCharName;//the name of the current player char
    public Slider fourthEnemyHealthBar;//HEALTH BAR REF
    public GameObject fourthEnemyselectionRectangle;
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
    [Space(10)]
    public GameObject PCDead1;
    public GameObject PCDead2;
    public GameObject PCDead3;
    public GameObject PCDead4; 

    /// <summary>
    /// refreshes the character tabs
    /// </summary>
    public void RefreshCharacterTabs()
    {
        RefreshViewEnemy();
        RefreshViewPlayer();
        RefreshHealthBarEnemy();
        RefreshHealthBarPlayer();

    }



    public void DisplayTargetedEnemyInfo(CharacterScript Target)
    {//this sets the viewable info for the current targeted character, in the right top part of the bottom UI. it is possible to select one target and hover over another to compare them.
        if (Target.associatedCharacter.charAvatar != null)
        {
            selectedCharAvatar.sprite = Target.associatedCharacter.charAvatar;
        }
        selectedEnemyCharName.text = Target.associatedCharacter.charName;
        if (Target.associatedCharacter.entityDescription != null)
        {
            selectedEnemyCharDescription.text = Target.associatedCharacter.entityDescription;
        }
        
        selectedEnemyCharEnemyType.text = ""; // for now until i actually add the enemy Type thing
    }



    /*
     * public Image selectedEnemyCharAvatar; //we will replace this object's image with the currently selected character's avatar
    public TextMeshProUGUI selectedEnemyCharName;//the name of the targeted enemy char
    public TextMeshProUGUI selectedEnemyCharEnemyType;//"The Wrathful/Kind/etc", the trait based title shown after the name
    //public GameObject selectedEnemyCharTraitIcon;//the trait's icon - the object/image where the texture is applied -- doubtful we're using it for enemies 
    public TextMeshProUGUI selectedEnemyCharDescription;//the description of the character
     * 
     * 
     * 
     * 
     */

    public void RefreshEnemyCharacterView()
    {//grabs the four most damaged enemy characters, or if all are same health, just the first four.
        //refresh this every time the number of enemy characters changes
        if (MainData.livingEnemyParty.Count == 0)
        {
            Debug.LogWarning("RefreshEnemyCharacterView() - livingEnemyParty has no enemies in it.");
            return;
        }
        Debug.LogWarning("RefreshEnemyCharacterView() just ran");
        List<Character> characters = new List<Character>(MainData.livingEnemyParty);
        characters.Sort((x, y) => x.currentHealth.CompareTo(y.currentHealth)); // ascending. swap y and x on the right side for descending. Yes, we are sorting by plain ol health without any ratio because it's better to hit the one with less life and not the 500hp behemoth who has only 100hp left and the game thinks it's equivalent to 25hp max100hp guy. also ratio.
        NPC1 = characters[0].selfScriptRef;
        if (characters.Count > 1)
        {
            NPC2 = characters[1].selfScriptRef;
        }
        if (characters.Count > 2)
        {
            NPC3 = characters[2].selfScriptRef;
        }
        if (characters.Count > 3)
        {
            NPC4 = characters[3].selfScriptRef;
        }
    }


    public void RefreshViewEnemy()
    {//run this after every spawning or death of an enemy
        if (MainData.livingEnemyParty.Count == 0)
        {
            Debug.LogWarning("RefreshEnemyCharacterView() - livingEnemyParty has no enemies in it.");
            return;
        }

        if (NPC1 != null)//checks if it exists
        {
            firstEnemyCharAvatar.sprite = NPC1.associatedCharacter.charAvatar; //if it does, show it in the tabs
            firstEnemyCharName.text = NPC1.associatedCharacter.charName;
        }
        if (NPC2 != null)
        {
            secondEnemyCharAvatar.sprite = NPC2.associatedCharacter.charAvatar;
            secondEnemyCharName.text = NPC2.associatedCharacter.charName;
        }
        if (NPC3 != null)
        {
            thirdEnemyCharAvatar.sprite = NPC3.associatedCharacter.charAvatar;
            thirdEnemyCharName.text = NPC3.associatedCharacter.charName;
        }
        if (NPC4 != null)
        {
            fourthEnemyCharAvatar.sprite = NPC4.associatedCharacter.charAvatar;
            fourthEnemyCharName.text = NPC4.associatedCharacter.charName;
        }



        //this requires RefreshEnemyCharacterView() to run before so the NPC slots are defined. the 4 most damaged ones.








    }

    public void RefreshViewPlayer()
    {//run this after any trait change, death, etc.
        if (PC1 != null)
        {
            firstCharAvatar.sprite = PC1.associatedCharacter.charAvatar;
            firstCharName.text = PC1.associatedCharacter.charName;
            if (PC1.associatedCharacter.charTrait != null)
            {
                firstCharTrait.text = PC1.associatedCharacter.charTrait.traitName;
            }
            else
            {
                firstCharTrait.text = "";
            }
            
        }
        if (PC2 != null)
        {
            secondCharAvatar.sprite = PC2.associatedCharacter.charAvatar;
            secondCharName.text = PC2.associatedCharacter.charName;
            if (PC2.associatedCharacter.charTrait != null)
            {
                secondCharTrait.text = PC2.associatedCharacter.charTrait.traitName;
            }
            else
            {
                secondCharTrait.text = "";
            }
        }
        if (PC3 != null)
        {
            thirdCharAvatar.sprite = PC3.associatedCharacter.charAvatar;
            thirdCharName.text = PC3.associatedCharacter.charName;
            if (PC3.associatedCharacter.charTrait != null)
            {
                thirdCharTrait.text = PC3.associatedCharacter.charTrait.traitName;
            }
            else
            {
                thirdCharTrait.text = "";
            }
        }
        if (PC4 != null)
        {
            fourthCharAvatar.sprite = PC4.associatedCharacter.charAvatar;
            fourthCharName.text = PC4.associatedCharacter.charName;
            if (PC1.associatedCharacter.charTrait != null)
            {
                fourthCharTrait.text = PC4.associatedCharacter.charTrait.traitName;
            }
            else
            {
                fourthCharTrait.text = "";
            }
        }
        
        
        

        
        
        
       
    }

    public void RefreshHealthBarEnemy()
    {//this is small enough and used enough we shouldn't run the whole refresh thing if possible

        if (NPC1 != null)
        {
            firstEnemyHealthBar.value = (NPC1.associatedCharacter.currentHealth / NPC1.associatedCharacter.baseHealth) * 100;
        }
        if (NPC2 != null)
        {
            secondEnemyHealthBar.value = (NPC2.associatedCharacter.currentHealth / NPC2.associatedCharacter.baseHealth) * 100;
        }
        if (NPC3 != null)
        {
            thirdEnemyHealthBar.value = (NPC3.associatedCharacter.currentHealth / NPC3.associatedCharacter.baseHealth) * 100;
        }
        if (NPC4 != null)
        {
            fourthEnemyHealthBar.value = (NPC4.associatedCharacter.currentHealth / NPC4.associatedCharacter.baseHealth) * 100;
        }



        
       
       
        
    }

    public void RefreshHealthBarPlayer()
    {//this is small enough and used enough we shouldn't run the whole refresh thing if possible

        if (PC1 != null)
        {
            firstHealthBar.value = (PC1.associatedCharacter.currentHealth / PC1.associatedCharacter.baseHealth) * 100;
        }
        if (PC2 != null)
        {
            secondHealthBar.value = (PC2.associatedCharacter.currentHealth / PC2.associatedCharacter.baseHealth) * 100;
        }
        if (PC3 != null)
        {
            thirdHealthBar.value = (PC3.associatedCharacter.currentHealth / PC3.associatedCharacter.baseHealth) * 100;
        }
        if (PC4 != null)
        {
            fourthHealthBar.value = (PC4.associatedCharacter.currentHealth / PC4.associatedCharacter.baseHealth) * 100;
        }
        
        
       
        
        RefreshPlayerDeathStatus();
    }


    public void RefreshPlayerDeathStatus()
    {//checks wether the player is dead so it can hide or show the death/inactive overlay
        if (!PC1.associatedCharacter.CheckIfCanAct())
        {
            PCDead1.SetActive(true);
        }
        else
        {
            PCDead1.SetActive(false);
        }
        if (!PC2.associatedCharacter.CheckIfCanAct())
        {
            PCDead2.SetActive(true);
        }
        else
        {
            PCDead2.SetActive(false);
        }
        if (!PC3.associatedCharacter.CheckIfCanAct())
        {
            PCDead3.SetActive(true);
        }
        else
        {
            PCDead3.SetActive(false);
        }
        if (!PC4.associatedCharacter.CheckIfCanAct())
        {
            PCDead4.SetActive(true);
        }
        else
        {
            PCDead4.SetActive(false);
        }

    }


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
