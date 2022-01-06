using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static EntitiesDefinition;

public class VendorItemScript : MonoBehaviour
{
    [Header("We will use the same object for both player and vendor's stuff.")]
    [Header("A prefab for each boolean state.")]
    public bool isVendorProperty;
    [Space(5)]
    public Image itemImage;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;
    public TextMeshProUGUI itemQuote;
    public TextMeshProUGUI itemPrice;
    public Button itemBackground;//we might tint this based on rarity?
    public Item associatedItem;

    public void RefreshItemData()
    {

        itemImage.sprite = associatedItem.itemSprite;
        itemName.text = associatedItem.itemName;
        itemDescription.text = associatedItem.description;


        var colors = itemBackground.colors;
        switch (associatedItem.rarity)
        {
            case "common":
                colors.normalColor = new Vector4(100,100,100,255);
                break;
            case "uncommon":
                colors.normalColor = new Vector4(100, 100, 100, 255);
                break;
            case "rare":
                colors.normalColor = new Vector4(100, 100, 100, 255);
                break;
            case "masterwork":
                colors.normalColor = new Vector4(100, 100, 100, 255);
                break;
            case "legendary":
                colors.normalColor = new Vector4(100, 100, 100, 255);
                break;


            default:
                break;
        }
        itemBackground.colors = colors;
    }


    /// <summary>
    /// this method sets the price. we are not doing it in RefreshItemData because we will use a bit of randomization based on the vendor's specific price modifier.
    /// </summary>
    public void SetPrice()
    {

    }

    public void SelectThis()
    {
        Debug.Log("clicktext vendorbutton");
        MainData.MainLoop.VendorScriptComponent.currentlySelectedShopItem = this.gameObject;


    }

}
