using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesignerEasyValueTweaking : MonoBehaviour
{
    //this script exists to make it easier for designers to tweak around the bonus/malus values of different items/trinkets.



    //[Header("Global variables")]
    //public int luckMaximum; // we will deal with this later. luck is not that important for now
    //MODIFY IN INSPECTOR. NOT HERE. THE VALUES HERE WILL NOT APPLY. UNLESS YOU GO INSPECTOR-> RESET. BUT NO REASON TO DO IT
    [Header("Items")]
    public int swordDamage = 0;
    public int HealthPotionHealthGiven = 0;
    [Space(15)]
    [Header("Traits")]
    [Space(5)]
    [Header("Caring")]
    public int caringActiveHealing = 0;
    public int caringHealingPotionPercentageHealingTakenMalus = 0; //percentage of healing actually taken by people with this. so 75 = 75% of normal health gain. 50 = 50% of a potion's normal health gain for whoever has the caring trait.
    public int caringPassiveHealthBonus = 0;
    public int caringPassiveDamageMalus = 0;
    [Header("Greed")]
    public int greedActiveSelfDamage = 0;
    public int greedActiveRevenue = 0;
    public int greedBountyPercentageBonus = 0; //50 = 50% more loot from enemies.
    public int greedPassiveHealthMalus = 0;
    public int greedPassiveSpeedMalus = 0;
    [Header("Angry")]
    public int angryActiveDamageMalus = 0; // this is dealt to both min and max dmg. so a plain damage reduction thing.
    public int angryActivePowerDamage = 0;//damage dealt by the active power, Lash Out
    public int angryPassiveDamageBonus = 0;//Damage increase from getting hit. works on next hit.
    public int angryPassiveDefenseMalus = 0;
    //public int angryActivePowerDamage = 0;//damage dealt by the active power, Lash Out[Header("Caring")]
    [Header("Wrath")]
    public int wrathDamageIncrease = 0;
    public int wrathSpeedIncrease = 0;
    //double strike has no value to edit
    [Space(15)]
    [Header("Character Defaults")]
    [Header("Player Characters")]
    public int PCStartingMaxHealth = 0;
    public int PCStartingMaxDamage = 0;
    public int PCStartingMinDamage = 0;
    public int PCStartingSpeed = 0;
    public int PCStartingLuck = 0;
    public int PCStartingDefense = 0;
    [Header("Enemy Characters")]
    public int flyingMonkeyMaxHealth = 0;
    public int flyingMonkeyMaxDamage = 0;
    public int flyingMonkeyMinDamage = 0;
    public int flyingMonkeySpeed = 0;
    public int flyingMonkeyLuck = 0;
    public int flyingMonkeyDefense = 0;















    // Start is called before the first frame update
    void Start()
    {
        MainData.itemManager = this;
    }

}
