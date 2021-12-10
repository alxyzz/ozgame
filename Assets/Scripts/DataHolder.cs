using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Entity;
using static LevelHelper;
using static TraitHelper;

public static class DataHolder
{
    public static GameManager MainLoop;



    public static AudioSource SoundtrackPlayer;
    public static Sprite mapCampfireSprite;
    public static Sprite MhopSprite;
    public static Sprite MossSprite;
    [Space(10)]
    public static Material MatDarkForest;
    public static AudioClip SoundDarkForest;
    [Space(10)]
    public static Material MatCatacombs;



    public static RefHolder references;


    //controls
    public static ControlsHelper ControlsHelperRef;

    public static bool levelsInitDone = false;//todo - loading time
    //todo - intro with a short story

    //sound
    public static SoundManager SoundManagerRef;

    //User Interface
    public static UserInterfaceHelper uiMan;


    //game state
    private static int turnNumber;
    private static int gold;
    private static float partyXP; //implement scaling for levelling up if we decide on actually having it. quadratic or linear?

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

    public static List<Character> playerParty = new List<Character>(); //all player party chars
    public static List<GameObject> playerPartyMemberObjects = new List<GameObject>();

    public static List<Character> enemyParty = new List<Character>(); //all enemy chars
    public static List<GameObject> enemyPartyMemberObjects = new List<GameObject>();
    //items


    //dicts
    public static Dictionary<string,Item> allConsumables = new Dictionary<string, Item>(); // ALL possible items
    public static Dictionary<string, Item> allEquipment = new Dictionary<string, Item>(); // ALL possible items

    public static Dictionary<string, Trait> traitList = new Dictionary<string, Trait>(); //all possible traits
    public static Dictionary<string, Trait> t1traitList = new Dictionary<string, Trait>(); //all possible tier one traits
    public static Dictionary<string, Trait> t2traitList = new Dictionary<string, Trait>(); //all possible tier two traits

    public static Dictionary<string, MapLevel> levelTemplates = new Dictionary<string, MapLevel>(); //all level templates

    public static Dictionary<string, Character> characterTypes = new Dictionary<string, Character>(); //all chars on current level







    //log
    public static TextMeshPro GameLogObject; //the game's log

    //items, inventory, traits
    public static DesignerTuningInterface itemManager; //script that allows easy value tweaking for designers
    public static List<Item> inventory = new List<Item>(); //items in possession
    

    public static List<Trait> traitInventory = new List<Trait>(); //traits in player inventory
    


    //shop/merchant
    public static int ShopItemCount = 3; //the maximum amount of items offered by a shop/merchant

    //make a trait recipe menu + a visibility hiding script based on already tried combinations

    //char dies? dead for rest of the run, traits and items go to inventory

    //also add trait mixing mechanics
    //also! ALLOW TRAIT SWITCHING AT CAMPFIREs - do overmap first


    public static GameObject backgroundObject; //just a reference to the cube that the background is painted on






    public static void GameLog(string text)
    {
        GameLogObject.text += "\n" + text;

    }


    public static void PassTurn()
    {//player pressed the Next button to let actions play out and let enemies act
        StartOfTurn();
        StatusEffectsProc(); //burns, poison, etc
        DoQueuedActions(); //player character attacks, uses abilities, potions, etc
        DoEnemyActions();
    }


    static void StatusEffectsProc()
    {


    }

    static void StartOfTurn()
    {
        Debug.Log("Turn " + turnNumber.ToString());


    }


    static void EndOfturn()
    {
        if (enemyParty.Count == 0)
        {
            GameLog("You have vanquished the enemies.");

        }


    }
    static void DoQueuedActions()
    {



    }


    static void BuildWorldmap()
    {
        ControlsHelperRef.BuildWorldCanvas();


    }


    public static void Travel(MapLevel ToThisLevel)
    {//is there anything better than a nice durum kebab with extra scharf/spicy sauce?
        if (currentLevel == null)
        {
            Debug.Log("currentLevel is Null.");
        }
        //SoundManagerRef.StopSoundtrack();
        currentLevel = ToThisLevel;
        if (currentLevel != null)
        {
            Debug.Log("currentLevel is not Null anymore.");
        }
        //backgroundObject.GetComponent<Renderer>().material = currentLevel.levelBackgroundMaterial;
        //SoundManagerRef.ChangeSoundtrack(currentLevel.levelSoundtrack);
        //SoundManagerRef.StartSoundtrack();
        //currentLevel.visited = true;
        BuildWorldmap();

    }

   


    public static void DoEnemyActions()
    {//do these after player acts

    }



}
