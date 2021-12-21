using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using static MainData;
using static TraitHelper;

public class EntitiesDefinition : MonoBehaviour
{
    //link all entity assets here. sounds, sprites, etc.
    //we will later have a version for each animation, but for now this is fine
    public Sprite ScarecrowSprite;
    public Sprite TinManSprite;
    public Sprite LionSprite;
    public Sprite FourthProtagonistSprite;
    [Space(10)]
    public Sprite Enemy1Sprite;
    public Sprite Enemy2Sprite;
    public Sprite Enemy3Sprite;
    [Space(10)]

    public Sprite HealthPotionSprite;
    public Sprite SharpeningStoneSprite;
    public Sprite HealingMushroomSprite;
    [Space(10)]
    public GameObject EnemyPrefab;
    

    /// <summary>
    /// A function to define a new being, allied or enemy, and add it into the dictionary based on the characterID string.
    /// </summary>
    /// <param name="characterID">All lowercase string for the entity desired, to be identified with in the dictionary.</param>
    /// <param name="charName">The proper capitalized name for the entity.</param>
    /// <param name="charDesc">A flavourful description.</param>
    /// <param name="attackVerb">The verb used when attacking, shown in the log.</param>
    /// <param name="isPlayer">Wether it is part of the player's party, or an enemy.</param>
    /// <param name="baseHP">The base health maximum value, before to any modifiers.</param>
    /// <param name="baseDMG">The base damage value, before any modifiers.</param>
    /// <param name="baseSPD">The base speed value, before any modifiers.</param>

    /// <param name="Defense"></param>
    /// <param name="Luck"></param>
    /// <param name="Mana"></param>
    /// <param name="newCharTurnSound"></param>
    /// <param name="newCharSprite"></param>
    /// <param name="newCharAvatar"></param>
    /// <returns></returns>
    public void MakeTemplateMob(string characterID, string charName, string charDesc, string attackVerb, bool isPlayer, int baseHP, int baseDMG, int baseSPD, int Defense, int Luck, int Mana, AudioClip newCharTurnSound, Sprite newCharSprite, Sprite newCharAvatar)
    {
        Character newCharacterDefinition = Character.CreateInstance<Character>();

        newCharacterDefinition.charType = characterID; //something like "goblin_spear", "tin_man" or "scarecrow" for the dictionary. 
        newCharacterDefinition.charName = charName;
        newCharacterDefinition.entityDescription = charDesc;
        newCharacterDefinition.attackverb = attackVerb;

        newCharacterDefinition.isPlayerPartyMember = isPlayer;


        newCharacterDefinition.turnSound = newCharTurnSound;
        newCharacterDefinition.charSprite = newCharSprite;
        newCharacterDefinition.charAvatar = newCharAvatar;



        newCharacterDefinition.baseHealth = baseHP;
        newCharacterDefinition.baseDamage = baseDMG;
        newCharacterDefinition.baseSpeed = baseSPD;

        newCharacterDefinition.currentHealth = baseHP;
        newCharacterDefinition.mana = Mana;

        newCharacterDefinition.luck = Luck;
        newCharacterDefinition.damage = baseDMG;
        newCharacterDefinition.speed = baseSPD; //to be recalculated later whenever a modifier gets applied.
        newCharacterDefinition.defense = Defense;


        newCharacterDefinition.hasActedThisTurn = false;

        MainData.characterTypes.Add(characterID, newCharacterDefinition);
    }


