using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static EntityDefiner;

public class TrinketMenuHandler : MonoBehaviour
{
    private TextMeshProUGUI currentCharacterStatistics;

    public List<InventoryBagObject> BagSlots = new List<InventoryBagObject>(); //this is where we put all the items that we can equip
    public List<InventoryEqObject> EquippedSlots = new List<InventoryEqObject>(); //this is where we put all the items that we can equip

    //selected item
    public Item currentlySelectedItem;
    public TextMeshProUGUI ItemDesc;
    public TextMeshProUGUI ItemName;
    public TextMeshProUGUI ItemValue;
    public TextMeshProUGUI ItemRarity;
    public TextMeshProUGUI ItemBonuses;//long string detailing the bonuses of an item, if any


    //character

    public Image CharacterImage;
    public TextMeshProUGUI CharName;


    public float clickCooldown;
    public bool coolDown = false;

    IEnumerator coolDownClick()
    {
        
        yield return new WaitForSecondsRealtime(clickCooldown);
        coolDown = false;
    }

    public void ClickedSlot(InventoryEqObject CharItem = null, InventoryBagObject BagItem = null)
    {
        coolDown = true;
        StartCoroutine(coolDownClick());

        if (CharItem != null)
        {//we clicked an item that is equipped
            if (CharItem.associatedItem == null)
            {
                return;
            }
            
            //we take the item from the character's inventory, and move it to the main inventory
            MainData.MainLoop.UserInterfaceHelperComponent.TrinketScreenCharacter.equippedItems.Remove(CharItem.associatedItem);
            MainData.equipmentInventory.Add(CharItem.associatedItem);
            currentlySelectedItem = CharItem.associatedItem;
            CharItem.associatedItem = null;
        }
        else

        if (BagItem != null)
        {//we clicked an item in the main inventory
            if (BagItem.associatedItem == null)
            {
                return;
            }

            //we take the item from the main inventory, and move it to the character's inventory
            MainData.MainLoop.UserInterfaceHelperComponent.TrinketScreenCharacter.equippedItems.Add(BagItem.associatedItem);
            MainData.equipmentInventory.Remove(BagItem.associatedItem);
            currentlySelectedItem = BagItem.associatedItem;
            BagItem.associatedItem = null;

        }

        RefreshInventory();

    }




    public void RefreshInventory(Character currChar = null, Item clickedItem = null)
    {
        Debug.Log("Refreshed inventory visuals.");
        
        if (currChar != null)
        {//just so we can use it just to refresh without having to provide a character
            MainData.MainLoop.UserInterfaceHelperComponent.TrinketScreenCharacter = currChar;
        }
        if (clickedItem != null)
        {//just so we can use it just to refresh without having to provide an item
            currentlySelectedItem = clickedItem;
        }




        PopulateItemSlots(); Debug.Log("refreshing items slots in inventory");
        RefreshCharacterDescription(); Debug.Log("refreshing char description in inventory");
        RefreshItemDescription(); Debug.Log("refreshing item description in inventory");
    }


    private void PopulateItemSlots()
    {//this might be a bit laggy 

        for (int i = 0; i < BagSlots.Count - 1; i++)
        {
            BagSlots[i].selfImage.sprite = null; //cleans them all first
        }

        for (int i = 0; i < EquippedSlots.Count - 1; i++)
        {
            EquippedSlots[i].selfImage.sprite = null; //cleans them all first
        }


        for (int i = 0; i < MainData.equipmentInventory.Count - 1; i++)
        {//first we assign the equipment inventory stuff
            if (MainData.equipmentInventory[i] != null)
            {
                BagSlots[i].associatedItem = MainData.equipmentInventory[i];
                Debug.Log("changed backpack slot image");
                BagSlots[i].selfImage.sprite = BagSlots[i].associatedItem.itemSprite;
            }

        }

        if (MainData.MainLoop.UserInterfaceHelperComponent.TrinketScreenCharacter != null)
        {
            for (int b = 0; b < MainData.MainLoop.UserInterfaceHelperComponent.TrinketScreenCharacter.equippedItems.Count; b++)
            {//then the ones in inventory, unequipped
                if (MainData.MainLoop.UserInterfaceHelperComponent.TrinketScreenCharacter.equippedItems[b] != null)
                {
                    Debug.Log("populated equipped slot " + b);
                    EquippedSlots[b].associatedItem = MainData.MainLoop.UserInterfaceHelperComponent.TrinketScreenCharacter.equippedItems[b];
                    EquippedSlots[b].selfImage.sprite = MainData.MainLoop.UserInterfaceHelperComponent.TrinketScreenCharacter.equippedItems[b].itemSprite;
                }

            }
        }



    }



