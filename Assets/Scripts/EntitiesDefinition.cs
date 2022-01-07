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
    public Sprite tinmanAvatar;
    public Sprite scarecrowAvatar;
    public Sprite monkeyAvatar;



    [Space(10)]
    [Header("Enemies")]

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
    [HideInInspector]
    public Sprite[] scarecrowAttackSheet;
    [HideInInspector]
    public Sprite[] monkeyAttackSheet;
    [HideInInspector]
    public Sprite[] monkeyHurtSheet;
    [HideInInspector]
    public Sprite[] tinmanAttackSheet;

    public GameObject ItemUseEffect;
    [Space(10)]
    [Header("Item properties")]
    public int HealthPotionHealthGiven;


    public void UseConsumable(Item consumable, Character target)
    {
        CombatHelper combathelp = MainData.MainLoop.CombatHelperComponent;
        EventLogging eventlog = MainData.MainLoop.EventLoggingComponent;
        if (consumable.itemQuantity < 1)
        {
            eventlog.Log("There's not enough " + consumable.itemName + " to do this. you don't have any " + consumable.itemName + "...");
            consumableInventory.Remove(consumable);
            MainData.MainLoop.UserInterfaceHelperComponent.RefreshInventorySlots();
            return;
        }

        if (!target.isPlayerPartyMember)
        {
            eventlog.Log(MainData.livingPlayerParty[UnityEngine.Random.Range(0, MainData.livingPlayerParty.Count)].charName + " has given " + target.charName + " a " + consumable.itemName + ".");
        }

        eventlog.Log(target.charName + " has quaffed a " + consumable.itemName + ".");
        if (consumable.beneficial && !target.isPlayerPartyMember)
        {
            string say = "";
            switch (UnityEngine.Random.Range(1, 6))
            {
                case 1:
                    say = "Thanks, fool.";
                    break;
                case 2:
                    say = "Don't mind if I do.";
                    break;
                case 3:
                    say = "How kind of you!";
                    break;
                case 4:
                    say = "Hey, these guys are cool. Do we really have to murder them?";
                    break;
                case 5:
                    say = "If you wish it.";
                    break;
                default://no six because it's exclusive on the upper range - let's do the default thing though just in case
                    say = "WOW!";
                    break;
            }
            eventlog.LogGray(target.charName + ": " + say);
        }
        switch (consumable.identifier)
        {
            case "health_potion":
                target.GainHealth(HealthPotionHealthGiven);//set this variable in the inspector above. for easy setting during runtime, too.
                MainData.MainLoop.CombatHelperComponent.DisplayFloatingDamageNumbers(HealthPotionHealthGiven, target, true);
                break;
            case "antidote_potion":
                List<StatusEffect> results = target.currentStatusEffects.FindAll(x => x.type == "poison");
                if (results.Count > 0)
                {
                    foreach (StatusEffect item in results)
                    {
                        target.currentStatusEffects.Remove(item);
                    }
                    eventlog.Log(target.charName + " is no longer poisoned!");
                }

                break;
            case "berserk_potion":
                List<StatusEffect> results2 = target.currentStatusEffects.FindAll(x => x.type == "berserk");
                if (results2.Count < 1)
                {
                    target.currentStatusEffects.Add(new StatusEffect("berserk", "A state of murderous anger which turns the affected into a powerful fighter, for the duration.", 3)); //NOT IMPLEMENTED AND JUST FOR TESTING
                }
                eventlog.Log(target.charName + " is now berserk!");
                break;





            default:
                break;
        }
        //Instantiate a health potion effect on the target at this point

        consumable.itemQuantity--;


        if (consumable.itemQuantity == 0)
        {
            consumableInventory.Remove(consumable);
            MainData.MainLoop.UserInterfaceHelperComponent.RefreshInventorySlots();
        }


    }

    public void LoadSpriteSheets()
    {


        scarecrowAttackSheet = Resources.LoadAll<Sprite>("scarecrow_attack");
        monkeyAttackSheet = Resources.LoadAll<Sprite>("Monkey_attack_sheet");
        monkeyHurtSheet = Resources.LoadAll<Sprite>("Monkey_hurt_sheet");
        tinmanAttackSheet = Resources.LoadAll<Sprite>("tinman_attack_sheet");
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
    public void MakeMobTemplate(string characterID, string charName, string charDesc, string attackVerb, bool isPlayer, int baseHP, int baseMinDMG, int baseMaxDMG, int baseSPD, int Defense, int Luck, int Mana, AudioClip newCharTurnSound, Sprite[] newCharSprite, Sprite newCharAvatar, int bountyy)
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
        newCharacterDefinition.Bounty = bountyy;

        newCharacterDefinition.hasActedThisTurn = false;

        MainData.characterTypes.Add(characterID, newCharacterDefinition);
    }


    public void DefinePC()
    {
        //string characterID, string charName, string charDesc, string attackVerb, bool isPlayer, int baseHP, int baseDMG, int baseSPD, int Defense,  int Luck, int Mana, AudioClip newCharTurnSound, Sprite newCharSprite, Sprite newCharAvatar)
        MakeMobTemplate("scarecrow", //characterID
                       "Scarecrow", // charName
                       "'Too many thoughts dull your blade, they say', 'But I would rather have a dull blade than a dull mind.'", // charDesc
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
                       scarecrowAttackSheet, //character's sprite 
                       scarecrowAvatar, 0); //character's avatar sprite

        MakeMobTemplate("tin_man",
                       "Tin Man",
                       "'There are wounds that never show on the body that are deeper and more hurtful than anything that bleeds.'",
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
                       tinmanAttackSheet, //character's sprite 
                       tinmanAvatar, 0); //character's avatar sprite

        MakeMobTemplate("lion",
                       "Lion",
                       "'Fear is weakness, they say. But even a coward can be dangerous, if cornered.'",
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
                       tinmanAttackSheet, //character's sprite 
                       tinmanAvatar, 0); //character's avatar sprite

        MakeMobTemplate("dorothy",
                       "Dorothy",
                       "'You don’t have a home until you leave it and then, when you have left it, you never can go back.'",
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
                       scarecrowAttackSheet, //character's sprite 
                       scarecrowAvatar, 0); //character's avatar sprite


    }


    public void DefineNPC()
    {
        MakeMobTemplate("flyingmonkey", //characterID
                       "Flying Monkey", // charName
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
                       monkeyAttackSheet, //character's sprite 
                       monkeyAvatar, 2); //character's avatar sprite
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
            MainData.MainLoop.EventLoggingComponent.LogGray("A " + d.associatedCharacter.charName + " suddenly steps out of the shadows.");

        }
        //refresh the miniview thingies whenever we spawn or kill shit
        MainData.MainLoop.inCombat = true;
        MainData.MainLoop.UserInterfaceHelperComponent.RefreshViewEnemy();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id">"donut_deepfried" or something. it's the name you refer it to in the dictionary.</param>
    /// <param name="description">description of the item</param>
    /// <param name="name">the capitalized name with spaces and such</param>
    /// <param name="rarityString">common/rare/masterwork/artifact</param>
    /// <param name="itemValue">value in eyes</param>
    /// <param name="itemStock">how much of this does the vendor have in stock </param>
    /// <param name="iSprite">the sprite of the item</param>
    /// <param name="isEquipment">if it's equipment, true. If it's a potion/food/consumable, false.</param>
    /// <param name="itemQuantityy">quantity duh</param>
    /// <param name="beneficial">wether this is harmful, or helpful</param>


    public void DefineConsumables()
    {
        MakeConsumableItemTemplate("health_potion",
                            "Wondrous concoction that makes wounds close before one's very eyes.",
                            "Health Potion",
                            HealthPotionSprite,
                            "uncommon", //rarity as a string
                            4,//value in eyes
                            5, //how much the birb gets in stock
                            1, //default quantity
                            true); //true - helpful. false - harmful. 




    }
    /// <summary>
    /// method to make new consumable items
    /// </summary>
    /// <param name="identifier">shorthand name with _ instead of spaces and no capitalization</param>
    /// <param name="description"></param>
    /// <param name="itemName"></param>
    /// <param name="itemSprite"></param>
    /// <param name="rarity">rarity as a string.</param>
    /// <param name="value">value in eyes</param>
    /// <param name="amtInStock">stock in the trade place</param>
    /// <param name="itemQuantity">quantity given.</param>
    /// <param name="beneficial">is it poison/a bomb or is it a potion</param>
    private void MakeConsumableItemTemplate(string identifier,
                    string description,
                    string itemName,
                    Sprite itemSprite,
                    string rarity,
                    int value,
                    int amtInStock = 1,
                    int itemQuantity = 1,
                    bool beneficial = true)
    {
        Item b = new Item(identifier,
              description,
              itemName,
              itemSprite,
              rarity,
              value,
              amtInStock,
              itemQuantity,
              beneficial,
              false,
              0,//irrelevant values for a consumable item
              0,
              0,
              0,
              0,
              0,
              0,
              0,
              0,
              0,
              0);
        MainData.allConsumables.Add(identifier, b);
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

        MakeEquipableItemTemplate("short_sword", //string ID
                                          "Common implement of war. Can usually be found between your ribs.",//the desc
                                          "Shortsword", //name
                                          HealthPotionSprite, //sprite
                                          "common", //rarity string
                                          6, //value in eyes
                                          1, //amount in birb's stock
                                          1, //default quantity
                                          true, //(true)beneficial or (false)harmful
                                          0, //speed bonus
                                          0, //health bonus
                                          0, //mana bonus
                                          2, //dmg bonus
                                          0, //defense bonus 
                                          0, //luck bonus 
                                          0, //health amp, multiplicative. as a percentage.
                                          0, //dam resist, multiplicative. as a percentage.
                                          0, //dam bonus multiplicative. as a percentage.
                                          0, //discount as a percentage, multiplicative
                                          0); //lifesteal multiplicative. as a percentage.



    }
    public void DefineTraits()
    {//generates traits, stores them all in a dictionary in the dataholder




    }


    public void MakeEquipableItemTemplate(string identifier,
                                          string description,
                                          string itemName,
                                          Sprite itemSprite,
                                          string rarity,
                                          int value,
                                          int amtInStock = 1,
                                          int itemQuantity = 1,
                                          bool beneficial = true,
                                          int speedmodifier = 0,
                                          int healthmodifier = 0,
                                          int manamodifier = 0,
                                          int dmgmodifier = 0,
                                          int defensemodifier = 0,
                                          int luckmodifier = 0,
                                          int multiplicativeHealingAmplification = 0,
                                          int multiplicativeDamageResistance = 0,
                                          int multiplicativeDamageBonus = 0,
                                          float discountPercentage = 0,
                                          int multiplicativeLifestealBonus = 0)
    {

        Item b = new Item(identifier,
                          description,
                          itemName,
                          itemSprite,
                          rarity,
                          value,
                          amtInStock,
                          itemQuantity,
                          beneficial,
                          true,
                          speedmodifier,
                          healthmodifier,
                          manamodifier,
                          dmgmodifier,
                          defensemodifier,
                          luckmodifier,
                          multiplicativeHealingAmplification,
                          multiplicativeDamageResistance,
                          multiplicativeDamageBonus,
                          discountPercentage,
                          multiplicativeLifestealBonus);
        MainData.allEquipment.Add(identifier, b);








    }
    public void GivePlayerTestConsumables()
    {

        GiveConsumable("health_potion", 3);

    }

    /// <summary>
    /// gives the party a consumable. if consumable already exists, adds to it's charges
    /// </summary>
    /// <param name="itemID"></param>
    public void GiveConsumable(string itemID, int amt)
    {
        Item c = MainData.allConsumables[itemID];
        Item b = new Item(c.identifier,
                    c.description,
                    c.itemName,
                    c.itemSprite,
                    c.rarity,
                    c.value,
                    c.amtInStock,
                    c.itemQuantity,
                    c.beneficial,
                    c.isEquipable,
                    c.speedmodifier,
                    c.healthmodifier,
                    c.manamodifier,
                    c.dmgmodifier,
                    c.defensemodifier,
                    c.luckmodifier,
                    c.multiplicativeHealingAmplification,
                    c.multiplicativeDamageResistance,
                    c.multiplicativeDamageBonus,
                    c.discountPercentage,
                    c.multiplicativeLifestealBonus); // so we don't have to define them for things that don't need them
        //now we check if we already have that item in our inventory.
        //if we do? we just add the quantity of this to that one.
        List<Item> results = MainData.consumableInventory.FindAll(x => x.identifier == itemID);
        Debug.Log(results);
        if (results.Count != 0)
        {
            results[0].itemQuantity += b.itemQuantity;//gives amt * new item's worth of charges to the existing consumable
            return;
        }
        b.itemQuantity = amt;
        MainData.consumableInventory.Add(b);//adds it to the inventory
        MainData.MainLoop.UserInterfaceHelperComponent.RefreshInventorySlots();//refreshes the inventory buttons/slots
    }





    public class Item
    {
        public string identifier; //stuff like "doner_kebab", "icecream_chocolate", "sword_steel"
        public string description;
        public string itemName;
        public Sprite itemSprite;
        public string rarity;
        public int value;
        public int amtInStock; // standard amount in stock, duh. This is for the shop.
        public int itemQuantity; // quantity of items held. for potion mostly. or bombs. etc. consumables. Or perhaps en enchanted sword with limited amount of shots.
        public bool beneficial; //for threat tracking and stuff.
        //for equipment
        public bool isEquipable;
        public Character currentWielder;




        public int speedmodifier; //flat, just a +1 or -1 etc. for all of these modifiers
        public int healthmodifier;
        public int manamodifier;
        public int dmgmodifier;
        public int defensemodifier;
        public int luckmodifier;
        public int multiplicativeHealingAmplification;//affects healing others

        public int multiplicativeDamageResistance;//affects damage taken.
        public int multiplicativeDamageBonus;//affects damage output. applied after ALL other bonuses.

        public float discountPercentage;//if positive, it's a discount yeah ,but if negative it increases price.
        public int multiplicativeLifestealBonus;


        /// <summary>
        /// Item constructor.
        /// </summary>
        /// <param name="identifier">stuff like "doner_kebab", "icecream_chocolate", "sword_steel"</param>
        /// <param name="description">description of the item</param>
        /// <param name="itemName">the name of the item. capitalized, etc.</param>
        /// <param name="itemSprite">the sprite of the item as shown in the inventory</param>
        /// <param name="rarity">the rarity, as a string. goes from common, to uncommon, to rare, to historic</param>
        /// <param name="value"> value in eyes. relevant in the shop</param>
        /// <param name="amtInStock">standard amount in stock, duh. This is for the shop.</param>
        /// <param name="itemQuantity">quantity of items held. for potion mostly. or bombs. etc. consumables. Or perhaps en enchanted sword with limited amount of shots.</param>
        /// <param name="beneficial">for threat tracking and stuff.</param>
        /// <param name="isEquipable">consumables don't have this. equipment and trinkets do.</param>
        public Item(string identifier,
                    string description,
                    string itemName,
                    Sprite itemSprite,
                    string rarity,
                    int value,
                    int amtInStock = 1,
                    int itemQuantity = 1,
                    bool beneficial = true,
                    bool isEquipable = true,
                    int speedmodifier = 0,
                    int healthmodifier = 0,
                    int manamodifier = 0,
                    int dmgmodifier = 0,
                    int defensemodifier = 0,
                    int luckmodifier = 0,

                    int multiplicativeHealingAmplification = 0,
                    int multiplicativeDamageResistance = 0,
                    int multiplicativeDamageBonus = 0,
                    float discountPercentage = 0,
                    int multiplicativeLifestealBonus = 0) // so we don't have to define them for things that don't need them
        {
            this.identifier = identifier;
            this.description = description;
            this.itemName = itemName;
            this.itemSprite = itemSprite;
            this.rarity = rarity;
            this.value = value;
            this.amtInStock = amtInStock;
            this.itemQuantity = itemQuantity;
            this.beneficial = beneficial;
            this.isEquipable = isEquipable;
            this.speedmodifier = speedmodifier;
            this.healthmodifier = healthmodifier;
            this.manamodifier = manamodifier;
            this.dmgmodifier = dmgmodifier;
            this.defensemodifier = defensemodifier;
            this.luckmodifier = luckmodifier;
            this.multiplicativeHealingAmplification = multiplicativeHealingAmplification;
            this.multiplicativeDamageResistance = multiplicativeDamageResistance;
            this.multiplicativeDamageBonus = multiplicativeDamageBonus;
            this.discountPercentage = discountPercentage;
            this.multiplicativeLifestealBonus = multiplicativeLifestealBonus;
        }
    }




    [System.Serializable]
    public class Character : ScriptableObject
    {

        List<Item> equippedItems = new List<Item>(); //this does not contain any consumables. those are in the collective inventory pool. this only contains items with isEquipable = true

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
        public Sprite[] charSprite; //this contains the attack too. the first sprite is the standing sprite.

        public Sprite WalkSprites;
        public Sprite charAvatar;//head pic

        public List<StatusEffect> currentStatusEffects = new List<StatusEffect>();

        public int currentHealth;
        public int Bounty = 2; //eyes given for kill
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
        public bool canAct = true; //wether it's stunned or not
        public bool isDead = false;
        public bool hasActedThisTurn = false;

        public string attackverb;





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


            MainData.MainLoop.CombatHelperComponent.DisplayFloatingDamageNumbers(damageRoll, this, false);
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
            MainData.MainLoop.CombatHelperComponent.DisplayFloatingDamageNumbers(dmg, this, false);
            HealthBar.value -= dmg / maxHealth * 100f;
            if (currentHealth <= 0)
            {
                GotKilled();
            }
        }

        public void GainHealth(int hp)
        {





            currentHealth += hp; //INCORPORATED ARMOR CALCULATION HERE 
            MainData.MainLoop.CombatHelperComponent.DisplayFloatingDamageNumbers(hp, this, true);
            if (!isPlayerPartyMember)
            {//this updates the health bar so we don't run the whole big total refresh method
                MainData.MainLoop.UserInterfaceHelperComponent.RefreshViewEnemy();
                MainData.MainLoop.UserInterfaceHelperComponent.RefreshHealthBarEnemy();
            }
            else
            {
                MainData.MainLoop.UserInterfaceHelperComponent.RefreshHealthBarPlayer();
            }
            if (currentHealth >= this.maxHealth)
            {
                currentHealth = maxHealth;
            }

        }
        public void StatusEffectProc()
        {

            if (currentStatusEffects.Count < 1)
            {
                return;
            }

            foreach (StatusEffect statusEff in currentStatusEffects)
            {
                switch (statusEff.type)
                {
                    case "poison":
                        this.TakeDamage(3);
                        MainData.MainLoop.EventLoggingComponent.LogGray(this.charName + " takes 3 poison damage.");
                        break;
                    case "burn":
                        this.TakeDamage(5);
                        MainData.MainLoop.EventLoggingComponent.LogGray(this.charName + " burns for 5 damage.");
                        break;
                    case "heal":
                        this.GainHealth(5);
                        MainData.MainLoop.EventLoggingComponent.LogGray(this.charName + " heals 5 damage.");
                        break;
                    case "regeneration":
                        MainData.MainLoop.EventLoggingComponent.LogGray(this.charName + " regenerates 10 damage.");
                        this.GainHealth(10);
                        break;
                    default:
                        currentStatusEffects.Remove(statusEff);//
                        break;
                }
                statusEff.turnsRemaining--;
                if (statusEff.turnsRemaining < 1)
                {
                    currentStatusEffects.Remove(statusEff);
                }
            }



        }


        public void GotKilled(Character killer = null)
        {
            if (killer != null)
            {
                MainData.MainLoop.EventLoggingComponent.Log(charName + " has been vanquished by " + killer.charName + ".");
            }
            else
            {
                MainData.MainLoop.EventLoggingComponent.Log(charName + " was killed in action.");
            }
            canAct = false;
            if (!isPlayerPartyMember)
            {//this updates the health bar so we don't run the whole big total refresh method
                MainData.MainLoop.UserInterfaceHelperComponent.RefreshViewEnemy();
                MainData.MainLoop.UserInterfaceHelperComponent.RefreshHealthBarEnemy();
                MainData.MainLoop.CombatHelperComponent.TargetSelectionCheck();

                MainData.MainLoop.Currency += Bounty;
                MainData.MainLoop.EventLoggingComponent.LogGray("The " + charName + " dropped " + Bounty + " eyes.");
                MainData.MainLoop.UserInterfaceHelperComponent.UpdateCurrencyCounter();
            }
            else
            {
                MainData.MainLoop.UserInterfaceHelperComponent.RefreshHealthBarPlayer();
            }
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
