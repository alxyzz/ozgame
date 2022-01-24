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
    public void RefreshItemData()
    {
        if (associatedItem == null)
        {
            return;
        }
        itemImage.sprite = associatedItem.itemSprite;
        itemName.text = associatedItem.itemName;
        itemDescription.text = associatedItem.description;

        if (associatedItem.amtInStock == 1)
        {
            itemQuantity.text = "";
        }
        else
        {
            itemQuantity.text = associatedItem.amtInStock.ToString();
        }
        Debug.LogWarning("Refresh Item Data at VendorItemScript - itemQuantity.text = associatedItem.amtInStock.ToString(); - amt in stock is " + associatedItem.amtInStock.ToString());
        if (associatedItem.value != 0)
        {
            itemPrice.text = associatedItem.value.ToString();
        }
        else
        {
            itemPrice.text = "FREE!";
        }

        var colors = itemBackground.colors;
        switch (associatedItem.rarity)
        
        {
            case "common":
                colors.normalColor = MainData.MainLoop.TweakingComponent.CommonColor;
                break;
            case "uncommon":
                colors.normalColor = MainData.MainLoop.TweakingComponent.UncommonColor;
                break;
            case "rare":
                colors.normalColor = MainData.MainLoop.TweakingComponent.RareColor;
                break;
            case "masterwork":
                colors.normalColor = MainData.MainLoop.TweakingComponent.MasterworkColor;
                break;
            default:
                colors.normalColor = MainData.MainLoop.TweakingComponent.CommonColor;
                break;
        }
        itemBackground.colors = colors;
    }


    /// <summary>
    /// this method sets the item associated to a copy of an item from the respective dictionary. this exact copy will be given to the player's inventory if bought.
    /// </summary>
    public void SetItem(string item, bool equipment)
    {

        if (equipment)
        {

            Item b = new Item(MainData.allEquipment[item].identifier,
                          MainData.allEquipment[item].description,
                          MainData.allEquipment[item].itemBlurb,
                          MainData.allEquipment[item].itemName,
                          MainData.allEquipment[item].itemSprite,
                          MainData.allEquipment[item].rarity,
                          MainData.allEquipment[item].value,
                          MainData.allEquipment[item].amtInStock,
                          MainData.allEquipment[item].itemQuantity,
                          MainData.allEquipment[item].beneficial,
                          MainData.allEquipment[item].isEquipable,
                          MainData.allEquipment[item].speedmodifier,
                          MainData.allEquipment[item].healthmodifier,
                          MainData.allEquipment[item].manamodifier,
                          MainData.allEquipment[item].dmgmodifier,
                          MainData.allEquipment[item].defensemodifier,
                          MainData.allEquipment[item].luckmodifier,
                          MainData.allEquipment[item].healingAmp,
                          MainData.allEquipment[item].DamageResistancePercentage,
                          MainData.allEquipment[item].DamageBonusPercentage,
                          MainData.allEquipment[item].discountPercentage,
                          MainData.allEquipment[item].Lifesteal);


            associatedItem = b;

        }
        else
        {
            Item b = new Item(MainData.allConsumables[item].identifier,
                          MainData.allConsumables[item].description,
                          MainData.allConsumables[item].itemBlurb,
                          MainData.allConsumables[item].itemName,
                          MainData.allConsumables[item].itemSprite,
                          MainData.allConsumables[item].rarity,
                          MainData.allConsumables[item].value,
                          MainData.allConsumables[item].amtInStock,
                          MainData.allConsumables[item].itemQuantity,
                          MainData.allConsumables[item].beneficial,
                          MainData.allConsumables[item].isEquipable,
                          MainData.allConsumables[item].speedmodifier,
                          MainData.allConsumables[item].healthmodifier,
                          MainData.allConsumables[item].manamodifier,
                          MainData.allConsumables[item].dmgmodifier,
                          MainData.allConsumables[item].defensemodifier,
                          MainData.allConsumables[item].luckmodifier,
                          MainData.allConsumables[item].healingAmp,
                          MainData.allConsumables[item].DamageResistancePercentage,
                          MainData.allConsumables[item].DamageBonusPercentage,
                          MainData.allConsumables[item].discountPercentage,
                          MainData.allConsumables[item].Lifesteal);


            associatedItem = b;

        }

        RefreshItemData();
    }



    /// <summary>
    /// this only selects the item as the one you will buy.
    /// </summary>
    public void ClickedThis()
    {
        Debug.Log("Clicked a vendor item.");
        MainData.MainLoop.VendorScriptComponent.currentlySelectedShopItem = this;

        MainData.MainLoop.VendorScriptComponent.RefreshText();
    }

}
