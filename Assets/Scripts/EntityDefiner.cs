using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static LevelHelper;
using static MainData;
using static TraitHelper;

public class EntityDefiner : MonoBehaviour
{
    [Header("define all entity assets here. sounds, sprites, etc.")]
    public Sprite tinmanAvatar;
    public Sprite scarecrowAvatar;
    public Sprite monkeyAvatar;
    public Sprite emeraldAvatar;
    public Sprite witchAvatar;
    public Sprite lionAvatar;
    public Sprite dorothyAvatar;
    [Space(5)]
    public Sprite scarecrowStanding;
    public Sprite tinmanStanding;
    public Sprite lionStanding;
    public Sprite dorothyStanding;
    [Space(5)]
    public string[] traitNames;
    public int[] traitCounter;
    [Space(5)]
    [Header("Enemies")]

    [Space(10)]

    [Header("Consumables")]
    [Space(10)]
    public Sprite HealthPotionSprite;
    public Sprite SharpeningStoneSprite;
    public Sprite HealingMushroomSprite;
    [Space(10)]
    [Header("Equipment")]
    public Sprite shortSwordSprite;
    public Sprite longSwordSprite;
    public Sprite VampiricSprite;
    public Sprite ConstitutionSprite;
    public Sprite PowerSprite;
    public Sprite EmeraldSprite;
    public Sprite GlassesSprite;
    public Sprite MonkeyFangSprite;
    public Sprite ManaweavingAmuletSprite;
    public Sprite IronArmorSprite;
    public Sprite AxeSprite;
    public Sprite TornadoSprite;
    public Sprite lifeGivingAmuletSprite;
    public Sprite ShieldSprite;
    public Sprite PuritySprite;
    public Sprite CloverSprite;
    public Sprite DualitySprite;
    public Sprite EmeraldArmorSprite;

    [Space(10)]
    //public GameObject EnemyPrefab;
    public GameObject SpawnAnimationPrefab;
    [Header("Spritesheets")]
    public Sprite[] scarecrowAttackSheet;
    public Sprite[] scarecrowWalk_Sheet;
    public Sprite[] scarecrowHurtSheet;
    public Sprite[] scarecrowIdleSheet;
    public Sprite[] scarecrowCastSheet;

    public Sprite[] lionAttackSheet;
    public Sprite[] lionWalkSheet;
    public Sprite[] lionHurtSheet;
    public Sprite[] lionIdleSheet;
    public Sprite[] lionCastSheet;

    public Sprite[] dorothyAttackSheet;
    public Sprite[] dorothyWalkSheet;
    public Sprite[] dorothyHurtSheet;
    public Sprite[] dorothyIdleSheet;
    public Sprite[] dorothyCastSheet;

    public Sprite[] tinmanAttackSheet;
    public Sprite[] tinmanWalkSheet;
    public Sprite[] tinmanHurtSheet;
    public Sprite[] tinmanIdleSheet;
    public Sprite[] tinmanCastSheet;

    public Sprite lionsSprite;
    public GameObject ItemUseEffect;

    public Sprite[] monkeyAttackSheet;
    public Sprite[] monkeyHurtSheet;
    public Sprite[] monkeyIdleSheet;

    public Sprite[] emeraldAttackSheet;
    public Sprite[] emeraldHurtSheet;
    public Sprite[] emeraldIdleSheet;

    public Sprite[] witchAttackSheet;
    public Sprite[] witchHurtSheet;
    public Sprite[] witchIdleSheet;

    AudioClip[] tinManSounds;
    AudioClip[] lionSounds;
    AudioClip[] scarecrowSounds;
    AudioClip[] dorothySounds;
    AudioClip[] monkeySounds;
    AudioClip[] corruptedSounds;
    AudioClip[] witchSounds;

    public int BaseCurrency;
    public int difficultyCurrency = 3;

    private void Start()
    {
        tinManSounds = new AudioClip[5]; //initialize tinman sounds
        tinManSounds[0] = Resources.Load<AudioClip>("SFX/TinMan/Hover");
        tinManSounds[1] = Resources.Load<AudioClip>("SFX/TinMan/Hurt");
        tinManSounds[2] = Resources.Load<AudioClip>("SFX/General/Attack");
        tinManSounds[3] = Resources.Load<AudioClip>("SFX/General/Crit");
        tinManSounds[4] = Resources.Load<AudioClip>("SFX/General/Attack2");

        lionSounds = new AudioClip[5]; //initialize lion sounds
        lionSounds[0] = Resources.Load<AudioClip>("SFX/Lion/Hover");
        lionSounds[1] = Resources.Load<AudioClip>("SFX/Lion/Hurt");
        lionSounds[2] = Resources.Load<AudioClip>("SFX/General/Attack");
        lionSounds[3] = Resources.Load<AudioClip>("SFX/General/Crit");
        lionSounds[4] = Resources.Load<AudioClip>("SFX/General/Attack2");

        scarecrowSounds = new AudioClip[5]; //initialize scarecrow sounds
        scarecrowSounds[0] = Resources.Load<AudioClip>("SFX/Scarecrow/Hover");
        scarecrowSounds[1] = Resources.Load<AudioClip>("SFX/Scarecrow/Hurt");
        scarecrowSounds[2] = Resources.Load<AudioClip>("SFX/General/Attack");
        scarecrowSounds[3] = Resources.Load<AudioClip>("SFX/General/Crit");
        scarecrowSounds[4] = Resources.Load<AudioClip>("SFX/General/Attack2");

        dorothySounds = new AudioClip[5]; //initialize dorothy sounds
        dorothySounds[0] = Resources.Load<AudioClip>("SFX/Dorothy/Hover");
        dorothySounds[1] = Resources.Load<AudioClip>("SFX/Dorothy/Hurt");
        dorothySounds[2] = Resources.Load<AudioClip>("SFX/General/Attack");
        dorothySounds[3] = Resources.Load<AudioClip>("SFX/General/Crit");
        dorothySounds[4] = Resources.Load<AudioClip>("SFX/General/Attack2");

        monkeySounds = new AudioClip[5]; //initialize Monkey sounds
        monkeySounds[1] = Resources.Load<AudioClip>("SFX/Monkey/Hurt");
        monkeySounds[2] = Resources.Load<AudioClip>("SFX/Monkey/Attack");
        monkeySounds[3] = Resources.Load<AudioClip>("SFX/General/Crit");
        monkeySounds[4] = Resources.Load<AudioClip>("SFX/General/Attack2");

        corruptedSounds = new AudioClip[5]; //initialize corrupt sounds
        corruptedSounds[1] = Resources.Load<AudioClip>("SFX/Corrupted/Hurt");
        corruptedSounds[2] = Resources.Load<AudioClip>("SFX/Corrupted/Attack");
        corruptedSounds[3] = Resources.Load<AudioClip>("SFX/General/Crit");
        corruptedSounds[4] = Resources.Load<AudioClip>("SFX/General/Attack2");

        witchSounds = new AudioClip[5]; //initialize corrupt sounds
        witchSounds[1] = Resources.Load<AudioClip>("SFX/Witch/Hurt");
        witchSounds[2] = Resources.Load<AudioClip>("SFX/Witch/Attack");
        witchSounds[3] = Resources.Load<AudioClip>("SFX/General/Crit");
        witchSounds[4] = Resources.Load<AudioClip>("SFX/Witch/Attack2");
    }


