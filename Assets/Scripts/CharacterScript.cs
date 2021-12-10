using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Entity;
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
            Storagestuff.playerPartyMemberObjects.Add(gameObject);
        }
        else
        {
            Storagestuff.enemyPartyMemberObjects.Add(gameObject);
        }
    }


    void CheckCharacterExistence()
    {
        if (associatedCharacter != null)
        {
            associatedCharacter.currentCharObj = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
       
        if (Storagestuff.ControlsHelperRef.SelectedChar == null)
        {
            Storagestuff.ControlsHelperRef.SelectedChar = gameObject;
        }
        else if (Storagestuff.ControlsHelperRef.SelectedChar != gameObject)
        {

            //SwapWith(GameManager.ControlsHelperRef.SelectedChar);
        }
        else if (Storagestuff.ControlsHelperRef.SelectedChar == gameObject)
        {
            Storagestuff.ControlsHelperRef.SelectedChar = null;
        }
    }



    void ToggleCharDetails(bool tr)
    {
        TextMeshProUGUI charactDescript = Storagestuff.uiMan.selectedCharDescription.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI charactName = Storagestuff.uiMan.selectedCharName.GetComponent<TextMeshProUGUI>();
        Image charactAvatar = Storagestuff.uiMan.selectedCharAvatar.GetComponent<Image>();
        TextMeshProUGUI charactTitle = Storagestuff.uiMan.selectedCharTraitDesc.GetComponent<TextMeshProUGUI>();
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


    void SwapWith()
    {

    }


    void OnMouseEnter()
    {
        
            ToggleCharDetails(true);
            transform.localScale = new Vector3(1.1f, 1.1f, 1f);
        
        
        //show text on bottom of screen
    }




    void OnMouseExit()
    {
        
            ToggleCharDetails(false);
        transform.localScale = new Vector3(1f, 1f, 1f);
        
    }


        
        

    
   
        
            
        

        

    

}
