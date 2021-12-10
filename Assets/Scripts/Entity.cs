using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Storagestuff;
using static TraitHelper;

public class Entity : MonoBehaviour
{
    public void GenerateParty()
    {




    }

    public void GenerateMonsters()
    {

    }

    public void GenerateConsumables()
    {

    }
    public void GenerateEquipment()
    {//generates traits, stores them all in a dictionary in the dataholder




    }
    public void GenerateTraits()
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
        public int charID;
        public string id;
        public string entityDescription;
        public CharacterScript currentCharObj;
        public bool isPlayerPartyMember;

        public Trait charTrait;
        public Sprite charSprite;

        public StatusEffect currentStatusEffect;

        public int currentHealth;
        private int baseHealth;
        private int baseDamage;

        public int defense;
        public int damage;
        public int luck;
        public int mana;


        public void TakeDamage(int dmg, string attackverb, Character attacker, bool critical)
        { //runs when being hit
            if (critical)
            { //make this red and bigger
                Storagestuff.GameLog("Critical strike!");
            }
           Storagestuff.GameLog(this.id + " the " + charTrait.name + " has been " + attackverb + "ed by " + attacker.id + "for " + dmg + " damage!");

        }

        //public void GainStatusEffect (StatusEffect statusEffect, int turns)
        //{
        //    statusEffect.Afflicted = this;
        //    statusEffect.Turns = turns;


        //}

        public void AttackRandom()
        {
            if (isPlayerPartyMember)
            {
                Character b = Storagestuff.enemyParty[Random.Range(0, Storagestuff.enemyParty.Count)];
                //b.TakeDamage();
                b.CheckHealth(this);
            }
            else
            {
                Character d = Storagestuff.playerParty[Random.Range(0, Storagestuff.playerParty.Count)];
                //d.TakeDamage();
                d.CheckHealth(this);
            }
        }
        /// <summary>
        /// the function is called once per turn to make sure damaging status effects apply, but is also called with a Character parameter after an attack.
        /// </summary>
        /// <param name="hitter"></param>
        public void CheckHealth(Character hitter = null)
        {



        }
        public void gotKilled(Character killer)
        {
            //GameLog(killed.charName + "has been vanquished!");
            if (isPlayerPartyMember)
            {
                playerParty.Remove(allChars.Find(x => x.GetID() == this.charID));
            }
            enemyParty.Remove(allChars.Find(x => x.GetID() == this.charID));
            allChars.Remove(allChars.Find(x => x.GetID() == this.charID));
            Destroy(this);
        }
        public int GetID()
        {
            return charID;
        }
    }


    public class StatusEffect
    {
        public string type;
        public string description;
        public int turnsRemaining;
    }

}
