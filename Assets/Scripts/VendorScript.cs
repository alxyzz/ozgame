using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static EntityDefiner;
using static LevelHelper;

public class VendorScript : MonoBehaviour
{


    [Space(5)]
    [Tooltip("These are used for the trader's cart movement.")]
    public GameObject Destination;//this is where it's supposed to go right before we interact with it
    public GameObject Startination;//this is where it spawns BEFORE it comes onscreen
    public GameObject Cart;//the cart itself. opens up the menu when clicked
    public SpriteRenderer CartImage;//the cart itself. opens up the menu when clicked
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
    public TextMeshProUGUI CurrentMoneyDisplay;
    //public GameObject SellBuyButton;
    public Text SellBuyButtonText;
    public GameObject VendorUIReference;
    //public GameObject VendorUIItemContainer;//this is where we spawn prefabs that allow us to select then buy the item in that slot
    //public int itemAmount; //S
    [Space(10)]
    [Tooltip("Prefab used for trader items. allows for buying when clicking the Buy button.")]
    public GameObject VendorUIItemEntryPrefab;

    [HideInInspector]
    public VendorItemScript currentlySelectedShopItem;
    [Space(5)]
    [HideInInspector]
    public bool isVendorHere;
    private bool cartMoving = false; //never mess with the cart while it is in motion, lest it tip over.
    [Space(5)]
    public TMPro.TextMeshProUGUI VendorItemName;
    public TMPro.TextMeshProUGUI VendorItemDesc;
    public TMPro.TextMeshProUGUI VendorItemQuote;
    public TMPro.TextMeshProUGUI VendorItemPrice;
    public Image itemImage;


    private Sprite[] idleAnimation;
    private Sprite[] mouseOverAnimation;
    public bool isAnimating = false;

    public int difficultyCurrency;

    public void SetupGraphics()
    {
        CartImage.sprite = idleAnimation[0];
        isAnimating = false;

    }
    public void LoadSpriteSheets()
    {
        idleAnimation = Resources.LoadAll<Sprite>("merchant_idle");
        mouseOverAnimation = Resources.LoadAll<Sprite>("merchant_mouseover");
    }
    public void MouseEnterCart()
    {
        if (cartMoving)
        {
            return;
        }
        isAnimating = false;
        StopAllCoroutines();
        StartCoroutine(mouseOverAnimate());
    }
    public void MouseExitCart()
    {
        if (cartMoving)
        {
            return;
        }
        StopAllCoroutines();
        StartCoroutine(IdleAnimation());

    }
    IEnumerator IdleAnimation()
    {

        for (int i = 0; i < idleAnimation.Length - 1; i++)
        {
            CartImage.sprite = idleAnimation[i];
            yield return new WaitForSecondsRealtime(0.06f);
        }
        StartCoroutine(IdleAnimation());

    }
    IEnumerator mouseOverAnimate()
    {
        for (int i = 0; i < 240 / 0.06; i++) //4 minutes is a good max animation time
        {
            for (int b = 0; b < mouseOverAnimation.Length - 1; b++)
            {
                CartImage.sprite = mouseOverAnimation[b];
                yield return new WaitForSecondsRealtime(0.06f);
            }
        }

        //we stop animating when mouse leaves the sprite of the trader

    }



    public void RefreshText()
    {
        if (currentlySelectedShopItem == null)
        {
            VendorItemDesc.text = "";
            VendorItemName.text = "";
            VendorItemQuote.text = "";
            VendorItemPrice.text = "";
            itemImage.sprite = MainData.MainLoop.UserInterfaceHelperComponent.transparency;
            return;
        }


        VendorItemDesc.text = currentlySelectedShopItem.associatedItem.description;
        VendorItemName.text = currentlySelectedShopItem.associatedItem.itemName;
        VendorItemQuote.text = currentlySelectedShopItem.associatedItem.itemBlurb;
        VendorItemPrice.text = currentlySelectedShopItem.associatedItem.value.ToString();
        itemImage.sprite = currentlySelectedShopItem.associatedItem.itemSprite;
    }


