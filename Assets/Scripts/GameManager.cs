using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EntitiesDefinition;
using static LevelHelper;

public class GameManager : MonoBehaviour
{
    public LayerMask IgnoreMe;
    public EntitiesDefinition EntityDefComponent;
    public LevelHelper LevelHelperComponent;
    public SoundManager SoundManagerComponent;
    public PositionHolder PositionHolderComponent;
    public ControlsHelper ControlsHelperComponent;
    public EventLogging EventLoggingComponent;
    public UserInterfaceHelper UserInterfaceHelperComponent;

    // Start is called before the first frame update
    void Start()//loads stuff up
    {

        UserInterfaceHelperComponent.GameUI.SetActive(false);//so i don't have to toggle em whenever i wanna test out something else
        UserInterfaceHelperComponent.MainMenuBack.SetActive(true);
        MainData.MainLoop = this;
        MainData.SoundManagerRef = SoundManagerComponent;


        
        
        //call things from here from now on
        //StartLoading(); //blacks screen out



        LevelHelperComponent.GenerateLevels(); //set up templates
        EntityDefComponent.GenerateTraits();
        EntityDefComponent.GenerateParty(); //set up characters
        EntityDefComponent.GenerateMonsters();
        EntityDefComponent.GenerateConsumables();
        //compile map data
        //setup overmap icons
        //compile 
        //finish up
        //StopLoading(); //restores vision


    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, ~IgnoreMe))
            {
                if (hit.transform.tag == "character")
                {
                    CharacterScript hitChar = hit.transform.gameObject.GetComponent<CharacterScript>();
                    hitChar.GotClicked();
                    Debug.Log("This is a character, this is "+ hit.transform.name);
                }
                else
                {
                    Debug.Log("This isn't a character, this is "+ hit.transform.name);
                }
            }
        }
    }



    public void PassTurn()
    {//player pressed the Next button to let actions play out and let enemies act
        Debug.Log("Turn " + MainData.turnNumber.ToString());
        ApplyEffectToAll(); //burns, poison, etc
        //actions apply immediately upon doing them so it's more pleasant to watch
        DoEnemyActions();
    }



    static void EndOfturn()
    {
        if (MainData.enemyParty.Count == 0)
        {
            //GameLog("You have vanquished the enemies.");

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
                    default:
                        Debug.Log("Unknown status effect proc'd on "+ item.name);
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
    {//is there anything better than a nice durum kebab with extra scharf/spicy sauce?
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
        RefreshWorldMap();

    }
    static void RefreshWorldMap()
    {
       // ControlsHelperScript.BuildWorldCanvas();


    }
    public static void DoEnemyActions()
    {//do these after player acts

    }

    public void GameLog(string text)
    {
        //ControlsHelperScript.text += "\n" + text;

    }
}
