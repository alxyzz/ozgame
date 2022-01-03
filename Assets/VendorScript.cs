using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendorScript : MonoBehaviour
{


    [Space(5)]
    public GameObject Destination;//this is where it's supposed to go right before we interact with it
    public GameObject Startination;//this is where it spawns BEFORE it comes onscreen
    public GameObject Cart;//the cart itself. opens up the menu when clicked
    public GameObject GoodbyeDestination;//this is where the vendor is going when the party moves beyond
    [Space(5)]
    public GameObject GameUIReference; //we disable this so we don't do bad stuff.
    public GameObject VendorUIReference;

    [Space(5)]
    private bool isVendorHere;
    private bool cartMoving = false; //never mess with the cart while it is in motion, lest it tip over.
    [Space(5)]
    [Header("These are related to the movement of the vendor.")]
    [Space(1)]
    [Header("Minimum distance from the destination at which the cart can stop moving towards the party.")]
    public float minDistance;
    [Header("Speed at which the cart moves. Should be same as the background layer of the same distance.")]
    public float speed;
    [Header("This should be around 0.01f to be smooth...")]
    public float moveInterval;




    //for when cart is arriving
    IEnumerator ArrivalAnimation()
    {
        cartMoving = true;
        MainData.MainLoop.LevelHelperComponent.MoveBackwards(); //ATTENTION - IF THIS SOMEHOW MAKES IT MOVE THE OTHER WAY - THE ORDER IS FLIPPED IN LEVELmANAGER. JUST USE MOVEBACKWARDS.
        isVendorHere = true;
        while (Vector3.Distance(Cart.transform.position, Destination.transform.position) > 0.2f)
        {
            Cart.transform.position = Vector3.MoveTowards(Cart.transform.position, Destination.transform.position, speed * Time.deltaTime);
            yield return new WaitForSecondsRealtime(moveInterval);
        }

        //Cart.transform.position = Destination.transform.position;
        MainData.MainLoop.LevelHelperComponent.MoveStop();
        cartMoving = false;
    }


    //for when the cart is leaving
    IEnumerator LeavingAnimation()
    {
        cartMoving = true;
        MainData.MainLoop.LevelHelperComponent.MoveBackwards();
        
        while (Vector3.Distance(Cart.transform.position, GoodbyeDestination.transform.position) > 0.2f)
        {
            Cart.transform.position = Vector3.MoveTowards(Cart.transform.position, GoodbyeDestination.transform.position, speed * Time.deltaTime);
            yield return new WaitForSecondsRealtime(moveInterval);
        }

        Cart.transform.position = Startination.transform.position; //we send it back to beginning
        MainData.MainLoop.LevelHelperComponent.MoveStop();
        isVendorHere = false;
        cartMoving = false;
    }


    //click the vendor
    //this should play a short nice animation
    public void OpenVendorMenu()
    {
        GameUIReference.SetActive(false);
        VendorUIReference.SetActive(true);
    }




    public void CloseVendorMenu()
    {//click back button in shop
        GameUIReference.SetActive(true);
        VendorUIReference.SetActive(false);
    }





    public void GenerateMerchantInventory()
    {

    }
    //we call this from
    //a button for testing
    //LevelManager when we reach the point where the thing spawns.
    public void MoveMerchant()
    {
        if (cartMoving)
        {
            return;
        }
        if (!isVendorHere )
        {//we make it come
            
            StartCoroutine(ArrivalAnimation());

        }
        else
        {//we make it leave
            StartCoroutine(LeavingAnimation());
        }
        
    }

}
