using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static EntityDefiner;

public class LevelHelper : MonoBehaviour
{
    //set up level template assets here.

    public AudioClip testsound;
    public AudioSource source;
    public AudioSource sourceC;
    public AudioSource walkingSound;
    public AudioSource mouseClick;
    public AudioClip townSoundTrack;
    public AudioClip mushroomForestSoundTrack;
    //THE IMAGES OF DIFFERENT LEVELS
    //BOSS LEVEL
    [Header("The first four layers which lay UNDER the chars")]
    public Sprite BossEnvLayer1Left;
    public Sprite BossEnvLayer1Mid;
    public Sprite BossEnvLayer1Right;
    [Space(5)]
    public Sprite BossEnvLayer2Left;
    public Sprite BossEnvLayer2Mid;
    public Sprite BossEnvLayer2Right;
    [Space(5)]
    public Sprite BossEnvLayer3Left;
    public Sprite BossEnvLayer3Mid;
    public Sprite BossEnvLayer3Right;
    [Space(5)]
    public Sprite BossEnvLayer4Left;
    public Sprite BossEnvLayer4Mid;
    public Sprite BossEnvLayer4Right;



    [Header("The last two layers which rest ABOVE the chars")]
    public Sprite BossEnvLayer5Left;
    public Sprite BossEnvLayer5Mid;
    public Sprite BossEnvLayer5Right;
    [Space(5)]
    public Sprite BossEnvLayer6Left;
    public Sprite BossEnvLayer6Mid;
    public Sprite BossEnvLayer6Right;

    //NORMAL, FOREST LEVEL
    [Header("The first four layers which lay UNDER the chars")]
    public Sprite EnvLayer1Left;
    public Sprite EnvLayer1Mid;
    public Sprite EnvLayer1Right;
    [Space(5)]
    public Sprite EnvLayer2Left;
    public Sprite EnvLayer2Mid;
    public Sprite EnvLayer2Right;
    [Space(5)]
    public Sprite EnvLayer3Left;
    public Sprite EnvLayer3Mid;
    public Sprite EnvLayer3Right;
    [Space(5)]
    public Sprite EnvLayer4Left;
    public Sprite EnvLayer4Mid;
    public Sprite EnvLayer4Right;



    [Header("The last two layers which rest ABOVE the chars")]
    public Sprite EnvLayer5Left;
    public Sprite EnvLayer5Mid;
    public Sprite EnvLayer5Right;
    [Space(5)]
    public Sprite EnvLayer6Left;
    public Sprite EnvLayer6Mid;
    public Sprite EnvLayer6Right;



    //THE OBJECTS WE ASSIGN THE IMAGE TO
    [Space(15)]
    [Header("The first four OBJECTS which lay UNDER the chars")]
    public Image ObjectEnvLayer1Left;
    public Image ObjectEnvLayer1Mid;
    public Image ObjectEnvLayer1Right;
    [Space(5)]
    public Image ObjectEnvLayer2Left;
    public Image ObjectEnvLayer2Mid;
    public Image ObjectEnvLayer2Right;
    [Space(5)]
    public Image ObjectEnvLayer3Left;
    public Image ObjectEnvLayer3Mid;
    public Image ObjectEnvLayer3Right;
    [Space(5)]
    public Image ObjectEnvLayer4Left;
    public Image ObjectEnvLayer4Mid;
    public Image ObjectEnvLayer4Right;



    [Header("The last two layers which rest ABOVE the chars")]
    public Image ObjectEnvLayer5Left;
    public Image ObjectEnvLayer5Mid;
    public Image ObjectEnvLayer5Right;
    [Space(5)]
    public Image ObjectEnvLayer6Left;
    public Image ObjectEnvLayer6Mid;
    public Image ObjectEnvLayer6Right;





    [Header("all background parallax object scripts are stored here.")]
    [Header("If you have any issue drag and dropping the script itself,")]
    [Header("remember, you can open up two inspector tabs :)")]

    public List<BackgroundLayerMovementParallax> parallaxLayers = new List<BackgroundLayerMovementParallax>();

    [Space(15)]//movement inside the local map stuff
    [HideInInspector]
    public float distanceWalked = 0;//shows how much we've physically advanced in the current level
    public float maximumDistance = 1500; //maximum distance before the level fades to black and you go on the overmap
    [Space(15)]
    //private bool isAtVendor = false;
    public bool EncountersPaused = false;
    public GameObject ButtonMoveOn;

