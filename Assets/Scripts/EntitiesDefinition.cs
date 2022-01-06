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




    public void LoadSpriteSheets()
    {


        scarecrowAttackSheet = Resources.LoadAll<Sprite>("scarecrow_attack");
        monkeyAttackSheet = Resources.LoadAll<Sprite>("Monkey_attack_sheet");
        monkeyHurtSheet = Resources.LoadAll<Sprite>("Monkey_hurt_sheet");
        tinmanAttackSheet = Resources.LoadAll<Sprite>("tinman_attack_sheet");
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
    public void MakeMobTemplate(string characterID, string charName, string charDesc, string attackVerb, bool isPlayer, int baseHP, int baseMinDMG, int baseMaxDMG, int baseSPD, int Defense, int Luck, int Mana, AudioClip newCharTurnSound, Sprite[] newCharSprite, Sprite newCharAvatar)
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
                       scarecrowAvatar); //character's avatar sprite

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
                       tinmanAvatar); //character's avatar sprite

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
                       tinmanAvatar); //character's avatar sprite

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
                       scarecrowAvatar); //character's avatar sprite


    }


    public void DefineNPC()
    {
        MakeMobTemplate("flyingmonkey", //characterID
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
                       monkeyAttackSheet, //character's sprite 
                       monkeyAvatar); //character's avatar sprite
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
                newname = "Shady";
                break;
            case 2:
                newname = "Spooky";
                break;
            case 3:
                newname = "Baddie";
                break;
            case 4:
                newname = "John";
                break;
            case 5:
                newname = "Slim";
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
            MainData.MainLoop.EventLoggingComponent.LogGray("A " + d.associatedCharacter.charName + " suddenly steps out of the shadows.");

        }
        //refresh the miniview thingies whenever we spawn or kill shit
        MainData.MainLoop.inCombat = true;
        MainData.MainLoop.UserInterfaceHelperComponent.RefreshViewEnemy();
    }







    private void DefineItem()
    {
        //ConsumableItem b = new ConsumableItem("doner", "Doner");
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
    private void MakeItemTemplate(string id, string description, string name, string rarityString, int itemValue, int itemStock, Sprite iSprite, bool isEquipment, int itemQuantityy, bool beneficial)
    {
        Item newConsumableDefinition = new Item(id, description, name, rarityString, itemValue, itemStock, iSprite, isEquipment);
        newConsumableDefinition.itemQuantity = itemQuantityy;

        MainData.allConsumables.Add(id, newConsumableDefinition);
        
    }

    public void DefineConsumables()
    {
        MakeItemTemplate("health_potion",//string ID
                         "Wondrous concoction that heals the body and mends the mind.",//description
                         "Health Potion", //name
                         "uncommon", //quality string
                         4,// value in eyes
                         5,//how much of this does the bird boi get in stock
                         HealthPotionSprite,//sprite?
                         false, //wearable equipment that provides bonuses to a singular character?
                         3, //quantity?
                         true);//beneficial?



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

    public void GivePlayerTestConsumables()
    {

        GiveConsumable("health_potion");
        GiveConsumable("health_potion");
        GiveConsumable("health_potion");
    }

    /// <summary>
    /// gives the party a consumable. if consumable already exists, adds to it's charges
    /// </summary>
    /// <param name="itemID"></param>
    public void GiveConsumable(string itemID)
    {

        Item c = MainData.allConsumables[itemID];
        Item b = new Item(c.identifier, c.description, c.itemName, c.rarity, c.value, c.amtInStock, c.itemSprite, false);
        List<Item> results = MainData.consumableInventory.FindAll(x => x.identifier == itemID);
        if (results.Count != 0)
        {
            Debug.LogError("results.Count was "+ results.Count.ToString());
            results[0].itemQuantity += b.itemQuantity;//gives a new item's worth of charges to the existing consumable
        }
        

        MainData.consumableInventory.Add(b);//adds it to the inventory
        Debug.Log("MainData.consumableInventory.Add(b);");
        MainData.MainLoop.UserInterfaceHelperComponent.RefreshInventorySlots();//refreshes the inventory buttons/slots
    }

    public void UseConsumable(Item consu, Character target)
    {
        CombatHelper combathelp = MainData.MainLoop.CombatHelperComponent;
        EventLogging eventlog = MainData.MainLoop.EventLoggingComponent;

        if (consu.itemQuantity < 1)
        {
            eventlog.Log("There's not enough " + consu.itemName + " to do this. You only have "+consu.itemQuantity + " of " + consu.itemName);
            MainData.MainLoop.UserInterfaceHelperComponent.RefreshInventorySlots();
            return;

        }
        consu.itemQuantity--;

        if (consu.beneficial && !target.isPlayerPartyMember)
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


        if (!combathelp.CurrentlyActiveChar.isEnemyCharacter)
        {
            eventlog.Log(combathelp.CurrentlyActiveChar.associatedCharacter.charName + " has given " + target.charName + " a " + consu.itemName + ".");
        }
        else
        {
            eventlog.Log(target.charName + " has quaffed a "+ consu.itemName+ ".");
        }



        switch (consu.identifier)
        {
            case "health_potion":
                target.GainHealth(HealthPotionHealthGiven);//set this variable in the inspector above. for easy setting during runtime, too.
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

                break;





            default:
                break;
        }
        //Instantiate a health potion effect on the target at this point


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

        public Item(string id,string description,string name, string rarityString, int itemValue, int itemStock, Sprite iSprite, bool isEquipment)
        {
            this.identifier = id;
            this.description = description;
            this.itemName = name;
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
        public Sprite[] charSprite; //this contains the attack too. the first sprite is the standing sprite.

        public Sprite WalkSprites;
        public Sprite charAvatar;//head pic

        public List<StatusEffect> currentStatusEffects = new List<StatusEffect>();

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
            HealthBar.value -= dmg / maxHealth * 100f;
            if (currentHealth <= 0)
            {
                GotKilled();
            }
        }

        public void GainHealth(int hp)
        {

            


            //MainData.MainLoop.CombatHelperComponent.DisplayFloatingDamageNumbers(hp, sssthis);
            currentHealth += hp; //INCORPORATED ARMOR CALCULATION HERE 
           
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
                MainData.MainLoop.EventLoggingComponent.Log(this.charName + " has been vanquished by " + killer.charName + ".");
            }
            else
            {
                MainData.MainLoop.EventLoggingComponent.Log(this.charName + " was killed in action.");
            }
            canAct = false;
            MainData.MainLoop.UserInterfaceHelperComponent.RefreshViewEnemy();


            if (!isPlayerPartyMember)
            {
                MainData.MainLoop.CombatHelperComponent.TargetSelectionCheck();
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
