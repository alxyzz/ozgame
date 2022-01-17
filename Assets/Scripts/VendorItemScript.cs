using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static EntityDefiner;

public class VendorItemScript : MonoBehaviour
{
    [Header("We will use the same object for both player and vendor's stuff.")]
    [Header("A prefab for each boolean state.")]
    [Space(5)]
    public Image itemImage;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;
    public TextMeshProUGUI itemPrice;
    public TextMeshProUGUI itemQuantity;
    public Button itemBackground;//we might tint this based on rarity?
    public Item associatedItem;
    [HideInInspector]
    public bool isInVendor = true;
    public void RefreshItemData()
    {
        if (associatedItem == null)
        {
            return;
        }
        itemImage.sprite = associatedItem.itemSprite;
        itemName.text = associatedItem.itemName;
        itemDescription.text = associatedItem.description;
        //if (associatedItem.value != 0)
        //{
        //    itemQuantity.text = associatedItem.value.ToString();
        //}
        //else
        //{
        //    itemQuantity.text = "";
        //}
        
        //var colors = itemBackground.colors;
        //switch (associatedItem.rarity)
        //{
        //    case "common":
        //        colors.normalColor = new Vector4(100, 100, 100, 255);
        //        break;
        //    case "uncommon":
        //        colors.normalColor = new Vector4(100, 100, 100, 255);
        //        break;
        //    case "rare":
        //        colors.normalColor = new Vector4(100, 100, 100, 255);
        //        break;
        //    case "masterwork":
        //        colors.normalColor = new Vector4(100, 100, 100, 255);
        //        break;
        //    case "legendary":
        //        colors.normalColor = new Vector4(100, 100, 100, 255);
        //        break;


        //    default:
        //        colors.normalColor = new Vector4(100, 100, 100, 255);
        //        break;
        //}
        //itemBackground.colors = colors;
    }


    /// <summary>
    /// this method sets the item associated.
    /// </summary>
    public void SetItem(string item, bool consumable)
    {

        if (consumable)
        {
            associatedItem = MainData.allConsumables[item];

        }
        else
        {
            associatedItem = MainData.allEquipment[item];
        }

        RefreshItemData();
    }




    public void ClickedThis()
    {


        if (isInVendor)
        {
            MainData.MainLoop.VendorScriptComponent.RefreshTransactionButtonText(false); //Buying, from player's perspective.
        }
        else
        {
            MainData.MainLoop.VendorScriptComponent.RefreshTransactionButtonText(true);//selling, from player's perspective
        }
        Debug.Log("clicktext vendorbutton");
        MainData.MainLoop.VendorScriptComponent.currentlySelectedShopItem = this;
        MainData.MainLoop.VendorScriptComponent.RefreshText();


    }

}
