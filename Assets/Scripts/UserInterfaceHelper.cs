﻿using System.Collections;
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
    public Image NPC1Avatar; //we will replace this object's image with the currently selected character's avatar
    public TextMeshProUGUI NPC1Name;//the name of the current player char
    public Slider NPC1HPbar;//HEALTH BAR REF
    public GameObject firstEnemyselectionRectangle;//the name of the current player char
    [Space(10)]
    [HideInInspector]
    public CharacterScript NPC2;
    public Image NPC2Avatar; //we will replace this object's image with the currently selected character's avatar
    public TextMeshProUGUI NPC2Name;//the name of the current player char
    public Slider NPC2HPbar;//HEALTH BAR REF
    public GameObject secondEnemyselectionRectangle;//the name of the current player char
    [Space(10)]
    [HideInInspector]
    public CharacterScript NPC3;
    public Image NPC3Avatar; //we will replace this object's image with the currently selected character's avatar
    public TextMeshProUGUI NPC3Name;//the name of the current player char
    public Slider NPC3HPbar;//HEALTH BAR REF
    public GameObject thirdEnemyselectionRectangle;//the name of the current player char
    [Space(10)]
    [HideInInspector]
    public CharacterScript NPC4;
    public Image NPC4Avatar; //we will replace this object's image with the currently selected character's avatar
    public TextMeshProUGUI NPC4Name;//the name of the current player char
    public Slider NPC4HPbar;//HEALTH BAR REF
    public GameObject fourthEnemyselectionRectangle;
    [Space(15)]
    [Header("enemy miniview parent objects")]
    public GameObject enemydata1;
    public GameObject enemydata2;
    public GameObject enemydata3;
    public GameObject enemydata4;
    [Space(5)]
    [Header("Health bar numbers - enemy")]
    public TextMeshProUGUI NPC1nmbr;
    public TextMeshProUGUI NPC2nmbr;
    public TextMeshProUGUI NPC3nmbr;
    public TextMeshProUGUI NPC4nmbr;
    [Header("Health bar numbers - player")]
    public TextMeshProUGUI PC1nmbr;
    public TextMeshProUGUI PC2nmbr;
    public TextMeshProUGUI PC3nmbr;
    public TextMeshProUGUI PC4nmbr;



    [Space(15)]
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
    }



    public void DisplayTargetedEnemyInfo(CharacterScript Target = null)
    {//this sets the viewable info for the current targeted character, in the right top part of the bottom UI. it is possible to select one target and hover over another to compare them.
        if (Target == null)
        {
            selectedCharAvatar.sprite = null;
            selectedEnemyCharName.text = "";
            selectedEnemyCharDescription.text = "";
            selectedEnemyCharEnemyType.text = "";
        }

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
    private void ReferenceEnemiesForDisplay()
    {//grabs the four most damaged enemy characters, or if all are same health, just the first four.
        //refresh this every time the number of enemy characters changes
        //runs in RefreshEnemyViewDat

        if (MainData.livingEnemyParty.Count < 1)
        {
            SetActiveEnemyTabs(0);
            Debug.LogWarning("RefreshEnemyCharacterView() - livingEnemyParty has no enemies in it.");
            return;
        }
        //NPC1 = null;
        //NPC2 = null;
        //NPC3 = null;
        //NPC4 = null;
        Debug.LogWarning("RefreshEnemyCharacterView() just ran");
        List<Character> characters = new List<Character>(MainData.livingEnemyParty);

        characters.Sort((x, y) => x.currentHealth.CompareTo(y.currentHealth));
        // ascending. swap y and x on the right side for descending. Yes, we are sorting by plain ol health without any ratio because it's better to hit the one with less life and not the 500hp behemoth who has only 100hp left and the game thinks it's equivalent to 25hp max100hp guy. also ratio.

        //MainData.MainLoop.EventLoggingComponent.LogGray("There are " + characters.Count + " enemy characters.");
        switch (characters.Count)
        {
            case 0:
                //this should never happen 
                SetActiveEnemyTabs(0);
                break;
            case 1:
                NPC1 = characters[0].selfScriptRef;
                SetActiveEnemyTabs(1);
                break;
            case 2:
                SetActiveEnemyTabs(2);
                NPC1 = characters[0].selfScriptRef;
                NPC2 = characters[1].selfScriptRef;
                break;
            case 3:
                SetActiveEnemyTabs(3);
                NPC1 = characters[0].selfScriptRef;
                NPC2 = characters[1].selfScriptRef;
                NPC3 = characters[2].selfScriptRef;
                break;
            default: //any case other than 0 1 2 3 and 4 is automatically > 3 so yeah
                SetActiveEnemyTabs(4);
                NPC1 = characters[0].selfScriptRef;
                NPC2 = characters[1].selfScriptRef;
                NPC3 = characters[2].selfScriptRef;
                NPC4 = characters[3].selfScriptRef;
                break;
        }
        RefreshHealthBarEnemy();
    }
    private void SetActiveEnemyTabs(int amount)
    {
        //1 - mess with first one
        //2 - mess with first+second
        //3 - mess with 1,2,3
        //4 - mess with all 4


        switch (amount)
        {
            case 0:// hide all
                enemydata1.SetActive(false);
                enemydata2.SetActive(false);
                enemydata3.SetActive(false);
                enemydata4.SetActive(false);
                break;
            case 1://show first
                enemydata1.SetActive(true);
                enemydata2.SetActive(false);
                enemydata3.SetActive(false);
                enemydata4.SetActive(false);
                break;
            case 2://show 1+second
                enemydata1.SetActive(true);
                enemydata2.SetActive(true);
                enemydata3.SetActive(false);
                enemydata4.SetActive(false);
                break;
            case 3://show 1,2+third
                enemydata1.SetActive(true);
                enemydata2.SetActive(true);
                enemydata3.SetActive(true);
                enemydata4.SetActive(false);
                break;
            case 4://show 1,2,3+fourth
                enemydata1.SetActive(true);
                enemydata2.SetActive(true);
                enemydata3.SetActive(true);
                enemydata4.SetActive(true);
                break;

        }

    }
    public void RefreshViewEnemy()
    {//run this after every spawning or death of an enemy
        if (MainData.livingEnemyParty.Count == 0)
        {
            Debug.LogWarning("RefreshEnemyCharacterView() - livingEnemyParty has no enemies in it.");
            NPC1HPbar.gameObject.SetActive(false);
            NPC2HPbar.gameObject.SetActive(false);
            NPC3HPbar.gameObject.SetActive(false);
            NPC4HPbar.gameObject.SetActive(false);

            NPC1Avatar.gameObject.SetActive(false);
            NPC2Avatar.gameObject.SetActive(false);
            NPC3Avatar.gameObject.SetActive(false);
            NPC4Avatar.gameObject.SetActive(false);

            NPC1HPbar.gameObject.SetActive(false);
            NPC2HPbar.gameObject.SetActive(false);
            NPC3HPbar.gameObject.SetActive(false);
            NPC4HPbar.gameObject.SetActive(false);

            NPC1Avatar.gameObject.SetActive(false);
            NPC2Avatar.gameObject.SetActive(false);
            NPC3Avatar.gameObject.SetActive(false);
            NPC4Avatar.gameObject.SetActive(false);






            //health numbers
            NPC1nmbr.gameObject.SetActive(false);
            NPC2nmbr.gameObject.SetActive(false);
            NPC3nmbr.gameObject.SetActive(false);
            NPC4nmbr.gameObject.SetActive(false);

            //PC1nmbr.gameObject.SetActive(false);
            //PC2nmbr.gameObject.SetActive(false);
            //PC3nmbr.gameObject.SetActive(false);
            //PC4nmbr.gameObject.SetActive(false);
            return;
        }
        Debug.LogWarning("RefreshEnemyCharacterView() ran.");
        ReferenceEnemiesForDisplay();
        if (NPC1 != null && NPC1.associatedCharacter != null)//checks if it exists
        {
            NPC1Avatar.gameObject.SetActive(true);
            NPC1Avatar.sprite = NPC1.associatedCharacter.charAvatar; //if it does, show it in the tabs
            NPC1Name.text = NPC1.associatedCharacter.charName;
            NPC1HPbar.gameObject.SetActive(true);
            NPC1nmbr.gameObject.SetActive(true);
        }
        else
        {
            NPC1Avatar.gameObject.SetActive(false);
        }
        if (NPC2 != null && NPC2.associatedCharacter != null)
        {
            NPC2Avatar.gameObject.SetActive(true);
            NPC2Avatar.sprite = NPC2.associatedCharacter.charAvatar;
            NPC2Name.text = NPC2.associatedCharacter.charName;
            NPC2HPbar.gameObject.SetActive(true);
            NPC2nmbr.gameObject.SetActive(true);
        }
        else
        {
            NPC2Avatar.gameObject.SetActive(false);
        }
        if (NPC3 != null && NPC3.associatedCharacter != null)
        {
            NPC3Avatar.gameObject.SetActive(true);
            NPC3Avatar.sprite = NPC3.associatedCharacter.charAvatar;
            NPC3Name.text = NPC3.associatedCharacter.charName;
            NPC3HPbar.gameObject.SetActive(true);
            NPC3nmbr.gameObject.SetActive(true);
        }
        else
        {
            NPC3Avatar.gameObject.SetActive(false);
        }
        if (NPC4 != null && NPC4.associatedCharacter != null)
        {
            NPC4Avatar.gameObject.SetActive(true);
            NPC4Avatar.sprite = NPC4.associatedCharacter.charAvatar;
            NPC4Name.text = NPC4.associatedCharacter.charName;
            NPC4HPbar.gameObject.SetActive(true);
            NPC4nmbr.gameObject.SetActive(true);
        }
        else
        {
            NPC4Avatar.gameObject.SetActive(false);
        }



    }
    private void RefreshViewPlayer()
    {//run this after any trait change, death, etc.
        if (PC1 != null)
        {
            firstCharAvatar.sprite = PC1.associatedCharacter.charAvatar;
            firstCharName.text = PC1.associatedCharacter.charName;
            PC1.associatedCharacter.HealthBar = firstHealthBar;
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
            PC2.associatedCharacter.HealthBar = secondHealthBar;

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
            PC3.associatedCharacter.HealthBar = thirdHealthBar;
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
            PC4.associatedCharacter.HealthBar = fourthHealthBar;
            if (PC1.associatedCharacter.charTrait != null)
            {
                fourthCharTrait.text = PC4.associatedCharacter.charTrait.traitName;
            }
            else
            {
                fourthCharTrait.text = "";
            }
        }



        RefreshPlayerDeathStatus();




    }
    private void RefreshHealthBarEnemy()
    {//this is small enough and used enough we shouldn't run the whole refresh thing if possible
        //this is only used when we get a new character to display or a character dies.
        //the health bar value is changed when hit, in the Character class' TakeDamageFromCharacter
        foreach (Character item in MainData.livingEnemyParty)
        {
            item.HealthBar = null;
        }//we do this so we don't have unwanted references from 

        if (NPC1 != null)
        {
            if (NPC1.associatedCharacter != null)
            {
                if (!NPC1.associatedCharacter.isDead)
                {


                    NPC1.associatedCharacter.HealthBar = NPC1HPbar;
                    NPC1HPbar.maxValue = NPC1.associatedCharacter.baseHealth;
                    NPC1HPbar.value = NPC1.associatedCharacter.currentHealth;
                }
                else
                {
                    NPC1HPbar.value = 0f;
                }
            }
            else
            {
                NPC1HPbar.value = 0f;
            }


        }

        if (NPC2 != null)
        {
            if (NPC2.associatedCharacter != null)
            {
                if (!NPC2.associatedCharacter.isDead)
                {
                    NPC2.associatedCharacter.HealthBar = NPC2HPbar;
                    NPC2HPbar.maxValue = NPC2.associatedCharacter.baseHealth;
                    NPC2HPbar.value = NPC2.associatedCharacter.currentHealth;
                }
                else
                {
                    NPC2HPbar.value = 0f;
                }
            }
            else
            {
                NPC2HPbar.value = 0f;
            }


        }
        if (NPC3 != null)
        {
            if (NPC3.associatedCharacter != null)
            {
                if (!NPC3.associatedCharacter.isDead)
                {
                    NPC3.associatedCharacter.HealthBar = NPC3HPbar;
                    NPC3HPbar.maxValue = NPC3.associatedCharacter.baseHealth;
                    NPC3HPbar.value = NPC3.associatedCharacter.currentHealth;
                }
                else
                {
                    NPC3HPbar.value = 0f;
                }
            }
            else
            {
                NPC3HPbar.value = 0f;
            }


        }
        if (NPC4 != null)
        {
            if (NPC4.associatedCharacter != null)
            {
                if (!NPC4.associatedCharacter.isDead)
                {
                    NPC4.associatedCharacter.HealthBar = NPC4HPbar;
                    NPC4HPbar.maxValue = NPC4.associatedCharacter.baseHealth;
                    NPC4HPbar.value = NPC4.associatedCharacter.currentHealth;
                }
                else
                {
                    NPC4HPbar.value = 0f;
                }

            }
            else
            {
                NPC4HPbar.value = 0f;
            }


        }
    }
    public void RefreshHealthBarPlayer()
    {

        if (PC1 != null)
        {
            PC1.associatedCharacter.HealthBar = firstHealthBar;
            firstHealthBar.maxValue = PC1.associatedCharacter.baseHealth;
            firstHealthBar.value = PC1.associatedCharacter.currentHealth;
        }
        else
        {
            Debug.Log("disabled p1 healthbar");
            firstHealthBar.gameObject.SetActive(false);
        }
        //
        if (PC2 != null)
        {
            PC2.associatedCharacter.HealthBar = secondHealthBar;
            secondHealthBar.maxValue = PC2.associatedCharacter.baseHealth;
            secondHealthBar.value = PC2.associatedCharacter.currentHealth;

        }
        else
        {
            secondHealthBar.gameObject.SetActive(false);
        }
        //
        if (PC3 != null)
        {
            PC3.associatedCharacter.HealthBar = thirdHealthBar;
            thirdHealthBar.maxValue = PC3.associatedCharacter.baseHealth;
            thirdHealthBar.value = PC3.associatedCharacter.currentHealth;

        }
        else
        {
            thirdHealthBar.gameObject.SetActive(false);
        }
        //
        if (PC4 != null)
        {
            PC4.associatedCharacter.HealthBar = fourthHealthBar;
            fourthHealthBar.maxValue = PC4.associatedCharacter.baseHealth;
            fourthHealthBar.value = PC4.associatedCharacter.currentHealth;

        }
        else
        {
            fourthHealthBar.gameObject.SetActive(false);
        }
        //



        RefreshPlayerDeathStatus();
    }
    private void RefreshPlayerDeathStatus()
    {//checks wether the player is dead so it can hide or show the death/inactive overlay
        if (!PC1.associatedCharacter.CheckIfCanAct())
        {
            PCDead1.SetActive(true);
            firstHealthBar.gameObject.SetActive(false);
        }
        else
        {
            firstHealthBar.gameObject.SetActive(true);
            PCDead1.SetActive(false);
        }
        if (!PC2.associatedCharacter.CheckIfCanAct())
        {
            PCDead2.SetActive(true);
            secondHealthBar.gameObject.SetActive(false);
        }
        else
        {
            secondHealthBar.gameObject.SetActive(true);
            PCDead2.SetActive(false);
        }
        if (!PC3.associatedCharacter.CheckIfCanAct())
        {
            PCDead3.SetActive(true);
            thirdHealthBar.gameObject.SetActive(false);
        }
        else
        {
            thirdHealthBar.gameObject.SetActive(true);
            PCDead3.SetActive(false);
        }
        if (!PC4.associatedCharacter.CheckIfCanAct())
        {
            PCDead4.SetActive(true);
            fourthHealthBar.gameObject.SetActive(false);
        }
        else
        {
            fourthHealthBar.gameObject.SetActive(true);
            PCDead4.SetActive(false);
        }

    }




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
