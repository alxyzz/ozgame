using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using static LevelHelper;
using static MainData;
using static TraitHelper;

public class EntitiesDefinition : MonoBehaviour
{
    [Header("define all entity assets here. sounds, sprites, etc.")]
    [Header("Scarecrow")]
    public Sprite ScarecrowSprite;
    public Sprite ScarecrowAttackAnimation;
    public Sprite ScarecrowAttackWalkAnimation;
    [Header("Tinman")]
    public Sprite TinManSprite;
    [Header("Lion")]
    public Sprite LionSprite;
    [Header("Dorothy")]
    public Sprite DorothySprite;
    [Space(10)]
    [Header("Enemies")]
    public Sprite Enemy1Sprite;
    public Sprite Enemy2Sprite;
    public Sprite Enemy3Sprite;
    [Space(10)]
    public Sprite HealthPotionSprite;
    public Sprite SharpeningStoneSprite;
    public Sprite HealingMushroomSprite;
    [Space(10)]
    [Header("Consumables")]
    public Sprite shawarma;
    public Sprite doner;
    public Sprite lahmacun;
    public Sprite cocacola;


    [Space(10)]
    //public GameObject EnemyPrefab;
    public GameObject SpawnAnimationPrefab;

    /// <summary>
    /// A function to define a new being, allied or enemy, and add it into the dictionary based on the characterID string.
    /// </summary>
    /// <param name="characterID">All lowercase string for the entity desired, to be identified with in the dictionary.</param>
    /// <param name="charName">The proper capitalized name for the entity.</param>
    /// <param name="charDesc">A flavourful description.</param>
    /// <param name="attackVerb">The verb used when attacking, shown in the log.</param>
    /// <param name="isPlayer">Wether it is part of the player's party, or an enemy.</param>
    /// <param name="baseHP">The base health maximum value, before to any modifiers.</param>
    /// <param name="baseMinDMG">The base damage value, before any modifiers.</param>
    /// <param name="baseSPD">The base speed value, before any modifiers.</param>

    /// <param name="Defense"></param>
    /// <param name="Luck"></param>
    /// <param name="Mana"></param>
    /// <param name="newCharTurnSound"></param>
    /// <param name="newCharSprite"></param>
    /// <param name="newCharAvatar"></param>
    /// <returns></returns>
    public void MakeTemplateMob(string characterID, string charName, string charDesc, string attackVerb, bool isPlayer, int baseHP, int baseMinDMG, int baseMaxDMG, int baseSPD, int Defense, int Luck, int Mana, AudioClip newCharTurnSound, Sprite newCharSprite, Sprite newCharAvatar)
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



        newCharacterDefinition.maxHealth = baseHP;
        newCharacterDefinition.baseDamageMin = baseMinDMG;
        newCharacterDefinition.baseDamageMin = baseMaxDMG;
        newCharacterDefinition.baseSpeed = baseSPD;

        newCharacterDefinition.currentHealth = baseHP;
        newCharacterDefinition.mana = Mana;