    public void DefinePC()
    {
        //string characterID, string charName, string charDesc, string attackVerb, bool isPlayer, int baseHP, int baseDMG, int baseSPD, int Defense,  int Luck, int Mana, AudioClip newCharTurnSound, Sprite newCharSprite, Sprite newCharAvatar)
        MakeTemplateMob("scarecrow", //characterID
                       "Scarecrow", // charName
                       "Lacks a brain and is driven to obtain it.", // charDesc
                       "rends", //verb used when attacking
                       true, //is it a player character(true), or is it an enemy(false)?
                       100, //the base HP value
                       25, // the base damage value
                       20, //base speed, higher is better
                       1, //defense
                       2, //luck
                       100, //mana
                       null, //sound for when it is this character's turn to act
                       ScarecrowSprite, //character's sprite 
                       null); //character's avatar sprite

        MakeTemplateMob("tin_man",
                       "Tin Man",
                       "Lacks a heart and will do anything to get it.",
                       "rends", //verb used when attacking
                       true, //is it a player character(true), or is it an enemy(false)?
                       100, //the base HP value
                       25, // the base damage value
                       60, //base speed, higher is better
                       1, //defense
                       2, //luck
                       100, //mana
                       null, //sound for when it is this character's turn to act
                       ScarecrowSprite, //character's sprite 
                       null); //character's avatar sprite

        MakeTemplateMob("lion",
                       "Lion",
                       "His lack of courage is apparent.",
                       "rends", //verb used when attacking
                       true, //is it a player character(true), or is it an enemy(false)?
                       100, //the base HP value
                       25, // the base damage value
                       40, //base speed, higher is better
                       1, //defense
                       2, //luck
                       100, //mana
                       null, //sound for when it is this character's turn to act
                       ScarecrowSprite, //character's sprite 
                       null); //character's avatar sprite

        MakeTemplateMob("dorothy",
                       "Dorothy",
                       "Wants to go home...",
                      "rends", //verb used when attacking
                       true, //is it a player character(true), or is it an enemy(false)?
                       100, //the base HP value
                       25, // the base damage value
                       30, //base speed, higher is better
                       1, //defense
                       2, //luck
                       100, //mana
                       null, //sound for when it is this character's turn to act
                       ScarecrowSprite, //character's sprite 
                       null); //character's avatar sprite


    }


    public void DefineNPC()
    {
        MakeTemplateMob("evilcrow", //characterID
                       "George", // charName
                       "Lacks a brain and is driven to obtain yours.", // charDesc
                       "rends", //verb used when attacking
                       false, //is it a player character(true), or is it an enemy(false)?
                       100, //the base HP value
                       25, // the base damage value
                       100, //base speed, higher is better
                       1, //defense
                       2, //luck
                       100, //mana
                       null, //sound for when it is this character's turn to act
                       ScarecrowSprite, //character's sprite 
                       null); //character's avatar sprite
    }

    public void SpawnEnemyTest()
    {//creates new enemies using a random, free, enemy spot. that's up to 7 enemies currently but just copy and arrange more if desired...

        // GameObject leftborder = MainData.MainLoop.PositionHolderComponent.EnemySpawnBoundaryLeft;
        // GameObject rightborder = MainData.MainLoop.PositionHolderComponent.EnemySpawnBoundaryRight;

        // GameObject b = Instantiate(EnemyPrefab, new Vector3(UnityEngine.Random.Range(leftborder.transform.position.x, rightborder.transform.position.x),
        //  rightborder.transform.position.y, 0), Quaternion.identity, MainData.MainLoop.backgroundObject.transform);

        //get random unused object
        if (freeEnemyPartyMemberObjects.Count == 0)
        {
            MainData.MainLoop.EventLoggingComponent.LogGray("Tried spawning, no more spots...");
            return;
        }
        //string bo = "";
        //foreach (GameObject item in freeEnemyPartyMemberObjects)
        //{
        //    bo += item.name + "\n";

        //}
        //Debug.LogWarning(bo);
        int x = UnityEngine.Random.Range(0, freeEnemyPartyMemberObjects.Count);
        MainData.MainLoop.EventLoggingComponent.LogDanger("Spawned enemy using spot at freeEnemyPartyMemberObjects[" + x.ToString() + "].");
        GameObject b = freeEnemyPartyMemberObjects[x]; //we get a random, inactive enemy spot
        freeEnemyPartyMemberObjects.RemoveAt(x);
        usedEnemyPartyMemberObjects.Add(b); //tracks usage
        //freeEnemyPartyMemberObjects.Remove(b);//officialy live
        b.SetActive(true);//we turn it on
        CharacterScript d = b.GetComponent<CharacterScript>();//get the Cscript reference
        d.SetupCharacterByTemplate(MainData.characterTypes["evilcrow"]); //assign an enemy template
        MainData.livingEnemyParty.Add(d.associatedCharacter);//add it to the living list
        MainData.MainLoop.EventLoggingComponent.LogGray("Spontaneous interdimensional emergence of malevolent entity detected.");
    }







