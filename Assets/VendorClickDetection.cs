using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendorClickDetection : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {//we open up that menu.
            Debug.Log("Vendor got clicked.");
            StaticDataHolder.MainLoop.VendorScriptComponent.OpenVendorMenu();
        }
    }

    //void OnMouseEnter()
    //{
    //}


    //Could make the vendor animate when hovered over.

    //void OnMouseExit()
    //{

    //}
}
