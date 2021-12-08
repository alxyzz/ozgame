using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterfaceHelper : MonoBehaviour
{

    public GameObject selectedCharAvatar; //we will replace this object's image with the currently selected character's avatar
    public GameObject selectedCharName;
    public GameObject selectedCharTraitDesc;
    public GameObject selectedCharTraitIcon;
    //if we decide wether we should allow characters to have more than one trait, rework this to display the trait icons in a list/row

    // Start is called before the first frame update
    void Start()
    {
        GameManager.uiMan = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
