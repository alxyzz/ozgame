using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LevelHelper;

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
    [HideInInspector]
    public bool isVendorHere;
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
    [Space(5)]
    [Header("These are related to the trading itself...")]
    [HideInInspector]
    [Header("How many items are sold.")]
    public int itemAmount; //

    //for when cart is arriving
    IEnumerator ArrivalAnimation()
    {
        cartMoving = true;
        StaticDataHolder.MainLoop.LevelHelperComponent.MoveBackwards(); //ATTENTION - IF THIS SOMEHOW MAKES IT MOVE THE OTHER WAY - THE ORDER IS FLIPPED IN LEVELmANAGER. JUST USE MOVEBACKWARDS.
        isVendorHere = true;
        while (Vector3.Distance(Cart.transform.position, Destination.transform.position) > 0.2f)
        {
            Cart.transform.position = Vector3.MoveTowards(Cart.transform.position, Destination.transform.position, speed * Time.deltaTime);
            yield return new WaitForSecondsRealtime(moveInterval);
        }

        //Cart.transform.position = Destination.transform.position;
        StaticDataHolder.MainLoop.LevelHelperComponent.MoveStop();
        cartMoving = false;
    }
    //for when the cart is leaving
    IEnumerator LeavingAnimation()
    {
        cartMoving = true;
        StaticDataHolder.MainLoop.LevelHelperComponent.MoveBackwards();
        
        while (Vector3.Distance(Cart.transform.position, GoodbyeDestination.transform.position) > 0.2f)
        {
            Cart.transform.position = Vector3.MoveTowards(Cart.transform.position, GoodbyeDestination.transform.position, speed * Time.deltaTime);
            yield return new WaitForSecondsRealtime(moveInterval);
        }

        Cart.transform.position = Startination.transform.position; //we send it back to beginning
        StaticDataHolder.MainLoop.LevelHelperComponent.MoveStop();
        isVendorHere = false;
        cartMoving = false;
    }
    //click the vendor
    //this could play a short nice animation, perhaps
    public void OpenVendorMenu()
    {
        if (StaticDataHolder.currentLevel.localMerchant != null)
        {
            StaticDataHolder.currentLevel.localMerchant = GenerateMerchantInventory();
        }

        if (StaticDataHolder.livingEnemyParty.Count > 0)
        {
            StaticDataHolder.MainLoop.EventLoggingComponent.Log("The merchant refuses to trade until you have dealt with your pursuers.");
            return;
        }
        VendorUIReference.SetActive(true);
    }
    public void CloseVendorMenu()
    {//click back button in shop
        if (StaticDataHolder.livingEnemyParty.Count > 0)
        {
            //this shouldn't happen
            StaticDataHolder.MainLoop.EventLoggingComponent.Log("As you deviate your attention from the merchant's wares, you realize that you had been followed...");
            return;
        }
        VendorUIReference.SetActive(false);
    }





    public Merchant GenerateMerchantInventory()
    {
        Merchant jimmy = new Merchant();


        return jimmy;
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
            StaticDataHolder.MainLoop.EventLoggingComponent.Log("You've stumbled across a merchant.");
            StartCoroutine(ArrivalAnimation());

        }
        else
        {//we make it leave
            StaticDataHolder.MainLoop.EventLoggingComponent.LogGray("The merchant silently watches you depart.");
            StartCoroutine(LeavingAnimation());
        }
        
    }

}
