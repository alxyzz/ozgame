using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static Entity;
using static TraitHelper;

public class LevelHelper
{
    //set up level template assets here.
    public Material testmat;
    public AudioClip testsound;
    public Material townBackground;
    public AudioClip townSoundTrack;
    public Material mushroomForestBackground;
    public AudioClip mushroomForestSoundTrack;







    // 

    public void GenerateLevels()
    {
        //Creates level templates, gives them references to assets, puts the level in GameManager's level list.
        //
        //ScriptableObject.CreateInstance("GameLevel");

        //set up level template variables here.

        MapLevel darkForest = new MapLevel("Dark Forest", "A dark, very scary forest.", "Travellers would be wise to hurry along..", 3, "Beasts", 1f, 1f, testmat, testsound, false);
        Storagestuff.levelTemplates.Add("darkforest", darkForest); //adds the template to the global list

        MapLevel town = new MapLevel("Abandoned Town", "This town is empty...", "It smells weird.", 3, "Doppelgangers", 1f, 1f, testmat, testsound, false);
        Storagestuff.levelTemplates.Add("town", town);



        Storagestuff.levelsInitDone = true; //to keep track of this, so we don't somehow generate the Overmap before initializing the templates
    }


    private MapLevel GetRandomLevel()
    {
        List<string> keyList = new List<string>(Storagestuff.levelTemplates.Keys);
        string randomKey = keyList[UnityEngine.Random.Range(0, keyList.Count + 1)];
        Debug.Log("Fetched random level: " + randomKey);
        return Storagestuff.levelTemplates[randomKey];
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
            for (int i = 0; i < Storagestuff.ShopItemCount; i++)
            {
                Item b = Storagestuff.MainLoop.EntityLoader.FetchRandomItem();
                ItemStock.Add(b);//just pick a random item
                debug += b.itemName + ", ";
            }
            
            soldTrait = Storagestuff.MainLoop.EntityLoader.FetchRandomTrait();
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

        public bool visited;

        public int levelRank; //from the beginning to the end, the rank goes from 0 to the maximum amount of levels we decide on.
        public List<MapLevel> previousLevels; //tracks levels that precede this one on the map
        public List<MapLevel> nextLevels; //tracks levels that succeed this one on the map

        public Material levelBackgroundMaterial;
        public AudioClip levelSoundtrack;

        public MapLevel(string name, string blurb, string desc, int roomcount, string enemyTypes, float startDiff, float diffIncrement, Material background, AudioClip soundtrack, bool Campfire)
        {//class constructor
            this.levelName = name;
            this.levelBlurb = blurb;
            this.levelDescription = desc;
            this.roomCount = roomcount;
            this.EnemyType = enemyTypes;
            this.startingDifficulty = startDiff;
            this.difficultyIncreasePerRoom = diffIncrement;
            this.levelBackgroundMaterial = background;
            this.levelSoundtrack = soundtrack;
            this.isCampfire = Campfire;


        }



        public string GetName()
        {
            return levelName;
        }

        
    }
}
