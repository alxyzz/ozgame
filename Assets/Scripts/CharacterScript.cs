using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static EntitiesDefinition;
using static TraitHelper;

public class CharacterScript : MonoBehaviour
{
    public bool isEnemy;
    public string characterstringID;
    public string charName;
    public string charDesc;
    public Trait charTrait;
    public Sprite charAvatar;
    //the previous are just for testing, we'll grab the stuff from the char reference below later
    public Character associatedCharacter;
    public float inflateStep = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        if (associatedCharacter != null)
        {
            associatedCharacter.currentCharObj = this;
        }
       
        if (!isEnemy)
        {
            MainData.playerPartyMemberObjects.Add(gameObject);
        }
        else
        {
            MainData.enemyPartyMemberObjects.Add(gameObject);
        }
    }



    //make a different button for this
    
    public void Die()
    {
        //play death animation
        //GameLog.DeathLog(associatedCharacter.charName + " has died.");
        //associatedCharacter = null;
        //strip away all images, names, etc. set to inactive.

    }


    public void Attack(CharacterScript target)
    {//add dodging
        target.associatedCharacter.TakeDamageFromCharacter(associatedCharacter.damage, associatedCharacter.attackverb, associatedCharacter, false);

    }







    public void GotClicked()
    {


    }








    public void SwapWith(CharacterScript target)
    {
        if (!target.isEnemy)
        {
            Vector3 targetPosition = target.transform.position;
            target.transform.position = this.transform.position;
            this.transform.position = targetPosition;
        }
        
    }


    void DisplayCharacterInformation(bool tr)
    {
        TextMeshProUGUI charactDescript = MainData.uiMan.selectedCharDescription.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI charactName = MainData.uiMan.selectedCharName.GetComponent<TextMeshProUGUI>();
        Image charactAvatar = MainData.uiMan.selectedCharAvatar.GetComponent<Image>();
        TextMeshProUGUI charactTitle = MainData.uiMan.selectedCharTraitDesc.GetComponent<TextMeshProUGUI>();
        if (tr)
        {
            charactDescript.text = charDesc;
            charactName.text = charName;
            if (charTrait != null)
            {
                charactTitle.text = charTrait.traitName;
            }
            charactAvatar.sprite = charAvatar;
        }
        else
        {
            charactDescript.text = "";
            charactName.text = "";
            charactTitle.text = "";
            charactAvatar.sprite = null;
        }
    }





    void OnMouseEnter()
    {
        
            DisplayCharacterInformation(true);
            transform.localScale = new Vector3(1.1f, 1.1f, 1f);
        
        
        //show text on bottom of screen
    }




    void OnMouseExit()
    {
        
            DisplayCharacterInformation(false);
        transform.localScale = new Vector3(1f, 1f, 1f);
        
    }


        
        

    
   
        
            
        

        

    

}