    public string[] enemyNames;

    public int difCurrency;
    public int level;





    public GameObject BossCanvas;
    public GameObject NormalCanvas1;
    public GameObject NormalCanvas2; // dirty fix to show custom level on boss spawn



    public void SetupBossEnvironment()
    {


        BossCanvas.SetActive(true);
        NormalCanvas1.SetActive(false);
        NormalCanvas2.SetActive(false);

        //ObjectEnvLayer1Left.sprite = BossEnvLayer1Left;
        //ObjectEnvLayer1Mid.sprite = BossEnvLayer1Mid;
        //ObjectEnvLayer1Right.sprite = BossEnvLayer1Right;

        //ObjectEnvLayer2Left.sprite = BossEnvLayer2Left;
        //ObjectEnvLayer2Mid.sprite = BossEnvLayer2Mid;
        //ObjectEnvLayer2Right.sprite = BossEnvLayer2Right;

        //ObjectEnvLayer3Left.sprite = BossEnvLayer3Left;
        //ObjectEnvLayer3Mid.sprite = BossEnvLayer3Mid;
        //ObjectEnvLayer3Right.sprite = BossEnvLayer3Right;

        //ObjectEnvLayer4Left.sprite = BossEnvLayer4Left;
        //ObjectEnvLayer4Mid.sprite = BossEnvLayer4Mid;
        //ObjectEnvLayer4Right.sprite = BossEnvLayer4Right;

        //ObjectEnvLayer5Left.sprite = BossEnvLayer5Left;
        //ObjectEnvLayer5Mid.sprite = BossEnvLayer5Mid;
        //ObjectEnvLayer5Right.sprite = BossEnvLayer5Right;

        //ObjectEnvLayer6Left.sprite = BossEnvLayer6Left;
        //ObjectEnvLayer6Mid.sprite = BossEnvLayer6Mid;
        //ObjectEnvLayer6Right.sprite = BossEnvLayer6Right;
    }


    public void SetupRegularEnvironment()
    {

        BossCanvas.SetActive(false);
        NormalCanvas1.SetActive(true);
        NormalCanvas2.SetActive(true);


        //ObjectEnvLayer1Left.sprite = EnvLayer1Left;
        //ObjectEnvLayer1Mid.sprite = EnvLayer1Mid;
        //ObjectEnvLayer1Right.sprite = EnvLayer1Right;

        //ObjectEnvLayer2Left.sprite = EnvLayer2Left;
        //ObjectEnvLayer2Mid.sprite = EnvLayer2Mid;
        //ObjectEnvLayer2Right.sprite = EnvLayer2Right;

        //ObjectEnvLayer3Left.sprite = EnvLayer3Left;
        //ObjectEnvLayer3Mid.sprite = EnvLayer3Mid;
        //ObjectEnvLayer3Right.sprite = EnvLayer3Right;

        //ObjectEnvLayer4Left.sprite = EnvLayer4Left;
        //ObjectEnvLayer4Mid.sprite = EnvLayer4Mid;
        //ObjectEnvLayer4Right.sprite = EnvLayer4Right;



        //ObjectEnvLayer5Left.sprite = EnvLayer5Left;
        //ObjectEnvLayer5Mid.sprite = EnvLayer5Mid;
        //ObjectEnvLayer5Right.sprite = EnvLayer5Right;

        //ObjectEnvLayer6Left.sprite = EnvLayer6Left;
        //ObjectEnvLayer6Mid.sprite = EnvLayer6Mid;
        //ObjectEnvLayer6Right.sprite = EnvLayer6Right;
    }





    public void Update()
    {
        CheckForEncounter();
        CheckForVendor();

    }


    private bool IsBetween(double testValue, double bound1, double bound2)
    {
        if (bound1 > bound2)
            return testValue >= bound2 && testValue <= bound1;
        return testValue >= bound1 && testValue <= bound2;
    }
    /// <summary>
    /// checks wether the party has reached a vendor. If so, stops the player
    /// </summary>
    private void CheckForVendor()
    {
        //if (MainData.currentLevel == null || !MainData.MainLoop.inCombat || isAtVendor)
        //{
        //    return;
        //}

        if (FoughtOnce && distanceWalked > 70)
        {
            MoveStop();
            MainData.MainLoop.VendorScriptComponent.InitializeShopItems();
            EncountersPaused = true;
            distanceWalked = -20;//back to beginning.
            FoughtOnce = false;
            MainData.MainLoop.VendorScriptComponent.MoveMerchant();
        }

    }

