using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static ItemHelper;
using static TraitHelper;

public class LevelHelper : MonoBehaviour
{
    //set up level template assets here.
    public GameObject BackgroundCube;
    public AudioSource SoundtrackPlayer;
    [Space(10)]
    public Material DarkForestMat;
    public AudioClip DarkForestSound;
    [Space(10)]
    public Material CatacombsMat;


    // Start is called before the first frame update
    void Start()
    {

            GameManager.backgroundObject = BackgroundCube;
        
        GenerateLevels();
        // GameManager.lvlManager= this; not needed yet
    }

    private void GenerateLevels()
    {
        //Creates level templates, gives them references to assets, puts the level in GameManager's level list.
        //
        //ScriptableObject.CreateInstance("GameLevel");

        //set up level template variables here.
        MapLevel darkforest = new MapLevel("Dark Forest", "A dark, scary forest.", "Travellers would be wise not to tarry along...", 3, "Beasts", 1f, 1f, DarkForestMat, DarkForestSound, false);
        darkforest.soundplayerReference = SoundtrackPlayer;
        darkforest.backgroundReference = BackgroundCube;
        GameManager.levelTemplates.Add(darkforest); //adds the template to the global list



        GameManager.levelsInitDone = true; //to keep track of this, so we don't somehow generate the Overmap before initializing the templates
    }





    public class Merchant
    {
        public string merchantName;
        public string merchantType;

        public List<Item> ItemStock = new List<Item>();
        public Trait soldTrait;


        public Merchant()
        {




        }



        public string getType()
        {
            return merchantType;
        }

        public void GenerateStock()
        {
            string debug = "";
            for (int i = 0; i < GameManager.ShopItemCount; i++)
            {
                Item b = GameManager.allItems[UnityEngine.Random.Range(1, GameManager.allItems.Count + 1)];
                ItemStock.Add(b);//just pick a random item
                debug += b.itemName + ", ";
            }

            soldTrait = GameManager.t1traitList[UnityEngine.Random.Range(1, GameManager.t1traitList.Count + 1)];//just pick a random trait
            debug += "and the trait of " + soldTrait.traitName;
            Debug.Log(this.merchantName + " has generated their stock: " + debug);


        }


    }



    public class MapLevel 
    {
        public bool isCampfire; //wether this tile has a campfire.
        public Merchant localMerchant;
        public string levelName; //name.
        public string levelBlurb; // short flavourful description, perhaps a relevant quote.
        public string levelDescription; //longer description for what it actually does.
        public int roomCount;// todo - make levels with variable rooms you can move to.
        public string EnemyType; //the kind of enemies you can encounter.
        public float startingDifficulty; //the difficulty it starts at.
        public float difficultyIncreasePerRoom; //how much the difficulty increases after every room.


        public int levelRank; //from the beginning to the end, the rank goes from 0 to the maximum amount of levels we decide on.
        public List<MapLevel> previousLevels; //tracks levels that precede this one on the map
        public List<MapLevel> nextLevels; //tracks levels that succeed this one on the map


        public Material levelBackground;
        public AudioClip levelSoundtrack;
        public LevelHelper lvlmanager;

        public GameObject backgroundReference;
        public AudioSource soundplayerReference;


        public MapLevel(string name, string blurb, string desc, int roomcount, string enemyTypes, float startDiff, float diffIncrement, Material background, AudioClip soundtrack, bool Campfire)
        {//class constructor
            this.levelName = name;
            this.levelBlurb = blurb;
            this.levelDescription = desc;
            this.roomCount = roomcount;
            this.EnemyType = enemyTypes;
            this.startingDifficulty = startDiff;
            this.difficultyIncreasePerRoom = diffIncrement;
            this.levelBackground = background;
            this.levelSoundtrack = soundtrack;
            this.isCampfire = Campfire;


        }



        public string GetName()
        {
            return levelName;
        }

        
    }
}