    //for when cart is arriving
    IEnumerator ArrivalAnimation()
    {
        cartMoving = true;
        MainData.MainLoop.LevelHelperComponent.MovePartyForwards(); //ATTENTION - IF THIS SOMEHOW MAKES IT MOVE THE OTHER WAY - THE ORDER IS FLIPPED IN LEVELmANAGER. JUST USE MOVEBACKWARDS.
        isVendorHere = true;
        while (Vector3.Distance(Cart.transform.position, Destination.transform.position) > 0.1f)
        {
            Cart.transform.position = Vector3.MoveTowards(Cart.transform.position, Destination.transform.position, speed * Time.deltaTime);
            yield return new WaitForSecondsRealtime(moveInterval);
        }



        Cart.transform.position = Destination.transform.position;
        MainData.MainLoop.LevelHelperComponent.MoveStop();
        cartMoving = false;
        StartCoroutine(IdleAnimation());
    }




    //for when the cart is leaving
    IEnumerator LeavingAnimation()
    {
        isAnimating = true;
        cartMoving = true;
        MainData.MainLoop.LevelHelperComponent.MovePartyForwards();

        while (Vector3.Distance(Cart.transform.position, GoodbyeDestination.transform.position) > 0.2f)
        {
            Cart.transform.position = Vector3.MoveTowards(Cart.transform.position, GoodbyeDestination.transform.position, speed * Time.deltaTime);
            yield return new WaitForSecondsRealtime(moveInterval);
        }

        Cart.transform.position = Startination.transform.position; //we send it back to beginning
        MainData.MainLoop.LevelHelperComponent.MoveStop();
        isVendorHere = false;
        cartMoving = false;
        MainData.MainLoop.LevelHelperComponent.distanceWalked = 0;
        MainData.MainLoop.LevelHelperComponent.EncountersPaused = false;

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
            MainData.MainLoop.EventLoggingComponent.Log("As you look away from the merchant's wares, you realize that you had been followed...");
            return;
        }
        VendorUIReference.SetActive(false);
    }
















    private void GenerateNewStock()
    {
        stock.Clear();
        stock.Add(MainData.MainLoop.EntityDefComponent.FetchConsumable("health_potion"));//copy of health_potion
        stock.Add(MainData.MainLoop.EntityDefComponent.FetchEquipment()); //copy of random equipment
        stock.Add(MainData.MainLoop.EntityDefComponent.FetchEquipment()); //copy of random equipment
        stock.Add(MainData.MainLoop.EntityDefComponent.FetchEquipment()); //copy of random equipment
        stock.Add(MainData.MainLoop.EntityDefComponent.FetchEquipment()); //copy of random equipment
        stock.Add(MainData.MainLoop.EntityDefComponent.FetchEquipment()); //copy of random equipment
    }

    List<Item> stock = new List<Item>();
    public List<VendorItemScript> vendorItems = new List<VendorItemScript>();


    public GameObject TradeObjectContainer; //holds all the vendoritem prefabs so we can scroll through em
    public void InitializeShopItems()
    {
        //first we clean the old stock
        CurrentMoneyDisplay.text = MainData.MainLoop.Currency.ToString();



        GenerateNewStock(); //something like a few potions and an item or two

        InitFirstTime();



    }


    private void InitFirstTime()
    {
        //clear all previous items if any
        CurrentMoneyDisplay.text = MainData.MainLoop.Currency.ToString();
        if (vendorItems.Count > 0)
        {
            foreach (VendorItemScript item in vendorItems)
            {
                item.gameObject.SetActive(false);
                Destroy(item.gameObject);
            }
        }
        vendorItems.Clear();
        //
        //now we create new items for every entry in stock

        foreach (Item item in stock)
        {
            Debug.Log("GameObject b = Instantiate(VendorUIItemEntryPrefab, TradeObjectContainer.transform);");
            GameObject b = Instantiate(VendorUIItemEntryPrefab, TradeObjectContainer.transform);
            b.transform.SetParent(TradeObjectContainer.transform);
            b.transform.position = b.transform.parent.transform.position;
            VendorItemScript i = b.GetComponent<VendorItemScript>();
            i.SetItem(item.identifier, item.isEquipable);
            i.RefreshItemData();
            vendorItems.Add(i);
            //item is created and set up at this point, only needs positioning
        }
        RefreshShopItems();
    }


    private void RefreshShopItems()
    {
        CurrentMoneyDisplay.text = MainData.MainLoop.Currency.ToString();
        Vector3 change = vendorItems[0].transform.position;
        foreach (VendorItemScript child in vendorItems)
        {
            List<Item> results = stock.FindAll(x => x == child.associatedItem);
            child.transform.position = change;
            change = new Vector3(child.transform.position.x, change.y - 155.2f, child.transform.position.z);
            child.RefreshItemData();
        }//now the item is properly positioned too in the scrollview. ready for player clicking
         //to remove all items just go foreach item in vendorItems and destroy the .gameObject









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

            MainData.MainLoop.LevelHelperComponent.SetupRegularEnvironment();

            MainData.MainLoop.EventLoggingComponent.Log("You've stumbled across a merchant.");
            StartCoroutine(ArrivalAnimation());

            return;
        }
        else if (order == false)
        {
            MainData.MainLoop.EventLoggingComponent.LogGray("The merchant silently watches you depart.");
            StartCoroutine(LeavingAnimation());
            MainData.MainLoop.LevelHelperComponent.EncountersPaused = false;
            MainData.MainLoop.LevelHelperComponent.distanceWalked = -60;//back to beginning.
            return;
        }

        if (cartMoving)
        {
            return;
        }
        if (!isVendorHere)
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









    /// <summary>
    /// returns false if not enough money, true if all ok. buys the item and puts it in your inventory.
    /// </summary>
    /// <returns></returns>
    private bool CheckSufficientMoney()
    {
        if (MainData.MainLoop.Currency < currentlySelectedShopItem.associatedItem.value)
        {
            //play fail sound
            Debug.Log("not enough money");
            return false;
        }
        return true;
    }

    public GameObject TraderInventoryScrollRect;





    /// <summary>
    /// runs when clicking the Buy button. player has to click an item before.
    /// </summary>
    public void ClickBuy()
    {
        Debug.LogWarning("clicked the buy button");
        if (currentlySelectedShopItem == null)
        {
            return;
        }

        if (CheckSufficientMoney())
        {
            Debug.LogWarning("Player has sufficient money for " + currentlySelectedShopItem.associatedItem.itemName);
            Debug.LogWarning("Selling " + currentlySelectedShopItem.associatedItem.itemName + " to player");
            MainData.MainLoop.Currency -= currentlySelectedShopItem.associatedItem.value;
            Item bought = currentlySelectedShopItem.associatedItem;
            if (bought.isEquipable)
            {
                if (MainData.equipmentInventory.Count >= 30)
                {
                    return;
                }
                MainData.equipmentInventory.Add(bought);
                stock.Remove(bought);

            }
            else
            {
                if (MainData.consumableInventory.Count == 3)
                {
                    return;
                }
                List<Item> results = MainData.consumableInventory.FindAll(x => x.identifier == bought.identifier);
                if (results.Count == 1)
                {
                    results[0].itemQuantity++;
                }
                else
                {
                    MainData.consumableInventory.Add(bought);
                }
                MainData.MainLoop.UserInterfaceHelperComponent.RefreshConsumableSlots();
            }
            bought.amtInStock--;
            currentlySelectedShopItem.itemQuantity.text = bought.amtInStock.ToString();
            //Debug.LogError(bought.itemName + " in stock after purchase - " + bought.amtInStock);
            if (bought.amtInStock == 0)
            {
                //Debug.LogError("reached here");
                currentlySelectedShopItem.gameObject.SetActive(false);
            }
            RefreshShopItems();
            MainData.MainLoop.UserInterfaceHelperComponent.UpdateCurrencyCounter();
        }



        //play transaction bing sound

    }



}
