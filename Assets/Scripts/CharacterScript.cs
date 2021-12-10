using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static EntityHelper;

public class CharacterScript : MonoBehaviour
{
    public bool isEnemy;
    public string characterstringID;
    public string charName;
    public string charDesc;
    public string charTrait;
    public Sprite charAvatar;
    public Character associatedCharacter;
    public float inflateStep = 0.01f;
    bool done = false;

    // Start is called before the first frame update
    void Start()
    {
        if (!isEnemy)
        {
            GameManager.playerPartyMemberObjects.Add(gameObject);
        }
        else
        {
            GameManager.enemyPartyMemberObjects.Add(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AssumeIdentity(Character charac)
    {


    }


    void OnMouseDown()
    {
       
        if (GameManager.ControlsHelperRef.SelectedChar == null)
        {
            GameManager.ControlsHelperRef.SelectedChar = gameObject;
        }
        else if (GameManager.ControlsHelperRef.SelectedChar != gameObject)
        {
            //SwapWith(GameManager.ControlsHelperRef.SelectedChar);
        }
        else if (GameManager.ControlsHelperRef.SelectedChar == gameObject)
        {
            GameManager.ControlsHelperRef.SelectedChar = null;
        }
    }



    void ToggleCharDetails(bool tr)
    {
        TextMeshProUGUI charactDescript = GameManager.uiMan.selectedCharDescription.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI charactName = GameManager.uiMan.selectedCharName.GetComponent<TextMeshProUGUI>();
        Image charactAvatar = GameManager.uiMan.selectedCharAvatar.GetComponent<Image>();
        TextMeshProUGUI charactTitle = GameManager.uiMan.selectedCharTraitDesc.GetComponent<TextMeshProUGUI>();
        if (tr)
        {
            charactDescript.text = charDesc;
            charactName.text = charName;
            charactTitle.text = charTrait;
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
