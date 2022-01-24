using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitHelper : MonoBehaviour
{

    public Sprite DefaultTraitSprite;

    void Start()
    {


        //GameManager.lvlManager = this;
    }




    public class Trait : System.ICloneable
    {
        public string identifier;
        public string traitName;
        public string traitBlurb;
        public string traitDescription;
        public string adjective;
        public Sprite traitSprite;
        public int manaCost;
        public bool forceCooldown;//in case we want to turn that stuff off for a while
        public int GenericTraitValue = 0; //we will just use this for whatever is needed at the moment. things like stacking damage boosts for Angry, since it's simpler than adding yet another value to the character class 
        public bool hasAppliedStats = false;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="adjec">this gets added to the character's name as a title</param>
        /// <param name="blurb">shorter description or quote etc</param>
        /// <param name="description">description, longer</param>
        /// <param name="sprit">the sprite of the trait</param>
        /// <param name="t2">true if it's a tier 2 trait, false if it's t1</param>
        public Trait(string id, string name, string adjec, string blurb, string description, Sprite sprit, bool t2)
        {
            this.identifier = id;
            this.traitName = name;
            this.traitBlurb = blurb;
            this.adjective = adjec;
            this.traitDescription = description;
            this.traitSprite = sprit;
            if (t2)
            {
                if (!MainData.t2traitList.ContainsKey(id))
                {
                    MainData.t2traitList.Add(id, this);//this is so we can drop a random t1/t2 trait after a boss fight or something
                }
                else
                {
                    Debug.LogError("Duplicate key add attempt to t2traitlist. Key - " + id);
                }
            }
            else
            {
                if (!MainData.t1traitList.ContainsKey(id))
                {
                    MainData.t1traitList.Add(id, this);
                }
                else
                {
                    Debug.LogError("Duplicate key add attempt to t1traitlist. Key - " + id);
                }

            }
            
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }



        public string GetName()
        {
            return traitName;
        }
    }





}

