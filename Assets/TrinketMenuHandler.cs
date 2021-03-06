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
    private int itemsInBag = 0;
    private int itemsInInv = 0;
    public List<InventoryEqObject> EquippedSlots = new List<InventoryEqObject>(); //this is where we put all the items that we can equip
    //selected item
    public Item currentlySelectedItem;
    public TextMeshProUGUI ItemDesc;
    public TextMeshProUGUI ItemName;
    public TextMeshProUGUI ItemValue;
    public TextMeshProUGUI ItemRarity;
    public TextMeshProUGUI ItemBonuses;//long string detailing the bonuses of an item, if any
    //character switching
    public Sprite[] CharSelMenuBackground; //we will change the UI based on which character is selected. these hold the sprites for it.
    public Image BackgroundImage; // the image we will change the sprite of with the ones above.
    public Image firstHeroButton, secondHeroButton, thirdHeroButton, fourthHeroButton; //buttons for switching characters



    //character
    private int spriteIndex = 0;
    public GameObject PopUpCharInfo;
    public TextMeshProUGUI statisticsText;
    public Image CharacterImage;
    public TextMeshProUGUI CharName;
    public float clickCooldown;
    public bool coolDown = false;





    public void ClickedHeroAvatar(int index) //0,1,2,3
    {
        BackgroundImage.sprite = CharSelMenuBackground[index];
        RefreshInventory(MainData.livingPlayerParty[index]);
        //play character turn sound here
    }



    public void RefreshHeroAvatars()
    {
        firstHeroButton.sprite = MainData.livingPlayerParty[0].charAvatar;
        secondHeroButton.sprite = MainData.livingPlayerParty[1].charAvatar;
        thirdHeroButton.sprite = MainData.livingPlayerParty[2].charAvatar;
        fourthHeroButton.sprite = MainData.livingPlayerParty[3].charAvatar;
    }



    IEnumerator CoolDownClick()
    {

        yield return new WaitForSecondsRealtime(clickCooldown);
        coolDown = false;
    }
    public void ClickedSlot(InventoryEqObject CharItem = null, InventoryBagObject BagItem = null)
    {
        coolDown = true;
        StartCoroutine(CoolDownClick());

        if (CharItem != null)
        {//we clicked an item that is equipped
            if (CharItem.associatedItem == null)
            {
                Debug.Log("Clicked inventory slot has no associated item.");
                return;
            }

            if (itemsInBag == BagSlots.Count)
            {//max items
                return;
            }

            if (CharItem.associatedItem.healthmodifier != 0)
            {
                if ((MainData.MainLoop.UserInterfaceHelperComponent.TrinketScreenCharacter.maxHealth - CharItem.associatedItem.healthmodifier) < 1)
                {
                    Debug.LogError("tried to take off an item that would result in dying. Cancelled.");
                    return; // don't allow people to put on items/take them off if somehow it will kill them. 
                }

                if ((MainData.MainLoop.UserInterfaceHelperComponent.TrinketScreenCharacter.currentHealth - CharItem.associatedItem.healthmodifier) < 1)
                {
                    Debug.LogError("tried to take off an item that would result in dying. Cancelled.");
                    return; // don't allow people to put on items/take them off if somehow it will kill them. 
                }
            }
            //we take the item from the character's inventory, and move it to the main inventory
            MainData.MainLoop.UserInterfaceHelperComponent.TrinketScreenCharacter.equippedItems.Remove(CharItem.associatedItem);
            if (CharItem.associatedItem.healthmodifier != 0) //if it messes with health, either malus or bonus
            {
                MainData.MainLoop.UserInterfaceHelperComponent.TrinketScreenCharacter.recentlyUnequippedItemsHP.Add(CharItem.associatedItem); //this tracks HP removal from item unequipping
            }
            Debug.Log("Removed from player inventory - " + CharItem.associatedItem.itemName);
            MainData.equipmentInventory.Add(CharItem.associatedItem);
            currentlySelectedItem = CharItem.associatedItem;
            CharItem.associatedItem = null;
        }
        else

        if (BagItem != null)
        {//we clicked an item in the main inventory
            if (BagItem.associatedItem == null)
            {
                Debug.LogWarning("Clicked inventory slot has no associated item.");
                return;
            }
            if (itemsInInv == 12)
            {//max items
                return;
            }


            if (BagItem.associatedItem.healthmodifier != 0)
            {
                if ((MainData.MainLoop.UserInterfaceHelperComponent.TrinketScreenCharacter.maxHealth + BagItem.associatedItem.healthmodifier) < 1)
                {
                    Debug.LogError("tried to put on an item that would result in dying. Cancelled.");
                    return; // don't allow people to put on items/take them off if somehow it will kill them. 
                }

                if ((MainData.MainLoop.UserInterfaceHelperComponent.TrinketScreenCharacter.currentHealth + BagItem.associatedItem.healthmodifier) < 1)
                {
                    Debug.LogError("tried to put on an item that would result in dying. Cancelled.");
                    return; // don't allow people to put on items/take them off if somehow it will kill them. 
                }
            }

            //we take the item from the main inventory, and move it to the character's inventory
            MainData.MainLoop.UserInterfaceHelperComponent.TrinketScreenCharacter.equippedItems.Add(BagItem.associatedItem);
            MainData.equipmentInventory.Remove(BagItem.associatedItem);
            currentlySelectedItem = BagItem.associatedItem;
            Debug.LogWarning("Added to player inventory - " + BagItem.associatedItem.itemName);
            BagItem.associatedItem = null;
            //int playerdamage = 0;
            //foreach (Item item in MainData.MainLoop.UserInterfaceHelperComponent.TrinketScreenCharacter.equippedItems)
            //{
            //    playerdamage += item.dmgmodifier;
            //}
            //Debug.LogWarning("damage is now " + playerdamage);
        }
        MainData.MainLoop.UserInterfaceHelperComponent.TrinketScreenCharacter.RecalculateStatsFromItemsOutsideCombat(); //this also clears recentlyUnequippedItems
        RefreshInventory();

    }/// <summary>
     /// refreshes all the information that can vary in the trinket screen, based on current character and current item. you can provide the second argument to show an item's description too.
     /// </summary>
     /// <param name="currChar"></param>
     /// <param name="clickedItem"></param>
    public void RefreshInventory(Character currChar = null, Item clickedItem = null)
    {
        if (currChar != null)
        {//just so we can use it just to refresh without having to provide a character
            MainData.MainLoop.UserInterfaceHelperComponent.TrinketScreenCharacter = currChar;
        }
        if (clickedItem != null)
        {//just so we can use it just to refresh without having to provide an item
            currentlySelectedItem = clickedItem;
        }
        PopulateItemSlots();
        RefreshCharName();
        RefreshItemDescription();
        RefreshCharacterStatistics();
        Debug.Log("Refreshed inventory visuals.");
    }
    private void PopulateItemSlots()
    {//this might be a bit laggy 
        itemsInBag = 0;
        itemsInInv = 0;
        for (int i = 0; i < BagSlots.Count; i++)
        {
            BagSlots[i].selfImage.sprite = null; //cleans them all first
            BagSlots[i].selfImage.color = MainData.MainLoop.TweakingComponent.GenericColor; //so its invisible
            BagSlots[i].background.color = MainData.MainLoop.TweakingComponent.GenericColor;
        }

        for (int i = 0; i < EquippedSlots.Count; i++)
        {
            EquippedSlots[i].selfImage.sprite = null; //cleans them all first
            EquippedSlots[i].selfImage.color = MainData.MainLoop.TweakingComponent.GenericColor;
            EquippedSlots[i].background.color = MainData.MainLoop.TweakingComponent.GenericColor;
        }


        for (int i = 0; i < MainData.equipmentInventory.Count; i++)
        {//first we assign the equipment inventory stuff
            if (MainData.equipmentInventory[i] != null)
            {
                BagSlots[i].associatedItem = MainData.equipmentInventory[i];
                Debug.Log("changed backpack slot image");
                BagSlots[i].selfImage.sprite = BagSlots[i].associatedItem.itemSprite;
                BagSlots[i].selfImage.color = Color.white;

                switch (BagSlots[i].associatedItem.rarity)
                {
                    case "common":
                        BagSlots[i].background.color = MainData.MainLoop.TweakingComponent.CommonColor;
                        break;
                    case "uncommon":
                        BagSlots[i].background.color = MainData.MainLoop.TweakingComponent.UncommonColor;
                        break;
                    case "rare":
                        BagSlots[i].background.color = MainData.MainLoop.TweakingComponent.RareColor;
                        break;
                    case "masterwork":
                        BagSlots[i].background.color = MainData.MainLoop.TweakingComponent.MasterworkColor;
                        break;

                    default:
                        BagSlots[i].background.color = MainData.MainLoop.TweakingComponent.GenericColor;
                        break;
                }

                itemsInBag++;
            }

        }

        if (MainData.MainLoop.UserInterfaceHelperComponent.TrinketScreenCharacter != null)
        {
            for (int b = 0; b < MainData.MainLoop.UserInterfaceHelperComponent.TrinketScreenCharacter.equippedItems.Count; b++)
            {//then the ones in inventory, unequipped
                if (MainData.MainLoop.UserInterfaceHelperComponent.TrinketScreenCharacter.equippedItems[b] != null)
                {
                    Debug.Log("populated equipped slot " + b); itemsInInv++;
                    EquippedSlots[b].associatedItem = MainData.MainLoop.UserInterfaceHelperComponent.TrinketScreenCharacter.equippedItems[b];
                    EquippedSlots[b].selfImage.sprite = MainData.MainLoop.UserInterfaceHelperComponent.TrinketScreenCharacter.equippedItems[b].itemSprite;
                    EquippedSlots[b].selfImage.color = Color.white;
                    switch (EquippedSlots[b].associatedItem.rarity)
                    {
                        case "common":
                            EquippedSlots[b].background.color = MainData.MainLoop.TweakingComponent.CommonColor;
                            break;
                        case "uncommon":
                            EquippedSlots[b].background.color = MainData.MainLoop.TweakingComponent.UncommonColor;
                            break;
                        case "rare":
                            EquippedSlots[b].background.color = MainData.MainLoop.TweakingComponent.RareColor;
                            break;
                        case "masterwork":
                            EquippedSlots[b].background.color = MainData.MainLoop.TweakingComponent.MasterworkColor;
                            break;
                        default:
                            EquippedSlots[b].background.color = MainData.MainLoop.TweakingComponent.GenericColor;
                            break;
                    }

                }

            }
        }



    }
    private void RefreshCharName()
    {

        if (MainData.MainLoop.UserInterfaceHelperComponent.TrinketScreenCharacter == null)
        {
            return;
        }
        CharName.text = MainData.MainLoop.UserInterfaceHelperComponent.TrinketScreenCharacter.charName;




    }
    public void StartAnimatingChar()
    {
        StartCoroutine(AnimateTrinketChar());
    }
    IEnumerator AnimateTrinketChar()
    {

        CharacterImage.sprite = MainData.MainLoop.UserInterfaceHelperComponent.TrinketScreenCharacter.idleSprite[spriteIndex];
        if (spriteIndex == MainData.MainLoop.UserInterfaceHelperComponent.TrinketScreenCharacter.idleSprite.Length - 1)
        {
            spriteIndex = 0;
        }
        spriteIndex++;
        yield return new WaitForSecondsRealtime(0.1f);
        StartCoroutine(AnimateTrinketChar());

    }
    public void RefreshCharacterStatistics()
    {
        Character current = MainData.MainLoop.UserInterfaceHelperComponent.TrinketScreenCharacter;
        string statistics = "";

        int speedmodifier = current.speed;
        int healthmodifier = current.maxHealth;
        int manamodifier = current.manaRegeneration;
        int dmgmodifier = (current.damageMax + current.damageMin) / 2;
        int defensemodifier = current.defense;
        int luckmodifier = current.luck;

        float multiplicativeHealingAmplification = 0;
        float multiplicativeDamageResistance = 0;
        float multiplicativeDamageBonus = 0;
        float discountPercentage = 0;
        float multiplicativeLifestealBonus = 0;


        foreach (Item item in current.equippedItems)
        {
            speedmodifier += item.speedmodifier;
            healthmodifier += item.healthmodifier;
            manamodifier += item.manamodifier;
            dmgmodifier += item.dmgmodifier;
            defensemodifier += item.defensemodifier;
            luckmodifier += item.luckmodifier;

            multiplicativeHealingAmplification += item.healingAmp;
            multiplicativeDamageResistance += item.DamageResistancePercentage;
            multiplicativeDamageBonus += item.DamageBonusPercentage;
            discountPercentage += item.discountPercentage;
            multiplicativeLifestealBonus += item.Lifesteal;
        }


        statistics += "\nSTATISTICS" + "\n";
        statistics += "Speed - " + speedmodifier + "\n";
        statistics += "Max Health - " + healthmodifier + "\n";
        statistics += "Mana - " + manamodifier + "\n";
        statistics += "Damage - " + dmgmodifier + "\n";
        statistics += "Defense - " + defensemodifier + "\n";
        statistics += "Luck - " + luckmodifier + "\n";
        statistics += "Healing Amplification - " + multiplicativeHealingAmplification + "%\n";
        statistics += "Damage Resistance - " + multiplicativeDamageResistance + "%\n";
        statistics += "Damage Amplification - " + multiplicativeDamageBonus + "%\n";
        statistics += "Discount - " + discountPercentage + "%\n";
        statistics += "Lifesteal - " + multiplicativeLifestealBonus + "%\n";





        statisticsText.text = statistics;














    }
    public void RefreshItemDescription(Item hovered = null)
    {

        //if (currentlySelectedItem == null)
        //{
        if (hovered != null)
        {
            currentlySelectedItem = hovered;
        }
        else
        {
            ItemDesc.text = "";
            ItemName.text = "";
            ItemRarity.text = "";
            ItemValue.gameObject.SetActive(false);
            ItemValue.text = "";
            ItemBonuses.text = "";
            return;
        }

        //}

        ItemValue.gameObject.SetActive(true);
        ItemDesc.text = currentlySelectedItem.description;
        ItemName.text = currentlySelectedItem.itemName;
        ItemRarity.text = currentlySelectedItem.rarity;
        ItemValue.text = currentlySelectedItem.value.ToString();

        string Bonuses = "";




        if (currentlySelectedItem.speedmodifier != 0)
            Bonuses += currentlySelectedItem.speedmodifier + " Speed \n";

        if (currentlySelectedItem.healthmodifier != 0)
            Bonuses += currentlySelectedItem.healthmodifier + "Health\n";

        if (currentlySelectedItem.manamodifier != 0)
            Bonuses += currentlySelectedItem.manamodifier + " Mana\n";

        if (currentlySelectedItem.dmgmodifier != 0)
            Bonuses += currentlySelectedItem.dmgmodifier + " Damage\n";

        if (currentlySelectedItem.defensemodifier != 0)
            Bonuses += currentlySelectedItem.defensemodifier + " Defense\n";

        if (currentlySelectedItem.luckmodifier != 0)
            Bonuses += currentlySelectedItem.luckmodifier + " Luck\n";

        if (currentlySelectedItem.healingAmp != 0)
            Bonuses += currentlySelectedItem.healingAmp + " Healing Amplification\n";

        if (currentlySelectedItem.DamageResistancePercentage != 0)
            Bonuses += currentlySelectedItem.DamageResistancePercentage + " Damage Resistance\n";

        if (currentlySelectedItem.DamageBonusPercentage != 0)
            Bonuses += currentlySelectedItem.DamageBonusPercentage + " Damage Amplification\n";

        if (currentlySelectedItem.discountPercentage != 0)
            Bonuses += currentlySelectedItem.discountPercentage + " Discount\n";

        if (currentlySelectedItem.Lifesteal != 0)
            Bonuses += currentlySelectedItem.Lifesteal + " Lifesteal\n";

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
