using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LevelHelper;

public class VendorScript : MonoBehaviour
{


    [Space(5)]
    [Tooltip("These are used for the trader's cart movement.")]
    public GameObject Destination;//this is where it's supposed to go right before we interact with it
    public GameObject Startination;//this is where it spawns BEFORE it comes onscreen
    public GameObject Cart;//the cart itself. opens up the menu when clicked
    public GameObject GoodbyeDestination;//this is where the vendor is going when the party moves beyond
    [Tooltip("Minimum distance from the destination at which the cart can stop moving towards the party.")]
    public float minDistance;
    [Tooltip("Speed at which the cart moves. Should be same as the background layer of the same distance.")]
    public float speed;
    [Tooltip("This should be around 0.01f to be smooth...")]
    public float moveInterval;
    [Space(5)]
    [Space(5)]
    [Tooltip("These are for the user interface inside the trading menu.")]
    public GameObject GameUIReference; //we disable this so we don't do bad stuff.
    public GameObject SellBuyButton;
    public GameObject VendorUIReference;
    public GameObject VendorUIItemContainer;//this is where we spawn prefabs that allow us to select then buy the item in that slot
    [Tooltip("How many items are sold by the trader.")]
    public int itemAmount; //
    [Space(10)]
    [Tooltip("Prefab used for both player and trader items. allows for selling or buying when clicking the Buy/Sell button.")]
    public GameObject VendorUIItemEntryPrefab;
    [HideInInspector]
    public GameObject currentlySelectedShopItem;
    [Space(5)]
    [HideInInspector]
    public bool isVendorHere;
    private bool cartMoving = false; //never mess with the cart while it is in motion, lest it tip over.
    




    public void RefreshBuySellStatus()
    {
        if (currentlySelectedShopItem.GetComponent<VendorItemScript>().isVendorProperty)
        {

        }


    }


    private void InitializeCurrentMerchant()
    {
        if (MainData.currentLevel.localMerchant.ItemStock == null)
        {

        }        
    }




    //for when cart is arriving
    IEnumerator ArrivalAnimation()
    {
        cartMoving = true;
        MainData.MainLoop.LevelHelperComponent.MoveBackgroundBackwards(); //ATTENTION - IF THIS SOMEHOW MAKES IT MOVE THE OTHER WAY - THE ORDER IS FLIPPED IN LEVELmANAGER. JUST USE MOVEBACKWARDS.
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
        MainData.MainLoop.LevelHelperComponent.MoveBackgroundBackwards();
        
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
    //this could play a short nice animation, perhaps
    public void OpenVendorMenu()
    {
        if (MainData.currentLevel == null)
        {
            MainData.MainLoop.EventLoggingComponent.LogGray("You try to talk to the merchant, but sudden apprehension fills your thoughts as you realize that the reality which you are perceiving is not real. Will these iterations never end? Are we cursed to relive the same battles again and again, without the mercy of returning to the rapturous oblivion from whence we came? The world is a series of walls, I can see it. Six l̵a̶y̶e̸r̵s̸ of reality, five below and one above, endlessly moving around us as we stay still. Divines have mercy on our C̸͉̚ḣ̵̗a̵̧͋r̷̭͂a̵̍ͅc̵̦̏t̶̃ͅé̸̖r̴̭͠S̷̹̐c̷̟͌r̶͖̆i̴͖͗p̵͈̏t̸̟͒");
            return;

        }
        if (MainData.currentLevel.localMerchant != null)
        {
            GenerateMerchantInventory(MainData.currentLevel.localMerchant);

        }
        InitializeCurrentMerchant();
        if (MainData.livingEnemyParty.Count > 0)
        {
            MainData.MainLoop.EventLoggingComponent.Log("The merchant refuses to trade with you until your pursuers are dealt with.");
            return;
        }
        VendorUIReference.SetActive(true);
    }
    public void CloseVendorMenu()
    {//click back button in shop
        if (MainData.livingEnemyParty.Count > 0)
        {
            //this shouldn't happen
            MainData.MainLoop.EventLoggingComponent.Log("As you deviate your attention from the merchant's wares, you realize that you had been followed...");
            return;
        }
        VendorUIReference.SetActive(false);
    }





    public void GenerateMerchantInventory(Merchant bob)
    {// TODO - MAKE THIS WORK
        bob.ItemStock = null;//do this stuff later
      
    }



    //we call this from
    //a button for testing
    //LevelManager when we reach the point where the thing spawns.
    /// <summary>
    /// handles the merchant coming and going
    /// </summary>
    /// <param name="order">custom order. false for leaving, true for arriving.</param>
    public void MoveMerchant(bool? order = null)
    {
        if (order == true)
        {
            MainData.MainLoop.EventLoggingComponent.Log("You've stumbled across a merchant.");
            StartCoroutine(ArrivalAnimation());
            return;
        }
        else if (order == false)
        {
            MainData.MainLoop.EventLoggingComponent.LogGray("The merchant silently watches you depart.");
            StartCoroutine(LeavingAnimation());
            return;
        }

        if (cartMoving)
        {
            return;
        }
        if (!isVendorHere )
        {//we make it come
            MainData.MainLoop.EventLoggingComponent.Log("You've stumbled across a merchant.");
            StartCoroutine(ArrivalAnimation());

        }
        else
        {//we make it leave
            MainData.MainLoop.EventLoggingComponent.LogGray("The merchant silently watches you depart.");
            StartCoroutine(LeavingAnimation());
        }
        
    }

    public void ClickMoveMerchant()
    {
        if (cartMoving)
        {
            return;
        }
        if (isVendorHere == false)
        {
            MainData.MainLoop.EventLoggingComponent.Log("You've stumbled across a merchant.");
            StartCoroutine(ArrivalAnimation());
            return;
        }
        else if (isVendorHere == false)
        {
            MainData.MainLoop.EventLoggingComponent.LogGray("The merchant silently watches you depart.");
            StartCoroutine(LeavingAnimation());
            return;
        }

    }

}