    public void UseConsumable(Item consumable, Character target)
    {
        CombatHelper combathelp = MainData.MainLoop.CombatHelperComponent;
        EventLogging eventlog = MainData.MainLoop.EventLoggingComponent;
        if (consumable.itemQuantity < 1)
        {
            eventlog.Log("There's not enough " + consumable.itemName + " to do this. you don't have any " + consumable.itemName + "...");
            consumableInventory.Remove(consumable);
            MainData.MainLoop.UserInterfaceHelperComponent.RefreshConsumableSlots();
            return;
        }

        if (!target.isPlayerPartyMember)
        {
            eventlog.Log(MainData.livingPlayerParty[UnityEngine.Random.Range(0, MainData.livingPlayerParty.Count)].charName + " has given " + target.charName + " a " + consumable.itemName + ".");
        }

        eventlog.Log(target.charName + " has used a " + consumable.itemName + ".");
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

                if (target.charTrait != null)
                {
                    switch (target.charTrait.identifier)
                    {
                        case "caring":
                            
                            float healthAfterCaringModifier = ((float)MainData.MainLoop.TweakingComponent.HealthPotionHealthGiven / 100) * (100 - (float)MainData.MainLoop.TweakingComponent.caringHealingPotionPercentageHealingTakenMalus);
                            target.GainHealth(Mathf.RoundToInt(healthAfterCaringModifier));
                            MainData.MainLoop.EventLoggingComponent.LogGray(target.charName + " feels sad knowing that the potion would be more effective for the others.");
                            MainData.MainLoop.CombatHelperComponent.DisplayFloatingDamageNumbers(target: target, heal: true, damage: Mathf.RoundToInt(healthAfterCaringModifier));
                            break;

                        default:
                            target.GainHealth(MainData.MainLoop.TweakingComponent.HealthPotionHealthGiven);//set this variable in the inspector above
                            MainData.MainLoop.CombatHelperComponent.DisplayFloatingDamageNumbers(damage: MainData.MainLoop.TweakingComponent.HealthPotionHealthGiven, target: target, heal: true);
                            break;
                    }
                }
                else
                {
                    target.GainHealth(MainData.MainLoop.TweakingComponent.HealthPotionHealthGiven);
                    MainData.MainLoop.CombatHelperComponent.DisplayFloatingDamageNumbers(damage: MainData.MainLoop.TweakingComponent.HealthPotionHealthGiven, target: target, heal: true);
                    break;
                }
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
            MainData.MainLoop.UserInterfaceHelperComponent.RefreshConsumableSlots();
        }


    }

    //public void LoadSpriteSheets()
    //{
    //    //monkeyAttackSheet = Resources.LoadAll<Sprite>("Monkey_attack_sheet");
    //    //monkeyHurtSheet = Resources.LoadAll<Sprite>("Monkey_hurt_sheet");
    //    //monkeyIdleSheet = Resources.LoadAll<Sprite>("monkey_idle");

    //    //scarecrowAttackSheet = Resources.LoadAll<Sprite>("scarecrow_attack");
    //    //scarecrowWalk_Sheet = Resources.LoadAll<Sprite>("scarecrow_walk");
    //    //scarecrowHurtSheet = Resources.LoadAll<Sprite>("scarecrow_hurt");
    //    //scarecrowIdleSheet = Resources.LoadAll<Sprite>("scarecrow_idle");

    //    //tinmanAttackSheet = Resources.LoadAll<Sprite>("tinman_attack");
    //    //tinmanWalkSheet = Resources.LoadAll<Sprite>("tinman_walk");
    //    //tinmanHurtSheet = Resources.LoadAll<Sprite>("tinman_hurt");
    //    //tinmanIdleSheet = Resources.LoadAll<Sprite>("tinman_idle");

    //    //lionAttackSheet = Resources.LoadAll<Sprite>("lion_attack");
    //    //lionWalkSheet = Resources.LoadAll<Sprite>("lion_walk");
    //    //lionHurtSheet = Resources.LoadAll<Sprite>("lion_hurt");
    //    //lionIdleSheet = Resources.LoadAll<Sprite>("lion_idle");

    //    //dorothyAttackSheet = Resources.LoadAll<Sprite>("dorothy_attack");
    //    //dorothyWalkSheet = Resources.LoadAll<Sprite>("dorothy_walk");
    //    //dorothyHurtSheet = Resources.LoadAll<Sprite>("dorothy_hurt");
    //    //dorothyIdleSheet = Resources.LoadAll<Sprite>("dorothy_idle");
    //}









    public Item FetchConsumable(string itemID)
    {//fetches a random t1 trait
        if (MainData.allConsumables[itemID] == null)
        {
            Debug.LogError("Nonexistant item identifier supplied at FetchConsumable.");
            return null;
        }

        Debug.Log("Fetched equipment: " + itemID);
        return new Item(MainData.allConsumables[itemID].identifier,
                      MainData.allConsumables[itemID].description,
                      MainData.allConsumables[itemID].itemBlurb,
                      MainData.allConsumables[itemID].itemName,
                      MainData.allConsumables[itemID].itemSprite,
                      MainData.allConsumables[itemID].rarity,
                      MainData.allConsumables[itemID].value,
                      MainData.allConsumables[itemID].amtInStock,
                      MainData.allConsumables[itemID].itemQuantity,
                      MainData.allConsumables[itemID].beneficial,
                      MainData.allConsumables[itemID].isEquipable,
                      MainData.allConsumables[itemID].speedmodifier,
                      MainData.allConsumables[itemID].healthmodifier,
                      MainData.allConsumables[itemID].manamodifier,
                      MainData.allConsumables[itemID].dmgmodifier,
                      MainData.allConsumables[itemID].defensemodifier,
                      MainData.allConsumables[itemID].luckmodifier,
                      MainData.allConsumables[itemID].healingAmp,
                      MainData.allConsumables[itemID].DamageResistancePercentage,
                      MainData.allConsumables[itemID].DamageBonusPercentage,
                      MainData.allConsumables[itemID].discountPercentage,
                      MainData.allConsumables[itemID].Lifesteal);


    }


    /// <summary>
    /// gives a copy of equipment from a dictionary
    /// if given no itemname it gives random one
    /// </summary>
    /// <returns></returns>
    public Item FetchEquipment(string itemID = null)
    {//fetches a random t1 trait
        if (itemID == null)
        {
            List<string> keyList = new List<string>(allEquipment.Keys);
            string randomKey = keyList[UnityEngine.Random.Range(0, keyList.Count)];
            Debug.Log("Fetched random equipment. key - " + randomKey);
            Item b = new Item(MainData.allEquipment[randomKey].identifier,
                          MainData.allEquipment[randomKey].description,
                          MainData.allEquipment[randomKey].itemBlurb,
                          MainData.allEquipment[randomKey].itemName,
                          MainData.allEquipment[randomKey].itemSprite,
                          MainData.allEquipment[randomKey].rarity,
                          MainData.allEquipment[randomKey].value,
                          MainData.allEquipment[randomKey].amtInStock,
                          MainData.allEquipment[randomKey].itemQuantity,
                          MainData.allEquipment[randomKey].beneficial,
                          MainData.allEquipment[randomKey].isEquipable,
                          MainData.allEquipment[randomKey].speedmodifier,
                          MainData.allEquipment[randomKey].healthmodifier,
                          MainData.allEquipment[randomKey].manamodifier,
                          MainData.allEquipment[randomKey].dmgmodifier,
                          MainData.allEquipment[randomKey].defensemodifier,
                          MainData.allEquipment[randomKey].luckmodifier,
                          MainData.allEquipment[randomKey].healingAmp,
                          MainData.allEquipment[randomKey].DamageResistancePercentage,
                          MainData.allEquipment[randomKey].DamageBonusPercentage,
                          MainData.allEquipment[randomKey].discountPercentage,
                          MainData.allEquipment[randomKey].Lifesteal);
            return b;

        }
        else
        {
            if (MainData.allEquipment[itemID] == null)
            {
                Debug.LogError("Nonexistant item identifier supplied at FetchEquipment.");
                return null;
            }
            Debug.Log("Fetched equipment: " + itemID);
            Item b = new Item(MainData.allEquipment[itemID].identifier,
                          MainData.allEquipment[itemID].description,
                          MainData.allEquipment[itemID].itemBlurb,
                          MainData.allEquipment[itemID].itemName,
                          MainData.allEquipment[itemID].itemSprite,
                          MainData.allEquipment[itemID].rarity,
                          MainData.allEquipment[itemID].value,
                          MainData.allEquipment[itemID].amtInStock,
                          MainData.allEquipment[itemID].itemQuantity,
                          MainData.allEquipment[itemID].beneficial,
                          MainData.allEquipment[itemID].isEquipable,
                          MainData.allEquipment[itemID].speedmodifier,
                          MainData.allEquipment[itemID].healthmodifier,
                          MainData.allEquipment[itemID].manamodifier,
                          MainData.allEquipment[itemID].dmgmodifier,
                          MainData.allEquipment[itemID].defensemodifier,
                          MainData.allEquipment[itemID].luckmodifier,
                          MainData.allEquipment[itemID].healingAmp,
                          MainData.allEquipment[itemID].DamageResistancePercentage,
                          MainData.allEquipment[itemID].DamageBonusPercentage,
                          MainData.allEquipment[itemID].discountPercentage,
                          MainData.allEquipment[itemID].Lifesteal);
            return b;
        }

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
    /// <param name= "DifficultyCost"></param>

    /// <param name="Defense"></param>
    /// <param name="Luck"></param>
    /// <param name="Mana"></param>
    /// <param name="newCharTurnSound"></param>
    /// <param name="attackAnimationSprites"></param>
    /// <param name="newCharAvatar"></param>
    /// <returns></returns>
    public void MakeMobTemplate(string characterID, string charName, string charDesc, string attackVerb, bool isPlayer, int baseHP, int baseMinDMG, int baseMaxDMG, int baseSPD, int Defense, int Luck, int Mana, AudioClip newCharTurnSound, Sprite[] attackAnimationSprites, int bountyy, Sprite newCharAvatar, Sprite noAnimSprite, Sprite[] HurtSprites, Sprite[] WalkSprite, Sprite[] IdleSprites, AudioClip[] SoundLibrary, Sprite[] castSprite, bool summoner = false, string summonedEntity = null)
    {
        Character Chara = Character.CreateInstance<Character>();
        Chara.charType = characterID; //something like "goblin_spear", "tin_man" or "scarecrow" for the dictionary. 
        Chara.charName = charName;
        Chara.entityDescription = charDesc;
        Chara.attackverb = attackVerb;
        Chara.isPlayerPartyMember = isPlayer;
        Chara.hurtSprites = HurtSprites;
        Chara.SoundLibrary = SoundLibrary;
        Chara.idleSprite = IdleSprites;
        Chara.WalkSprites = WalkSprite;
        Chara.turnSound = newCharTurnSound;
        Chara.attackAnimation = attackAnimationSprites;
        Chara.castSprites = castSprite;
        if (noAnimSprite != null)
        {
            Chara.standingSprite = noAnimSprite;
            //MainData.MainLoop.EventLoggingComponent.Log("noAnimsprite not null");

        }
        else
        {
            Chara.standingSprite = attackAnimationSprites[0];
            //MainData.MainLoop.EventLoggingComponent.Log("noAnimsprite null");
        }
        Chara.charAvatar = newCharAvatar;
        Chara.maxHealth = baseHP;
        Chara.baseDamageMin = baseMinDMG;
        Chara.baseDamageMin = baseMaxDMG;
        Chara.baseSpeed = baseSPD;
        Chara.currentHealth = baseHP;
        Chara.manaRegeneration = Mana;
        Chara.manaTotal = 100;
        Chara.luck = Luck;
        Chara.damageMin = baseMinDMG;
        Chara.damageMax = baseMaxDMG;
        Chara.speed = baseSPD; //to be recalculated later whenever a modifier gets applied.
        Chara.defense = Defense;
        Chara.valueBounty = bountyy;
        Chara.hasActedThisTurn = false;
        MainData.characterTypes.Add(characterID, Chara);
    }


    public void DefinePC()
    {
        //string characterID, string charName, string charDesc, string attackVerb, bool isPlayer, int baseHP, int baseDMG, int baseSPD, int Defense,  int Luck, int Mana, AudioClip newCharTurnSound, Sprite newCharSprite, Sprite newCharAvatar)
        MakeMobTemplate("scarecrow", //characterID
                       "Scarecrow", // charName
                       "'Scarecrow '", // charDesc
                       "rends", //verb used when attacking
                       true, //is it a player character(true), or is it an enemy(false)?
                       MainData.MainLoop.TweakingComponent.PCStartingMaxHealth, //the base HP value
                       MainData.MainLoop.TweakingComponent.PCStartingMinDamage, // the base minimum damage value
                       MainData.MainLoop.TweakingComponent.PCStartingMaxDamage, //the base maximum damage value.
                       MainData.MainLoop.TweakingComponent.PCStartingSpeed, //base speed, higher is better
                       MainData.MainLoop.TweakingComponent.PCStartingDefense, //defense
                       MainData.MainLoop.TweakingComponent.PCStartingLuck, //luck
                       MainData.MainLoop.TweakingComponent.PCStartingManaRegen, //mana
                       null, //sound for when it is this character's turn to act
                       scarecrowAttackSheet, //character's attack animation sprite 
                       0, //bounty, not relevant for PC
                       scarecrowAvatar, //avatar
                       scarecrowStanding, //standing sprite if there is no attacksheet since we usually just use the first frame
                       scarecrowHurtSheet, //hurt
                       scarecrowWalk_Sheet, //walk
                       scarecrowIdleSheet,
                       scarecrowSounds, scarecrowCastSheet); //idle sprites

        MakeMobTemplate("tin_man",
                       "Tin Man",
                       "'Tin guy'",
                       "chops", //verb used when attacking
                       true, //is it a player character(true), or is it an enemy(false)?
                       MainData.MainLoop.TweakingComponent.PCStartingMaxHealth, //the base HP value
                       MainData.MainLoop.TweakingComponent.PCStartingMinDamage, // the base minimum damage value
                       MainData.MainLoop.TweakingComponent.PCStartingMaxDamage, //the base maximum damage value.
                       MainData.MainLoop.TweakingComponent.PCStartingSpeed, //base speed, higher is better
                       MainData.MainLoop.TweakingComponent.PCStartingDefense, //defense
                       MainData.MainLoop.TweakingComponent.PCStartingLuck, //luck
                       MainData.MainLoop.TweakingComponent.PCStartingManaRegen, //mana
                       null, //sound for when it is this character's turn to act
                       tinmanAttackSheet, //character's attack animation sprite 
                       0, //bounty, not relevant for PC
                       tinmanAvatar, //avatar
                       tinmanStanding,//standing sprite if there is no attacksheet since we usually just use the first frame
                       tinmanHurtSheet,
                       tinmanWalkSheet,
                       tinmanIdleSheet,
                       tinManSounds, tinmanCastSheet);

        MakeMobTemplate("lion",
                       "Lion",
                       "'Fear is weakness, they say. But even a coward can be dangerous, if cornered.'",
                       "rends", //verb used when attacking
                       true, //is it a player character(true), or is it an enemy(false)?
                       MainData.MainLoop.TweakingComponent.PCStartingMaxHealth, //the base HP value
                       MainData.MainLoop.TweakingComponent.PCStartingMinDamage, // the base  minimum damage value
                       MainData.MainLoop.TweakingComponent.PCStartingMaxDamage, //the base maximum damage value.
                       MainData.MainLoop.TweakingComponent.PCStartingSpeed, //base speed, higher is better
                       MainData.MainLoop.TweakingComponent.PCStartingDefense, //defense
                       MainData.MainLoop.TweakingComponent.PCStartingLuck, //luck
                       MainData.MainLoop.TweakingComponent.PCStartingManaRegen, //mana
                       null, //sound for when it is this character's turn to act
                       lionAttackSheet, //character's attack animation sprite 
                       0, //bounty, not relevant for PC
                       lionAvatar, //avatar
                       lionStanding, //standing sprite if there is no attacksheet since we usually just use the first frame
                       lionHurtSheet,
                       lionWalkSheet,
                       lionIdleSheet,
                       lionSounds, lionCastSheet);

        MakeMobTemplate("dorothy",
                       "Dorothy",
                       "'You don’t have a home until you leave it and then, when you have left it, you never can go back.'",
                      "cuts", //verb used when attacking
                       true, //is it a player character(true), or is it an enemy(false)?
                       MainData.MainLoop.TweakingComponent.PCStartingMaxHealth, //the base HP value
                       MainData.MainLoop.TweakingComponent.PCStartingMinDamage, // the base  minimum damage value
                       MainData.MainLoop.TweakingComponent.PCStartingMaxDamage, //the base maximum damage value.
                       MainData.MainLoop.TweakingComponent.PCStartingSpeed, //base speed, higher is better
                       MainData.MainLoop.TweakingComponent.PCStartingDefense, //defense
                       MainData.MainLoop.TweakingComponent.PCStartingLuck, //luck
                       MainData.MainLoop.TweakingComponent.PCStartingManaRegen, //mana
                       null, //sound for when it is this character's turn to act
                       dorothyAttackSheet, //character's attack animation sprite 
                       0, //bounty, not relevant for PC
                       dorothyAvatar, //avatar
                       dorothyStanding, //standing sprite if there is no attacksheet since we usually just use the first frame
                       dorothyHurtSheet,
                       dorothyWalkSheet,
                       dorothyIdleSheet,
                       dorothySounds, dorothyCastSheet);


    }





    public void DefineNPC()
    {
        MakeMobTemplate("weakflyingmonkey", //characterID
                       "Weak Flying Monkey", // charName
                       "Evil flying monkey hell-bent on causing mischief and wreaking havok.", // charDesc
                       "claws", //verb used when attacking
                       false, //is it a player character(true), or is it an enemy(false)?
                       MainData.MainLoop.TweakingComponent.weakflyingMonkeyMaxHealth, //the base HP value
                       MainData.MainLoop.TweakingComponent.weakflyingMonkeyMinDamage, // the minimum damage value
                       MainData.MainLoop.TweakingComponent.weakflyingMonkeyMaxDamage, //the maximum damage value.
                       MainData.MainLoop.TweakingComponent.weakflyingMonkeySpeed, //base speed, higher is better
                       MainData.MainLoop.TweakingComponent.weakflyingMonkeyDefense, //defense
                       MainData.MainLoop.TweakingComponent.weakflyingMonkeyLuck, //luck
                       0, //mana
                       null, //sound for when it is this character's turn to act
                       monkeyAttackSheet, //character's attack animation sprite 
                       2, monkeyAvatar, monkeyAttackSheet[0], monkeyHurtSheet, scarecrowWalk_Sheet, monkeyIdleSheet,
                       monkeySounds, null);

        MakeMobTemplate("flyingmonkey", //characterID
                       "Flying Monkey", // charName
                       "Evil flying monkey hell-bent on causing mischief and wreaking havok.", // charDesc
                       "claws", //verb used when attacking
                       false, //is it a player character(true), or is it an enemy(false)?
                       MainData.MainLoop.TweakingComponent.flyingMonkeyMaxHealth, //the base HP value
                       MainData.MainLoop.TweakingComponent.flyingMonkeyMinDamage, // the minimum damage value
                       MainData.MainLoop.TweakingComponent.flyingMonkeyMaxDamage, //the maximum damage value.
                       MainData.MainLoop.TweakingComponent.flyingMonkeySpeed, //base speed, higher is better
                       MainData.MainLoop.TweakingComponent.flyingMonkeyDefense, //defense
                       MainData.MainLoop.TweakingComponent.flyingMonkeyLuck, //luck
                       0, //mana
                       null, //sound for when it is this character's turn to act
                       monkeyAttackSheet, //character's attack animation sprite 
                       2, monkeyAvatar, monkeyAttackSheet[0], monkeyHurtSheet, scarecrowWalk_Sheet, monkeyIdleSheet,
                       monkeySounds, null);

        MakeMobTemplate("strongflyingmonkey", //characterID
                       "Strong Flying Monkey", // charName
                       "Evil flying monkey hell-bent on causing mischief and wreaking havok.", // charDesc
                       "claws", //verb used when attacking
                       false, //is it a player character(true), or is it an enemy(false)?
                       MainData.MainLoop.TweakingComponent.strongflyingMonkeyMaxHealth, //the base HP value
                       MainData.MainLoop.TweakingComponent.strongflyingMonkeyMinDamage, // the minimum damage value
                       MainData.MainLoop.TweakingComponent.strongflyingMonkeyMaxDamage, //the maximum damage value.
                       MainData.MainLoop.TweakingComponent.strongflyingMonkeySpeed, //base speed, higher is better
                       MainData.MainLoop.TweakingComponent.strongflyingMonkeyDefense, //defense
                       MainData.MainLoop.TweakingComponent.strongflyingMonkeyLuck, //luck
                       0, //mana
                       null, //sound for when it is this character's turn to act
                       monkeyAttackSheet, //character's attack animation sprite 
                       2, monkeyAvatar, monkeyAttackSheet[0], monkeyHurtSheet, scarecrowWalk_Sheet, monkeyIdleSheet,
                       monkeySounds, null);

        MakeMobTemplate("hellflyingmonkey", //characterID
                       "Hellbent Flying Monkey", // charName
                       "Evil flying monkey hell-bent on causing mischief and wreaking havok.", // charDesc
                       "claws", //verb used when attacking
                       false, //is it a player character(true), or is it an enemy(false)?
                       MainData.MainLoop.TweakingComponent.hellbentflyingMonkeyMaxHealth, //the base HP value
                       MainData.MainLoop.TweakingComponent.hellbentflyingMonkeyMinDamage, // the minimum damage value
                       MainData.MainLoop.TweakingComponent.hellbentflyingMonkeyMaxDamage, //the maximum damage value.
                       MainData.MainLoop.TweakingComponent.hellbentflyingMonkeySpeed, //base speed, higher is better
                       MainData.MainLoop.TweakingComponent.hellbentflyingMonkeyDefense, //defense
                       MainData.MainLoop.TweakingComponent.hellbentflyingMonkeyLuck, //luck
                       0, //mana
                       null, //sound for when it is this character's turn to act
                       monkeyAttackSheet, //character's attack animation sprite 
                       2, monkeyAvatar, monkeyAttackSheet[0], monkeyHurtSheet, scarecrowWalk_Sheet, monkeyIdleSheet,
                       monkeySounds, null);

        MakeMobTemplate("corrupted", //characterID
                       "Corrupt Citizen", // charName
                       "A former citizen of the Emerald Palace, now corrupted by its magic.", // charDesc
                       "claws", //verb used when attacking
                       false, //is it a player character(true), or is it an enemy(false)?
                       MainData.MainLoop.TweakingComponent.corruptedMaxHealth, //the base HP value
                       MainData.MainLoop.TweakingComponent.corruptedMinDamage, // the minimum damage value
                       MainData.MainLoop.TweakingComponent.corruptedMaxDamage, //the maximum damage value.
                       MainData.MainLoop.TweakingComponent.corruptedSpeed, //base speed, higher is better
                       MainData.MainLoop.TweakingComponent.corruptedDefense, //defense
                       MainData.MainLoop.TweakingComponent.corruptedLuck, //luck
                       0, //mana
                       null, //sound for when it is this character's turn to act
                       emeraldAttackSheet, //character's attack animation sprite 
                       2, emeraldAvatar, emeraldAttackSheet[0], emeraldHurtSheet, scarecrowWalk_Sheet, emeraldIdleSheet,
                       corruptedSounds, null);

        MakeMobTemplate("weakcorrupted", //characterID
               "Weak Corrupt Citizen", // charName
               "A former citizen of the Emerald Palace, now corrupted by its magic.", // charDesc
               "claws", //verb used when attacking
               false, //is it a player character(true), or is it an enemy(false)?
               MainData.MainLoop.TweakingComponent.weakcorruptedMaxHealth, //the base HP value
               MainData.MainLoop.TweakingComponent.weakcorruptedMinDamage, // the minimum damage value
               MainData.MainLoop.TweakingComponent.weakcorruptedMaxDamage, //the maximum damage value.
               MainData.MainLoop.TweakingComponent.weakcorruptedSpeed, //base speed, higher is better
               MainData.MainLoop.TweakingComponent.weakcorruptedDefense, //defense
               MainData.MainLoop.TweakingComponent.weakcorruptedLuck, //luck
               0, //mana
               null, //sound for when it is this character's turn to act
               emeraldAttackSheet, //character's attack animation sprite 
               2, emeraldAvatar, emeraldAttackSheet[0], emeraldHurtSheet, scarecrowWalk_Sheet, emeraldIdleSheet,
               corruptedSounds, null);

        MakeMobTemplate("toughcorrupted", //characterID
               "Tough Corrupt Citizen", // charName
               "A former citizen of the Emerald Palace, now corrupted by its magic.", // charDesc
               "claws", //verb used when attacking
               false, //is it a player character(true), or is it an enemy(false)?
               MainData.MainLoop.TweakingComponent.toughcorruptedMaxHealth, //the base HP value
               MainData.MainLoop.TweakingComponent.toughcorruptedMinDamage, // the minimum damage value
               MainData.MainLoop.TweakingComponent.toughcorruptedMaxDamage, //the maximum damage value.
               MainData.MainLoop.TweakingComponent.toughcorruptedSpeed, //base speed, higher is better
               MainData.MainLoop.TweakingComponent.toughcorruptedDefense, //defense
               MainData.MainLoop.TweakingComponent.toughcorruptedLuck, //luck
               0, //mana
               null, //sound for when it is this character's turn to act
               emeraldAttackSheet, //character's attack animation sprite 
               2, emeraldAvatar, emeraldAttackSheet[0], emeraldHurtSheet, scarecrowWalk_Sheet, emeraldIdleSheet,
               corruptedSounds, null);

        MakeMobTemplate("legendarycorrupted", //characterID
               "Legendary Corrupt Citizen", // charName
               "A former citizen of the Emerald Palace, now corrupted by its magic.", // charDesc
               "claws", //verb used when attacking
               false, //is it a player character(true), or is it an enemy(false)?
               MainData.MainLoop.TweakingComponent.legendarycorruptedMaxHealth, //the base HP value
               MainData.MainLoop.TweakingComponent.legendarycorruptedMinDamage, // the minimum damage value
               MainData.MainLoop.TweakingComponent.legendarycorruptedMaxDamage, //the maximum damage value.
               MainData.MainLoop.TweakingComponent.legendarycorruptedSpeed, //base speed, higher is better
               MainData.MainLoop.TweakingComponent.legendarycorruptedDefense, //defense
               MainData.MainLoop.TweakingComponent.legendarycorruptedLuck, //luck
               0, //mana
               null, //sound for when it is this character's turn to act
               emeraldAttackSheet, //character's attack animation sprite 
               2, emeraldAvatar, emeraldAttackSheet[0], emeraldHurtSheet, scarecrowWalk_Sheet, emeraldIdleSheet,
               corruptedSounds, null);

        MakeMobTemplate("wickedwitch", //characterID
               "Wicked Witch of the East", // charName
               "A former citizen of the Emerald Palace, now corrupted by its magic.", // charDesc
               "claws", //verb used when attacking
               false, //is it a player character(true), or is it an enemy(false)?
               MainData.MainLoop.TweakingComponent.witchMaxHealth, //the base HP value
               MainData.MainLoop.TweakingComponent.witchMinDamage, // the minimum damage value
               MainData.MainLoop.TweakingComponent.witchMaxDamage, //the maximum damage value.
               MainData.MainLoop.TweakingComponent.witchSpeed, //base speed, higher is better
               MainData.MainLoop.TweakingComponent.witchDefense, //defense
               MainData.MainLoop.TweakingComponent.witchLuck, //luck
               0, //mana
               null, //sound for when it is this character's turn to act
               witchAttackSheet, //character's attack animation sprite 
               2, witchAvatar, witchAttackSheet[0], witchHurtSheet, scarecrowWalk_Sheet, witchIdleSheet,
               witchSounds, null, summoner: true, summonedEntity: "weakflyingmonkey");

    }


    //public void SpawnEnemyTest()
    //{//creates new enemies using a random, free, enemy spot. that's up to 7 enemies currently but just copy and arrange more if desired...

    //    // GameObject leftborder = StaticDataHolder.MainLoop.PositionHolderComponent.EnemySpawnBoundaryLeft;
    //    // GameObject rightborder = StaticDataHolder.MainLoop.PositionHolderComponent.EnemySpawnBoundaryRight;

    //    // GameObject b = Instantiate(EnemyPrefab, new Vector3(UnityEngine.Random.Range(leftborder.transform.position.x, rightborder.transform.position.x),
    //    //  rightborder.transform.position.y, 0), Quaternion.identity, StaticDataHolder.MainLoop.backgroundObject.transform);

    //    //get random unused object
    //    if (freeEnemyPartyMemberObjects.Count == 0)
    //    {
    //        MainData.MainLoop.EventLoggingComponent.LogGray("Tried spawning, no more spots...");
    //        return;
    //    }
    //    //string bo = "";
    //    //foreach (GameObject item in freeEnemyPartyMemberObjects)
    //    //{
    //    //    bo += item.name + "\n";

    //    //}

    //    int x = UnityEngine.Random.Range(0, freeEnemyPartyMemberObjects.Count);
    //    MainData.MainLoop.EventLoggingComponent.LogDanger("Spawned enemy using spot at freeEnemyPartyMemberObjects[" + x.ToString() + "].");
    //    GameObject b = freeEnemyPartyMemberObjects[x]; //we get a random, inactive enemy spot
    //    freeEnemyPartyMemberObjects.RemoveAt(x);
    //    usedEnemyPartyMemberObjects.Add(b); //tracks usage
    //    //freeEnemyPartyMemberObjects.Remove(b);//officialy live
    //    b.SetActive(true);//we turn it on
    //    CharacterWorldspaceScript d = b.GetComponent<CharacterWorldspaceScript>();//get the Cscript reference





    //    d.SetupCharacterByTemplate(MainData.characterTypes["flyingmonkey"]); //assign an enemy template
    //                                                                         //StaticDataHolder.livingEnemyParty.Add(d.associatedCharacter);//they are added to the living list in the above method
    //    MainData.MainLoop.EventLoggingComponent.LogGray("A new friend has arrived.");
    //    MainData.MainLoop.inCombat = true;
    //    MainData.MainLoop.UserInterfaceHelperComponent.RefreshViewEnemy();

    //    //these are for making smoke appear behind new spawns if desired later on.
    //    //GameObject cx = Instantiate(SpawnAnimationPrefab, d.gameObject.transform.position, Quaternion.identity);
    //    //StartCoroutine(DelAfterTime(cx));
    //}


    public void SpawnEncounter(Encounter b)
    {
        difficultyCurrency++;
        MainData.MainLoop.LevelHelperComponent.GENERATE();

        List<CharacterWorldspaceScript> enemiesSpawned = new List<CharacterWorldspaceScript>();
        b.spawned = true;//no repeat customers

        foreach (string item in b.enemies)
        {
            int x = UnityEngine.Random.Range(0, freeEnemyPartyMemberObjects.Count); //random spot

            //MainData.MainLoop.EventLoggingComponent.LogDanger("Spawned enemy using spot at freeEnemyPartyMemberObjects[" + x.ToString() + "].");
            GameObject f = freeEnemyPartyMemberObjects[x]; //we get a random, inactive enemy spot
            freeEnemyPartyMemberObjects.RemoveAt(x); //we remove the spot from the inactive/free enemy spot list
            usedEnemyPartyMemberObjects.Add(f); //track usage...
            f.SetActive(true);//we turn the spot on on
            CharacterWorldspaceScript d = f.GetComponent<CharacterWorldspaceScript>();//get the Cscript reference
            d.SetupCharacterByTemplate(MainData.characterTypes[item]); //assign and set up an enemy template to the spot
            //they are added to the living list in the above method
            MainData.MainLoop.EventLoggingComponent.LogGray("A " + d.associatedCharacter.charName + " suddenly steps out of the shadows.");
            enemiesSpawned.Add(d);
        }
        //refresh the miniview thingies whenever we spawn or kill stuff
        MainData.MainLoop.inCombat = true;
        MainData.MainLoop.UserInterfaceHelperComponent.RefreshViewEnemy();


        foreach (CharacterWorldspaceScript item in enemiesSpawned)
        {
            item.SetupIdleAnimAndStart();
            float baseScaleAmnt = MainData.MainLoop.LevelHelperComponent.difCurrency / 10; //Calculate stat scaling

            item.associatedCharacter.baseDamageMin += ((int)baseScaleAmnt) * 2; //Apply scaling to damage (min and max equally)
            item.associatedCharacter.baseDamageMax += ((int)baseScaleAmnt) * 2;
            item.associatedCharacter.defense += (int)((int)baseScaleAmnt * 0.3); //Apply bonus defense
            item.associatedCharacter.maxHealth += (int)baseScaleAmnt;
            item.associatedCharacter.currentHealth += (int)baseScaleAmnt;
            item.associatedCharacter.InitializeHealthBar();
            item.associatedCharacter.baseSpeed += (int)((int)baseScaleAmnt * 0.7);

            Debug.Log("DIFFICULTY = " + baseScaleAmnt);
        }

        //MainData.MainLoop.EventLoggingComponent.LogDisplayGradualText("You get a bad feeling.");
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
        MakeConsumableItemTemplate("health_potion", //dictionary key string
                            "Wondrous concoction that makes wounds close before one's very eyes.",
                            "Overuse may cause halitosis.", //short flavourful blurb
                            "Health Infusion",
                            HealthPotionSprite,
                            "uncommon", //rarity as a string
                            4,//value in currency
                            30, //how much the trader gets in stock
                            1, //default quantity
                            true); //helpful(true) or harmful(false)? Used for threat
        MakeConsumableItemTemplate("mana_potion",  //dictionary key string
                            "Willpower, concentration and pure energy in a bottle.",
                            " ", //short flavourful blurb
                            "Mana Potion",
                            HealthPotionSprite,
                            "uncommon", //rarity as a string
                            4,//value in currency
                            15, //how much the trader gets in stock
                            1, //default quantity
                            true); //true - helpful.               false - harmful. 
        MakeConsumableItemTemplate("antidote_potion",  //dictionary key string
                            "A must-have in the forest.",
                            " ", //short flavourful blurb
                            "Antidote Potion",
                            HealthPotionSprite,
                            "uncommon", //rarity as a string
                            4,//value in currency
                            2, //how much the trader gets in stock
                            1, //default quantity
                            true); //true - helpful.               false - harmful. 




    }
    /// <summary>
    /// method to make new consumable items
    /// </summary>
    /// <param name="identifier">shorthand name with _ instead of spaces and no capitalization</param>
    /// <param name="description"></param>
    ///<param name="blurby"> short quote/description. for the shop.</param>
    /// <param name="itemName"></param>
    /// <param name="itemSprite"></param>
    /// <param name="rarity">rarity as a string.</param>
    /// <param name="value">value in eyes</param>
    /// <param name="amtInStock">stock in the trade place</param>
    /// <param name="itemQuantity">quantity given.</param>
    /// <param name="beneficial">is it poison/a bomb or is it a potion</param>
    private void MakeConsumableItemTemplate(string identifier,
                    string description,
                    string blurby,
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
              blurby,
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
        CharacterWorldspaceScript slot1ref = MainData.MainLoop.PositionHolderComponent.PartySlot1.GetComponent<CharacterWorldspaceScript>();
        CharacterWorldspaceScript slot2ref = MainData.MainLoop.PositionHolderComponent.PartySlot2.GetComponent<CharacterWorldspaceScript>();
        CharacterWorldspaceScript slot3ref = MainData.MainLoop.PositionHolderComponent.PartySlot3.GetComponent<CharacterWorldspaceScript>();
        CharacterWorldspaceScript slot4ref = MainData.MainLoop.PositionHolderComponent.PartySlot4.GetComponent<CharacterWorldspaceScript>();


        slot1ref.SetupCharacterByTemplate(MainData.characterTypes["scarecrow"]);
        slot2ref.SetupCharacterByTemplate(MainData.characterTypes["tin_man"]);
        slot3ref.SetupCharacterByTemplate(MainData.characterTypes["lion"]);
        slot4ref.SetupCharacterByTemplate(MainData.characterTypes["dorothy"]);
        foreach (Character item in MainData.livingPlayerParty)
        {
            item.RecalculateThreatFromStats();
        }

        //get random trait
        //nonrepeating
        //foreach (Character item in MainData.livingPlayerParty)
        //{




        //}


        MainData.MainLoop.UserInterfaceHelperComponent.PC1 = slot1ref;
        MainData.MainLoop.UserInterfaceHelperComponent.PC2 = slot2ref;
        MainData.MainLoop.UserInterfaceHelperComponent.PC3 = slot3ref;
        MainData.MainLoop.UserInterfaceHelperComponent.PC4 = slot4ref;

        MainData.MainLoop.UserInterfaceHelperComponent.pc1clickableoverview.associatedCharacter = slot1ref.associatedCharacter;
        MainData.MainLoop.UserInterfaceHelperComponent.pc2clickableoverview.associatedCharacter = slot2ref.associatedCharacter;
        MainData.MainLoop.UserInterfaceHelperComponent.pc3clickableoverview.associatedCharacter = slot3ref.associatedCharacter;
        MainData.MainLoop.UserInterfaceHelperComponent.pc4clickableoverview.associatedCharacter = slot4ref.associatedCharacter;


        foreach (Character item in MainData.livingPlayerParty)
        {
            item.RecalculateStatsFromTraits();
            //item.RecalculateStatsFromItems(); this is done when we attack or use the stat in most cases, using either GetCompoundSpeed() or such, or a foreach loop looking into the items
            item.RecalculateThreatFromStats();
        }
    }

    public void TestGivePlayerItems()
    {

        Item b = new Item(MainData.allEquipment["short_sword"].identifier,
                          MainData.allEquipment["short_sword"].description,
                          MainData.allEquipment["short_sword"].itemBlurb,
                          MainData.allEquipment["short_sword"].itemName,
                          MainData.allEquipment["short_sword"].itemSprite,
                          MainData.allEquipment["short_sword"].rarity,
                          MainData.allEquipment["short_sword"].value,
                          MainData.allEquipment["short_sword"].amtInStock,
                          MainData.allEquipment["short_sword"].itemQuantity,
                          MainData.allEquipment["short_sword"].beneficial,
                          MainData.allEquipment["short_sword"].isEquipable,
                          MainData.allEquipment["short_sword"].speedmodifier,
                          MainData.allEquipment["short_sword"].healthmodifier,
                          MainData.allEquipment["short_sword"].manamodifier,
                          MainData.allEquipment["short_sword"].dmgmodifier,
                          MainData.allEquipment["short_sword"].defensemodifier,
                          MainData.allEquipment["short_sword"].luckmodifier,
                          MainData.allEquipment["short_sword"].healingAmp,
                          MainData.allEquipment["short_sword"].DamageResistancePercentage,
                          MainData.allEquipment["short_sword"].DamageBonusPercentage,
                          MainData.allEquipment["short_sword"].discountPercentage,
                          50);

        //MainData.equipmentInventory.Add(FetchEquipment());
        //MainData.equipmentInventory.Add(FetchEquipment());
        //MainData.equipmentInventory.Add(FetchEquipment());
        //MainData.equipmentInventory.Add(FetchEquipment());
        //MainData.equipmentInventory.Add(FetchEquipment());
        MainData.equipmentInventory.Add(b);
        //MainData.equipmentInventory.Add(d);
        //MainData.equipmentInventory.Add(c);

    }


    public void DistributeStartingTraits()
    {
        //List<Trait> t1copy = MainData.t1traitList.Values.ToList();
        //List<Trait> partyNewTraits = new List<Trait>();
        //for (int i = 0; i < 4; i++)
        //{
        //    int randy = Random.Range(0, t1copy.Count + 1);
        //    partyNewTraits.Add(t1copy[randy]);
        //    t1copy.RemoveAt(randy);
        //    livingPlayerParty[i].ChangeTrait(partyNewTraits[i]);
        //    MainData.MainLoop.EventLoggingComponent.LogDanger(t1copy[randy].traitName + "has been given to " + livingPlayerParty[i].charName);
        //}

        CharacterWorldspaceScript slot1ref = MainData.MainLoop.PositionHolderComponent.PartySlot1.GetComponent<CharacterWorldspaceScript>();
        CharacterWorldspaceScript slot2ref = MainData.MainLoop.PositionHolderComponent.PartySlot2.GetComponent<CharacterWorldspaceScript>();
        CharacterWorldspaceScript slot3ref = MainData.MainLoop.PositionHolderComponent.PartySlot3.GetComponent<CharacterWorldspaceScript>();
        CharacterWorldspaceScript slot4ref = MainData.MainLoop.PositionHolderComponent.PartySlot4.GetComponent<CharacterWorldspaceScript>();

        int i = 0;

        while (i < 4)
        {
            int r = Random.Range(0, 7);

            if (i == 0)
            {
                i++;
                traitCounter[0] = r;
                slot1ref.associatedCharacter.ChangeTrait(MainData.t1traitList[traitNames[r]]);
            }
            else if (i == 1 && r != traitCounter[0])
            {
                i++;
                traitCounter[1] = r;
                slot2ref.associatedCharacter.ChangeTrait(MainData.t1traitList[traitNames[r]]);
            }
            else if (i == 2 && r != traitCounter[0] && r != traitCounter[1])
            {
                i++;
                traitCounter[2] = r;
                slot3ref.associatedCharacter.ChangeTrait(MainData.t1traitList[traitNames[r]]);
            }
            else if (i == 3 && r != traitCounter[0] && r != traitCounter[1] && r != traitCounter[2])
            {
                i++;
                traitCounter[3] = r;
                slot4ref.associatedCharacter.ChangeTrait(MainData.t1traitList[traitNames[r]]);
            }
            else if (i == 4)
            {
                i++;
            }
        }


        MainData.MainLoop.UserInterfaceHelperComponent.RefreshCharacterTabs();
    }


    public void DefineEquipment()
    {//generates traits, stores them all in a dictionary in the dataholder

        MakeEquipableItemTemplate("short_sword", //string ID
                                          "A sword, preeminent hand weapon through a long period of history. It consists of a metal blade varying in length, breadth, and configuration but longer than a dagger and fitted with a handle or hilt usually equipped with a guard. This one is normal.",//the desc
                                          "Common implement of war. Can usually be found between your ribs.", //blurb
                                          "Shortsword", //name
                                          shortSwordSprite, //sprite
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
                                          0, //healing, multiplicative. as a percentage.
                                          0, //dam resist, multiplicative. as a percentage.
                                          0, //dam bonus multiplicative. as a percentage.
                                          0, //discount as a percentage, multiplicative
                                          0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("damaged_short_sword", //string ID
                                  "A sword, preeminent hand weapon through a long period of history. It consists of a metal blade varying in length, breadth, and configuration but longer than a dagger and fitted with a handle or hilt usually equipped with a guard. This one is damaged.",//the desc
                                  "Common implement of war. Can usually be found between your ribs.", //blurb
                                  "Shortsword", //name
                                  shortSwordSprite, //sprite
                                  "common", //rarity string
                                  1, //value in eyes
                                  1, //amount in birb's stock
                                  1, //default quantity
                                  true, //(true)beneficial or (false)harmful
                                  0, //speed bonus
                                  0, //health bonus
                                  0, //mana bonus
                                  1, //dmg bonus
                                  0, //defense bonus 
                                  0, //luck bonus 
                                  0, //healing, multiplicative. as a percentage.
                                  0, //dam resist, multiplicative. as a percentage.
                                  0, //dam bonus multiplicative. as a percentage.
                                  0, //discount as a percentage, multiplicative
                                  0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("sharp_short_sword", //string ID
                                  "A sword, preeminent hand weapon through a long period of history. It consists of a metal blade varying in length, breadth, and configuration but longer than a dagger and fitted with a handle or hilt usually equipped with a guard. This one is sharp.",//the desc
                                  "Common implement of war. Can usually be found between your ribs.", //blurb
                                  "Shortsword", //name
                                  shortSwordSprite, //sprite
                                  "common", //rarity string
                                  8, //value in eyes
                                  1, //amount in birb's stock
                                  1, //default quantity
                                  true, //(true)beneficial or (false)harmful
                                  0, //speed bonus
                                  0, //health bonus
                                  0, //mana bonus
                                  4, //dmg bonus
                                  0, //defense bonus 
                                  0, //luck bonus 
                                  0, //healing, multiplicative. as a percentage.
                                  0, //dam resist, multiplicative. as a percentage.
                                  0, //dam bonus multiplicative. as a percentage.
                                  0, //discount as a percentage, multiplicative
                                  0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("light_short_sword", //string ID
                                  "A sword, preeminent hand weapon through a long period of history. It consists of a metal blade varying in length, breadth, and configuration but longer than a dagger and fitted with a handle or hilt usually equipped with a guard. This one is light.",//the desc
                                  "Common implement of war. Can usually be found between your ribs.", //blurb
                                  "Shortsword", //name
                                  shortSwordSprite, //sprite
                                  "common", //rarity string
                                  6, //value in eyes
                                  1, //amount in birb's stock
                                  1, //default quantity
                                  true, //(true)beneficial or (false)harmful
                                  3, //speed bonus
                                  0, //health bonus
                                  0, //mana bonus
                                  2, //dmg bonus
                                  0, //defense bonus 
                                  0, //luck bonus 
                                  0, //healing, multiplicative. as a percentage.
                                  0, //dam resist, multiplicative. as a percentage.
                                  0, //dam bonus multiplicative. as a percentage.
                                  0, //discount as a percentage, multiplicative
                                  0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("cheap_short_sword", //string ID
                                  "A sword, preeminent hand weapon through a long period of history. It consists of a metal blade varying in length, breadth, and configuration but longer than a dagger and fitted with a handle or hilt usually equipped with a guard. This one is cheap.",//the desc
                                  "Common implement of war. Can usually be found between your ribs.", //blurb
                                  "Shortsword", //name
                                  shortSwordSprite, //sprite
                                  "common", //rarity string
                                  5, //value in eyes
                                  1, //amount in birb's stock
                                  1, //default quantity
                                  true, //(true)beneficial or (false)harmful
                                  0, //speed bonus
                                  0, //health bonus
                                  0, //mana bonus
                                  2, //dmg bonus
                                  0, //defense bonus 
                                  0, //luck bonus 
                                  0, //healing, multiplicative. as a percentage.
                                  0, //dam resist, multiplicative. as a percentage.
                                  0, //dam bonus multiplicative. as a percentage.
                                  0, //discount as a percentage, multiplicative
                                  0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("long_sword", //string ID
                                  "A sword, preeminent double-hand weapon through a long period of history. Consisting of the strongest metal known in the kingdom and crafted by the most talented blacksmiths, this weapon is sure to make any combat encounter short. This one is normal.",//the desc
                                  "Common implement of war. Can usually be found between your ribs.", //blurb
                                  "Longsword", //name
                                  longSwordSprite, //sprite
                                  "uncommon", //rarity string
                                  8, //value in eyes
                                  1, //amount in birb's stock
                                  1, //default quantity
                                  true, //(true)beneficial or (false)harmful
                                  0, //speed bonus
                                  0, //health bonus
                                  0, //mana bonus
                                  2, //dmg bonus
                                  0, //defense bonus 
                                  0, //luck bonus 
                                  0, //healing, multiplicative. as a percentage.
                                  0, //dam resist, multiplicative. as a percentage.
                                  15, //dam bonus multiplicative. as a percentage.
                                  0, //discount as a percentage, multiplicative
                                  0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("dull_long_sword", //string ID
                          "A sword, preeminent double-hand weapon through a long period of history. Consisting of the strongest metal known in the kingdom and crafted by the most talented blacksmiths, this weapon is sure to make any combat encounter short. This one is dull.",//the desc
                          "Common implement of war. Can usually be found between your ribs.", //blurb
                          "Longsword", //name
                          longSwordSprite, //sprite
                          "uncommon", //rarity string
                          6, //value in eyes
                          1, //amount in birb's stock
                          1, //default quantity
                          true, //(true)beneficial or (false)harmful
                          0, //speed bonus
                          0, //health bonus
                          0, //mana bonus
                          1, //dmg bonus
                          0, //defense bonus 
                          0, //luck bonus 
                          0, //healing, multiplicative. as a percentage.
                          0, //dam resist, multiplicative. as a percentage.
                          15, //dam bonus multiplicative. as a percentage.
                          0, //discount as a percentage, multiplicative
                          0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("vengeful_long_sword", //string ID
                          "A sword, preeminent double-hand weapon through a long period of history. Consisting of the strongest metal known in the kingdom and crafted by the most talented blacksmiths, this weapon is sure to make any combat encounter short. This one is haunted.",//the desc
                          "Common implement of war. Can usually be found between your ribs.", //blurb
                          "Longsword", //name
                          longSwordSprite, //sprite
                          "uncommon", //rarity string
                          13, //value in eyes
                          1, //amount in birb's stock
                          1, //default quantity
                          true, //(true)beneficial or (false)harmful
                          0, //speed bonus
                          0, //health bonus
                          0, //mana bonus
                          5, //dmg bonus
                          0, //defense bonus 
                          0, //luck bonus 
                          0, //healing, multiplicative. as a percentage.
                          0, //dam resist, multiplicative. as a percentage.
                          15, //dam bonus multiplicative. as a percentage.
                          0, //discount as a percentage, multiplicative
                          10); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("damaged_long_sword", //string ID
                          "A sword, preeminent double-hand weapon through a long period of history. Consisting of the strongest metal known in the kingdom and crafted by the most talented blacksmiths, this weapon is sure to make any combat encounter short. This one is damaged.",//the desc
                          "Common implement of war. Can usually be found between your ribs.", //blurb
                          "Longsword", //name
                          longSwordSprite, //sprite
                          "uncommon", //rarity string
                          3, //value in eyes
                          1, //amount in birb's stock
                          1, //default quantity
                          true, //(true)beneficial or (false)harmful
                          0, //speed bonus
                          0, //health bonus
                          0, //mana bonus
                          1, //dmg bonus
                          0, //defense bonus 
                          0, //luck bonus 
                          0, //healing, multiplicative. as a percentage.
                          0, //dam resist, multiplicative. as a percentage.
                          7, //dam bonus multiplicative. as a percentage.
                          0, //discount as a percentage, multiplicative
                          0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("enchanted_long_sword", //string ID
                          "A sword, preeminent double-hand weapon through a long period of history. Consisting of the strongest metal known in the kingdom and crafted by the most talented blacksmiths, this weapon is sure to make any combat encounter short. This one is enchanted.",//the desc
                          "Common implement of war. Can usually be found between your ribs.", //blurb
                          "Longsword", //name
                          longSwordSprite, //sprite
                          "uncommon", //rarity string
                          9, //value in eyes
                          1, //amount in birb's stock
                          1, //default quantity
                          true, //(true)beneficial or (false)harmful
                          0, //speed bonus
                          0, //health bonus
                          25, //mana bonus
                          2, //dmg bonus
                          0, //defense bonus 
                          0, //luck bonus 
                          0, //healing, multiplicative. as a percentage.
                          0, //dam resist, multiplicative. as a percentage.
                          15, //dam bonus multiplicative. as a percentage.
                          0, //discount as a percentage, multiplicative
                          0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("vampiric_ring", //string ID
                          "A vampire's ring, recovered by archeologists exploring ancient ruins. Crafted with fine gold and blood ruby, it contains the haunting spirit of a vampire within, granting the wearer unique, supernatural abilities. This one is normal.",//the desc
                          "A rare, special relic. You feel drawn in by it's emminating power.", //blurb
                          "Vampiric Ring", //name
                          VampiricSprite, //sprite
                          "rare", //rarity string
                          7, //value in eyes
                          1, //amount in birb's stock
                          1, //default quantity
                          true, //(true)beneficial or (false)harmful
                          0, //speed bonus
                          0, //health bonus
                          0, //mana bonus
                          0, //dmg bonus
                          0, //defense bonus 
                          0, //luck bonus 
                          0, //healing, multiplicative. as a percentage.
                          0, //dam resist, multiplicative. as a percentage.
                          10, //dam bonus multiplicative. as a percentage.
                          0, //discount as a percentage, multiplicative
                          10); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("warding_vampiric_ring", //string ID
                  "A vampire's ring, recovered by archeologists exploring ancient ruins. Crafted with fine gold and blood ruby, it contains the haunting spirit of a vampire within, granting the wearer unique, supernatural abilities. This one is tough.",//the desc
                  "A rare, special relic. You feel drawn in by it's emminating power.", //blurb
                  "Vampiric Ring", //name
                  VampiricSprite, //sprite
                  "rare", //rarity string
                  9, //value in eyes
                  1, //amount in birb's stock
                  1, //default quantity
                  true, //(true)beneficial or (false)harmful
                  0, //speed bonus
                  0, //health bonus
                  0, //mana bonus
                  0, //dmg bonus
                  2, //defense bonus 
                  0, //luck bonus 
                  0, //healing, multiplicative. as a percentage.
                  0, //dam resist, multiplicative. as a percentage.
                  10, //dam bonus multiplicative. as a percentage.
                  0, //discount as a percentage, multiplicative
                  10); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("lucky_vampiric_ring", //string ID
                  "A vampire's ring, recovered by archeologists exploring ancient ruins. Crafted with fine gold and blood ruby, it contains the haunting spirit of a vampire within, granting the wearer unique, supernatural abilities. This one feels lucky.",//the desc
                  "A rare, special relic. You feel drawn in by it's emminating power.", //blurb
                  "Vampiric Ring", //name
                  VampiricSprite, //sprite
                  "rare", //rarity string
                  8, //value in eyes
                  1, //amount in birb's stock
                  1, //default quantity
                  true, //(true)beneficial or (false)harmful
                  0, //speed bonus
                  0, //health bonus
                  0, //mana bonus
                  0, //dmg bonus
                  0, //defense bonus 
                  10, //luck bonus 
                  0, //healing, multiplicative. as a percentage.
                  0, //dam resist, multiplicative. as a percentage.
                  10, //dam bonus multiplicative. as a percentage.
                  0, //discount as a percentage, multiplicative
                  10); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("enchanted_vampiric_ring", //string ID
                  "A vampire's ring, recovered by archeologists exploring ancient ruins. Crafted with fine gold and blood ruby, it contains the haunting spirit of a vampire within, granting the wearer unique, supernatural abilities. This one is enchanted.",//the desc
                  "A rare, special relic. You feel drawn in by it's emminating power.", //blurb
                  "Vampiric Ring", //name
                  VampiricSprite, //sprite
                  "rare", //rarity string
                  8, //value in eyes
                  1, //amount in birb's stock
                  1, //default quantity
                  true, //(true)beneficial or (false)harmful
                  0, //speed bonus
                  0, //health bonus
                  25, //mana bonus
                  0, //dmg bonus
                  0, //defense bonus 
                  0, //luck bonus 
                  0, //healing, multiplicative. as a percentage.
                  0, //dam resist, multiplicative. as a percentage.
                  10, //dam bonus multiplicative. as a percentage.
                  0, //discount as a percentage, multiplicative
                  10); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("cheap_vampiric_ring", //string ID
                  "A vampire's ring, recovered by archeologists exploring ancient ruins. Crafted with fine gold and blood ruby, it contains the haunting spirit of a vampire within, granting the wearer unique, supernatural abilities. This one is cheap.",//the desc
                  "A rare, special relic. You feel drawn in by it's emminating power.", //blurb
                  "Vampiric Ring", //name
                  VampiricSprite, //sprite
                  "rare", //rarity string
                  6, //value in eyes
                  1, //amount in birb's stock
                  1, //default quantity
                  true, //(true)beneficial or (false)harmful
                  0, //speed bonus
                  0, //health bonus
                  0, //mana bonus
                  0, //dmg bonus
                  0, //defense bonus 
                  0, //luck bonus 
                  0, //healing, multiplicative. as a percentage.
                  0, //dam resist, multiplicative. as a percentage.
                  10, //dam bonus multiplicative. as a percentage.
                  0, //discount as a percentage, multiplicative
                  10); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("shard_constitution", //string ID
                  "A rare gem radiating unexplainable magic. Excavated from the royal mining site, rare gems like these have been told to contain the souls of dragons who died underground in their lairs. Others say they are unique combinations of minerals. This one is normal.",//the desc
                  "A powerful and rare gem. You feel drawn in by it's emminating power.", //blurb
                  "Shard of Constitution", //name
                  ConstitutionSprite, //sprite
                  "common", //rarity string
                  4, //value in eyes
                  1, //amount in birb's stock
                  1, //default quantity
                  true, //(true)beneficial or (false)harmful
                  0, //speed bonus
                  5, //health bonus
                  25, //mana bonus
                  0, //dmg bonus
                  0, //defense bonus 
                  0, //luck bonus 
                  0, //healing, multiplicative. as a percentage.
                  10, //dam resist, multiplicative. as a percentage.
                  0, //dam bonus multiplicative. as a percentage.
                  0, //discount as a percentage, multiplicative
                  0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("enchanted_shard_constitution", //string ID
          "A rare gem radiating unexplainable magic. Excavated from the royal mining site, rare gems like these have been told to contain the souls of dragons who died underground in their lairs. Others say they are unique combinations of minerals. This one is enchanted.",//the desc
          "A powerful and rare gem. You feel drawn in by it's emminating power.", //blurb
          "Shard of Constitution", //name
          ConstitutionSprite, //sprite
          "common", //rarity string
          1, //value in eyes
          1, //amount in birb's stock
          1, //default quantity
          true, //(true)beneficial or (false)harmful
          0, //speed bonus
          5, //health bonus
          50, //mana bonus
          0, //dmg bonus
          0, //defense bonus 
          0, //luck bonus 
          0, //healing, multiplicative. as a percentage.
          10, //dam resist, multiplicative. as a percentage.
          0, //dam bonus multiplicative. as a percentage.
          0, //discount as a percentage, multiplicative
          0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("warding_shard_constitution", //string ID
          "A rare gem radiating unexplainable magic. Excavated from the royal mining site, rare gems like these have been told to contain the souls of dragons who died underground in their lairs. Others say they are unique combinations of minerals. This one is tough.",//the desc
          "A powerful and rare gem. You feel drawn in by it's emminating power.", //blurb
          "Shard of Constitution", //name
          ConstitutionSprite, //sprite
          "common", //rarity string
          6, //value in eyes
          1, //amount in birb's stock
          1, //default quantity
          true, //(true)beneficial or (false)harmful
          0, //speed bonus
          5, //health bonus
          25, //mana bonus
          0, //dmg bonus
          2, //defense bonus 
          0, //luck bonus 
          0, //healing, multiplicative. as a percentage.
          10, //dam resist, multiplicative. as a percentage.
          0, //dam bonus multiplicative. as a percentage.
          0, //discount as a percentage, multiplicative
          0); //lifesteal multiplicative. as a percentage.  

        MakeEquipableItemTemplate("damaged_shard_constitution", //string ID
          "A rare gem radiating unexplainable magic. Excavated from the royal mining site, rare gems like these have been told to contain the souls of dragons who died underground in their lairs. Others say they are unique combinations of minerals. This one is damaged.",//the desc
          "A powerful and rare gem. You feel drawn in by it's emminating power.", //blurb
          "Shard of Constitution", //name
          ConstitutionSprite, //sprite
          "common", //rarity string
          1, //value in eyes
          1, //amount in birb's stock
          1, //default quantity
          true, //(true)beneficial or (false)harmful
          0, //speed bonus
          2, //health bonus
          10, //mana bonus
          0, //dmg bonus
          0, //defense bonus 
          0, //luck bonus 
          0, //healing, multiplicative. as a percentage.
          0, //dam resist, multiplicative. as a percentage.
          0, //dam bonus multiplicative. as a percentage.
          0, //discount as a percentage, multiplicative
          0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("light_shard_constitution", //string ID
          "A rare gem radiating unexplainable magic. Excavated from the royal mining site, rare gems like these have been told to contain the souls of dragons who died underground in their lairs. Others say they are unique combinations of minerals. This one is light.",//the desc
          "A powerful and rare gem. You feel drawn in by it's emminating power.", //blurb
          "Shard of Constitution", //name
          ConstitutionSprite, //sprite
          "common", //rarity string
          4, //value in eyes
          1, //amount in birb's stock
          1, //default quantity
          true, //(true)beneficial or (false)harmful
          3, //speed bonus
          5, //health bonus
          25, //mana bonus
          0, //dmg bonus
          0, //defense bonus 
          0, //luck bonus 
          0, //healing, multiplicative. as a percentage.
          10, //dam resist, multiplicative. as a percentage.
          0, //dam bonus multiplicative. as a percentage.
          0, //discount as a percentage, multiplicative
          0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("shard_power", //string ID
          "A rare gem radiating unexplainable magic. Excavated from the royal mining site, rare gems like these have been told to contain the souls of dragons who died underground in their lairs. Others say they are unique combinations of minerals. This one is normal.",//the desc
          "A powerful and rare gem. You feel drawn in by it's emminating power.", //blurb
          "Shard of Power", //name
          PowerSprite, //sprite
          "common", //rarity string
          4, //value in eyes
          1, //amount in birb's stock
          1, //default quantity
          true, //(true)beneficial or (false)harmful
          0, //speed bonus
          0, //health bonus
          0, //mana bonus
          0, //dmg bonus
          0, //defense bonus 
          0, //luck bonus 
          0, //healing, multiplicative. as a percentage.
          10, //dam resist, multiplicative. as a percentage.
          10, //dam bonus multiplicative. as a percentage.
          0, //discount as a percentage, multiplicative
          0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("warding_shard_power", //string ID
          "A rare gem radiating unexplainable magic. Excavated from the royal mining site, rare gems like these have been told to contain the souls of dragons who died underground in their lairs. Others say they are unique combinations of minerals. This one is tough.",//the desc
          "A powerful and rare gem. You feel drawn in by it's emminating power.", //blurb
          "Shard of Power", //name
          PowerSprite, //sprite
          "common", //rarity string
          6, //value in eyes
          1, //amount in birb's stock
          1, //default quantity
          true, //(true)beneficial or (false)harmful
          0, //speed bonus
          0, //health bonus
          0, //mana bonus
          0, //dmg bonus
          2, //defense bonus 
          0, //luck bonus 
          0, //healing, multiplicative. as a percentage.
          10, //dam resist, multiplicative. as a percentage.
          10, //dam bonus multiplicative. as a percentage.
          0, //discount as a percentage, multiplicative
          0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("vengeful_shard_power", //string ID
  "A rare gem radiating unexplainable magic. Excavated from the royal mining site, rare gems like these have been told to contain the souls of dragons who died underground in their lairs. Others say they are unique combinations of minerals. This one is haunted.",//the desc
  "A powerful and rare gem. You feel drawn in by it's emminating power.", //blurb
  "Shard of Power", //name
  PowerSprite, //sprite
  "common", //rarity string
  9, //value in eyes
  1, //amount in birb's stock
  1, //default quantity
  true, //(true)beneficial or (false)harmful
  0, //speed bonus
  0, //health bonus
  0, //mana bonus
  3, //dmg bonus
  0, //defense bonus 
  0, //luck bonus 
  0, //healing, multiplicative. as a percentage.
  10, //dam resist, multiplicative. as a percentage.
  10, //dam bonus multiplicative. as a percentage.
  0, //discount as a percentage, multiplicative
  10); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("cheap_shard_power", //string ID
  "A rare gem radiating unexplainable magic. Excavated from the royal mining site, rare gems like these have been told to contain the souls of dragons who died underground in their lairs. Others say they are unique combinations of minerals. This one is cheap.",//the desc
  "A powerful and rare gem. You feel drawn in by it's emminating power.", //blurb
  "Shard of Power", //name
  PowerSprite, //sprite
  "common", //rarity string
  3, //value in eyes
  1, //amount in birb's stock
  1, //default quantity
  true, //(true)beneficial or (false)harmful
  0, //speed bonus
  0, //health bonus
  0, //mana bonus
  0, //dmg bonus
  0, //defense bonus 
  0, //luck bonus 
  0, //healing, multiplicative. as a percentage.
  10, //dam resist, multiplicative. as a percentage.
  10, //dam bonus multiplicative. as a percentage.
  0, //discount as a percentage, multiplicative
  0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("lucky_shard_power", //string ID
  "A rare gem radiating unexplainable magic. Excavated from the royal mining site, rare gems like these have been told to contain the souls of dragons who died underground in their lairs. Others say they are unique combinations of minerals. This one feels lucky.",//the desc
  "A powerful and rare gem. You feel drawn in by it's emminating power.", //blurb
  "Shard of Power", //name
  PowerSprite, //sprite
  "common", //rarity string
  5, //value in eyes
  1, //amount in birb's stock
  1, //default quantity
  true, //(true)beneficial or (false)harmful
  0, //speed bonus
  0, //health bonus
  0, //mana bonus
  0, //dmg bonus
  0, //defense bonus 
  10, //luck bonus 
  0, //healing, multiplicative. as a percentage.
  10, //dam resist, multiplicative. as a percentage.
  10, //dam bonus multiplicative. as a percentage.
  0, //discount as a percentage, multiplicative
  0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("shard_emerald", //string ID
  "A rare gem radiating unexplainable magic. Excavated from the royal mining site, rare gems like these have been told to contain the souls of dragons who died underground in their lairs. Others say they are unique combinations of minerals. This one is normal.",//the desc
  "A powerful and rare gem. You feel drawn in by it's emminating power.", //blurb
  "Shard of Emerald", //name
  EmeraldSprite, //sprite
  "common", //rarity string
  4, //value in eyes
  1, //amount in birb's stock
  1, //default quantity
  true, //(true)beneficial or (false)harmful
  0, //speed bonus
  0, //health bonus
  0, //mana bonus
  0, //dmg bonus
  0, //defense bonus 
  15, //luck bonus 
  0, //healing, multiplicative. as a percentage.
  0, //dam resist, multiplicative. as a percentage.
  0, //dam bonus multiplicative. as a percentage.
  0, //discount as a percentage, multiplicative
  0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("lucky_shard_emerald", //string ID
          "A rare gem radiating unexplainable magic. Excavated from the royal mining site, rare gems like these have been told to contain the souls of dragons who died underground in their lairs. Others say they are unique combinations of minerals. This one feels lucky.",//the desc
          "A powerful and rare gem. You feel drawn in by it's emminating power.", //blurb
          "Shard of Emerald", //name
          EmeraldSprite, //sprite
          "common", //rarity string
          5, //value in eyes
          1, //amount in birb's stock
          1, //default quantity
          true, //(true)beneficial or (false)harmful
          0, //speed bonus
          0, //health bonus
          0, //mana bonus
          0, //dmg bonus
          0, //defense bonus 
          25, //luck bonus 
          0, //healing, multiplicative. as a percentage.
          0, //dam resist, multiplicative. as a percentage.
          0, //dam bonus multiplicative. as a percentage.
          0, //discount as a percentage, multiplicative
          0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("warding_shard_emerald", //string ID
          "A rare gem radiating unexplainable magic. Excavated from the royal mining site, rare gems like these have been told to contain the souls of dragons who died underground in their lairs. Others say they are unique combinations of minerals. This one is tough.",//the desc
          "A powerful and rare gem. You feel drawn in by it's emminating power.", //blurb
          "Shard of Emerald", //name
          EmeraldSprite, //sprite
          "common", //rarity string
          6, //value in eyes
          1, //amount in birb's stock
          1, //default quantity
          true, //(true)beneficial or (false)harmful
          0, //speed bonus
          0, //health bonus
          0, //mana bonus
          0, //dmg bonus
          2, //defense bonus 
          15, //luck bonus 
          0, //healing, multiplicative. as a percentage.
          0, //dam resist, multiplicative. as a percentage.
          0, //dam bonus multiplicative. as a percentage.
          0, //discount as a percentage, multiplicative
          0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("damaged_shard_emerald", //string ID
          "A rare gem radiating unexplainable magic. Excavated from the royal mining site, rare gems like these have been told to contain the souls of dragons who died underground in their lairs. Others say they are unique combinations of minerals. This one is damaged.",//the desc
          "A powerful and rare gem. You feel drawn in by it's emminating power.", //blurb
          "Shard of Emerald", //name
          EmeraldSprite, //sprite
          "common", //rarity string
          1, //value in eyes
          1, //amount in birb's stock
          1, //default quantity
          true, //(true)beneficial or (false)harmful
          0, //speed bonus
          0, //health bonus
          0, //mana bonus
          0, //dmg bonus
          0, //defense bonus 
          6, //luck bonus 
          0, //healing, multiplicative. as a percentage.
          0, //dam resist, multiplicative. as a percentage.
          0, //dam bonus multiplicative. as a percentage.
          0, //discount as a percentage, multiplicative
          0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("enchanted_shard_emerald", //string ID
          "A rare gem radiating unexplainable magic. Excavated from the royal mining site, rare gems like these have been told to contain the souls of dragons who died underground in their lairs. Others say they are unique combinations of minerals. This one is enchanted.",//the desc
          "A powerful and rare gem. You feel drawn in by it's emminating power.", //blurb
          "Shard of Emerald", //name
          EmeraldSprite, //sprite
          "common", //rarity string
          5, //value in eyes
          1, //amount in birb's stock
          1, //default quantity
          true, //(true)beneficial or (false)harmful
          0, //speed bonus
          0, //health bonus
          25, //mana bonus
          0, //dmg bonus
          0, //defense bonus 
          15, //luck bonus 
          0, //healing, multiplicative. as a percentage.
          0, //dam resist, multiplicative. as a percentage.
          0, //dam bonus multiplicative. as a percentage.
          0, //discount as a percentage, multiplicative
          0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("emerald_glasses", //string ID
  "These glasses have been crafted from by the most talented tinkerers in all of the Emerald palace, using highly refined emerald sheets as their lenses. Usually used only by artificers, these glasses help the wearer see hidden details in the world.",//the desc
  "A beautifully crafted pair of glasses. You feel like you can see everything when wearing them.", //blurb
  "Emerald Glasses", //name
  GlassesSprite, //sprite
  "rare", //rarity string
  6, //value in eyes
  1, //amount in birb's stock
  1, //default quantity
  true, //(true)beneficial or (false)harmful
  0, //speed bonus
  0, //health bonus
  0, //mana bonus
  0, //dmg bonus
  0, //defense bonus 
  33, //luck bonus 
  0, //healing, multiplicative. as a percentage.
  0, //dam resist, multiplicative. as a percentage.
  0, //dam bonus multiplicative. as a percentage.
  0, //discount as a percentage, multiplicative
  0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("warding_emerald_glasses", //string ID
"These glasses have been crafted from by the most talented tinkerers in all of the Emerald palace, using highly refined emerald sheets as their lenses. Usually used only by artificers, these glasses help the wearer see hidden details in the world. This pair is tough.",//the desc
"A beautifully crafted pair of glasses. You feel like you can see everything when wearing them.", //blurb
"Emerald Glasses", //name
GlassesSprite, //sprite
"rare", //rarity string
8, //value in eyes
1, //amount in birb's stock
1, //default quantity
true, //(true)beneficial or (false)harmful
0, //speed bonus
0, //health bonus
0, //mana bonus
0, //dmg bonus
2, //defense bonus 
33, //luck bonus 
0, //healing, multiplicative. as a percentage.
0, //dam resist, multiplicative. as a percentage.
0, //dam bonus multiplicative. as a percentage.
0, //discount as a percentage, multiplicative
0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("light_emerald_glasses", //string ID
"These glasses have been crafted from by the most talented tinkerers in all of the Emerald palace, using highly refined emerald sheets as their lenses. Usually used only by artificers, these glasses help the wearer see hidden details in the world. This pair is light.",//the desc
"A beautifully crafted pair of glasses. You feel like you can see everything when wearing them.", //blurb
"Emerald Glasses", //name
GlassesSprite, //sprite
"rare", //rarity string
6, //value in eyes
1, //amount in birb's stock
1, //default quantity
true, //(true)beneficial or (false)harmful
3, //speed bonus
0, //health bonus
0, //mana bonus
0, //dmg bonus
0, //defense bonus 
33, //luck bonus 
0, //healing, multiplicative. as a percentage.
0, //dam resist, multiplicative. as a percentage.
0, //dam bonus multiplicative. as a percentage.
0, //discount as a percentage, multiplicative
0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("lucky_emerald_glasses", //string ID
"These glasses have been crafted from by the most talented tinkerers in all of the Emerald palace, using highly refined emerald sheets as their lenses. Usually used only by artificers, these glasses help the wearer see hidden details in the world. This pair feels lucky.",//the desc
"A beautifully crafted pair of glasses. You feel like you can see everything when wearing them.", //blurb
"Emerald Glasses", //name
GlassesSprite, //sprite
"rare", //rarity string
7, //value in eyes
1, //amount in birb's stock
1, //default quantity
true, //(true)beneficial or (false)harmful
0, //speed bonus
0, //health bonus
0, //mana bonus
0, //dmg bonus
0, //defense bonus 
44, //luck bonus 
0, //healing, multiplicative. as a percentage.
0, //dam resist, multiplicative. as a percentage.
0, //dam bonus multiplicative. as a percentage.
0, //discount as a percentage, multiplicative
0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("cheap_emerald_glasses", //string ID
"These glasses have been crafted from by the most talented tinkerers in all of the Emerald palace, using highly refined emerald sheets as their lenses. Usually used only by artificers, these glasses help the wearer see hidden details in the world. This pair is cheap.",//the desc
"A beautifully crafted pair of glasses. You feel like you can see everything when wearing them.", //blurb
"Emerald Glasses", //name
GlassesSprite, //sprite
"rare", //rarity string
5, //value in eyes
1, //amount in birb's stock
1, //default quantity
true, //(true)beneficial or (false)harmful
0, //speed bonus
0, //health bonus
0, //mana bonus
0, //dmg bonus
0, //defense bonus 
33, //luck bonus 
0, //healing, multiplicative. as a percentage.
0, //dam resist, multiplicative. as a percentage.
0, //dam bonus multiplicative. as a percentage.
0, //discount as a percentage, multiplicative
0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("monkey_fang", //string ID
"Pulled from the mouth of a beast. These teeth, found from flying monkeys, differ quite a lot from their non-avian counterparts. These teeth are greater in size and are serrated, hinting at the Flying Monkey's more predatory nature.",//the desc
"A dangerous piece of natural weaponry. Don't cut yourself on it.", //blurb
"Monkey Fang", //name
MonkeyFangSprite, //sprite
"common", //rarity string
5, //value in eyes
1, //amount in birb's stock
1, //default quantity
true, //(true)beneficial or (false)harmful
0, //speed bonus
0, //health bonus
0, //mana bonus
1, //dmg bonus
0, //defense bonus 
0, //luck bonus 
0, //healing, multiplicative. as a percentage.
0, //dam resist, multiplicative. as a percentage.
10, //dam bonus multiplicative. as a percentage.
0, //discount as a percentage, multiplicative
10); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("sharp_monkey_fang", //string ID
"Pulled from the mouth of a beast. These teeth, found from flying monkeys, differ quite a lot from their non-avian counterparts. These teeth are greater in size and are serrated, hinting at the Flying Monkey's more predatory nature. This one is sharp.",//the desc
"A dangerous piece of natural weaponry. Don't cut yourself on it.", //blurb
"Monkey Fang", //name
MonkeyFangSprite, //sprite
"common", //rarity string
7, //value in eyes
1, //amount in birb's stock
1, //default quantity
true, //(true)beneficial or (false)harmful
0, //speed bonus
0, //health bonus
0, //mana bonus
3, //dmg bonus
0, //defense bonus 
0, //luck bonus 
0, //healing, multiplicative. as a percentage.
0, //dam resist, multiplicative. as a percentage.
10, //dam bonus multiplicative. as a percentage.
0, //discount as a percentage, multiplicative
10); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("vengeful_monkey_fang", //string ID
"Pulled from the mouth of a beast. These teeth, found from flying monkeys, differ quite a lot from their non-avian counterparts. These teeth are greater in size and are serrated, hinting at the Flying Monkey's more predatory nature. This one is haunted.",//the desc
"A dangerous piece of natural weaponry. Don't cut yourself on it.", //blurb
"Monkey Fang", //name
MonkeyFangSprite, //sprite
"common", //rarity string
10, //value in eyes
1, //amount in birb's stock
1, //default quantity
true, //(true)beneficial or (false)harmful
0, //speed bonus
0, //health bonus
0, //mana bonus
4, //dmg bonus
0, //defense bonus 
0, //luck bonus 
0, //healing, multiplicative. as a percentage.
0, //dam resist, multiplicative. as a percentage.
10, //dam bonus multiplicative. as a percentage.
0, //discount as a percentage, multiplicative
20); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("dull_monkey_fang", //string ID
"Pulled from the mouth of a beast. These teeth, found from flying monkeys, differ quite a lot from their non-avian counterparts. These teeth are greater in size and are serrated, hinting at the Flying Monkey's more predatory nature. This one is dull.",//the desc
"A dangerous piece of natural weaponry. Don't cut yourself on it.", //blurb
"Monkey Fang", //name
MonkeyFangSprite, //sprite
"common", //rarity string
3, //value in eyes
1, //amount in birb's stock
1, //default quantity
true, //(true)beneficial or (false)harmful
0, //speed bonus
0, //health bonus
0, //mana bonus
0, //dmg bonus
0, //defense bonus 
0, //luck bonus 
0, //healing, multiplicative. as a percentage.
0, //dam resist, multiplicative. as a percentage.
10, //dam bonus multiplicative. as a percentage.
0, //discount as a percentage, multiplicative
10); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("cheap_monkey_fang", //string ID
"Pulled from the mouth of a beast. These teeth, found from flying monkeys, differ quite a lot from their non-avian counterparts. These teeth are greater in size and are serrated, hinting at the Flying Monkey's more predatory nature. This one is cheap.",//the desc
"A dangerous piece of natural weaponry. Don't cut yourself on it.", //blurb
"Monkey Fang", //name
MonkeyFangSprite, //sprite
"common", //rarity string
4, //value in eyes
1, //amount in birb's stock
1, //default quantity
true, //(true)beneficial or (false)harmful
0, //speed bonus
0, //health bonus
0, //mana bonus
1, //dmg bonus
0, //defense bonus 
0, //luck bonus 
0, //healing, multiplicative. as a percentage.
0, //dam resist, multiplicative. as a percentage.
10, //dam bonus multiplicative. as a percentage.
0, //discount as a percentage, multiplicative
10); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("mana_amulet", //string ID
"An amulet used for training mages in arcane schools due to it's ability to enhance its user's magical abilities and stamina. Although they aren't common to find, they are a fairly common commodity in the magical world, being used by most witches and wizards.",//the desc
"A fragile gem encased in a hard metal. You feel more focused when wearing the amulet.", //blurb
"Mana-Weaving Amulet", //name
ManaweavingAmuletSprite, //sprite
"common", //rarity string
4, //value in eyes
1, //amount in birb's stock
1, //default quantity
true, //(true)beneficial or (false)harmful
0, //speed bonus
0, //health bonus
50, //mana bonus
0, //dmg bonus
0, //defense bonus 
0, //luck bonus 
20, //healing, multiplicative. as a percentage.
0, //dam resist, multiplicative. as a percentage.
0, //dam bonus multiplicative. as a percentage.
0, //discount as a percentage, multiplicative
0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("enchanted_mana_amulet", //string ID
"An amulet used for training mages in arcane schools due to it's ability to enhance its user's magical abilities and stamina. Although they aren't common to find, they are a fairly common commodity in the magical world, being used by most witches and wizards. This one is enchanted.",//the desc
"A fragile gem encased in a hard metal. You feel more focused when wearing the amulet.", //blurb
"Mana-Weaving Amulet", //name
ManaweavingAmuletSprite, //sprite
"common", //rarity string
5, //value in eyes
1, //amount in birb's stock
1, //default quantity
true, //(true)beneficial or (false)harmful
0, //speed bonus
0, //health bonus
75, //mana bonus
0, //dmg bonus
0, //defense bonus 
0, //luck bonus 
20, //healing, multiplicative. as a percentage.
0, //dam resist, multiplicative. as a percentage.
0, //dam bonus multiplicative. as a percentage.
0, //discount as a percentage, multiplicative
0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("warding_mana_amulet", //string ID
"An amulet used for training mages in arcane schools due to it's ability to enhance its user's magical abilities and stamina. Although they aren't common to find, they are a fairly common commodity in the magical world, being used by most witches and wizards. This one is tough.",//the desc
"A fragile gem encased in a hard metal. You feel more focused when wearing the amulet.", //blurb
"Mana-Weaving Amulet", //name
ManaweavingAmuletSprite, //sprite
"common", //rarity string
2, //value in eyes
1, //amount in birb's stock
1, //default quantity
true, //(true)beneficial or (false)harmful
0, //speed bonus
0, //health bonus
50, //mana bonus
0, //dmg bonus
2, //defense bonus 
0, //luck bonus 
20, //healing, multiplicative. as a percentage.
0, //dam resist, multiplicative. as a percentage.
0, //dam bonus multiplicative. as a percentage.
0, //discount as a percentage, multiplicative
0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("light_mana_amulet", //string ID
"An amulet used for training mages in arcane schools due to it's ability to enhance its user's magical abilities and stamina. Although they aren't common to find, they are a fairly common commodity in the magical world, being used by most witches and wizards. This one is light.",//the desc
"A fragile gem encased in a hard metal. You feel more focused when wearing the amulet.", //blurb
"Mana-Weaving Amulet", //name
ManaweavingAmuletSprite, //sprite
"common", //rarity string
4, //value in eyes
1, //amount in birb's stock
1, //default quantity
true, //(true)beneficial or (false)harmful
3, //speed bonus
0, //health bonus
50, //mana bonus
0, //dmg bonus
0, //defense bonus 
0, //luck bonus 
20, //healing, multiplicative. as a percentage.
0, //dam resist, multiplicative. as a percentage.
0, //dam bonus multiplicative. as a percentage.
0, //discount as a percentage, multiplicative
0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("iron_armor", //string ID
"A heavy piece of armor worn by knights and paladins. It can protect against most attacks, and even though this one has been through hundreds of battles and taken thousands more blows, it still looks like it was forged yesterday.",//the desc
"A powerful piece of armor. You feel safer being around it.", //blurb
"Iron Armor", //name
IronArmorSprite, //sprite
"common", //rarity string
10, //value in eyes
1, //amount in birb's stock
1, //default quantity
true, //(true)beneficial or (false)harmful
-3, //speed bonus
0, //health bonus
0, //mana bonus
0, //dmg bonus
3, //defense bonus 
0, //luck bonus 
0, //healing, multiplicative. as a percentage.
25, //dam resist, multiplicative. as a percentage.
0, //dam bonus multiplicative. as a percentage.
0, //discount as a percentage, multiplicative
0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("light_iron_armor", //string ID
"A heavy piece of armor worn by knights and paladins. It can protect against most attacks, and even though this one has been through hundreds of battles and taken thousands more blows, it still looks like it was forged yesterday. This one is light.",//the desc
"A powerful piece of armor. You feel safer being around it.", //blurb
"Iron Armor", //name
IronArmorSprite, //sprite
"common", //rarity string
10, //value in eyes
1, //amount in birb's stock
1, //default quantity
true, //(true)beneficial or (false)harmful
0, //speed bonus
0, //health bonus
0, //mana bonus
0, //dmg bonus
3, //defense bonus 
0, //luck bonus 
0, //healing, multiplicative. as a percentage.
25, //dam resist, multiplicative. as a percentage.
0, //dam bonus multiplicative. as a percentage.
0, //discount as a percentage, multiplicative
0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("warding_iron_armor", //string ID
"A heavy piece of armor worn by knights and paladins. It can protect against most attacks, and even though this one has been through hundreds of battles and taken thousands more blows, it still looks like it was forged yesterday. This one is tough.",//the desc
"A powerful piece of armor. You feel safer being around it.", //blurb
"Iron Armor", //name
IronArmorSprite, //sprite
"common", //rarity string
12, //value in eyes
1, //amount in birb's stock
1, //default quantity
true, //(true)beneficial or (false)harmful
-3, //speed bonus
0, //health bonus
0, //mana bonus
0, //dmg bonus
5, //defense bonus 
0, //luck bonus 
0, //healing, multiplicative. as a percentage.
25, //dam resist, multiplicative. as a percentage.
0, //dam bonus multiplicative. as a percentage.
0, //discount as a percentage, multiplicative
0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("damaged_iron_armor", //string ID
"A heavy piece of armor worn by knights and paladins. It can protect against most attacks, and even though this one has been through hundreds of battles and taken thousands more blows, it still looks like it was forged yesterday. This one is damaged.",//the desc
"A powerful piece of armor. You feel safer being around it.", //blurb
"Iron Armor", //name
IronArmorSprite, //sprite
"common", //rarity string
5, //value in eyes
1, //amount in birb's stock
1, //default quantity
true, //(true)beneficial or (false)harmful
-3, //speed bonus
0, //health bonus
0, //mana bonus
0, //dmg bonus
1, //defense bonus 
0, //luck bonus 
0, //healing, multiplicative. as a percentage.
10, //dam resist, multiplicative. as a percentage.
0, //dam bonus multiplicative. as a percentage.
0, //discount as a percentage, multiplicative
0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("cheap_iron_armor", //string ID
"A heavy piece of armor worn by knights and paladins. It can protect against most attacks, and even though this one has been through hundreds of battles and taken thousands more blows, it still looks like it was forged yesterday. This one is cheap.",//the desc
"A powerful piece of armor. You feel safer being around it.", //blurb
"Iron Armor", //name
IronArmorSprite, //sprite
"common", //rarity string
9, //value in eyes
1, //amount in birb's stock
1, //default quantity
true, //(true)beneficial or (false)harmful
-3, //speed bonus
0, //health bonus
0, //mana bonus
0, //dmg bonus
3, //defense bonus 
0, //luck bonus 
0, //healing, multiplicative. as a percentage.
25, //dam resist, multiplicative. as a percentage.
0, //dam bonus multiplicative. as a percentage.
0, //discount as a percentage, multiplicative
0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("wood_axe", //string ID
"A simple axe used for wood cutting. The slight marks on the end and the blood stains on the handle however point to this axe being used in more 'creative' ways.",//the desc
"A worn down, but reliable axe. It chops through bone better than wood.", //blurb
"Wood-Cutter's Axe", //name
AxeSprite, //sprite
"common", //rarity string
6, //value in eyes
1, //amount in birb's stock
1, //default quantity
true, //(true)beneficial or (false)harmful
0, //speed bonus
0, //health bonus
0, //mana bonus
0, //dmg bonus
0, //defense bonus 
0, //luck bonus 
0, //healing, multiplicative. as a percentage.
0, //dam resist, multiplicative. as a percentage.
33, //dam bonus multiplicative. as a percentage.
0, //discount as a percentage, multiplicative
0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("vengeful_wood_axe", //string ID
"A simple axe used for wood cutting. The slight marks on the end and the blood stains on the handle however point to this axe being used in more 'creative' ways. This one is haunted.",//the desc
"A worn down, but reliable axe. It chops through bone better than wood.", //blurb
"Wood-Cutter's Axe", //name
AxeSprite, //sprite
"common", //rarity string
11, //value in eyes
1, //amount in birb's stock
1, //default quantity
true, //(true)beneficial or (false)harmful
0, //speed bonus
0, //health bonus
0, //mana bonus
3, //dmg bonus
0, //defense bonus 
0, //luck bonus 
0, //healing, multiplicative. as a percentage.
0, //dam resist, multiplicative. as a percentage.
33, //dam bonus multiplicative. as a percentage.
0, //discount as a percentage, multiplicative
10); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("sharp_wood_axe", //string ID
"A simple axe used for wood cutting. The slight marks on the end and the blood stains on the handle however point to this axe being used in more 'creative' ways. This one is sharp.",//the desc
"A worn down, but reliable axe. It chops through bone better than wood.", //blurb
"Wood-Cutter's Axe", //name
AxeSprite, //sprite
"common", //rarity string
8, //value in eyes
1, //amount in birb's stock
1, //default quantity
true, //(true)beneficial or (false)harmful
0, //speed bonus
0, //health bonus
0, //mana bonus
2, //dmg bonus
0, //defense bonus 
0, //luck bonus 
0, //healing, multiplicative. as a percentage.
0, //dam resist, multiplicative. as a percentage.
33, //dam bonus multiplicative. as a percentage.
0, //discount as a percentage, multiplicative
0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("dull_wood_axe", //string ID
"A simple axe used for wood cutting. The slight marks on the end and the blood stains on the handle however point to this axe being used in more 'creative' ways. This one is dull.",//the desc
"A worn down, but reliable axe. It chops through bone better than wood.", //blurb
"Wood-Cutter's Axe", //name
AxeSprite, //sprite
"common", //rarity string
5, //value in eyes
1, //amount in birb's stock
1, //default quantity
true, //(true)beneficial or (false)harmful
0, //speed bonus
0, //health bonus
0, //mana bonus
0, //dmg bonus
0, //defense bonus 
0, //luck bonus 
0, //healing, multiplicative. as a percentage.
0, //dam resist, multiplicative. as a percentage.
25, //dam bonus multiplicative. as a percentage.
0, //discount as a percentage, multiplicative
0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("cheap_wood_axe", //string ID
"A simple axe used for wood cutting. The slight marks on the end and the blood stains on the handle however point to this axe being used in more 'creative' ways. This one is cheap.",//the desc
"A worn down, but reliable axe. It chops through bone better than wood.", //blurb
"Wood-Cutter's Axe", //name
AxeSprite, //sprite
"common", //rarity string
5, //value in eyes
1, //amount in birb's stock
1, //default quantity
true, //(true)beneficial or (false)harmful
0, //speed bonus
0, //health bonus
0, //mana bonus
0, //dmg bonus
0, //defense bonus 
0, //luck bonus 
0, //healing, multiplicative. as a percentage.
0, //dam resist, multiplicative. as a percentage.
33, //dam bonus multiplicative. as a percentage.
0, //discount as a percentage, multiplicative
0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("tornado_bottle", //string ID
"A bottled tornado. Holding the glass it's in, you can feel the power swirling around inside, as if the rage of a god was contained all within that small storm. The shopkeeper warns you not to open it, at least not near him.",//the desc
"A small tornado swirling inside a bottle. Opening it could cause disaster.", //blurb
"Tornado in a Bottle", //name
TornadoSprite, //sprite
"uncommon", //rarity string
2, //value in eyes
1, //amount in birb's stock
1, //default quantity
true, //(true)beneficial or (false)harmful
9, //speed bonus
0, //health bonus
0, //mana bonus
0, //dmg bonus
0, //defense bonus 
0, //luck bonus 
0, //healing, multiplicative. as a percentage.
0, //dam resist, multiplicative. as a percentage.
0, //dam bonus multiplicative. as a percentage.
0, //discount as a percentage, multiplicative
0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("light_tornado_bottle", //string ID
"A bottled tornado. Holding the glass it's in, you can feel the power swirling around inside, as if the rage of a god was contained all within that small storm. The shopkeeper warns you not to open it, at least not near him.",//the desc
"A small tornado swirling inside a bottle. Opening it could cause disaster.", //blurb
"Tornado in a Bottle", //name
TornadoSprite, //sprite
"uncommon", //rarity string
2, //value in eyes
1, //amount in birb's stock
1, //default quantity
true, //(true)beneficial or (false)harmful
12, //speed bonus
0, //health bonus
0, //mana bonus
0, //dmg bonus
0, //defense bonus 
0, //luck bonus 
0, //healing, multiplicative. as a percentage.
0, //dam resist, multiplicative. as a percentage.
0, //dam bonus multiplicative. as a percentage.
0, //discount as a percentage, multiplicative
0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("cheap_tornado_bottle", //string ID
"A bottled tornado. Holding the glass it's in, you can feel the power swirling around inside, as if the rage of a god was contained all within that small storm. The shopkeeper warns you not to open it, at least not near him. This one is cheap.",//the desc
"A small tornado swirling inside a bottle. Opening it could cause disaster.", //blurb
"Tornado in a Bottle", //name
TornadoSprite, //sprite
"uncommon", //rarity string
1, //value in eyes
1, //amount in birb's stock
1, //default quantity
true, //(true)beneficial or (false)harmful
9, //speed bonus
0, //health bonus
0, //mana bonus
0, //dmg bonus
0, //defense bonus 
0, //luck bonus 
0, //healing, multiplicative. as a percentage.
0, //dam resist, multiplicative. as a percentage.
0, //dam bonus multiplicative. as a percentage.
0, //discount as a percentage, multiplicative
0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("enchanted_tornado_bottle", //string ID
"A bottled tornado. Holding the glass it's in, you can feel the power swirling around inside, as if the rage of a god was contained all within that small storm. The shopkeeper warns you not to open it, at least not near him. This one is enchanted.",//the desc
"A small tornado swirling inside a bottle. Opening it could cause disaster.", //blurb
"Tornado in a Bottle", //name
TornadoSprite, //sprite
"uncommon", //rarity string
3, //value in eyes
1, //amount in birb's stock
1, //default quantity
true, //(true)beneficial or (false)harmful
9, //speed bonus
0, //health bonus
25, //mana bonus
0, //dmg bonus
0, //defense bonus 
0, //luck bonus 
0, //healing, multiplicative. as a percentage.
0, //dam resist, multiplicative. as a percentage.
0, //dam bonus multiplicative. as a percentage.
0, //discount as a percentage, multiplicative
0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("lucky_tornado_bottle", //string ID
"A bottled tornado. Holding the glass it's in, you can feel the power swirling around inside, as if the rage of a god was contained all within that small storm. The shopkeeper warns you not to open it, at least not near him. This one feels lucky.",//the desc
"A small tornado swirling inside a bottle. Opening it could cause disaster.", //blurb
"Tornado in a Bottle", //name
TornadoSprite, //sprite
"uncommon", //rarity string
3, //value in eyes
1, //amount in birb's stock
1, //default quantity
true, //(true)beneficial or (false)harmful
9, //speed bonus
0, //health bonus
0, //mana bonus
0, //dmg bonus
0, //defense bonus 
10, //luck bonus 
0, //healing, multiplicative. as a percentage.
0, //dam resist, multiplicative. as a percentage.
0, //dam bonus multiplicative. as a percentage.
0, //discount as a percentage, multiplicative
0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("life_amulet", //string ID
          "An amulet used to help nurse people back to health. Using a combination of rare gems imbued with various spells, the amulet can help protect the wearer from both phyisical damage, as well as viruses or infections.",//the desc
          "Usually found on the necks of frontline soldiers, helping them heal small wounds during combat.", //blurb
          "Life-Giving Amulet", //name
          lifeGivingAmuletSprite, //sprite
          "common", //rarity string
          6, //value in eyes
          1, //amount in birb's stock
          1, //default quantity
          true, //(true)beneficial or (false)harmful
          0, //speed bonus
          10, //health bonus
          0, //mana bonus
          0, //dmg bonus
          0, //defense bonus 
          0, //luck bonus 
          50, //healing, multiplicative. as a percentage.
          0, //dam resist, multiplicative. as a percentage.
          0, //dam bonus multiplicative. as a percentage.
          0, //discount as a percentage, multiplicative
          0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("warding_life_amulet", //string ID
          "An amulet used to help nurse people back to health. Using a combination of rare gems imbued with various spells, the amulet can help protect the wearer from both phyisical damage, as well as viruses or infections. This one is tough.",//the desc
          "Usually found on the necks of frontline soldiers, helping them heal small wounds during combat.", //blurb
          "Life-Giving Amulet", //name
          lifeGivingAmuletSprite, //sprite
          "common", //rarity string
          8, //value in eyes
          1, //amount in birb's stock
          1, //default quantity
          true, //(true)beneficial or (false)harmful
          0, //speed bonus
          10, //health bonus
          0, //mana bonus
          0, //dmg bonus
          2, //defense bonus 
          0, //luck bonus 
          50, //healing, multiplicative. as a percentage.
          0, //dam resist, multiplicative. as a percentage.
          0, //dam bonus multiplicative. as a percentage.
          0, //discount as a percentage, multiplicative
          0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("enchanted_life_amulet", //string ID
          "An amulet used to help nurse people back to health. Using a combination of rare gems imbued with various spells, the amulet can help protect the wearer from both phyisical damage, as well as viruses or infections. This one is enchanted.",//the desc
          "Usually found on the necks of frontline soldiers, helping them heal small wounds during combat.", //blurb
          "Life-Giving Amulet", //name
          lifeGivingAmuletSprite, //sprite
          "common", //rarity string
          7, //value in eyes
          1, //amount in birb's stock
          1, //default quantity
          true, //(true)beneficial or (false)harmful
          0, //speed bonus
          10, //health bonus
          25, //mana bonus
          0, //dmg bonus
          0, //defense bonus 
          0, //luck bonus 
          50, //healing, multiplicative. as a percentage.
          0, //dam resist, multiplicative. as a percentage.
          0, //dam bonus multiplicative. as a percentage.
          0, //discount as a percentage, multiplicative
          0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("cheap_life_amulet", //string ID
          "An amulet used to help nurse people back to health. Using a combination of rare gems imbued with various spells, the amulet can help protect the wearer from both phyisical damage, as well as viruses or infections. This one is cheap.",//the desc
          "Usually found on the necks of frontline soldiers, helping them heal small wounds during combat.", //blurb
          "Life-Giving Amulet", //name
          lifeGivingAmuletSprite, //sprite
          "common", //rarity string
          5, //value in eyes
          1, //amount in birb's stock
          1, //default quantity
          true, //(true)beneficial or (false)harmful
          0, //speed bonus
          10, //health bonus
          0, //mana bonus
          0, //dmg bonus
          0, //defense bonus 
          0, //luck bonus 
          50, //healing, multiplicative. as a percentage.
          0, //dam resist, multiplicative. as a percentage.
          0, //dam bonus multiplicative. as a percentage.
          0, //discount as a percentage, multiplicative
          0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("light_life_amulet", //string ID
          "An amulet used to help nurse people back to health. Using a combination of rare gems imbued with various spells, the amulet can help protect the wearer from both phyisical damage, as well as viruses or infections. This one is light.",//the desc
          "Usually found on the necks of frontline soldiers, helping them heal small wounds during combat.", //blurb
          "Life-Giving Amulet", //name
          lifeGivingAmuletSprite, //sprite
          "common", //rarity string
          6, //value in eyes
          1, //amount in birb's stock
          1, //default quantity
          true, //(true)beneficial or (false)harmful
          3, //speed bonus
          10, //health bonus
          0, //mana bonus
          0, //dmg bonus
          0, //defense bonus 
          0, //luck bonus 
          50, //healing, multiplicative. as a percentage.
          0, //dam resist, multiplicative. as a percentage.
          0, //dam bonus multiplicative. as a percentage.
          0, //discount as a percentage, multiplicative
          0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("paladin_shield", //string ID
          "A shield only equipped by paladins of the highest ranks. It symbolises protection, safety, and a guard's unwavering faith to their king. How this shopkeeper got a hold of it is baffling.",//the desc
          "A symbolic bulwark given to the most trusted and respected paladins.", //blurb
          "Paladin's Shield", //name
          ShieldSprite, //sprite
          "rare", //rarity string
          15, //value in eyes
          1, //amount in birb's stock
          1, //default quantity
          true, //(true)beneficial or (false)harmful
          0, //speed bonus
          0, //health bonus
          0, //mana bonus
          0, //dmg bonus
          3, //defense bonus 
          0, //luck bonus 
          0, //healing, multiplicative. as a percentage.
          50, //dam resist, multiplicative. as a percentage.
          0, //dam bonus multiplicative. as a percentage.
          0, //discount as a percentage, multiplicative
          0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("warding_paladin_shield", //string ID
          "A shield only equipped by paladins of the highest ranks. It symbolises protection, safety, and a guard's unwavering faith to their king. How this shopkeeper got a hold of it is baffling. This one is tough.",//the desc
          "A symbolic bulwark given to the most trusted and respected paladins.", //blurb
          "Paladin's Shield", //name
          ShieldSprite, //sprite
          "rare", //rarity string
          17, //value in eyes
          1, //amount in birb's stock
          1, //default quantity
          true, //(true)beneficial or (false)harmful
          0, //speed bonus
          0, //health bonus
          0, //mana bonus
          0, //dmg bonus
          5, //defense bonus 
          0, //luck bonus 
          0, //healing, multiplicative. as a percentage.
          50, //dam resist, multiplicative. as a percentage.
          0, //dam bonus multiplicative. as a percentage.
          0, //discount as a percentage, multiplicative
          0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("lucky_paladin_shield", //string ID
          "A shield only equipped by paladins of the highest ranks. It symbolises protection, safety, and a guard's unwavering faith to their king. How this shopkeeper got a hold of it is baffling. This one feels lucky.",//the desc
          "A symbolic bulwark given to the most trusted and respected paladins.", //blurb
          "Paladin's Shield", //name
          ShieldSprite, //sprite
          "rare", //rarity string
          16, //value in eyes
          1, //amount in birb's stock
          1, //default quantity
          true, //(true)beneficial or (false)harmful
          0, //speed bonus
          0, //health bonus
          0, //mana bonus
          0, //dmg bonus
          3, //defense bonus 
          10, //luck bonus 
          0, //healing, multiplicative. as a percentage.
          50, //dam resist, multiplicative. as a percentage.
          0, //dam bonus multiplicative. as a percentage.
          0, //discount as a percentage, multiplicative
          0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("vengeful_paladin_shield", //string ID
          "A shield only equipped by paladins of the highest ranks. It symbolises protection, safety, and a guard's unwavering faith to their king. How this shopkeeper got a hold of it is baffling. This one is haunted.",//the desc
          "A symbolic bulwark given to the most trusted and respected paladins.", //blurb
          "Paladin's Shield", //name
          ShieldSprite, //sprite
          "rare", //rarity string
          20, //value in eyes
          1, //amount in birb's stock
          1, //default quantity
          true, //(true)beneficial or (false)harmful
          0, //speed bonus
          0, //health bonus
          0, //mana bonus
          3, //dmg bonus
          3, //defense bonus 
          0, //luck bonus 
          0, //healing, multiplicative. as a percentage.
          50, //dam resist, multiplicative. as a percentage.
          0, //dam bonus multiplicative. as a percentage.
          0, //discount as a percentage, multiplicative
          10); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("damaged_paladin_shield", //string ID
          "A shield only equipped by paladins of the highest ranks. It symbolises protection, safety, and a guard's unwavering faith to their king. How this shopkeeper got a hold of it is baffling. This one is damaged.",//the desc
          "A symbolic bulwark given to the most trusted and respected paladins.", //blurb
          "Paladin's Shield", //name
          ShieldSprite, //sprite
          "rare", //rarity string
          10, //value in eyes
          1, //amount in birb's stock
          1, //default quantity
          true, //(true)beneficial or (false)harmful
          0, //speed bonus
          0, //health bonus
          0, //mana bonus
          0, //dmg bonus
          1, //defense bonus 
          0, //luck bonus 
          0, //healing, multiplicative. as a percentage.
          20, //dam resist, multiplicative. as a percentage.
          0, //dam bonus multiplicative. as a percentage.
          0, //discount as a percentage, multiplicative
          0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("emblem_purity", //string ID
          "This emblem has been forged through generations by the nomadic tribes of the east. It symbolises the rejection of modern civilization and the acceptance of changing and ruling the natural world through your will and strength alone.",//the desc
          "A charm worn by the shamans of nomadic tribes to show their domination over the natural world.", //blurb
          "Emblem of Purity", //name
          PuritySprite, //sprite
          "common", //rarity string
          8, //value in eyes
          1, //amount in birb's stock
          1, //default quantity
          true, //(true)beneficial or (false)harmful
          0, //speed bonus
          20, //health bonus
          -100, //mana bonus
          2, //dmg bonus
          2, //defense bonus 
          0, //luck bonus 
          0, //healing, multiplicative. as a percentage.
          10, //dam resist, multiplicative. as a percentage.
          10, //dam bonus multiplicative. as a percentage.
          0, //discount as a percentage, multiplicative
          0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("cheap_emblem_purity", //string ID
          "This emblem has been forged through generations by the nomadic tribes of the east. It symbolises the rejection of modern civilization and the acceptance of changing and ruling the natural world through your will and strength alone. This one is cheap.",//the desc
          "A charm worn by the shamans of nomadic tribes to show their domination over the natural world.", //blurb
          "Emblem of Purity", //name
          PuritySprite, //sprite
          "common", //rarity string
          7, //value in eyes
          1, //amount in birb's stock
          1, //default quantity
          true, //(true)beneficial or (false)harmful
          0, //speed bonus
          20, //health bonus
          -100, //mana bonus
          2, //dmg bonus
          2, //defense bonus 
          0, //luck bonus 
          0, //healing, multiplicative. as a percentage.
          10, //dam resist, multiplicative. as a percentage.
          10, //dam bonus multiplicative. as a percentage.
          0, //discount as a percentage, multiplicative
          0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("sharp_emblem_purity", //string ID
          "This emblem has been forged through generations by the nomadic tribes of the east. It symbolises the rejection of modern civilization and the acceptance of changing and ruling the natural world through your will and strength alone. This one is sharp.",//the desc
          "A charm worn by the shamans of nomadic tribes to show their domination over the natural world.", //blurb
          "Emblem of Purity", //name
          PuritySprite, //sprite
          "common", //rarity string
          10, //value in eyes
          1, //amount in birb's stock
          1, //default quantity
          true, //(true)beneficial or (false)harmful
          0, //speed bonus
          20, //health bonus
          -100, //mana bonus
          4, //dmg bonus
          2, //defense bonus 
          0, //luck bonus 
          0, //healing, multiplicative. as a percentage.
          10, //dam resist, multiplicative. as a percentage.
          10, //dam bonus multiplicative. as a percentage.
          0, //discount as a percentage, multiplicative
          0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("vengeful_emblem_purity", //string ID
          "This emblem has been forged through generations by the nomadic tribes of the east. It symbolises the rejection of modern civilization and the acceptance of changing and ruling the natural world through your will and strength alone. This one is haunted.",//the desc
          "A charm worn by the shamans of nomadic tribes to show their domination over the natural world.", //blurb
          "Emblem of Purity", //name
          PuritySprite, //sprite
          "common", //rarity string
          13, //value in eyes
          1, //amount in birb's stock
          1, //default quantity
          true, //(true)beneficial or (false)harmful
          0, //speed bonus
          20, //health bonus
          -100, //mana bonus
          5, //dmg bonus
          2, //defense bonus 
          0, //luck bonus 
          0, //healing, multiplicative. as a percentage.
          10, //dam resist, multiplicative. as a percentage.
          10, //dam bonus multiplicative. as a percentage.
          0, //discount as a percentage, multiplicative
          10); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("light_emblem_purity", //string ID
          "This emblem has been forged through generations by the nomadic tribes of the east. It symbolises the rejection of modern civilization and the acceptance of changing and ruling the natural world through your will and strength alone. This one is light.",//the desc
          "A charm worn by the shamans of nomadic tribes to show their domination over the natural world.", //blurb
          "Emblem of Purity", //name
          PuritySprite, //sprite
          "common", //rarity string
          8, //value in eyes
          1, //amount in birb's stock
          1, //default quantity
          true, //(true)beneficial or (false)harmful
          3, //speed bonus
          20, //health bonus
          -100, //mana bonus
          2, //dmg bonus
          2, //defense bonus 
          0, //luck bonus 
          0, //healing, multiplicative. as a percentage.
          10, //dam resist, multiplicative. as a percentage.
          10, //dam bonus multiplicative. as a percentage.
          0, //discount as a percentage, multiplicative
          0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("clover", //string ID
          "A 4 leaf clover. Said to provide those who wear it with extreme luck.",//the desc
          "Just a leaf. How lucky can it really be?", //blurb
          "Lucky Leaf", //name
          CloverSprite, //sprite
          "masterwork", //rarity string
          10, //value in eyes
          1, //amount in birb's stock
          1, //default quantity
          true, //(true)beneficial or (false)harmful
          0, //speed bonus
          0, //health bonus
          0, //mana bonus
          0, //dmg bonus
          0, //defense bonus 
          50, //luck bonus 
          0, //healing, multiplicative. as a percentage.
          0, //dam resist, multiplicative. as a percentage.
          0, //dam bonus multiplicative. as a percentage.
          0, //discount as a percentage, multiplicative
          0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("emerald_armor", //string ID
          "Unlike normal armor worn by soliders from other nations, the emerald armor worn by emerald palace guards are quite brittle and are destroyed quite easily. Instead, they use emerald's unique magical abilities to enhance the user's combat prowess.",//the desc
          "A piece of unique armor. Wearing it as protection would end quite badly.", //blurb
          "Emerald Armor", //name
          EmeraldArmorSprite, //sprite
          "uncommon", //rarity string
          5, //value in eyes
          1, //amount in birb's stock
          1, //default quantity
          true, //(true)beneficial or (false)harmful
          1, //speed bonus
          5, //health bonus
          25, //mana bonus
          0, //dmg bonus
          1, //defense bonus 
          0, //luck bonus 
          0, //healing, multiplicative. as a percentage.
          0, //dam resist, multiplicative. as a percentage.
          0, //dam bonus multiplicative. as a percentage.
          0, //discount as a percentage, multiplicative
          0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("enchanted_emerald_armor", //string ID
          "Unlike normal armor worn by soliders from other nations, the emerald armor worn by emerald palace guards are quite brittle and are destroyed quite easily. Instead, they use emerald's unique magical abilities to enhance the user's combat prowess. This one is enchanted.",//the desc
          "A piece of unique armor. Wearing it as protection would end quite badly.", //blurb
          "Emerald Armor", //name
          EmeraldArmorSprite, //sprite
          "uncommon", //rarity string
          6, //value in eyes
          1, //amount in birb's stock
          1, //default quantity
          true, //(true)beneficial or (false)harmful
          1, //speed bonus
          5, //health bonus
          50, //mana bonus
          0, //dmg bonus
          1, //defense bonus 
          0, //luck bonus 
          0, //healing, multiplicative. as a percentage.
          0, //dam resist, multiplicative. as a percentage.
          0, //dam bonus multiplicative. as a percentage.
          0, //discount as a percentage, multiplicative
          0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("cheap_emerald_armor", //string ID
          "Unlike normal armor worn by soliders from other nations, the emerald armor worn by emerald palace guards are quite brittle and are destroyed quite easily. Instead, they use emerald's unique magical abilities to enhance the user's combat prowess. This one is cheap.",//the desc
          "A piece of unique armor. Wearing it as protection would end quite badly.", //blurb
          "Emerald Armor", //name
          EmeraldArmorSprite, //sprite
          "uncommon", //rarity string
          4, //value in eyes
          1, //amount in birb's stock
          1, //default quantity
          true, //(true)beneficial or (false)harmful
          1, //speed bonus
          5, //health bonus
          25, //mana bonus
          0, //dmg bonus
          1, //defense bonus 
          0, //luck bonus 
          0, //healing, multiplicative. as a percentage.
          0, //dam resist, multiplicative. as a percentage.
          0, //dam bonus multiplicative. as a percentage.
          0, //discount as a percentage, multiplicative
          0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("warding_emerald_armor", //string ID
          "Unlike normal armor worn by soliders from other nations, the emerald armor worn by emerald palace guards are quite brittle and are destroyed quite easily. Instead, they use emerald's unique magical abilities to enhance the user's combat prowess. This one is tough.",//the desc
          "A piece of unique armor. Wearing it as protection would end quite badly.", //blurb
          "Emerald Armor", //name
          EmeraldArmorSprite, //sprite
          "uncommon", //rarity string
          7, //value in eyes
          1, //amount in birb's stock
          1, //default quantity
          true, //(true)beneficial or (false)harmful
          1, //speed bonus
          5, //health bonus
          25, //mana bonus
          0, //dmg bonus
          3, //defense bonus 
          0, //luck bonus 
          0, //healing, multiplicative. as a percentage.
          0, //dam resist, multiplicative. as a percentage.
          0, //dam bonus multiplicative. as a percentage.
          0, //discount as a percentage, multiplicative
          0); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("vengeful_emerald_armor", //string ID
          "Unlike normal armor worn by soliders from other nations, the emerald armor worn by emerald palace guards are quite brittle and are destroyed quite easily. Instead, they use emerald's unique magical abilities to enhance the user's combat prowess. This one is haunted.",//the desc
          "A piece of unique armor. Wearing it as protection would end quite badly.", //blurb
          "Emerald Armor", //name
          EmeraldArmorSprite, //sprite
          "uncommon", //rarity string
          10, //value in eyes
          1, //amount in birb's stock
          1, //default quantity
          true, //(true)beneficial or (false)harmful
          1, //speed bonus
          5, //health bonus
          25, //mana bonus
          3, //dmg bonus
          1, //defense bonus 
          0, //luck bonus 
          0, //healing, multiplicative. as a percentage.
          0, //dam resist, multiplicative. as a percentage.
          0, //dam bonus multiplicative. as a percentage.
          0, //discount as a percentage, multiplicative
          10); //lifesteal multiplicative. as a percentage.

        MakeEquipableItemTemplate("duality", //string ID
          "The beauty of this blade cannot be overstated. Just looking at it sends chills down one's spine. Legend says that this blade is able to cut an opponent in half just by drawing it from a sheath. No price could be high enough for this weapon.",//the desc
          "The duality of man given form to a blade.", //blurb
          "Duality", //name
          DualitySprite, //sprite
          "masterwork", //rarity string
          20, //value in eyes
          1, //amount in birb's stock
          1, //default quantity
          true, //(true)beneficial or (false)harmful
          0, //speed bonus
          0, //health bonus
          0, //mana bonus
          0, //dmg bonus
          0, //defense bonus 
          0, //luck bonus 
          0, //healing, multiplicative. as a percentage.
          0, //dam resist, multiplicative. as a percentage.
          100, //dam bonus multiplicative. as a percentage.
          0, //discount as a percentage, multiplicative
          0); //lifesteal multiplicative. as a percentage.



    }
    public void DefineTraits()
    {//generates traits, stores them all in a dictionary in the dataholder

        Trait t1 = new Trait("caring", //string ID
                                   "Caring", //name
                                   "Caring", //adjective given to characters with this
                                   "The power which lies in the heart is not to be underestimated.", // blurb. wax poetic here as much as you want
                                   "Passive: Healing potions are less effective on this character.\nActive: <color=#45BA3D>Heal</color> Heals an ally for 50% HP.", //functional description
                                   null, //sprite
                                   false,
                                   MainData.MainLoop.TweakingComponent.CaringManaCost); //false = t1. true = t2.
                                                                                        //gets automatically sent to the desired trait list upon generation, in the constructor

        Debug.Log("Added new trait - [" + t1.identifier + "].");
        MainData.traitList.Add(t1.identifier, t1);

        Trait t2 = new Trait("wrath", //string ID
                                   "Wrath", //name
                                   "Wrathful", //adjective given to characters with this
                                   "wrath stuff", // blurb. wax poetic here as much as you want
                                   "Passive:  Increased damage, lower defense.\nActive: <color=#F87777>Double Strike</color> Attacks twice.", //functional description
                                   null, //sprite
                                   false,
                                   MainData.MainLoop.TweakingComponent.WrathManaCost); //false = t1. true = t2.
                                                                                       //gets automatically sent to the desired trait list upon generation, in the constructor
        Debug.Log("Added new trait - [" + t2.identifier + "].");
        MainData.traitList.Add(t2.identifier, t2);

        Trait t3 = new Trait("greed", //string ID
                                   "Greed", //name
                                   "Greedy", //adjective given to characters with this
                                   "greed stuff", // blurb. wax poetic here as much as you want
                                   "Passive: 10% loot bonus when completing a battle.\nActive: <color=#E8BE0D>Barter</color> Exchange some HP for gold during a battle", //functional description
                                   null, //sprite
                                   false,
                                   MainData.MainLoop.TweakingComponent.GreedManaCost); //false = t1. true = t2.
                                                                                       //gets automatically sent to the desired trait list upon generation, in the constructor
        Debug.Log("Added new trait - [" + t3.identifier + "].");
        MainData.traitList.Add(t3.identifier, t3);

        Trait t4 = new Trait("angry", //string ID
                                   "Anger", //name
                                   "Angry", //adjective given to characters with this
                                   "anger stuff.", // blurb. wax poetic here as much as you want
                                   "Passive: Taking damage increases the power of this unit’s next attack.\nActive: <color=#FF2C22>Lash out</color>, dealing massive damage. Reduces future damage.", //functional description
                                   null, //sprite
                                   false,
                                   MainData.MainLoop.TweakingComponent.AngerManaCost); //false = t1. true = t2.
        //gets automatically sent to the desired trait list upon generation, in the constructor
        Debug.Log("Added new trait - [" + t4.identifier + "].");
        MainData.traitList.Add(t4.identifier, t4);

        Trait t5 = new Trait("bulwark", //string ID
                                   "Stoic", //name
                                   "Bulwark", //adjective given to characters with this
                                   "bulwark stuff.", // blurb. wax poetic here as much as you want
                                   "Passive: Taking damage permanently increases the maximum HP of this unit.\nActive: <color=#FF2C22>Taunt</color>, becoming the primary focus of enemies this battle.", //functional description
                                   null, //sprite
                                   false,
                                   MainData.MainLoop.TweakingComponent.BulwarkManaCost); //false = t1. true = t2.
        //gets automatically sent to the desired trait list upon generation, in the constructor
        Debug.Log("Added new trait - [" + t5.identifier + "].");
        MainData.traitList.Add(t5.identifier, t5);

        Trait t6 = new Trait("nurturing", //string ID
                           "Nurturing", //name
                           "Nurturing", //adjective given to characters with this
                           "nurturing stuff.", // blurb. wax poetic here as much as you want
                           "Passive: Slight increase to health, slight decrease in damage and speed.\nActive: <color=#55ff22>Inspire </color>you and an ally, healing you both for a small amount.", //functional description
                           null, //sprite
                           false,
                           MainData.MainLoop.TweakingComponent.NurturingManaCost); //false = t1. true = t2.
        //gets automatically sent to the desired trait list upon generation, in the constructor
        Debug.Log("Added new trait - [" + t6.identifier + "].");
        MainData.traitList.Add(t6.identifier, t6);
        
        Trait t7 = new Trait("malicious", //string ID
                   "Malicious", //name
                   "Malicious", //adjective given to characters with this
                   "malicious stuff.", // blurb. wax poetic here as much as you want
                   "Passive: At the end of this unit's turn, enemies take damage based on their health.\nActive: <color=#FF2C22>Unleash</color> your power, dealing damage based on mana used.", //functional description
                   null, //sprite
                   false,
                   MainData.MainLoop.TweakingComponent.MaliciousManaCost); //false = t1. true = t2.
        //gets automatically sent to the desired trait list upon generation, in the constructor
        Debug.Log("Added new trait - [" + t7.identifier + "].");
        MainData.traitList.Add(t7.identifier, t7);

        Trait t8 = new Trait("perfectionist", //string ID
           "Perfectionist", //name
           "Perfectionist", //adjective given to characters with this
           "perfectionist stuff.", // blurb. wax poetic here as much as you want
           "Passive: Take extra damage while not at full health.\nActive: <color=#efea48>Improve</color> yourself, increasing all of your stats.", //functional description
           null, //sprite
           false,
           MainData.MainLoop.TweakingComponent.PerfectionistManaCost); //false = t1. true = t2.
        //gets automatically sent to the desired trait list upon generation, in the constructor
        Debug.Log("Added new trait - [" + t8.identifier + "].");
        MainData.traitList.Add(t8.identifier, t8);

    }

    public void MakeEquipableItemTemplate(string identifier,
                                          string description,
                                          string blurby,
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
                                          int multiplicativeHealingAmplification = 1,
                                          int multiplicativeDamageResistance = 1,
                                          int multiplicativeDamageBonus = 1,
                                          float discountPercentage = 0,
                                          int multiplicativeLifestealBonus = 1)
    {

        Item b = new Item(identifier,
                          description,
                          blurby,
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
                     c.itemBlurb,
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
                    c.healingAmp,
                    c.DamageResistancePercentage,
                    c.DamageBonusPercentage,
                    c.discountPercentage,
                    c.Lifesteal); // so we don't have to define them for things that don't need them
        //now we check if we already have that item in our inventory.
        //if we do? we just add the quantity of this to that one.
        List<Item> results = MainData.consumableInventory.FindAll(x => x.identifier == itemID);
        if (results.Count != 0)
        {
            results[0].itemQuantity += b.itemQuantity;//gives amt * new item's worth of charges to the existing consumable
            return;
        }
        b.itemQuantity = amt;
        MainData.consumableInventory.Add(b);//adds it to the inventory
        MainData.MainLoop.UserInterfaceHelperComponent.RefreshConsumableSlots();//refreshes the inventory buttons/slots
    }





    public class Item
    {
        public string identifier; //stuff like "doner_kebab", "icecream_chocolate", "sword_steel"

        public string description;
        public string itemBlurb;
        public string itemName;
        public Sprite itemSprite;

        public string rarity;//common, uncommon, rare, masterwork. that's it
        public int value;
        public int amtInStock; // standard amount in stock, duh. This is for the shop.
        public int itemQuantity; // quantity of items held. for potion mostly. or bombs. etc. consumables. Or perhaps en enchanted sword with limited amount of shots.

        public bool beneficial; //for threat tracking and stuff.
        //for equipment
        public bool isEquipable;

        public Character currentWielder;

        public int speedmodifier; //just a +5 or -5 or the like. for all of these modifiers
        public int healthmodifier;
        public int manamodifier;
        public int dmgmodifier;
        public int defensemodifier;
        public int luckmodifier;
        public float healingAmp;//affects healing others

        public float DamageResistancePercentage;//affects damage taken.
        public float DamageBonusPercentage;//affects damage output. applied after ALL other bonuses.

        public float discountPercentage;//if positive, it's a discount yeah ,but if negative it increases price.
        public float Lifesteal;


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
                    string blurb,
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
                    int manamodifier = 0, //not yet handled
                    int dmgmodifier = 0, //handled
                    int defensemodifier = 0, //handled
                    int luckmodifier = 0, //not yet

                    float multiplicativeHealingAmplification = 1f,
                    float multiplicativeDamageResistance = 1f,
                    float multiplicativeDamageBonus = 1f,
                    float discountPercentage = 0f,
                    float multiplicativeLifestealBonus = 1) // so we don't have to define them for things that don't need them
        {
            this.identifier = identifier;
            this.description = description;
            this.itemBlurb = blurb;
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
            this.healingAmp = multiplicativeHealingAmplification;
            this.DamageResistancePercentage = multiplicativeDamageResistance;
            this.DamageBonusPercentage = multiplicativeDamageBonus;
            this.discountPercentage = discountPercentage;
            this.Lifesteal = multiplicativeLifestealBonus;
        }
    }




    [System.Serializable]
    public class Character : ScriptableObject
    {

        public List<Item> equippedItems = new List<Item>(); //this does not contain any consumables. those are in the collective inventory pool. this only contains items with isEquipable = true, which have an effect on stats
        public List<Item> recentlyUnequippedItemsHP = new List<Item>(); //for removing HP after unequipping
        public Trait charTrait;

        public float threatFromStats;
        public float Threat = 0;

        public string charType; //something like "goblin_spear", "tin_man" or "scarecrow" for the dictionary. 
        public string charName;
        public string attackverb;
        public string entityDescription;
        public CharacterWorldspaceScript selfScriptRef;
        public Vector3 InitialPosition; //yeah screw having a single variable in CombatHelper.cs we're doing this. set in CharacterScript or template use
        public bool isPlayerPartyMember;

        public AudioClip[] SoundLibrary;
        public AudioClip turnSound;

        public Sprite[] attackAnimation; //this contains the attack too. the first sprite is the standing sprite if one is not supplied.
        public Sprite standingSprite;
        public Sprite[] WalkSprites;
        public Sprite[] hurtSprites;
        public Sprite[] idleSprite;
        public Sprite[] castSprites;
        public Sprite charAvatar;//head pic

        public List<StatusEffect> currentStatusEffects = new List<StatusEffect>(); //status effects are just 2 strings, 1 int and 1 image referenced from tweaker probably, so not even gonna define all that stuff above. just define it on creation because it's 4 arguments really and we want the turns remaining to vary anyways.

        public int currentHealth;
        public float valueBounty = 2f; //eyes given for kill
        public int maxHealth;
        public int baseDamageMin;
        public int baseDamageMax; //NOTE - damage is taken into calculation directly in the attack method.
        public int baseSpeed; //good to go
        public int threatBonus;
        public Slider HealthBar;
        public Slider ManaBar;

        public int damageMin;
        public int damageMax;//handled
        public int speed; //NOTE - these are calculated by calling the GetCompoundSpeed(), GetCompoundDefense() and GetCompoundLuck() methods, just like you'd use the variable
        public int defense; //handled in attacking code
        public int luck; //negative luck - more chance to critfail. positive luck - more chance to critical hit/win. always a bit of a chance to have a critical fail or win.
        public int manaRegeneration; //soon
        public int manaTotal; //always max 100. regenerates at a variable rate.
        public int difficultyCost;

        public bool canAct = true; //wether it's stunned or not

        public bool isDead = false;
        public bool hasActedThisTurn = false;

        public bool Summoner = false;
        public int summoningCurrentDelay = 0;
        public int summoningInterval = 4;
        public string summonedEnemy = "weakflyingmonkey";

        //public GameObject statusEffectObject;


        //public void ShowStatusEffectIcon()
        //{
        //    if (statusEffectObject != null)
        //    {
        //        Destroy(statusEffectObject);
        //    }
        //    GameObject status = Instantiate(MainData.MainLoop.UserInterfaceHelperComponent.StatusEffectPrefab, new Vector3(selfScriptRef.transform.position.x, selfScriptRef.transform.position.y +5, selfScriptRef.transform.position.z), true, selfScriptRef);
        //    status.GetComponent<StatusEffectIcon>().sprite = currentStatusEffects.



        //}


        public int GetCompoundSpeed()
        {
            int speednew = baseSpeed;
            if (equippedItems.Count != 0)
            {
                foreach (Item item in equippedItems)
                {
                    speednew += item.speedmodifier;
                }
            }
            return speednew;
        }

        public int GetCompoundDefense()
        {
            int Defensenew = defense;
            if (equippedItems.Count != 0)
            {

                foreach (Item item in equippedItems)
                {
                    Defensenew += item.defensemodifier;
                }
            }
            return Defensenew;
        }

        public int GetCompoundLuck()
        {
            int Lucknew = luck;
            if (equippedItems.Count != 0)
            {
                foreach (Item item in equippedItems)
                {
                    Lucknew += item.luckmodifier;
                }
            }
            return Lucknew;
        }


        /// <summary>
        /// poor tinman always gets smacked
        /// </summary>
        public void RecalculateThreatFromStats()
        {
            threatFromStats = (speed + currentHealth + damageMin + defense + threatBonus) / 4; //averages them, sounds reasonable
            Debug.Log(threatBonus + " THREAT BONUS");
        }



        /// <summary>
        /// recalculates stats on the start of each round.  handles applying stats the first time, too. use the method to safely remove stats so we don't build up/lose more stats than expected
        /// </summary>
        public void RecalculateStatsFromTraits()
        {
            // Wrath: Increased damage, lower defense. Double strike: Attacks twice.
            //+5 damage, +3 speed

            if (charTrait != null)
            {
                switch (charTrait.identifier)
                {
                    case "wrath":
                        if (!charTrait.hasAppliedStats)
                        {
                            damageMax += MainData.MainLoop.TweakingComponent.wrathDamageIncrease;
                            damageMin += MainData.MainLoop.TweakingComponent.wrathDamageIncrease;
                            speed += MainData.MainLoop.TweakingComponent.wrathSpeedIncrease;
                            charTrait.hasAppliedStats = true;
                        }
                        break;

                    case "bulwark":
                        if (!charTrait.hasAppliedStats)
                        {
                            damageMax += MainData.MainLoop.TweakingComponent.bulwarkDamageIncrease;
                            damageMin += MainData.MainLoop.TweakingComponent.bulwarkDamageIncrease;
                            defense += MainData.MainLoop.TweakingComponent.bulwarkDefenseIncrease;
                            speed -= Mathf.Clamp(MainData.MainLoop.TweakingComponent.bulwarkSpeedDecrease, 0, 999);
                            charTrait.hasAppliedStats = true;
                        }
                        break;

                    case "greed":
                        if (!charTrait.hasAppliedStats)
                        {
                            maxHealth -= MainData.MainLoop.TweakingComponent.greedPassiveHealthMalus;
                            currentHealth -= MainData.MainLoop.TweakingComponent.greedPassiveHealthMalus;
                            if (currentHealth <= 0) //i think you could
                            {
                                currentHealth = 1;
                            }
                            speed -= MainData.MainLoop.TweakingComponent.greedPassiveSpeedMalus;
                            charTrait.hasAppliedStats = true;
                        }
                        break;

                    case "caring":
                        if (!charTrait.hasAppliedStats)
                        {
                            damageMax -= MainData.MainLoop.TweakingComponent.caringPassiveDamageMalus;
                            damageMin -= MainData.MainLoop.TweakingComponent.caringPassiveDamageMalus;
                            maxHealth += MainData.MainLoop.TweakingComponent.caringPassiveHealthBonus;
                            currentHealth += MainData.MainLoop.TweakingComponent.caringPassiveHealthBonus;
                            charTrait.hasAppliedStats = true;
                        }
                        break;

                    case "nurturing":
                        if (!charTrait.hasAppliedStats)
                        {
                            damageMax -= MainData.MainLoop.TweakingComponent.nurturePassiveDamageMalus;
                            damageMin -= MainData.MainLoop.TweakingComponent.nurturePassiveDamageMalus;
                            maxHealth += MainData.MainLoop.TweakingComponent.nurturePassiveHealthBonus;
                            currentHealth += MainData.MainLoop.TweakingComponent.nurturePassiveHealthBonus;
                            speed -= MainData.MainLoop.TweakingComponent.nurturePassiveSpeedMalus;
                            charTrait.hasAppliedStats = true;
                        }
                        break;

                    case "angry":
                        if (!charTrait.hasAppliedStats)
                        {
                            damageMax += MainData.MainLoop.TweakingComponent.angryPassiveDamageBonus;
                            damageMin += MainData.MainLoop.TweakingComponent.angryPassiveDamageBonus;
                            defense -= MainData.MainLoop.TweakingComponent.angryPassiveDefenseMalus;
                            charTrait.hasAppliedStats = true;
                        }
                        break;

                    default:
                        break;
                }
            }

        }
        /// <summary>
        /// is proc'd on equipping or unequipping items in the inventory
        /// </summary>
        public void RecalculateStatsFromItemsOutsideCombat()
        {
            if (recentlyUnequippedItemsHP.Count > 0)
            {//purge stats of unequipped items so we don't mess up the stats
                for (int i = 0; i < recentlyUnequippedItemsHP.Count; i++)
                {//removes the health bonus/malus.
                    maxHealth -= recentlyUnequippedItemsHP[i].healthmodifier;
                    currentHealth -= recentlyUnequippedItemsHP[i].healthmodifier;
                }
                if (currentHealth <= 0)
                {
                    MainData.MainLoop.EventLoggingComponent.LogGray("Loss of life-sustaining items sent " + charName + " to the brink of death.");
                    currentHealth = 1;
                }

                threatBonus = 0;
                Debug.Log(threatBonus + " THREAT BONUS");

                recentlyUnequippedItemsHP.Clear();
            }

            List<Item> eqCopy = new List<Item>(equippedItems); //so we can mess with the list it's made from without messing up the foreach loop
            foreach (Item item in eqCopy)
            {
                if (item.healthmodifier != 0) //bonuses or maluses work
                {
                    //if for whatever reason the change brings you under 0 hp, remove it from inventory, but this shouldn't happen because we check for it on clicking an item in inventory
                    if ((maxHealth + item.healthmodifier) < 0 || (currentHealth + item.healthmodifier) < 0)
                    {
                        MainData.equipmentInventory.Add(item);
                        equippedItems.Remove(item);

                        MainData.MainLoop.EventLoggingComponent.LogGray(charName + " tried to take" + item.itemName + " from the backpack, but hesitated at the last second.");
                        //we skip over this iteration and remove the item from inventory. no need to remove from recentlyUnequippedItemsHP
                    }
                    else
                    {
                        maxHealth += item.healthmodifier;
                        currentHealth += item.healthmodifier;
                    }
                }
            }


        }


        public void RegenerateMana()
        {//simple
            manaTotal += manaRegeneration;
            if (manaTotal > 100)
            {
                manaTotal = 100;
            }
        }


        /// <summary>
        /// this NEEDS to be used ANYTIME we change the trait of a character. otherwise bonuses/maluses will stack.... that's bad.
        /// </summary>
        public void RemoveTraitSafely()
        {

            if (charTrait != null)
            {
                switch (charTrait.identifier)
                {
                    case "wrath":

                        damageMax -= MainData.MainLoop.TweakingComponent.wrathDamageIncrease;
                        damageMin -= MainData.MainLoop.TweakingComponent.wrathDamageIncrease;
                        speed -= MainData.MainLoop.TweakingComponent.wrathSpeedIncrease;
                        charTrait = null;

                        break;

                    case "bulwark":

                            damageMax -= MainData.MainLoop.TweakingComponent.bulwarkDamageIncrease;
                            damageMin -= MainData.MainLoop.TweakingComponent.bulwarkDamageIncrease;
                            defense -= MainData.MainLoop.TweakingComponent.bulwarkDefenseIncrease;
                            speed += Mathf.Clamp(MainData.MainLoop.TweakingComponent.bulwarkSpeedDecrease, 0, 999);
                            charTrait = null;

                        break;

                    case "greed":

                        maxHealth += MainData.MainLoop.TweakingComponent.greedPassiveHealthMalus;
                        currentHealth += MainData.MainLoop.TweakingComponent.greedPassiveHealthMalus;
                        if (currentHealth > maxHealth)
                        {
                            currentHealth = maxHealth;
                        }
                        speed += MainData.MainLoop.TweakingComponent.greedPassiveSpeedMalus;
                        charTrait = null;

                        break;

                    case "caring":

                        damageMax += MainData.MainLoop.TweakingComponent.caringPassiveDamageMalus;
                        damageMin += MainData.MainLoop.TweakingComponent.caringPassiveDamageMalus;
                        maxHealth -= MainData.MainLoop.TweakingComponent.caringPassiveHealthBonus;
                        charTrait = null;

                        break;


                    case "nurturing":

                            damageMax += MainData.MainLoop.TweakingComponent.nurturePassiveDamageMalus;
                            damageMin += MainData.MainLoop.TweakingComponent.nurturePassiveDamageMalus;
                            maxHealth -= MainData.MainLoop.TweakingComponent.nurturePassiveHealthBonus;
                            currentHealth -= MainData.MainLoop.TweakingComponent.nurturePassiveHealthBonus;
                            speed += MainData.MainLoop.TweakingComponent.nurturePassiveSpeedMalus;
                            charTrait = null;
                        break;

                    case "angry":

                        damageMax -= MainData.MainLoop.TweakingComponent.angryPassiveDamageBonus;
                        damageMin -= MainData.MainLoop.TweakingComponent.angryPassiveDamageBonus;
                        defense += MainData.MainLoop.TweakingComponent.angryPassiveDefenseMalus;
                        charTrait = null;

                        break;

                    default:
                        Debug.LogError("Unknown trait at RemoveTraitSafely(). switch -> default case");
                        break;
                }

            }
            else
            {
                Debug.LogError("RemoveTraitSafely() ran for a traitless mob, for some reason. " + charName + " is the name of the mob, " + this.selfScriptRef.name + " is the script reference.");
            }


        }

        public void ChangeTrait(Trait newTrait)
        {
            if (charTrait != null)
            {
                MainData.MainLoop.EventLoggingComponent.Log(this.charName + " is no longer " + charTrait.adjective + ". ");
                RemoveTraitSafely();
            }


            //MainData.MainLoop.EventLoggingComponent.Log(this.charName + " is now " + newTrait.adjective + ". ");
            charTrait = (Trait)newTrait.Clone();//so we can have multiple characters with same trait because we're not just using a reference from the central trait lists
        }

        public bool CheckTrait(string b)
        {
            if (this.charTrait.traitName == b)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void TakeDamageFromCharacter(Character attacker, bool burn)
        {

            //here we deal with the generic damage modification
            int damagemod = 0;
            int defensemod = 0;

            if (attacker.equippedItems.Count > 0)
            {
                foreach (Item item in attacker.equippedItems)
                {
                    damagemod += item.dmgmodifier;
                }
                foreach (Item item in equippedItems)
                {

                    defensemod += item.defensemodifier;
                }

            }


            if (attacker.charTrait != null)
            {//here we deal with the attacker's traits.

                switch (attacker.charTrait.identifier)
                {
                    case "angry":
                        damagemod += attacker.charTrait.GenericTraitValue; //adds the whole value to damage output
                        attacker.charTrait.GenericTraitValue = 0;//resets it upon use
                        break;

                    case "malicious":
                        if (burn)
                        {
                            TakeDamage(Mathf.Clamp(maxHealth / 10, 1, 999));
                            return;
                        }
                        break;

                    default:
                        Debug.Log(attacker.charName + "'s trait passive not relevant to hitting.");
                        break;
                }

            }

            defense += defensemod;
            int baseDamageRoll = UnityEngine.Random.Range(attacker.damageMin, attacker.damageMax + 1) + damagemod;
            int damageRoll = baseDamageRoll - defense;
            MainData.MainLoop.LevelHelperComponent.PlaySound(SoundLibrary[1]);
            if (damageRoll <= 1)
            {
                damageRoll = 1;
            }

            if (defense < 0)
            {
                defense = 0;
            }
            if (charTrait != null)//gotta check if the mob has a trait because this also runs on attacking an enemy mob
            {
                if (charTrait.identifier == "perfectionist" && currentHealth != maxHealth)
                {
                    Debug.Log("HAPPENING");
                    damageRoll *= 2;
                }
            }
            
            //MainData.MainLoop.EventLoggingComponent.Log("damage without modifiers - " + (damageRoll - damagemod) + ", defense without modifiers " + (defense - defensemod));



            string lifestealText = "";
            //Lifesteal
            List<Item> results = attacker.equippedItems.FindAll(x => x.Lifesteal > 0);
            if (results.Count > 0)
            {
                float lifestealmod = 1f;
                int countyy = 1;
                foreach (Item item in attacker.equippedItems)
                {
                    if (item.Lifesteal > 0)
                    {
                        lifestealmod *= item.Lifesteal;
                        countyy++;
                    }
                }
                    lifestealmod /= countyy; //averages the lifesteal
                    float percentageheal = ((float)damageRoll / 100) * lifestealmod; //could also add health amp here but seems overkill;
                    lifestealText = percentageheal.ToString();
                    attacker.GainHealth(Mathf.RoundToInt(percentageheal));
                
            }
            
                
            


            //This is where we deal with traits deal with incoming damage
            if (charTrait != null)
            {

                switch (charTrait.identifier)
                {
                    case "angry":            //Anger's damage increase on getting hit

                        charTrait.GenericTraitValue += MainData.MainLoop.TweakingComponent.angryPassiveDamageBonus;
                        break;

                    case "bulwark":            //Bulwark HP increase

                        maxHealth += 1;
                        
                        break;

                    default:
                        Debug.LogWarning(charTrait.identifier + " unused trait at TakeDamageFromCharacter(). Not a bad thing usually.");
                        break;
                }
            }

            ///// BASIC DAMAGE AND DEFENSE MODIFIERS FROM ITEMS 

            float damageMultFloat = 0; //we turn these into ints after
            float damageResistFloat = 0;
            int damageMult = 0;
            int damageResist = 0;
            if (attacker.equippedItems.FindIndex(f => f.DamageBonusPercentage > 0) != -1) //it returns -1 if none are found
            {
                foreach (Item item in attacker.equippedItems)
                {
                    damageMultFloat += item.DamageBonusPercentage; //typecast parsing, just removes digits beyond the .
                }
            }
            if (attacker.equippedItems.FindIndex(f => f.DamageResistancePercentage > 0) != -1) //it returns -1 if none are found
            {
                foreach (Item item in equippedItems)
                {
                    damageResistFloat += item.DamageResistancePercentage;
                }
            }


            damageMult = Mathf.RoundToInt(damageMultFloat); 
            damageResist = Mathf.RoundToInt(damageResistFloat);

            if (damageMult != 0)
            {
                damageRoll = (damageRoll / 100) * (100 + damageMult);
            }

            if (damageResist != 0)
            {
                damageRoll = damageRoll / 100 * (100 - damageResist);
            }

            int temp = defense;//temporary value so we can show that the hit passed through armor

            //LUCK
            bool critical = false;
            bool solidblow = false;
            bool stunned = false;
            string luckmessage = "";
            //d100
            int luckAfterItems = attacker.GetCompoundLuck();
            int randomLuck = Random.Range(1, 101) + attacker.GetCompoundLuck(); //the character's luck is added on top of the random roll. Negative luck - more bad stuff happens
            //MainData.MainLoop.EventLoggingComponent.LogDanger("Random Luck Number was " + randomLuck + " including char luck - and the char luck was " + luck);
            //relational switch cases are not available in this C# version so imma just use if 

            //BAD LUCK HERE =================================
            if (randomLuck <= 1)
            { //CRITICAL FAILURE - TRIP
                attacker.currentStatusEffects.Add(new StatusEffect("stun", "This character is stunned.", 1));

                luckmessage = attacker.charName + " tries to attack " + charName + ", but through a twist of fate slips and bumps their head on a rock!";
                MainData.MainLoop.CombatHelperComponent.DisplayFloatingDamageNumbers(message: "Stunned!", target: attacker, heal: false);
                return;
            }
            else if (randomLuck <= 5)
            {//MISS
                MainData.MainLoop.EventLoggingComponent.Log(attacker.charName + " attempts to attack " + charName + ", but ill luck strikes and " + attacker.charName + " misses!");
                MainData.MainLoop.CombatHelperComponent.DisplayFloatingDamageNumbers(message: "Miss!", target: attacker, heal: false);
                return;
            }

            else if (randomLuck <= 15)
            { //GLANCING HIT
                damageRoll /= 3;
                luckmessage = "Glancing hit! Damage reduced to a third.";
            }






            //GOOD LUCK HERE ========================================
            else if (randomLuck >= 100)
            { //CRITICAL SUCCESS - DOUBLE DAMAGE + STUN
                //this.currentStatusEffects.Add(new StatusEffect("stun", "This character is stunned.", 1));
               // stunned = true;
                luckmessage = attacker.charName + " slips through " + this.charName + "'s defense and lands an eviscerating hit! " + this.charName + " is stunned! (2x Damage, Stun)";
                damageRoll = (damageRoll + defense) * 2; //double damage and passed through armor, stuns
                temp = 0;
            }
            else if (randomLuck >= 95)
            {//CRITICAL HIT - DOUBLE DAMAGE and IGNORES ARMOR
                luckmessage = attacker.charName + " slips through " + this.charName + "'s defense and lands an eviscerating hit! (2x Damage)";
                damageRoll = (damageRoll + defense) * 2; //double damage and passed through armor
                critical = true;
                temp = 0;
            }

            else if (randomLuck >= 85)
            { //SOLID BLOW. improved damage
                damageRoll = (int)(damageRoll * 1.5f);
                luckmessage = " What a solid blow! (1.5X Damage)";
                solidblow = true;
            }

            // STATUS EFFECTS




            //
            //(2 / 10) * 100

            currentHealth -= damageRoll; //INCORPORATED ARMOR CALCULATION HERE 
            if (solidblow)
            {
                MainData.MainLoop.CombatHelperComponent.DisplayFloatingDamageNumbers(damage: damageRoll, target: this, heal: false, message: "Solid Blow!");
            }
            else if (critical)
            {
                MainData.MainLoop.CombatHelperComponent.DisplayFloatingDamageNumbers(damage: damageRoll, target: this, heal: false, message: "Critical!");
            }
            else if (stunned)
            {
                MainData.MainLoop.CombatHelperComponent.DisplayFloatingDamageNumbers(damage: damageRoll, target: this, heal: false, message: "Stunned!");
            }
            else
            {

                MainData.MainLoop.CombatHelperComponent.DisplayFloatingDamageNumbers(damage: damageRoll, target: this, heal: false);
            }

            if (luckmessage != "") MainData.MainLoop.EventLoggingComponent.LogGray(luckmessage); //we describe the attack if it was special in some way.
            MainData.MainLoop.EventLoggingComponent.Log(attacker.charName + " " + attacker.attackverb + " the " + charName + " for " + (damageRoll + temp) + " damage. Armor protects for " + temp + " damage! ");
            if (lifestealText != "") MainData.MainLoop.EventLoggingComponent.Log(attacker.charName + " regains " + lifestealText + " health!");

            attacker.Threat += (damageRoll); // WE APPLY THREAT
            selfScriptRef.GotHurt();
            if (!isPlayerPartyMember)
            {//this updates the health bar so we don't run the whole big total refresh method
                MainData.MainLoop.UserInterfaceHelperComponent.RefreshViewEnemy();
                MainData.MainLoop.UserInterfaceHelperComponent.RefreshHealthBarEnemy();
            }
            else
            {
                MainData.MainLoop.UserInterfaceHelperComponent.RefreshHealthManaBarsPlayer();
            }
            if (currentHealth <= 0)
            {
                GotKilled(attacker);
            }
        }







        public void TakeDamage(int dmg)
        { //generic take damage function
            if (charTrait != null)
            {
                if (charTrait.traitName == "perfectionist" && currentHealth != maxHealth)
                {
                    currentHealth -= dmg;
                }
            }
            
            currentHealth -= dmg;
            MainData.MainLoop.EventLoggingComponent.Log(this.charName + " is hurt " + "for " + dmg + " damage!");
            MainData.MainLoop.CombatHelperComponent.DisplayFloatingDamageNumbers(damage: dmg, target: this, heal: false);

            if (!isPlayerPartyMember)
            {//this updates the health bar so we don't run the whole big total refresh method
                MainData.MainLoop.UserInterfaceHelperComponent.RefreshViewEnemy();
                MainData.MainLoop.UserInterfaceHelperComponent.RefreshHealthBarEnemy();
            }
            else
            {
                MainData.MainLoop.UserInterfaceHelperComponent.RefreshHealthManaBarsPlayer();
            }
            if (currentHealth <= 0)
            {
                GotKilled();
            }
        }

        public void GainHealth(int hp)
        {
            float AmplificationSum = 0;
            float healthFloat = 0;
            //we take all the bonuses
            foreach (Item item in equipmentInventory)
            {
                if (item.healingAmp != 0)
                {
                    AmplificationSum += item.healingAmp;
                }
            }
            if (AmplificationSum != 0)
            {
                healthFloat = ((float)hp / 100) * (100 + AmplificationSum);
                currentHealth += Mathf.RoundToInt(healthFloat);
                Debug.LogWarning(healthFloat + " is the health value gained post formula with health amplification.");
                MainData.MainLoop.CombatHelperComponent.DisplayFloatingDamageNumbers(target: this, damage: Mathf.RoundToInt(healthFloat), heal: true);
            }
            else
            {
                currentHealth += hp;
                Debug.LogWarning(hp + " is the health value gained. no healthAmp.");
                MainData.MainLoop.CombatHelperComponent.DisplayFloatingDamageNumbers(target: this, damage: hp, heal: true);
            }
            
           
            if (currentHealth >= this.maxHealth)
            {
                currentHealth = maxHealth;
            }
            MainData.MainLoop.UserInterfaceHelperComponent.RefreshCharacterTabs();

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
                    case "stun": //we use this to know when the stun times out.
                        this.canAct = false;
                        MainData.MainLoop.EventLoggingComponent.LogGray(this.charName + " is helpless for " + (statusEff.turnsRemaining - 1).ToString() + " more turns.");
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
                    switch (statusEff.type)
                    {
                        case "stun":
                            this.canAct = true;
                            MainData.MainLoop.EventLoggingComponent.LogGray(this.charName + " can act again.");
                            break;
                        default:
                            break;
                    }





                    currentStatusEffects.Remove(statusEff);
                }
            }



        }

        public void InitializeHealthBar()
        {
            MainData.MainLoop.UserInterfaceHelperComponent.RefreshHealthBarEnemy();
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
            isDead = true;
            if (!isPlayerPartyMember)
            {//this updates the health bar so we don't run the whole big total refresh method
                MainData.MainLoop.UserInterfaceHelperComponent.RefreshViewEnemy();
                MainData.MainLoop.UserInterfaceHelperComponent.RefreshHealthBarEnemy();
                MainData.MainLoop.CombatHelperComponent.TargetSelectionCheck();

                MainData.recentDeadEnemies.Add(this);

                MainData.MainLoop.Currency += valueBounty;
                MainData.MainLoop.EventLoggingComponent.LogGray("The " + charName + " dropped " + valueBounty + " coins.");
                MainData.MainLoop.UserInterfaceHelperComponent.UpdateCurrencyCounter();
            }
            else
            {
                List<Item> eqCopy = new List<Item>(equippedItems);
                foreach (Item item in eqCopy)
                {
                    MainData.equipmentInventory.Add(item);
                }
                equippedItems.Clear();
                MainData.MainLoop.UserInterfaceHelperComponent.RefreshHealthManaBarsPlayer();

                if (MainData.livingPlayerParty.Count == 0)
                {
                    MainData.MainLoop.LostTheGame();
                }
            }
           
            MainData.recentDeadEnemies.Add(this);
            selfScriptRef.Die();
        }
        public void HandleListsUponDeath()
        {
            if (isDead)
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
        public Sprite statusEffectImage;





        public StatusEffect(string type, string description, int turnsRemaining)
        {
            this.type = type;
            this.description = description;
            this.turnsRemaining = turnsRemaining;
        }

    }

}