        newCharacterDefinition.luck = Luck;
        newCharacterDefinition.damageMin = baseMinDMG;
        newCharacterDefinition.damageMax = baseMaxDMG;
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
                       40, // the base minimum damage value
                       50, //the base maximum damage value.
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
                       "chops", //verb used when attacking
                       true, //is it a player character(true), or is it an enemy(false)?
                       100, //the base HP value
                       40, // the base minimum damage value
                       50, //the base maximum damage value.
                       60, //base speed, higher is better
                       1, //defense
                       2, //luck
                       100, //mana
                       null, //sound for when it is this character's turn to act
                       TinManSprite, //character's sprite 
                       null); //character's avatar sprite

        MakeTemplateMob("lion",
                       "Lion",
                       "His lack of courage is apparent.",
                       "rends", //verb used when attacking
                       true, //is it a player character(true), or is it an enemy(false)?
                       100, //the base HP value
                       40, // the base  minimum damage value
                       50, //the base maximum damage value.
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
                      "cuts", //verb used when attacking
                       true, //is it a player character(true), or is it an enemy(false)?
                       100, //the base HP value
                       40, // the base  minimum damage value
                       50, //the base maximum damage value.
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
        MakeTemplateMob("flyingmonkey", //characterID
                       "George", // charName
                       "Evil flying monkey hellbent on causing mischief and wreaking havok.", // charDesc
                       "claws", //verb used when attacking
                       false, //is it a player character(true), or is it an enemy(false)?
                       45, //the base HP value
                       20, // the minimum damage value
                       30, //the maximum damage value.
                       5, //base speed, higher is better
                       1, //defense
                       2, //luck
                       100, //mana
                       null, //sound for when it is this character's turn to act
                       ScarecrowSprite, //character's sprite 
                       null); //character's avatar sprite
    }

    public void SpawnEnemyTest()
    {//creates new enemies using a random, free, enemy spot. that's up to 7 enemies currently but just copy and arrange more if desired...

        // GameObject leftborder = StaticDataHolder.MainLoop.PositionHolderComponent.EnemySpawnBoundaryLeft;
        // GameObject rightborder = StaticDataHolder.MainLoop.PositionHolderComponent.EnemySpawnBoundaryRight;

        // GameObject b = Instantiate(EnemyPrefab, new Vector3(UnityEngine.Random.Range(leftborder.transform.position.x, rightborder.transform.position.x),
        //  rightborder.transform.position.y, 0), Quaternion.identity, StaticDataHolder.MainLoop.backgroundObject.transform);

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

        int x = UnityEngine.Random.Range(0, freeEnemyPartyMemberObjects.Count);
        MainData.MainLoop.EventLoggingComponent.LogDanger("Spawned enemy using spot at freeEnemyPartyMemberObjects[" + x.ToString() + "].");
        GameObject b = freeEnemyPartyMemberObjects[x]; //we get a random, inactive enemy spot
        freeEnemyPartyMemberObjects.RemoveAt(x);
        usedEnemyPartyMemberObjects.Add(b); //tracks usage
        //freeEnemyPartyMemberObjects.Remove(b);//officialy live
        b.SetActive(true);//we turn it on
        CharacterScript d = b.GetComponent<CharacterScript>();//get the Cscript reference




        
        d.SetupCharacterByTemplate(MainData.characterTypes["flyingmonkey"]); //assign an enemy template
                                                                         //StaticDataHolder.livingEnemyParty.Add(d.associatedCharacter);//they are added to the living list in the above method
        string newname;
        switch (UnityEngine.Random.Range(1, 7))
        {
            case 1:
                newname = "Billy";
                break;
            case 2:
                newname = "John";
                break;
            case 3:
                newname = "Maria";
                break;
            case 4:
                newname = "Hans";
                break;
            case 5:
                newname = "Harry Potter";
                break;
            default:
                newname = "aasfasfasfasf";
                break;

        }
        d.associatedCharacter.charName = newname;


        MainData.MainLoop.EventLoggingComponent.LogGray("A new friend has arrived.");
        MainData.MainLoop.inCombat = true;
        MainData.MainLoop.UserInterfaceHelperComponent.RefreshViewEnemy();

        //these are for making smoke appear behind new spawns if desired later on.
        //GameObject cx = Instantiate(SpawnAnimationPrefab, d.gameObject.transform.position, Quaternion.identity);
        //StartCoroutine(DelAfterTime(cx));
    }

    //IEnumerator DelAfterTime(GameObject b)
    //{

    //    yield return new WaitForSecondsRealtime(2f);
    //    Destroy(b);
    //}




    public void SpawnEncounter(Encounter b)
    {
        b.spawned = true;//no repeat customers
        if (freeEnemyPartyMemberObjects.Count == 0)
        {
            MainData.MainLoop.EventLoggingComponent.LogGray("Tried spawning, no more spots...");
            return;
        }
        foreach (string item in b.enemies)
        {
            int x = UnityEngine.Random.Range(0, freeEnemyPartyMemberObjects.Count); //random spot

            MainData.MainLoop.EventLoggingComponent.LogDanger("Spawned enemy using spot at freeEnemyPartyMemberObjects[" + x.ToString() + "].");
            GameObject f = freeEnemyPartyMemberObjects[x]; //we get a random, inactive enemy spot
            freeEnemyPartyMemberObjects.RemoveAt(x); //we remove the spot from the inactive/free enemy spot list
            usedEnemyPartyMemberObjects.Add(f); //track usage...
            f.SetActive(true);//we turn the spot on on
            CharacterScript d = f.GetComponent<CharacterScript>();//get the Cscript reference
            d.SetupCharacterByTemplate(MainData.characterTypes[item]); //assign and set up an enemy template to the spot
            //they are added to the living list in the above method
            MainData.MainLoop.EventLoggingComponent.LogGray("A " + d.associatedCharacter.charName + "suddenly steps out of the shadows.");

        }
        //refresh the miniview thingies whenever we spawn or kill shit
        MainData.MainLoop.inCombat = true;
        MainData.MainLoop.UserInterfaceHelperComponent.RefreshViewEnemy();
    }







    private void DefineItem()
    {
        //ConsumableItem b = new ConsumableItem("doner", "Doner");
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

        MainData.MainLoop.UserInterfaceHelperComponent.PC1 = slot1ref;
        MainData.MainLoop.UserInterfaceHelperComponent.PC2 = slot2ref;
        MainData.MainLoop.UserInterfaceHelperComponent.PC3 = slot3ref;
        MainData.MainLoop.UserInterfaceHelperComponent.PC4 = slot4ref;


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

    public class Item
    {
        public string identifier; //stuff like "doner_kebab", "icecream_chocolate", "sword_steel"
        public string description;
        public string name;
        public Sprite itemSprite;
        public string rarity;
        public int value;
        public int amtInStock; // standard amount in stock, duh.

        //for equipment
        public bool isEquipable;
        public Character currentWielder;

        public Item(string id,string description,string name, string rarityString, int itemValue, int itemStock, Sprite iSprite, bool isEquipment)
        {
            this.identifier = id;
            this.description = description;
            this.name = name;
            this.itemSprite = iSprite;
            this.rarity = rarityString;
            this.value = itemValue;
            this.amtInStock = itemStock;
            this.isEquipable = isEquipment;

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
        public Vector3 InitialPosition; //yeah screw having a single variable in CombatHelper.cs we're doing this. set in CharacterScript or template use
        public bool isPlayerPartyMember;

        public Trait charTrait;
        public AudioClip turnSound;
        public Sprite charSprite;

        public Sprite WalkSprites;

        public Sprite AttackSprites;
        public Sprite charAvatar;

        public StatusEffect currentStatusEffect;

        public int currentHealth;

        public int maxHealth;
        public int baseDamageMin;
        public int baseDamageMax;
        public int baseSpeed;
        public Slider HealthBar;
        public int speed; //NOTE - RECOMPUTE THESE BEFORE EVERY TURN TO TRACK TRAITS CHANGING IT
        public int defense;
        public int damageMin;
        public int damageMax;
        public int luck;
        public int mana;
        private bool canAct = true; //wether it's stunned or not
        public bool isDead = false;
        public bool hasActedThisTurn = false;

        public string attackverb;



        public bool CheckIfCanAct()
        {
            return canAct;
        }

        public void RecalculateThreatFromStats()
        {
            threatFromStats = ((((currentHealth + damageMin) / 2) / speed) * 100);
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
            int damageRoll = UnityEngine.Random.Range(attacker.damageMin, attacker.damageMax + 1) - defense;
            MainData.MainLoop.EventLoggingComponent.Log(attacker.charName + " " + attacker.attackverb + " the " + charName + " for " + (damageRoll + defense) + " damage. Armor protects for " + defense + " damage!");
            if (charTrait != null)
            {
                //This is where we deal with traits that do stuff to our damage.
                switch (charTrait.traitName)
                {
                    case "blah":
                        break;

                    default:
                        break;
                }
            }
            

            MainData.MainLoop.CombatHelperComponent.DisplayFloatingDamageNumbers(damageRoll, this);
            currentHealth -= damageRoll; //INCORPORATED ARMOR CALCULATION HERE 
            attacker.Threat += (damageRoll - defense); // WE APPLY THREAT
            if (!isPlayerPartyMember)
            {//this updates the health bar so we don't run the whole big total refresh method
                MainData.MainLoop.UserInterfaceHelperComponent.RefreshViewEnemy();
                MainData.MainLoop.UserInterfaceHelperComponent.RefreshHealthBarEnemy();
            }
            else
            {
                MainData.MainLoop.UserInterfaceHelperComponent.RefreshHealthBarPlayer();
            }
            if (currentHealth <= 0)
            {
                GotKilled(attacker);
            }
        }

        public void TakeDamage(int dmg)
        { //generic take damage function
            currentHealth -= dmg;
            MainData.MainLoop.EventLoggingComponent.Log(this.charName + " the " + charTrait.name + " is hurt " + "for " + dmg + " damage!");
            HealthBar.value -= dmg / maxHealth * 100f;
            if (currentHealth <= 0)
            {
                GotKilled();
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


        public void GotKilled(Character killer = null)
        {
            if (killer != null)
            {
                MainData.MainLoop.EventLoggingComponent.Log(this.charName + " has been vanquished by " + killer.charName + ".");
            }
            else
            {
                MainData.MainLoop.EventLoggingComponent.Log(this.charName + " was killed in action.");
            }
            canAct = false;
            MainData.MainLoop.UserInterfaceHelperComponent.RefreshViewEnemy();

            HandleListsUponDeath();

            selfScriptRef.Die();
        }

        private void HandleListsUponDeath()
        {
            MainData.allChars.Remove(this);
            if (isPlayerPartyMember)
            {
                MainData.livingPlayerParty.Remove(this);
                MainData.deadPlayerParty.Add(this);
            }
            else
            {
                MainData.livingEnemyParty.Remove(this);
                MainData.deadEnemyParty.Add(this);
            }

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
