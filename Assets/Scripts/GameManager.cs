using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EntitiesDefinition;
using static LevelHelper;

public class GameManager : MonoBehaviour
{
    public UIParallax BackgroundParallaxObject;
    public bool gameStarted = false;
    public bool inCombat = false;
    public LayerMask IgnoreMe;
    public EntitiesDefinition EntityDefComponent;
    public LevelHelper LevelHelperComponent;
    public SoundManager SoundManagerComponent;
    public PositionHolder PositionHolderComponent;
    public EventLogging EventLoggingComponent;
    public UserInterfaceHelper UserInterfaceHelperComponent;
    public CombatHelper CombatHelperComponent;
    public GameObject backgroundObject;
    public VendorScript VendorScriptComponent;
    public ObjectPooling ObjPooler;

    // Start is called before the first frame update
    void Start()//loads stuff up
    {
        UserInterfaceHelperComponent.CombatHighlightObject.SetActive(false);
        MainData.MainLoop = this;
        MainData.SoundManagerRef = SoundManagerComponent;
        //StartLoading(); 
        UserInterfaceHelperComponent.PopulateUISlotList();
        EntityDefComponent.LoadSpriteSheets();
        EventLoggingComponent.TMPComponent.text = "";
        PositionHolderComponent.RegisterEnemySpots();
        PositionHolderComponent.RegisterPlayerSpots();

        EntityDefComponent.DefineTraits();
        EntityDefComponent.DefinePC(); //set up Pcharacter templates
        EntityDefComponent.DefineNPC();//set up NPcharacter templates
        EntityDefComponent.DefineConsumables();
        LevelHelperComponent.GenerateLevels(); //set up templates
        LevelHelperComponent.SetupDemoLevel();
        EntityDefComponent.BuildParty();
        PositionHolderComponent.PrepPartySpots();
        //GivePlayerTestConsumables();
        UserInterfaceHelperComponent.RefreshCharacterTabs();
        ToggleMainMenu(true);//true for visible, false for not visible





        //compile map data
        //get every child MapIconScript component of mapicon parent object
        //run the LinkMaps() function from each

        //compile 
        //finish up
        //StopLoading(); //restores vision


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
        if (CombatHelperComponent.allHaveActed)
        {
            //play new turn sound here
            MainData.turnNumber++;
            Debug.Log("Turn " + MainData.turnNumber.ToString());
            EventLoggingComponent.LogDanger("Start of turn " + MainData.turnNumber.ToString() + ".");
            ProcStatusEffects(); //burns, poison, etc. Ticks down the duration left by one, too

            if (MainData.livingEnemyParty.Count > 0) //if there's no enemy there's no need to fight
            {
                inCombat = true;
                CombatHelperComponent.InitiateCombatTurn();
            }
            else
            {
                MainData.turnNumber = 0;

                EventLoggingComponent.Log("All enemies have been vanquished.");
                MainData.MainLoop.UserInterfaceHelperComponent.RefreshViewEnemy();
                inCombat = false;
                //PurgeStatusEffects();

                //Highlight Map button
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
