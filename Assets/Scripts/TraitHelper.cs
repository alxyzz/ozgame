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
        public string traitName;
        public string traitBlurb;
        public string traitDescription;

        public Sprite traitSprite;


        public Trait(string name, string blurb, string description, Sprite sprit)
        {
            traitName = name;
            traitBlurb = blurb;
            traitDescription = description;
            traitSprite = sprit; 

        }




        private void Start()
        {
            DataHolder.traitList.Add(this);
        }
        public string GetName()
        {
            return traitName;
        }
    }





}

