using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static EntitiesDefinition;
using static LevelHelper;
using static TraitHelper;

public static class StaticDataHolder
{
    public static GameManager MainLoop;



    public static AudioSource SoundtrackPlayer;
    [Space(10)]
    public static Material MatCatacombs;
    public static DesignerTuningInterface itemManager; //script that allows easy value tweaking for designers


    public static PositionHolder references;


    //controls
    public static bool controlsEnabled;

    public static bool levelsInitDone = false;//todo - loading time
    //todo - intro with a short story

    //sound
    public static SoundManager SoundManagerRef;

    //game state
    public static int turnNumber;
    public static int gold;

    //levels


    public static MapLevel currentLevel; //current level duh sillypants


    //public static OvermapGenerator overmapGeneratorRef; //the overmap generator object, it assigns itself at start
    //public static OverMap gameOvermap;
    public static List<MapIconScript> mapIcons = new List<MapIconScript>(); //all map icons in the game
    public static MapIconScript currentMapIcon; 

    public static MapLevel startingLevel;
    public static MapLevel finalLevel;


    //character storage
    public static Dictionary<int, Character> CharacterDict = new Dictionary<int, Character>();


    //party
    public static List<Character> allChars = new List<Character>(); //all chars on current level

    public static List<Character> livingPlayerParty = new List<Character>(); //all living player party chars
    public static List<Character> deadPlayerParty = new List<Character>(); //all dead player party chars
    public static List<GameObject> playerPartyMemberObjects = new List<GameObject>();

    public static List<Character> livingEnemyParty = new List<Character>(); //all enemy chars
    public static List<Character> deadEnemyParty = new List<Character>(); //all enemy chars

    public static List<GameObject> usedEnemyPartyMemberObjects = new List<GameObject>();
    public static List<GameObject> freeEnemyPartyMemberObjects = new List<GameObject>();






    //dicts
    public static Dictionary<string,Item> allConsumables = new Dictionary<string, Item>(); // ALL possible items
    public static Dictionary<string, Item> allEquipment = new Dictionary<string, Item>(); // ALL possible items

    public static Dictionary<string, Trait> traitList = new Dictionary<string, Trait>(); //all possible traits
    public static Dictionary<string, Trait> t1traitList = new Dictionary<string, Trait>(); //all possible tier one traits
    public static Dictionary<string, Trait> t2traitList = new Dictionary<string, Trait>(); //all possible tier two traits

    public static Dictionary<string, MapLevel> levelTemplates = new Dictionary<string, MapLevel>(); //all level templates

    public static Dictionary<string, Character> characterTypes = new Dictionary<string, Character>(); //all chars on current level



    public static GameObject GetRandomEnemySpot()
    {


        return freeEnemyPartyMemberObjects[Random.Range(1, freeEnemyPartyMemberObjects.Count+1)];
    }



    //log
    public static TextMeshPro GameLogObject; //the game's log

    //items, inventory, traits
    
    public static List<Item> equipmentInventory = new List<Item>(); //items in possession
    public static List<Item> consumableInventory = new List<Item>(); //items in possession


    public static List<Trait> traitInventory = new List<Trait>(); //traits in player inventory
    


    //shop/merchant
    public static int ShopItemCount = 3; //the maximum amount of items offered by a shop/merchant

    //make a trait recipe menu + a visibility hiding script based on already tried combinations

    //char dies? dead for rest of the run, traits and items go to inventory

    //also add trait mixing mechanics
    //also! ALLOW TRAIT SWITCHING AT CAMPFIREs - do overmap first


    public static GameObject backgroundObject; //just a reference to the cube that the background is painted on




    


    

    


    
    


    


    




    


    



}