    private void RefreshCharacterDescription()
    {
        if (MainData.MainLoop.UserInterfaceHelperComponent.TrinketScreenCharacter == null)
        {
            return;
        }
       // CharacterImage.sprite = MainData.MainLoop.UserInterfaceHelperComponent.TrinketScreenCharacter.standingSprite;
        CharName.text = MainData.MainLoop.UserInterfaceHelperComponent.TrinketScreenCharacter.charName;
        StartCoroutine(AnimateTrinketChar());



    }

    private int spriteIndex = 0;
    IEnumerator AnimateTrinketChar()
    {

        CharacterImage.sprite = MainData.MainLoop.UserInterfaceHelperComponent.TrinketScreenCharacter.idleSprite[spriteIndex];
        if (spriteIndex == MainData.MainLoop.UserInterfaceHelperComponent.TrinketScreenCharacter.idleSprite.Length-1)
        {
            spriteIndex = 0;
        }
        spriteIndex++;
        yield return new WaitForSecondsRealtime(0.1f);
        StartCoroutine(AnimateTrinketChar());

    }

    private void RefreshItemDescription()
    {

        if (currentlySelectedItem == null)
        {

            ItemDesc.text = "";
            ItemName.text = "";
            ItemRarity.text = "";
            ItemValue.text = "";
            ItemBonuses.text = "";
            return;
        }


        ItemDesc.text = currentlySelectedItem.description;
        ItemName.text = currentlySelectedItem.itemName;
        ItemRarity.text = currentlySelectedItem.rarity;
        ItemValue.text = currentlySelectedItem.value.ToString();

        string Bonuses = "";




        if (currentlySelectedItem.speedmodifier != 0)
            Bonuses += currentlySelectedItem.speedmodifier + " Speed \b";








        //if (currentlySelectedItem.healthmodifier != 0)

        //   Bonuses +=  currentlySelectedItem.healthmodifier > 0 ? 12 : null;










        if (currentlySelectedItem.manamodifier != 0)
            Bonuses += currentlySelectedItem.manamodifier + " mana \b";

        if (currentlySelectedItem.dmgmodifier != 0)
            Bonuses += currentlySelectedItem.dmgmodifier + " dmg \b";

        if (currentlySelectedItem.defensemodifier != 0)
            Bonuses += currentlySelectedItem.defensemodifier + " Defense \b";

        if (currentlySelectedItem.luckmodifier != 0)
            Bonuses += currentlySelectedItem.luckmodifier + " Luck \b";

        if (currentlySelectedItem.healingAmp != 0)
            Bonuses += currentlySelectedItem.healingAmp + " Healing Amplification \b";

        if (currentlySelectedItem.DamageResistancePercentage != 0)
            Bonuses += currentlySelectedItem.DamageResistancePercentage + " Damage Resistance \b";

        if (currentlySelectedItem.DamageBonusPercentage != 0)
            Bonuses += currentlySelectedItem.DamageBonusPercentage + " Damage Amplification \b";

        if (currentlySelectedItem.discountPercentage != 0)
            Bonuses += currentlySelectedItem.discountPercentage + " Discount \b";

        if (currentlySelectedItem.Lifesteal != 0)
            Bonuses += currentlySelectedItem.Lifesteal + " Lifesteal \b";

        ItemBonuses.text = Bonuses;


        switch (currentlySelectedItem.rarity)
        {

            case "common":
                ItemRarity.color = new Color(0.7f, 0.7f, 0.7f);
                break;
            case "uncommon":
                ItemRarity.color = new Color(1f, 1f, 1f);
                break;
            case "rare":
                ItemRarity.color = new Color(1f, 0.5f, 0.5f);
                break;
            case "historic":
                ItemRarity.color = new Color(0.5f, 0.5f, 1f);
                break;

            default:
                ItemRarity.color = new Color(1f, 1f, 1f);
                break;
        }


    }




}