    public void DefineConsumables()
    {

    }

    public void BuildParty()
    {

        //this takes the needed template, applies it to the charscript in the party slot, then calls the refresh method
        CharacterScript slot1ref = MainData.MainLoop.PositionHolderComponent.PartySlot1.GetComponent<CharacterScript>();
        CharacterScript slot2ref = MainData.MainLoop.PositionHolderComponent.PartySlot2.GetComponent<CharacterScript>();
        CharacterScript slot3ref = MainData.MainLoop.PositionHolderComponent.PartySlot3.GetComponent<CharacterScript>();
        CharacterScript slot4ref = MainData.MainLoop.PositionHolderComponent.PartySlot4.GetComponent<CharacterScript>();


        slot1ref.SetupCharacterByTemplate(MainData.characterTypes["scarecrow"]);
        slot2ref.SetupCharacterByTemplate(MainData.characterTypes["tin_man"]);
        slot3ref.SetupCharacterByTemplate(MainData.characterTypes["lion"]);
        slot4ref.SetupCharacterByTemplate(MainData.characterTypes["dorothy"]);
        foreach (Character item in MainData.livingPlayerParty)
        {
            item.RecalculateThreatFromStats();
        }
    }

    public void GenerateEquipment()
    {//generates traits, stores them all in a dictionary in the dataholder




    }
    public void DefineTraits()
    {//generates traits, stores them all in a dictionary in the dataholder




    }

    public Item FetchRandomItem()
    {//fetches a random t1 trait
        List<string> keyList = new List<string>(allConsumables.Keys);
        string randomKey = keyList[UnityEngine.Random.Range(0, keyList.Count + 1)];
        Debug.Log("Fetched random item: " + randomKey);
        return allConsumables[randomKey];

    }


    public Trait FetchRandomTrait()
    {//fetches a random t1 trait
        List<string> keyList = new List<string>(traitList.Keys);
        string randomKey = keyList[UnityEngine.Random.Range(0, keyList.Count + 1)];
        Debug.Log("Fetched random trait: " + randomKey);
        return traitList[randomKey];

    }
    public Trait FetchRandomT1Trait()
    {//fetches a random t1 trait
        List<string> keyList = new List<string>(t1traitList.Keys);
        string randomKey = keyList[UnityEngine.Random.Range(0, keyList.Count + 1)];
        Debug.Log("Fetched random t1 trait: " + randomKey);
        return t1traitList[randomKey];

    }

    public Trait FetchRandomT2Trait()
    {//fetches a random t1 trait
        List<string> keyList = new List<string>(t2traitList.Keys);
        string randomKey = keyList[UnityEngine.Random.Range(0, keyList.Count + 1)];
        Debug.Log("Fetched random t2 trait: " + randomKey);
        return t2traitList[randomKey];

    }

    public class Item : MonoBehaviour
    {
        public int itemID;
        public string entityName;
        public string entityDescription;
        public string itemName;

        public Item(int id, string name)
        {
            this.itemID = id;
            this.itemName = name;
        }
    }


    [System.Serializable]
    public class Character : ScriptableObject
    {

        public float threatFromStats;
        public float Threat = 0;

        public string charType; //something like "goblin_spear", "tin_man" or "scarecrow" for the dictionary. 
        public string charName;
        public string entityDescription;
        public CharacterScript selfScriptRef;
        public bool isPlayerPartyMember;

        public Trait charTrait;
        public AudioClip turnSound;
        public Sprite charSprite;
        public Sprite charAvatar;

        public StatusEffect currentStatusEffect;

        public int currentHealth;

        public int baseHealth;
        public int baseDamage;
        public int baseSpeed;

        public int speed; //NOTE - RECOMPUTE THESE BEFORE EVERY TURN TO TRACK TRAITS CHANGING IT
        public int defense;
        public int damage;
        public int luck;
        public int mana;
        private bool canAct = true;
        public bool hasActedThisTurn = false;

