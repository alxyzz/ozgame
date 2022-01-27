using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTweakingInterface : MonoBehaviour
{
    //this script exists to make it easier for designers to tweak around the bonus/malus values of different items/trinkets.



    //[Header("Global variables")]
    //public int luckMaximum; // we will deal with this later. luck is not that important for now
    //MODIFY IN INSPECTOR. NOT HERE. THE VALUES HERE WILL NOT APPLY. UNLESS YOU GO INSPECTOR-> RESET. BUT NO REASON TO DO IT
    [Header("Items")]
    public int swordDamage = 0;
    public int HealthPotionHealthGiven = 0;
    [Space(5)]
    [Header("Traits")]

    [Space(5)]
    public int caringActiveHealing = 0;
    public int caringHealingPotionPercentageHealingTakenMalus = 0; //percentage of healing actually taken by people with this. so 75 = 75% of normal health gain. 50 = 50% of a potion's normal health gain for whoever has the caring trait.
    public int caringPassiveHealthBonus = 0;
    public int caringPassiveDamageMalus = 0;
    [Space(5)]
    public int nurtureActiveHealing = 0;
    public int nurturePassiveHealthBonus = 0;
    public int nurturePassiveDamageMalus = 0;
    public int nurturePassiveSpeedMalus = 0;
    [Space(5)]
    public int greedActiveSelfDamage = 0;
    public int greedActiveRevenue = 0;
    public int greedBountyPercentageBonus = 0; //50 = 50% more loot from enemies.
    public int greedPassiveHealthMalus = 0;
    public int greedPassiveSpeedMalus = 0;
    [Space(5)]
    public int angryActiveDamageMalus = 0; // this is dealt to both min and max dmg. so a plain damage reduction thing.
    public int angryActivePowerDamage = 0;//damage dealt by the active power, Lash Out
    public int angryPassiveDamageBonus = 0;//Damage increase from getting hit. works on next hit.
    public int angryPassiveDefenseMalus = 0;
    //public int angryActivePowerDamage = 0;//damage dealt by the active power, Lash Out[Header("Caring")]
    [Space(5)]
    public int wrathDamageIncrease = 0;
    public int wrathSpeedIncrease = 0;
    [Space(5)]
    public int bulwarkDamageIncrease = 0;
    public int bulwarkSpeedDecrease = 0;
    public int bulwarkDefenseIncrease = 0;
    [Space(5)]
    [Header("These should be from 0 to 100, no more. You gain the amount per turn.")]
    public int GreedManaCost;
    public int AngerManaCost;
    public int CaringManaCost;
    public int WrathManaCost;
    public int BulwarkManaCost;
    public int NurturingManaCost; //6

    //double strike has no value to edit
    [Space(5)]
    [Header("Character Defaults")]
    [Header("Player Characters")]
    public int PCStartingMaxHealth = 0;
    public int PCStartingMinDamage = 0;
    public int PCStartingMaxDamage = 0;
    public int PCStartingSpeed = 0;
    public int PCStartingLuck = 0;
    public int PCStartingDefense = 0;
    public int PCStartingManaRegen = 0;
    [Header("Enemy Characters")]
    public int flyingMonkeyMaxHealth = 0;
    public int flyingMonkeyMinDamage = 0;
    public int flyingMonkeyMaxDamage = 0;

    public int flyingMonkeySpeed = 0;
    public int flyingMonkeyLuck = 0;
    public int flyingMonkeyDefense = 0;
    //Normal Monkey
    [Space(5)]

    public int strongflyingMonkeyMaxHealth = 0;
    public int strongflyingMonkeyMinDamage = 0;
    public int strongflyingMonkeyMaxDamage = 0;

    public int strongflyingMonkeySpeed = 0;
    public int strongflyingMonkeyLuck = 0;
    public int strongflyingMonkeyDefense = 0;
    //Strong monkey
    [Space(5)]

    public int weakflyingMonkeyMaxHealth = 0;
    public int weakflyingMonkeyMinDamage = 0;
    public int weakflyingMonkeyMaxDamage = 0;

    public int weakflyingMonkeySpeed = 0;
    public int weakflyingMonkeyLuck = 0;
    public int weakflyingMonkeyDefense = 0;
    //Weak monkey
    [Space(5)]

    public int hellbentflyingMonkeyMaxHealth = 0;
    public int hellbentflyingMonkeyMinDamage = 0;
    public int hellbentflyingMonkeyMaxDamage = 0;

    public int hellbentflyingMonkeySpeed = 0;
    public int hellbentflyingMonkeyLuck = 0;
    public int hellbentflyingMonkeyDefense = 0;
    //Hellbent Monkey
    [Space(5)]

    public int corruptedMaxHealth = 0;
    public int corruptedMinDamage = 0;
    public int corruptedMaxDamage = 0;

    public int corruptedSpeed = 0;
    public int corruptedLuck = 0;
    public int corruptedDefense = 0;
    //Weak monkey
    [Space(5)]

    public int weakcorruptedMaxHealth = 0;
    public int weakcorruptedMinDamage = 0;
    public int weakcorruptedMaxDamage = 0;

    public int weakcorruptedSpeed = 0;
    public int weakcorruptedLuck = 0;
    public int weakcorruptedDefense = 0;
    //Weak monkey
    [Space(5)]

    public int toughcorruptedMaxHealth = 0;
    public int toughcorruptedMinDamage = 0;
    public int toughcorruptedMaxDamage = 0;

    public int toughcorruptedSpeed = 0;
    public int toughcorruptedLuck = 0;
    public int toughcorruptedDefense = 0;
    //Weak monkey
    [Space(5)]

    public int legendarycorruptedMaxHealth = 0;
    public int legendarycorruptedMinDamage = 0;
    public int legendarycorruptedMaxDamage = 0;

    public int legendarycorruptedSpeed = 0;
    public int legendarycorruptedLuck = 0;
    public int legendarycorruptedDefense = 0;
    //Weak monkey
    [Space(5)]

    [Space(5)]
    [Header("ItemColors")]
    public Color CommonColor;
    public Color UncommonColor;
    public Color RareColor;
    public Color MasterworkColor;
    public Color GenericColor;

    // Start is called before the first frame update
    void Start()
    {
        MainData.itemManager = this;
    }

}
