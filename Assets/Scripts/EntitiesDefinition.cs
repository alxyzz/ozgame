﻿using System.Collections;
using System.Collections.Generic;
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


    public Sprite HealthPotionSprite;
    public Sprite SharpeningStoneSprite;
    public Sprite HealingMushroomSprite;


    /// <summary>
    /// A function to define a new being, allied or enemy, and add it into the dictionary based on the characterID string.
    /// </summary>
    /// <param name="characterID">All lowercase string for the entity desired, to be identified with in the dictionary.</param>
    /// <param name="charName">The proper capitalized name for the entity.</param>
    /// <param name="charDesc">A flavourful description.</param>
    /// <param name="attackVerb">The verb used when attacking, shown in the log.</param>
    /// <param name="isPlayer">Wether it is part of the player's party, or an enemy.</param>
    /// <param name="baseHP">The base health maximum value, before to any modifiers</param>
    /// <param name="baseDMG">The base damage value, before any modifiers.</param>
    /// <param name="baseSPD">The base speed value, before any modifiers.</param>

    /// <param name="Defense"></param>
    /// <param name="Luck"></param>
    /// <param name="Mana"></param>
    /// <param name="newCharTurnSound"></param>
    /// <param name="newCharSprite"></param>
    /// <param name="newCharAvatar"></param>
    /// <returns></returns>
    public void CreateCreature(string characterID, string charName, string charDesc, string attackVerb, bool isPlayer, int baseHP, int baseDMG, int baseSPD, int Defense,  int Luck, int Mana, AudioClip newCharTurnSound, Sprite newCharSprite, Sprite newCharAvatar)
    {
        Character newCharacterDefinition = new Character();
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


    public void DefinePartyMembers()
    {
        //string characterID, string charName, string charDesc, string attackVerb, bool isPlayer, int baseHP, int baseDMG, int baseSPD, int Defense,  int Luck, int Mana, AudioClip newCharTurnSound, Sprite newCharSprite, Sprite newCharAvatar)
        CreateCreature("scarecrow", "Scarecrow", "Lacks a heart and is driven to obtain it.", "rends", true, 100, 25, 1, 1, 2, 100, null, null, ScarecrowSprite);




    }


    public void DefineMonsters()
    {

    }

    public void DefineConsumables()
    {

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
        string randomKey = keyList[Random.Range(0, keyList.Count + 1)];
        Debug.Log("Fetched random item: " + randomKey);
        return allConsumables[randomKey];

    }


    public Trait FetchRandomTrait()
    {//fetches a random t1 trait
        List<string> keyList = new List<string>(traitList.Keys);
        string randomKey = keyList[Random.Range(0, keyList.Count + 1)];
        Debug.Log("Fetched random trait: " + randomKey);
        return traitList[randomKey];

    }
    public Trait FetchRandomT1Trait()
    {//fetches a random t1 trait
        List<string> keyList = new List<string>(t1traitList.Keys);
        string randomKey = keyList[Random.Range(0,keyList.Count+1)];
        Debug.Log("Fetched random t1 trait: " + randomKey);
        return t1traitList[randomKey];

    }

    public Trait FetchRandomT2Trait()
    {//fetches a random t1 trait
        List<string> keyList = new List<string>(t2traitList.Keys);
        string randomKey = keyList[Random.Range(0, keyList.Count + 1)];
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
    public class Character : MonoBehaviour
    {
        public string charType; //something like "goblin_spear", "tin_man" or "scarecrow" for the dictionary. 
        public string charName;
        public string entityDescription;
        public CharacterScript currentCharObj;
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

        public int speed; //NOTE - RECOMPUTE THIS BEFORE EVERY TURN TO TRACK TRAITS CHANGING IT
        public int defense;
        public int damage;
        public int luck;
        public int mana;

        public bool hasActedThisTurn = false;

        public string attackverb;

        public CharacterScript selfScriptRef;


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


        public void TakeDamageFromCharacter(int dmg, string attackverb, Character attacker, bool critical)
        { //runs when being hit
            if (critical)
            { //make this red and bigger
                MainData.MainLoop.GameLog("Critical strike!");
            }
           MainData.MainLoop.GameLog(this.charName + " the " + charTrait.name + " has been " + attackverb + "ed by " + attacker.charName + "for " + dmg + " damage!");
            currentHealth -= dmg; //INCORPORATE ARMOR CALCULATION HERE
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



        public void GainStatusEffect (StatusEffect statusEffect, int turns)
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
                    this.currentStatusEffect = new StatusEffect(type, "Wether as an effect of natural biology or magical forces, "+ this.charName +" is currently regenerating at a highly enhanced rate.", duration);
                    break;
                default:
                    break;
            }
        }
        public void AttackRandom()
        {
            if (isPlayerPartyMember)
            {
                Character b = MainData.enemyParty[Random.Range(0, MainData.enemyParty.Count)];
                //b.TakeDamage();

            }
            else
            {
                Character d = MainData.playerParty[Random.Range(0, MainData.playerParty.Count)];
                //d.TakeDamage();

            }
        }
        public void gotKilled(Character killer = null)
        {
            //GameLog(killed.charName + "has been vanquished!");
            if (isPlayerPartyMember)
            {
                playerParty.Remove(allChars.Find(x => x.GetID() == this.charType));
            }

            enemyParty.Remove(allChars.Find(x => x.GetID() == this.charType));
            allChars.Remove(allChars.Find(x => x.GetID() == this.charType));
            Destroy(this);
        }
        public string GetID()
        {
            return charType;
        }
    }


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