        public string attackverb;


        public bool CheckIfCanAct()
        {
            return canAct;
        }

        public void RecalculateThreatFromStats()
        {
            threatFromStats = ((((currentHealth + damage) / 2) / speed) * 100);
        }



        public void RecalculateSpeed() // maybe make it recompute all traits later 
        {//another thing to watch out for is wether the monster copy from the dictionary is really a copy or a reference. dont want to change the dictionary entry (in case of being a ref)


        }
        public bool CheckTrait(string b)
        {
            if (this.charTrait.name == b)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void TakeDamageFromCharacter(Character attacker)
        {

            Debug.Log(attacker.charName + attacker.attackverb + " the " + charName + " for " + attacker.damage + " damage");

            MainData.MainLoop.EventLoggingComponent.Log(attacker.charName + attacker.attackverb + " the " + charName + " for " + attacker.damage + " damage");

            currentHealth -= (attacker.damage - defense); //INCORPORATE ARMOR CALCULATION HERE 
            attacker.Threat += (attacker.damage - defense);
            if (currentHealth <= 0)
            {
                gotKilled(attacker);
            }
        }

        public void TakeDamage(int dmg)
        { //generic take damage function
            currentHealth -= dmg;
            MainData.MainLoop.GameLog(this.charName + " the " + charTrait.name + " is hurt " + "for " + dmg + " damage!");
            if (currentHealth <= 0)
            {
                gotKilled();
            }
        }



        public void GainStatusEffect(StatusEffect statusEffect, int turns)
        {
            this.currentStatusEffect = statusEffect;
            this.currentStatusEffect.turnsRemaining = turns;
        }


        public void StatusEffectProc(string type, int duration)
        {
            switch (type)
            {
                case "poison":
                    this.currentStatusEffect = new StatusEffect(type, "This being is affected by substances that can cause injury or death.", duration);
                    break;
                case "burn":
                    this.currentStatusEffect = new StatusEffect(type, "This being is on fire.", duration);
                    break;
                case "heal":
                    this.currentStatusEffect = new StatusEffect(type, "Wether as an effect of natural biology or magical forces, this being is currently regaining health at an enhanced rate.", duration);
                    break;
                case "regeneration":
                    this.currentStatusEffect = new StatusEffect(type, "Wether as an effect of natural biology or magical forces, " + this.charName + " is currently regenerating at a highly enhanced rate.", duration);
                    break;
                default:
                    break;
            }
        }


        public void gotKilled(Character killer = null)
        {
            if (killer != null)
            {
                MainData.MainLoop.EventLoggingComponent.Log(this.charName + " has been vanquished by " + killer.charName + ".");
            }
            else
            {
                MainData.MainLoop.EventLoggingComponent.Log(this.charName + " was killed in action.");
            }
            
            //if (isPlayerPartyMember)
            //{
            //    playerParty.Remove(allChars.Find(x => x.GetID() == this.charType));
            //}
            canAct = false;
            DealWithLists();

            selfScriptRef.Die();
        }

        private void DealWithLists()
        {
            allChars.Remove(this);
            if (isPlayerPartyMember)
            {
                livingPlayerParty.Remove(this);
                deadPlayerParty.Add(this);
            }
            else
            {
                livingEnemyParty.Remove(this);
                deadEnemyParty.Add(this);
            }
            
        }


        public string GetID()
        {
            return charType;
        }


    }
    //public static T DeepClone<T>(T obj)
    //{
    //    using (var ms = new MemoryStream())
    //    {
    //        var formatter = new BinaryFormatter();
    //        formatter.Serialize(ms, obj);
    //        ms.Position = 0;
    //        return (T)formatter.Deserialize(ms);
    //    }
    //}

    public class StatusEffect
    {
        public string type;
        public string description;
        public int turnsRemaining;

        public StatusEffect(string type, string description, int turnsRemaining)
        {
            this.type = type;
            this.description = description;
            this.turnsRemaining = turnsRemaining;
        }

    }

}