    bool FoughtOnce = false;
    /// <summary>
    /// checks if player has walked 5 integers near an encounter. if so, trigger it to spawn and turns combat on.
    /// </summary>
    /// 
    private void CheckForEncounter()
    {

        if (EncountersPaused)
        {
            return;
        }


        if (MainData.currentLevel == null || MainData.MainLoop.inCombat)
        {
            return;
        }


        if (FoughtOnce == false)
        {
            if (distanceWalked > 40)
            {
                MoveStop();
                ButtonMoveOn.SetActive(false);
                MainData.MainLoop.EntityDefComponent.SpawnEncounter(MainData.currentLevel.Encounters[0]);
                //MainData.currentLevel.Encounters[1].spawned = false; //so we have a nice stable loop.
                FoughtOnce = true;
                // MainData.MainLoop.EventLoggingComponent.Log("Spawned an encounter. EncounterOrder is " + FoughtOnce);
                MainData.MainLoop.UserInterfaceHelperComponent.ToggleFightButtonVisiblity(true);
            }
        }





    }



    /// <summary>
    /// party moves forwards, aka Background moves backwards. 
    /// </summary>
    public void MovePartyForwards()
    {
        if (MainData.MainLoop.inCombat)
        {
            return;
        }
        SetupRegularEnvironment();
        Debug.Log("Getting player objects for walking anim");
        foreach (Character item in MainData.livingPlayerParty)
        {
            item.selfScriptRef.StartWalk();
        }

        foreach (BackgroundLayerMovementParallax item in parallaxLayers)
        {
            item.ChangeDirection(false);
        }
        if (MainData.MainLoop.VendorScriptComponent.isVendorHere)
        {
            MainData.MainLoop.VendorScriptComponent.MoveMerchant();
        }
        ButtonMoveOn.SetActive(false);// for the demo.
    }

    public void MovePartyBackwards()
    {
        if (MainData.MainLoop.inCombat)
        {
            return;
        }
        foreach (BackgroundLayerMovementParallax item in parallaxLayers)
        {
            item.ChangeDirection(true);
        }
    }

    public void MoveStop()
    {
        if (MainData.MainLoop.inCombat)
        {
            return;
        }

        foreach (Character item in MainData.livingPlayerParty)
        {
            item.selfScriptRef.StopWalk();
        }


        foreach (BackgroundLayerMovementParallax item in parallaxLayers)
        {
            item.ChangeDirection(null);
        }
        ButtonMoveOn.SetActive(true);// for the demo.
    }








    /// <summary>
    /// generates and returns multiple encounters for a level at an even spacing
    /// </summary>
    /// <param name="encounterAmt"> how many encounters in a level. all of the encounters are the same, for the moment. identical enemy party composition-wise</param>
    /// <param name="monsters">what kind of creatures spawn. can be any mix of any existant enemy mob. amount is based on how many instances of a singular type you add to the list</param>
    /// <param name="distance">point at which the encounters START, new encounters being incremented by encounterSpacing defined locally </param>
    /// <returns></returns>
    private List<Encounter> GenerateEncountersForLevel(int encounterAmt, List<string> monsters, float distance)
    {
        float encounterSpacing = 10;
        List<Encounter> encounter = new List<Encounter>();
        for (int i = 0; i < encounterAmt; i++)
        {
            Encounter b = new Encounter();
            List<string> enemiesList = new List<string>();
            foreach (string item in monsters)
            {
                enemiesList.Add(item);
            }
            b.enemies = monsters;
            b.distancePoint = distance;
            distance += encounterSpacing;
            //string descriptor = "";
            //foreach (string item in monsters)
            //{
            //    descriptor = descriptor + item + ", ";
            //}
            //MainData.MainLoop.EventLoggingComponent.LogGray("Prepared a " + descriptor + " ambush at " + distance);
            encounter.Add(b);
        }


        return encounter;
    }




    public void SetupDemoLevel()
    {
        MainData.currentLevel = MainData.levelTemplates["darkforest"];
    }

