﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EntitiesDefinition;
using static LevelHelper;

public class GameManager : MonoBehaviour
{
    public bool gameStarted = false;
    public LayerMask IgnoreMe;
    public EntitiesDefinition EntityDefComponent;
    public LevelHelper LevelHelperComponent;
    public SoundManager SoundManagerComponent;
    public PositionHolder PositionHolderComponent;
    public EventLogging EventLoggingComponent;
    public UserInterfaceHelper UserInterfaceHelperComponent;
    public CombatHelper CombatHelperComponent;
   

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
        EntityDefComponent.DefineTraits();
        EntityDefComponent.DefinePartyMembers(); //set up characters
        EntityDefComponent.DefineMonsters();//define all entities here
        EntityDefComponent.DefineConsumables();
        EntityDefComponent.BuildParty();
        PositionHolderComponent.PrepPartyPlaces();
        //compile map data
        //get every child MapIconScript component of mapicon parent object
        //run the LinkMaps() function from each

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
    {
        //play new turn sound here
        MainData.turnNumber++;
        Debug.Log("Turn " + MainData.turnNumber.ToString());
        ApplyEffectToAll(); //burns, poison, etc
       
        //"Start of turn turnnumber"
        if (MainData.enemyParty.Count >= 1) //if there's no enemy there's no need to fight
        {
            
            CombatHelperComponent.InitiateCombatTurn();
            //HighlightPauseButton();
            PassTurn();
        }
        else
        {
            MainData.turnNumber = 0;
            Debug.Log("All enemies have been vanquished.");
            //Highlight Map button
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
