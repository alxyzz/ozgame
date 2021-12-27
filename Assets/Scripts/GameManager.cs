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
   

    // Start is called before the first frame update
    void Start()//loads stuff up
    {


        UserInterfaceHelperComponent.CombatHighlightObject.SetActive(false);
        MainData.MainLoop = this;
        MainData.SoundManagerRef = SoundManagerComponent;

        //call things from here from now on
        //StartLoading(); //blacks screen out
        EventLoggingComponent.TMPComponent.text = "";
        PositionHolderComponent.RegisterEnemySpots();
        PositionHolderComponent.RegisterPlayerSpots();
        
        EntityDefComponent.DefineTraits();
        EntityDefComponent.DefinePC(); //set up characters
        EntityDefComponent.DefineNPC();//define all entities here
        EntityDefComponent.DefineConsumables();
        //LevelHelperComponent.GenerateLevels(); //set up templates
        //LevelHelperComponent.SetupDemoLevel();
        EntityDefComponent.BuildParty();
        PositionHolderComponent.PrepPartyPlaces();

        UserInterfaceHelperComponent.GameUI.SetActive(false);
        UserInterfaceHelperComponent.MainMenuBack.SetActive(true); //opens up the main menu
        UserInterfaceHelperComponent.MenuCanvas.SetActive(true);
        //EntityDefComponent.SpawnEnemyTest();
        


        //compile map data
        //get every child MapIconScript component of mapicon parent object
        //run the LinkMaps() function from each

        //compile 
        //finish up
        //StopLoading(); //restores vision


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




    public void StartCombat()
    {

    }

    public void PassTurn()
    {

        if (CombatHelperComponent.allHaveActed)
        {
            //play new turn sound here
            MainData.turnNumber++;
            Debug.Log("Turn " + MainData.turnNumber.ToString());
            EventLoggingComponent.LogDanger("Start of turn " + MainData.turnNumber.ToString() + ".");
            ApplyEffectToAll(); //burns, poison, etc

            if (MainData.livingEnemyParty.Count > 0) //if there's no enemy there's no need to fight
            {
                inCombat = true;
                CombatHelperComponent.InitiateCombatTurn();

            }
            else
            {
                MainData.turnNumber = 0;

                EventLoggingComponent.Log("All enemies have been vanquished.");
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


    public void ApplyEffectToAll()
    {//applies the status effect, if any, to every creature on map.
        foreach (Character item in MainData.allChars)
        {
            if (item.currentStatusEffect != null)
            {
                
                //what do we do here?
                //easy, just check the type of status effect ( a string )
                //then decide what to do based on what type it is
                switch (item.currentStatusEffect.type)
                {
                    case "poison":
                        //AnimateParticles(EntityDefinition.poisonParticle); we will later have element-specific particles, dunno
                        item.TakeDamage(-1);
                        break;
                    case "burn"://a stronger poisonlike Damage over Time (DOT) effect
                        item.TakeDamage(-5);
                        break;
                    case "heal":
                        item.TakeDamage(-1);
                        break;
                    case "regeneration": //a stronger heal over time effect
                        item.TakeDamage(-5);
                        break;
                    //case "admired": //stat boost while it's on
                    //    item.TakeDamage(-5);
                    //    break;
                    default:
                        Debug.Log("Unknown status effect proc'd on "+ item.charName);
                        break;
                }
                item.currentStatusEffect.turnsRemaining--;
                if (item.currentStatusEffect.turnsRemaining <= 0)
                {
                    item.currentStatusEffect = null;
                }


            }
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