    public void GenerateLevels()
    {
        List<string> teamrocket = new List<string>();
        for (int i = 0; i < 3; i++) //change the 5 to whatever amount to change monkey quantity.
        {
            teamrocket.Add(enemyNames[0]);
        }



        MapLevel darkForest = new MapLevel("Dark Forest",
                                           "A forest where it is very dark :D",
                                           "This forest contains numerous animals of the carnivorous persuasion, the wide of majority of which reminisce fondly upon past memories of anthropophagy.",
                                           "Beasts",//generic description for enemies
                                           1f,//difficulty
                                           1f,//difficulty increment
                                           testsound,//background music
                                           false,//wether there's a campfire
                                           GenerateEncountersForLevel(1, //how many encounters
                                                                      teamrocket,
                                                                      50));//encounters start from this point


        MainData.levelTemplates.Add("darkforest", darkForest); //adds the template to the global list
    }

    public void GENERATE()
    {
        difCurrency = MainData.MainLoop.EntityDefComponent.difficultyCurrency + 3;
        List<string> teamrocket = new List<string>();
        level += 1;
        if (level == 1000)
        {
            SceneManager.LoadScene("End");
        }
        if (level == 20)
        {
            List<string> boss = new List<string>();
            boss.Add(enemyNames[8]);
            boss.Add(enemyNames[7]);
            boss.Add(enemyNames[7]);
            boss.Add(enemyNames[7]);
            boss.Add(enemyNames[7]);
            boss.Add(enemyNames[7]);
            boss.Add(enemyNames[7]);
            level = 998;
            MainData.currentLevel.Encounters.Clear();
            MainData.currentLevel.Encounters = GenerateEncountersForLevel(1, //how many encounters
                                       boss,
                                     50);//encounters start from this point
        }
        if (level < 20)
        {
            for (int i = 0; i < difCurrency; i++)
            {
                int r = Random.Range(0, Mathf.Clamp((difCurrency / 2) - 1, 0, 8));
                r += (difCurrency / 5);
                Mathf.Clamp(r, 0, 8);
                teamrocket.Add(enemyNames[r]);
            }
            MainData.currentLevel.Encounters.Clear();
            MainData.currentLevel.Encounters = GenerateEncountersForLevel(1, //how many encounters
                                       teamrocket,
                                       50);//encounters start from this point

        }
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



        public float xLocation;//the point where we will meet the merchant
        public List<Item> ItemStock = new List<Item>();

        public Merchant()
        {

        }
    }
    public class Encounter
    {
        public bool spawned = false; //wether it has already been spawned
        public float distancePoint; //the point in which this encounter spawns
        public List<string> enemies; // the enemies that will spawn in this encounter. are spawned by their ID from the StaticDataHolder enemy dictionary.
    }



    public void PlaySound(AudioClip soundToPlay)
    {
        source.PlayOneShot(soundToPlay);
    }

    public void PlaySoundCombat(AudioClip soundToPlay)
    {
        sourceC.PlayOneShot(soundToPlay);
    }

    public void PlaySoundClick()
    {
        mouseClick.Play();
    }

    public void setWalkingVolume(float volume)
    {
        walkingSound.volume = volume;
    }


    public class MapLevel
    {
        public bool isCampfire; //wether this tile has a campfire.
        public Merchant localMerchant;
        public string levelName; //name.
        public string levelBlurb; // short flavourful description, perhaps a relevant quote.
        public string levelDescription; //longer description for what it actually does.
        public string EnemyType; //the kind of enemies you can encounter.
        public float startingDifficulty; //the difficulty it starts at.
        public float difficultyIncreasePerRoom; //how much the difficulty increases after every room.

        public List<Encounter> Encounters; //how this works is that given enemy groups can be added to a certain float number, the distance, and when the player's party reaches it, it stops and spawns those enemies.


        public bool visited;

        public int levelRank; //from the beginning to the end, the rank goes from 0 to the maximum amount of levels we decide on.
        public List<MapLevel> previousLevels; //tracks levels that precede this one on the map
        public List<MapLevel> nextLevels; //tracks levels that succeed this one on the map

        // public Material levelBackgroundMaterial;
        public AudioClip levelSoundtrack;

        public MapLevel(string name, string blurb, string desc, string enemyTypes, float startDiff, float diffIncrement, AudioClip soundtrack, bool Campfire, List<Encounter> encount)
        {//class constructor
            this.levelName = name;
            this.levelBlurb = blurb;
            this.levelDescription = desc;
            this.EnemyType = enemyTypes;
            this.startingDifficulty = startDiff;
            this.difficultyIncreasePerRoom = diffIncrement;
            //  this.levelBackgroundMaterial = background;
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
