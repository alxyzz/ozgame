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


    void GenerateTraits()
    {




    }

    public class Trait : MonoBehaviour
    {
        public string identifier;
        public string traitName;
        public string traitBlurb;
        public string traitDescription;
        public string adjective;
        public Sprite traitSprite;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="adjec">this gets added to the character's name as a title</param>
        /// <param name="blurb">shorter description or quote etc</param>
        /// <param name="description">description, longer</param>
        /// <param name="sprit">the sprite of the trait</param>
        /// <param name="t2">true if it's a tier 2 trait, false if it's t1</param>
        public Trait(string id,string name,string adjec, string blurb, string description, Sprite sprit, bool t2)
        {
            this.identifier = id;
            this.traitName = name;
            this.traitBlurb = blurb;
            this.adjective = adjec;
            this.traitDescription = description;
            this.traitSprite = sprit;
            if (t2)
            {
                MainData.traitList.Add(id, this);
            }
            else
            {
                MainData.traitList.Add(id, this);
            }
            MainData.traitList.Add(id, this);
        }





        public string GetName()
        {
            return traitName;
        }
    }





}

