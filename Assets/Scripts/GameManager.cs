using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static EntityDefiner;
using static LevelHelper;

public class GameManager : MonoBehaviour
{
    public UIParallax BackgroundParallaxObject;
    public bool gameStarted = false;
   
    public float Currency = 0f;
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
    public ContentTweakingInterface TweakingComponent;
    public TrinketMenuHandler InventoryHelperComponent;

    // Start is called before the first frame update
    void Start()//loads stuff up
    {
        UserInterfaceHelperComponent.CombatHighlightObject.SetActive(false);
        MainData.MainLoop = this;
        MainData.SoundManagerRef = SoundManagerComponent;
        //StartLoading(); 
        //EntityDefComponent.LoadSpriteSheets();
        EventLoggingComponent.TMPComponent.text = "";
        PositionHolderComponent.RegisterEnemySpots();
        PositionHolderComponent.RegisterPlayerSpots();
        PositionHolderComponent.RegisterAllSpots();

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
        EntityDefComponent.TestGivePlayerItems();
        EntityDefComponent.DistributeStartingTraits();

        VendorScriptComponent.LoadSpriteSheets();
        VendorScriptComponent.SetupGraphics();
        
        VendorScriptComponent.RefreshText();

        UserInterfaceHelperComponent.RefreshCharacterTabs();
        //UserInterfaceHelperComponent.SetCursorTexture();


        InitiateCharacterAnimation();

        UserInterfaceHelperComponent.ToggleFightButtonVisiblity(false);
        ToggleMainMenu(true);//true for visible, false for not visible




        //compile map data
        //get every child MapIconScript component of mapicon parent object
        //run the LinkMaps() function from each

        //compile 
        //finish up
        //StopLoading(); //restores vision


    }


    public GameObject LostGameScreen;
    public void LostTheGame()
    {
        CombatHelperComponent.EndCombat();
        inCombat = false;
        UserInterfaceHelperComponent.GameUI.SetActive(false);
        foreach (Character item in MainData.allChars)
        {
            item.selfScriptRef.gameObject.SetActive(false);
        }
        LostGameScreen.SetActive(true);




    }


    public void InitiateCharacterAnimation()
    {
        foreach (Character item in MainData.livingPlayerParty)
        {
            Debug.Log("InitiateCharacterAnimation() for player party");
            item.selfScriptRef.SetupIdleAnimAndStart(); //randomized idle phase variation ftw
        }
    }


    //private void DirtyFixStilLSprites()
    //{
    //    Character Lion = MainData.livingPlayerParty.Find(e => e.charType == "lion");
    //    Lion.selfScriptRef.spriteRenderer.sprite = EntityDefComponent.lionStanding;

    //    Character Dorothy = MainData.livingPlayerParty.Find(e => e.charType == "dorothy");
    //    Dorothy.selfScriptRef.spriteRenderer.sprite = EntityDefComponent.dorothyStanding;

    //    Character scarecrow = MainData.livingPlayerParty.Find(e => e.charType == "scarecrow");
    //    scarecrow.selfScriptRef.spriteRenderer.sprite = EntityDefComponent.scarecrowStanding;

    //    Character tinguy = MainData.livingPlayerParty.Find(e => e.charType == "tin_man");
    //    tinguy.selfScriptRef.spriteRenderer.sprite = EntityDefComponent.tinmanStanding;
    //}


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
            EventLoggingComponent.Log("No enemies left.");

            MainData.MainLoop.UserInterfaceHelperComponent.RefreshCharacterTabs();
            inCombat = false;
            return;
        }
       


        if (CombatHelperComponent.allHaveActed)
        {
            //play new turn sound here
            MainData.turnNumber++;
            if (MainData.livingEnemyParty.Count > 0)
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
                CombatHelperComponent.allHaveActed = true;
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
