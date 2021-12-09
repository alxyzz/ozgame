using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ItemHelper;

public class CharacterScript : MonoBehaviour
{
    public bool isEnemy;
    public string characterstringID;
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
        Debug.Log(this.name + " was clicked.");
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


    void SwapWith()
    {

    }


    void OnMouseEnter()
    {
        transform.localScale = new Vector3(1.1f, 1.1f, 1f);
        //show text on bottom of screen
    }




    void OnMouseExit()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
    }


        
        

    
   
        
            
        

        

    

}
