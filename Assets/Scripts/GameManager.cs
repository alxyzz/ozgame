﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EntityDefiner;
using static LevelHelper;

public class GameManager : MonoBehaviour
{
    public UIParallax BackgroundParallaxObject;
    public bool gameStarted = false;
    [HideInInspector]
    public float Currency = 999f;
    public bool inCombat = false;
    public LayerMask IgnoreMe;
    public EntityDefiner EntityDefComponent;
    public LevelHelper LevelHelperComponent;
    public SoundManager SoundManagerComponent;
    public PositionHolder PositionHolderComponent;
    public EventLogging EventLoggingComponent;
    public UserInterfaceHelper UserInterfaceHelperComponent;
    public CombatHelper CombatHelperComponent;
    public VendorScript VendorScriptComponent;
    public ContentTweakingInterface ContentValueTweaking;

    // Start is called before the first frame update
    void Start()//loads stuff up
    {
        UserInterfaceHelperComponent.CombatHighlightObject.SetActive(false);
        MainData.MainLoop = this;
        MainData.SoundManagerRef = SoundManagerComponent;
        //StartLoading(); 
        EntityDefComponent.LoadSpriteSheets();
        EventLoggingComponent.TMPComponent.text = "";
        PositionHolderComponent.RegisterEnemySpots();
        PositionHolderComponent.RegisterPlayerSpots();

        EntityDefComponent.DefineTraits();
        EntityDefComponent.DefinePC(); //set up Pcharacter templates
        EntityDefComponent.DefineNPC();//set up NPcharacter templates
        EntityDefComponent.DefineConsumables();
        EntityDefComponent.DefineEquipment();
        LevelHelperComponent.GenerateLevels(); //set up templates
        LevelHelperComponent.SetupDemoLevel();

        EntityDefComponent.BuildParty();
        PositionHolderComponent.PrepPartySpots();

        EntityDefComponent.GivePlayerTestConsumables();
        EntityDefComponent.DistributeStartingTraits();

        VendorScriptComponent.LoadSpriteSheets();
        VendorScriptComponent.SetupGraphics();
        VendorScriptComponent.GenerateMerchantInventory();
        VendorScriptComponent.RefreshText();

        UserInterfaceHelperComponent.RefreshCharacterTabs();
        //UserInterfaceHelperComponent.SetCursorTexture();


        UserInterfaceHelperComponent.ToggleFightButtonVisiblity(false);
        ToggleMainMenu(true);//true for visible, false for not visible
        DirtyFixStilLSprites();




        //compile map data
        //get every child MapIconScript component of mapicon parent object
        //run the LinkMaps() function from each

        //compile 
        //finish up
        //StopLoading(); //restores vision


    }

    private void DirtyFixStilLSprites()
    {
        //because we need animation for them to work properly

        Character Lion = MainData.livingPlayerParty.Find(e => e.charType == "lion");
        Lion.selfScriptRef.spriteRenderer.sprite = EntityDefComponent.lionsSprite;
        Character Dorothy = MainData.livingPlayerParty.Find(e => e.charType == "dorothy");
        Dorothy.selfScriptRef.spriteRenderer.sprite = EntityDefComponent.dorothyStillTest;
    }


    private void ToggleMainMenu(bool togg)//true for visible, false for not visible
    {
        UserInterfaceHelperComponent.GameUI.SetActive(!togg);
        UserInterfaceHelperComponent.MainMenuBack.SetActive(togg); //opens up the main menu
        UserInterfaceHelperComponent.MenuCanvas.SetActive(togg);
    }

    public void Update()
    {
        //if (Input.GetMouseButtonDown(0)){
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;

        //    if (Physics.Raycast(ray, out hit, ~IgnoreMe))
        //    {
        //        if (hit.transform.tag == "character")
        //        {
        //            CharacterScript hitChar = hit.transform.gameObject.GetComponent<CharacterScript>();
        //            hitChar.GotClicked();
        //            Debug.Log("This is a character, this is "+ hit.transform.name);
        //        }
        //        else
        //        {
        //            Debug.Log("This isn't a character, this is "+ hit.transform.name);
        //        }
        //    }
        //}

        //if (Input.GetKeyDown(KeyCode.Mouse0)) 
        //{

        //    CombatHelperComponent.HighlightCheck();

        //}
    }






    public void PassTurn()
    {
        StopAllCoroutines();
        List<Character> clone = new List<Character>(MainData.allChars);
        foreach (Character item in clone)
        {
            item.HandleListsUponDeath();
        }

        if (MainData.livingEnemyParty.Count < 1)
        {
            CombatHelperComponent.EndCombat();
            StopAllCoroutines();
        }

        List<Character> results = new List<Character>();
        //MainData.MainLoop.EventLoggingComponent.LogDanger("results.Count! " + results.Count);
        try
        {
            results = MainData.livingEnemyParty.FindAll(x => x.isDead == false);
        }
        catch (System.Exception)
        {
            MainData.MainLoop.CombatHelperComponent.EndCombat();
            EventLoggingComponent.Log("All enemies have been vanquished.");
            MainData.MainLoop.UserInterfaceHelperComponent.RefreshViewEnemy();
           
            inCombat = false;
            return;
        }
       


        if (CombatHelperComponent.allHaveActed)
        {
            //play new turn sound here
            MainData.turnNumber++;
            Debug.Log("Turn " + MainData.turnNumber.ToString());
            EventLoggingComponent.LogDanger("Start of turn " + MainData.turnNumber.ToString() + ".");
            ProcStatusEffects(); //burns, poison, etc. Ticks down the duration left by one, too

            if (results.Count > 0) //if there's no enemy there's no need to fight
            {
                inCombat = true;
                CombatHelperComponent.InitiateCombatTurn();
                MainData.MainLoop.UserInterfaceHelperComponent.ToggleFightButtonVisiblity(false);
            }
            else
            {
                MainData.turnNumber = 0;

                EventLoggingComponent.Log("All enemies have been vanquished.");
                MainData.MainLoop.UserInterfaceHelperComponent.RefreshViewEnemy();
                inCombat = false;
            }
        }
        else
        {
            EventLoggingComponent.Log("Cannot pass the turn; Some characters still have to move.");
        }

    }


    public void ProcStatusEffects()
    {//applies the status effect, if any, to every creature on map.
        foreach (Character item in MainData.allChars)
        {
            item.StatusEffectProc();
        }

    }



    // Update is called once per frame


    public void Travel(MapLevel ToThisLevel)
    {//im hungry
        if (MainData.currentLevel == null)
        {
            Debug.Log("currentLevel is Null.");
        }
        //SoundManagerRef.StopSoundtrack();
        MainData.currentLevel = ToThisLevel;
        if (MainData.currentLevel != null)
        {
            Debug.Log("currentLevel is not Null anymore.");
        }
        //backgroundObject.GetComponent<Renderer>().material = currentLevel.levelBackgroundMaterial;
        //SoundManagerRef.ChangeSoundtrack(currentLevel.levelSoundtrack);
        //SoundManagerRef.StartSoundtrack();
        //currentLevel.visited = true;

        //black out screen whiles setting up things.





        RefreshWorldMap();

    }




    static void RefreshWorldMap()
    {
        // ControlsHelperScript.BuildWorldCanvas();


    }

    public void GameLog(string text)
    {
        //ControlsHelperScript.text += "\n" + text;

    }
}
