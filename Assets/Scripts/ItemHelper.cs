using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;
using static TraitHelper;

public class ItemHelper : MonoBehaviour
{
    private void Start()
    {
        
    }


    void GenerateParty()
    {



    }


    void GenerateMonsters()
    {


    }

    void GenerateItems()
    {



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



    
    public class Character : MonoBehaviour
    {
        public int ID;
        public string entityName;
        public string entityDescription;
        public bool isPlayerPartyMember;
        public Trait charTrait;
        public Sprite charSprite;
        public int health;

        public StatusEffect currentStatusEffect;

        private int baseHealth;
        private int baseDamage;

        public void TakeDamage(int dmg, string attackverb, Character attacker, bool critical)
        { //runs when being hit
            if (critical)
            { //make this red and bigger
                GameManager.GameLog("Critical strike!");
            }
           // GameManager.GameLog(this.entityName + " the " + charTrait.name + " has been " + attackverb + "ed by " + attacker.entityName + "for " + dmg + " damage!");

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
                Character b = GameManager.enemyParty[Random.Range(0, GameManager.enemyParty.Count)];
                //b.TakeDamage();
                b.CheckHealth(this);
            }
            else
            {
                Character d = GameManager.playerParty[Random.Range(0, GameManager.playerParty.Count)];
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
            allChars.Remove(allChars.Find(x => x.GetID() == this.ID));
            Destroy(this);
        }
        public int GetID()
        {
            return ID;
        }
    }


    public class StatusEffect
    {
        public string type;
        public string description;
        public int turnsRemaining;
    }

}
