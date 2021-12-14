using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static EntitiesDefinition;
using static TraitHelper;

public class CharacterScript : MonoBehaviour
{
    //the previous are just for testing, we'll grab the stuff from the char reference below later
    public Character associatedCharacter;
    public float inflateStep = 0.01f;
    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (associatedCharacter != null)
        {
            associatedCharacter.currentCharObj = this;
        }
       
        if (associatedCharacter.isPlayerPartyMember)
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


    public void Death()
    {
        spriteRenderer.sprite = null;
        associatedCharacter = null;

    }

    public void SetupCharacterAfterTemplate(Character template)
    {
        associatedCharacter = template;
        spriteRenderer.sprite = associatedCharacter.charSprite;
    }

    public void GotClicked()
    {
        //highlight this character
        //track selection in maindata or gamemanager

    }








    public void SwapWith(CharacterScript target)
    {
        if (target.associatedCharacter.isPlayerPartyMember)
        {
            Vector3 targetPosition = target.transform.position;
            target.transform.position = this.transform.position;
            this.transform.position = targetPosition;
        }
        
    }


    void RefreshCharacterScript(bool show)
    {
        TextMeshProUGUI charactDescript = MainData.uiMan.selectedCharDescription.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI charactName = MainData.uiMan.selectedCharName.GetComponent<TextMeshProUGUI>();
        Image charactAvatar = MainData.uiMan.selectedCharAvatar.GetComponent<Image>();
        TextMeshProUGUI charactTitle = MainData.uiMan.selectedCharTraitDesc.GetComponent<TextMeshProUGUI>();
        if (show)
        {
            charactDescript.text = associatedCharacter.entityDescription;
            charactName.text = associatedCharacter.charName;
            if (associatedCharacter.charTrait != null)
            {
                charactTitle.text = associatedCharacter.charTrait.traitName;
            }
            charactAvatar.sprite = associatedCharacter.charAvatar;
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
        
            RefreshCharacterScript(true);
            transform.localScale = new Vector3(1.1f, 1.1f, 1f);
        
        
        //show text on bottom of screen
    }


    private void OnMouseOver()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {

            GotClicked();
        }
    }



    void OnMouseExit()
    {
        
            RefreshCharacterScript(false);
        transform.localScale = new Vector3(1f, 1f, 1f);
        
    }


        
        

    
   
        
            
        

        

    

}
