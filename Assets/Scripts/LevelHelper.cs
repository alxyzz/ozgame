﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static EntitiesDefinition;
using static TraitHelper;

public class LevelHelper : MonoBehaviour
{
    //set up level template assets here.
    public Material testmat;
    public AudioClip testsound;
    public Material townBackground;
    public AudioClip townSoundTrack;
    public Material mushroomForestBackground;
    public AudioClip mushroomForestSoundTrack;




    [Space(15)]//movement inside the local map stuff
    private float distanceWalked;//shows how much we've physically advanced in the current level
    public float maximumDistance; //maximum distance before the level fades to black and you go on the overmap

    // 











    private List<Tuple<float, List<string>>> GenerateEncounter(int encounterAmt, string type, float distanceBetweenEncounters,int enemyNmbr = 0)
    {
        if (enemyNmbr == 0)
        {
            int enemyAmount = UnityEngine.Random.Range(1, 6);
        }
        else
        {
            int enemyAmount = enemyNmbr;
        }

        string enemyType = type;
       //the encounters can start from around 200f distance, so...
       List<Tuple<float, List<string>>> encounter = new List<Tuple<float, List<string>>>();
        for (int i = 0; i < encounterAmt; i++)
        {
            List<string> b = new List<string>();
            for (int x = 0; x < enemyNmbr; x++)
            {
                b.Add(type);//adds as many strings of that number as required. for each, a mob will be created after that template
            }
            Tuple<float, List<string>> bobert = new Tuple<float, List<string>>(distanceBetweenEncounters, b);
            distanceBetweenEncounters += UnityEngine.Random.Range(80, 151);
            encounter.Add(bobert);
        }


        return encounter;
    }
    









    public void GenerateLevels()
    {
        //Creates level templates, gives them references to assets, puts the level in GameManager's level list.
        //
        //ScriptableObject.CreateInstance("GameLevel");

        //set up level template variables here.





        MapLevel darkForest = new MapLevel("Dark Forest", //the name of the level
                                           "A dark, very scary forest.", //a short blurb that gets shown on entry
                                           "Dangerous things linger where people do not. And people do not linger here for good reason.", //a longer description
                                           3,
                                           "Beasts",
                                           1f,
                                           1f,
                                           testmat,
                                           testsound,
                                           false, GenerateEncounter(3, "evilcrow", 150, 4)
                                           );


        MainData.levelTemplates.Add("darkforest", darkForest); //adds the template to the global list







        //MapLevel town = new MapLevel("Abandoned Town", "This town is empty...", "It smells weird.", 3, "Doppelgangers", 1f, 1f, testmat, testsound, false);
        //MainData.levelTemplates.Add("town", town);



        //MainData.levelsInitDone = true; //to keep track of this, so we don't somehow generate the Overmap before initializing the templates
    }


    private MapLevel GetRandomLevel()
    {
        List<string> keyList = new List<string>(MainData.levelTemplates.Keys);
        string randomKey = keyList[UnityEngine.Random.Range(0, keyList.Count + 1)];
        Debug.Log("Fetched random level: " + randomKey);
        return MainData.levelTemplates[randomKey];
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
            for (int i = 0; i < MainData.ShopItemCount; i++)
            {
                Item b = MainData.MainLoop.EntityDefComponent.FetchRandomItem();
                ItemStock.Add(b);//just pick a random item
                debug += b.itemName + ", ";
            }
            
            soldTrait = MainData.MainLoop.EntityDefComponent.FetchRandomTrait();
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

        public List<Tuple<float, List<string>>> Encounters; //how this works is that given enemy groups can be added to a certain float number, the distance, and when the player's party reaches it, it stops and spawns those enemies.


        public bool visited;

        public int levelRank; //from the beginning to the end, the rank goes from 0 to the maximum amount of levels we decide on.
        public List<MapLevel> previousLevels; //tracks levels that precede this one on the map
        public List<MapLevel> nextLevels; //tracks levels that succeed this one on the map

        public Material levelBackgroundMaterial;
        public AudioClip levelSoundtrack;

        public MapLevel(string name, string blurb, string desc, int roomcount, string enemyTypes, float startDiff, float diffIncrement, Material background, AudioClip soundtrack, bool Campfire, List<Tuple<float,List<string>>> encount)
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
            this.Encounters = encount;

        }



        public string GetName()
        {
            return levelName;
        }

        
    }
}
