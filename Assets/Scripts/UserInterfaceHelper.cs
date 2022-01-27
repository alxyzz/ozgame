using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static EntityDefiner;
using static LevelHelper;
using static TraitHelper;

public class UserInterfaceHelper : MonoBehaviour
{
    [Header("references to the objects used for displaying info about current PLAYER character")]
    public Image selectedCharAvatar; //we will replace this object's image with the currently selected character's avatar
    public GameObject selectedCharName;//the name of the current player char
    public GameObject selectedCharTraitDesc;//"The Wrathful/Kind/etc", the trait based title shown after the name
    public Image selectedCharTraitIcon; //the trait's icon - the object/image where the texture is applied
    public GameObject selectedCharDescription; //the description of the character
    [Space(10)]
    [Header("references to the objects used for displaying info about currently TARGETED ENEMY character")]
    public Image selectedEnemyCharAvatar; //we will replace this object's image with the currently selected character's avatar
    public TextMeshProUGUI selectedEnemyCharName;//the name of the targeted enemy char
    public TextMeshProUGUI selectedEnemyCharEnemyType;//"The Wrathful/Kind/etc", the trait based title shown after the name
    //public GameObject selectedEnemyCharTraitIcon;//the trait's icon - the object/image where the texture is applied -- doubtful we're using it for enemies 
    public TextMeshProUGUI selectedEnemyCharDescription;//the description of the character
    [Space(10)]
    [Header("the stuff related to PLAYER party members in the lower part of the UI - the little images + name + health bar")]
    [HideInInspector]
    public CharacterWorldspaceScript PC1;
    public Image firstCharAvatar; //we will replace this object's image with the currently selected character's avatar
    public TextMeshProUGUI firstCharName;//the name of the current player char
    public TextMeshProUGUI firstCharTrait;//the name of the current player char
    public Slider firstHealthBar;//the name of the current player char
    public Slider firstManaBar;//the name of the current player char
    public GameObject firstselectionRectangle; // A REFERENCE TO THE RECTANGLE THAT HOLDS A REFERENCE TO THE CHARACTER'S SCRIPT, WHICH WILL THEN GET CLICKED IF YOU CLICK THE 
    [Space(10)]
    [HideInInspector]
    public CharacterWorldspaceScript PC2;
    public Image secondCharAvatar; //we will replace this object's image with the currently selected character's avatar
    public TextMeshProUGUI secondCharName;//the name of the current player char
    public TextMeshProUGUI secondCharTrait;//the name of the current player char
    public Slider secondHealthBar;//the name of the current player cha
    public Slider secondManaBar;//the name of the current player char
    public GameObject secondselectionRectangle; // A REFERENCE TO THE RECTANGLE THAT HOLDS A REFERENCE TO THE CHARACTER'S SCRIPT, WHICH WILL THEN GET CLICKED IF YOU CLICK TH
    [Space(10)]
    [HideInInspector]
    public CharacterWorldspaceScript PC3;
    public Image thirdCharAvatar; //we will replace this object's image with the currently selected character's avatar
    public TextMeshProUGUI thirdCharName;//the name of the current player char
    public TextMeshProUGUI thirdCharTrait;//the name of the current player char
    public Slider thirdHealthBar;//the name of the current player char
    public Slider thirdManaBar;//the name of the current player char
    public GameObject thirdselectionRectangle; // A REFERENCE TO THE RECTANGLE THAT HOLDS A REFERENCE TO THE CHARACTER'S SCRIPT, WHICH WILL THEN GET CLICKED IF YOU CLICK THE 
    [Space(10)]
    [HideInInspector]
    public CharacterWorldspaceScript PC4;
    public Image fourthCharAvatar; //we will replace this object's image with the currently selected character's avatar
    public TextMeshProUGUI fourthCharName;//the name of the current player char
    public TextMeshProUGUI fourthCharTrait;//the name of the current player char
    public Slider fourthHealthBar;//the name of the current player char
    public Slider fourthManaBar;//the name of the current player char
    public GameObject fourthselectionRectangle; // A REFERENCE TO THE RECTANGLE THAT HOLDS A REFERENCE TO THE CHARACTER'S SCRIPT, WHICH WILL THEN GET CLICKED IF YOU CLICK THE
    [Space(5)]
    [Header("the stuff related to ENEMY party members in the lower part of the UI - the little images + name + health bar")]
    [Header("NOTE - there are >4, so refresh based on lowest health. so you can just click the buttons to target them")]
    [Space(10)]
    [HideInInspector]
    public CharacterWorldspaceScript NPC1;// NOTE - THESE ARE DEFINED IN EntityDefinition.cs
    public Image NPC1Avatar; //we will replace this object's image with the currently selected character's avatar
    public TextMeshProUGUI NPC1Name;//the name of the current player char
    public Slider NPC1HPbar;//HEALTH BAR REF
    public GameObject firstEnemyselectionRectangle;//the name of the current player char
    [Space(10)]
    [HideInInspector]
    public CharacterWorldspaceScript NPC2;
    public Image NPC2Avatar; //we will replace this object's image with the currently selected character's avatar
    public TextMeshProUGUI NPC2Name;//the name of the current player char
    public Slider NPC2HPbar;//HEALTH BAR REF
    public GameObject secondEnemyselectionRectangle;//the name of the current player char
    [Space(10)]
    [HideInInspector]
    public CharacterWorldspaceScript NPC3;
    public Image NPC3Avatar; //we will replace this object's image with the currently selected character's avatar
    public TextMeshProUGUI NPC3Name;//the name of the current player char
    public Slider NPC3HPbar;//HEALTH BAR REF
    public GameObject thirdEnemyselectionRectangle;//the name of the current player char
    [Space(10)]
    [HideInInspector]
    public CharacterWorldspaceScript NPC4;
    public Image NPC4Avatar; //we will replace this object's image with the currently selected character's avatar
    public TextMeshProUGUI NPC4Name;//the name of the current player char
    public Slider NPC4HPbar;//HEALTH BAR REF
    public GameObject fourthEnemyselectionRectangle;
    [Space(15)]
    [Header("enemy miniview parent objects")]
    public GameObject enemydata1;
    public GameObject enemydata2;
    public GameObject enemydata3;
    public GameObject enemydata4;
    [Space(5)]
    [Header("Health bar numbers - enemy")]
    public TextMeshProUGUI NPC1nmbr;
    public TextMeshProUGUI NPC2nmbr;
    public TextMeshProUGUI NPC3nmbr;
    public TextMeshProUGUI NPC4nmbr;
    [Header("Health bar numbers - player")]
    public TextMeshProUGUI PC1nmbr;
    public TextMeshProUGUI PC2nmbr;
    public TextMeshProUGUI PC3nmbr;
    public TextMeshProUGUI PC4nmbr;
    [Header("Mana bar numbers - player")]

    public TextMeshProUGUI PC1nmbrMana;
    public TextMeshProUGUI PC2nmbrMana;
    public TextMeshProUGUI PC3nmbrMana;
    public TextMeshProUGUI PC4nmbrMana;




    [Space(15)]
    public miniviewScript pc1Miniview; //this is for the overview stuff we click to get the inventory
    public miniviewScript pc2Miniview;
    public miniviewScript pc3Miniview;
    public miniviewScript pc4Miniview;


    [Space(15)]
    [Header("references to consumable slots - this is just so you can change their sprite based on what item is in that slot")]
    public GameObject ConsumableSlot1;
    public GameObject ConsumableSlot2;
    public GameObject ConsumableSlot3;
    [Space(10)]
    public GameObject worldmapDescription;
    public GameObject worldmapName;
    [Space(10)]
    [Header("loading stuff - unused for now")]
    public GameObject darkText;
    public float travelMicroDelay;
    public float transparencyIncrement;
    [Header("Canvas of the entire game activity area")]
    public GameObject GameUI;
    [Space(15)]
    [Header("various menu canvases")]
    public GameObject MainMenuBack;
    public GameObject ExitConfirmationCanvas;
    public GameObject SettingsCanvas;
    public GameObject MenuCanvas;
    public GameObject SettingsParallaxButton; //just so we can change the text
    public GameObject MainMenuStart;//for changing the text for subsequent menu opening from Start to Continue
    [Space(10)]
    [Header("ingame buttons, dealt with in UserInterfaceHelper usually by calling a method there on click, for some in CombatHelper")]
    public GameObject PassTurnButton;
    public GameObject AttackButton;
    public GameObject AbilityButton;

    [Space(15)]
    public GameObject WorldMapCanvas; //for activating it on click
    public GameObject WorldCanvasLevelPrefab; //prefab of a singular icon  on the overmap
    public GameObject SelectedChar;
    [Space(10)]
    public GameObject CombatHighlightObject;
    [Space(10)]
    public GameObject PCDead1;//reference to the object we activate when the char is dead
    public GameObject PCDead2;
    public GameObject PCDead3;
    public GameObject PCDead4;
    [Space(10)]
    [Header("Consumable usage.")]
    public bool isDraggingItem; // this is used to keep track of wether you have an item in your hand
    public Sprite draggedItemSprite;
    public Item draggedItem;
    private List<GameObject> consumableSlots = new List<GameObject>();
    [Space(5)]
    public Texture2D cursornormal;
    public Texture2D cursorpressed;
    [Space(5)]
    public GameObject TraitToolTip;
    public TextMeshProUGUI ToolTipTraitName;
    public TextMeshProUGUI ToolTipTraitDescription;
    public Image ToolTipTraitImage;
    public void RefreshToolTip(Character c = null)
    {
        ToolTipTraitName.text = c.charName +" the "+ c.charTrait.adjective;
        if (c.charTrait == null)
        {
            ToolTipTraitDescription.text = "";
            ToolTipTraitImage.sprite = null;
        }
        else
        {
            ToolTipTraitDescription.text = c.charTrait.traitDescription;
            if (c.charTrait.traitSprite != null)
            {
                ToolTipTraitImage.gameObject.SetActive(true);
                ToolTipTraitImage.sprite = c.charTrait.traitSprite;
            }
            else
            {
                ToolTipTraitImage.gameObject.SetActive(false);
            }
            
        }
    }
    public void Awake()
    {
        Cursor.SetCursor(cursorpressed, new Vector2(0, 0), CursorMode.Auto);
    }
    public void Update()
    {
        if (Input.GetMouseButton(0))
            Cursor.SetCursor(cursorpressed, new Vector2(0, 0), CursorMode.Auto);
        else
            Cursor.SetCursor(cursornormal, new Vector2(0, 0), CursorMode.Auto);

        Cursor.lockState = UnityEngine.CursorLockMode.None;
        //Screen.lockCursor = false;
    }
    public void ToggleFightButtonVisiblity(bool boolin)
    {
        PassTurnButton.SetActive(boolin);
    }
    [HideInInspector]
    public Character TrinketScreenCharacter;
    public GameObject PlayHUD; //the UI for playing. we turn that stuff off when we mess with trinkets
    public TrinketMenuHandler TrinketMenu;
    public void ToggleEquipmentInventory()
    {

        //click sound
        if (!TrinketMenu.gameObject.activeSelf)
        {        //we make it visible 
            TrinketMenu.gameObject.SetActive(true);
            PlayHUD.SetActive(false);
            //display trinkets
            TrinketMenu.RefreshHeroAvatars();
            TrinketMenu.RefreshInventory(currChar: TrinketScreenCharacter);
            
            TrinketMenu.StartAnimatingChar();


        }
        else
        {
            //click sound.
            TrinketMenu.gameObject.SetActive(false);
            TrinketMenu.currentlySelectedItem = null;
            TrinketMenu.StopAllCoroutines();
            PlayHUD.SetActive(true);
            RefreshCharacterTabs();
        }
    }
    public MiniviewClickDetector pc1clickableoverview;
    public MiniviewClickDetector pc2clickableoverview;
    public MiniviewClickDetector pc3clickableoverview;
    public MiniviewClickDetector pc4clickableoverview;










    //character UI stuff
    /// <summary>
    /// refreshes the character tabs
    /// </summary>
    public void RefreshCharacterTabs()
    {

        RefreshViewEnemy();
        RefreshViewPlayer();
    }
    public void PopulateUISlotList()
    {
        consumableSlots.Add(ConsumableSlot1);
        consumableSlots.Add(ConsumableSlot2);
        consumableSlots.Add(ConsumableSlot3);

    }

    public TextMeshProUGUI CurrencyView;
    public void UpdateCurrencyCounter()
    {
        CurrencyView.text = MainData.MainLoop.Currency.ToString();
    }
    /// <summary>
    /// refreshes the consumable item icons. should be ran whenever the inventory contents change...
    /// </summary>
    public void RefreshConsumableSlots()
    {//here we refresh the existence and quantity of items in the slot
        switch (MainData.consumableInventory.Count)
        {
            case 1:
                ConsumableSlot1.SetActive(true);
                ConsumableSlot1.GetComponent<consumableSlotScript>().itemContent = MainData.consumableInventory[0];
                ConsumableSlot1.GetComponent<consumableSlotScript>().selfImage.sprite = MainData.consumableInventory[0].itemSprite;
                ConsumableSlot1.GetComponentInChildren<Text>().text = MainData.consumableInventory[0].itemQuantity.ToString();

                ConsumableSlot2.GetComponentInChildren<Text>().text = ""; 
                ConsumableSlot3.GetComponentInChildren<Text>().text = "";
                ConsumableSlot2.SetActive(false);
                ConsumableSlot3.SetActive(false);
                break;
            case 2:
                ConsumableSlot1.SetActive(true);
                ConsumableSlot1.GetComponent<consumableSlotScript>().itemContent = MainData.consumableInventory[0];
                ConsumableSlot1.GetComponent<consumableSlotScript>().selfImage.sprite = MainData.consumableInventory[0].itemSprite;
                ConsumableSlot1.GetComponentInChildren<Text>().text = MainData.consumableInventory[0].itemQuantity.ToString();
                ConsumableSlot2.SetActive(true);
                ConsumableSlot2.GetComponent<consumableSlotScript>().itemContent = MainData.consumableInventory[1];
                ConsumableSlot2.GetComponent<consumableSlotScript>().selfImage.sprite = MainData.consumableInventory[1].itemSprite;
                ConsumableSlot2.GetComponentInChildren<Text>().text = MainData.consumableInventory[1].itemQuantity.ToString();
  
                ConsumableSlot3.GetComponentInChildren<Text>().text = "";
                ConsumableSlot3.SetActive(false);
                break;
            case 3:
                ConsumableSlot1.SetActive(true);
                ConsumableSlot2.SetActive(true);
                ConsumableSlot3.SetActive(true);
                ConsumableSlot1.GetComponent<consumableSlotScript>().itemContent = MainData.consumableInventory[0];
                ConsumableSlot2.GetComponent<consumableSlotScript>().itemContent = MainData.consumableInventory[1];
                ConsumableSlot3.GetComponent<consumableSlotScript>().itemContent = MainData.consumableInventory[2];
                ConsumableSlot1.GetComponent<consumableSlotScript>().selfImage.sprite = MainData.consumableInventory[0].itemSprite;
                ConsumableSlot2.GetComponent<consumableSlotScript>().selfImage.sprite = MainData.consumableInventory[1].itemSprite;
                ConsumableSlot3.GetComponent<consumableSlotScript>().selfImage.sprite = MainData.consumableInventory[2].itemSprite;

                ConsumableSlot1.GetComponentInChildren<Text>().text = MainData.consumableInventory[0].itemQuantity.ToString();
                ConsumableSlot2.GetComponentInChildren<Text>().text = MainData.consumableInventory[1].itemQuantity.ToString();
                ConsumableSlot3.GetComponentInChildren<Text>().text = MainData.consumableInventory[2].itemQuantity.ToString();
                break;

            default:
                ConsumableSlot1.SetActive(false);
                ConsumableSlot2.SetActive(false);
                ConsumableSlot3.SetActive(false);
                ConsumableSlot1.GetComponent<consumableSlotScript>().itemContent = null;
                ConsumableSlot2.GetComponent<consumableSlotScript>().itemContent = null;
                ConsumableSlot3.GetComponent<consumableSlotScript>().itemContent = null;
                ConsumableSlot1.GetComponentInChildren<Text>().text = "";
                ConsumableSlot2.GetComponentInChildren<Text>().text = "";
                ConsumableSlot3.GetComponentInChildren<Text>().text = "";
                break;
        }


        foreach (GameObject item in consumableSlots)
        {//every slot gets dealt with
            consumableSlotScript slotScript = item.GetComponent<consumableSlotScript>();
            if (slotScript.itemContent == null) { return; }
            if (slotScript.itemContent.itemQuantity == 0)
            {
                MainData.consumableInventory.Remove(slotScript.itemContent);
            }
            else
            {
                if (slotScript.itemContent.itemQuantity == 1)
                {
                    slotScript.quantityText.GetComponent<Text>().text = "";
                }
                else
                {
                    slotScript.quantityText.GetComponent<Text>().text = slotScript.itemContent.itemQuantity.ToString();
                }
            }
            if (slotScript.itemContent != null)
            {
                if (slotScript.itemContent.itemSprite != null)
                {//we change the sprite to represent the item
                    Debug.Log("we have set a sprite");
                    item.GetComponent<Image>().sprite = slotScript.itemContent.itemSprite;
                }
            }
            else
            {//no item - no sprite
                Debug.LogError("it reaches here");
                item.GetComponent<Image>().sprite = null;
            }
        }
    }
    /// <summary>
    /// this displays the info of currently selected character (allied or enemy) in the top right part of the bottom bar
    /// </summary>
    /// <param name="Target"></param>
    /// 
    public Sprite transparency;
    public void DisplayTargetedCharacterInfo(CharacterWorldspaceScript Target = null)
    {//this sets the viewable info for the current targeted character, in the right top part of the bottom UI. it is possible to select one target and hover over another to compare them.
        if (Target == null || Target.associatedCharacter == null)
        {
            selectedEnemyCharAvatar.sprite = transparency;
            selectedEnemyCharName.text = "";
            selectedEnemyCharDescription.text = "";
            selectedEnemyCharEnemyType.text = "";
            return;
        }
        if (Target.associatedCharacter.charAvatar != null)
        {
            selectedEnemyCharAvatar.sprite = Target.associatedCharacter.charAvatar;
        }
        selectedEnemyCharName.text = Target.associatedCharacter.charName;
        if (Target.associatedCharacter.entityDescription != null)
        {
            selectedEnemyCharDescription.text = Target.associatedCharacter.entityDescription;
        }
        selectedEnemyCharEnemyType.text = ""; // for now until i actually add the enemy Type thing, if it's even worthwhile.
    }
    private void ReferenceEnemiesForDisplay()
    {//grabs the four most damaged enemy characters, or if all are same health, just the first four.
        //refresh this every time the number of enemy characters changes
        //runs in RefreshEnemyViewDat

        if (MainData.livingEnemyParty.Count < 1)
        {
            SetActiveEnemyTabs(0);
            //Debug.LogWarning("RefreshEnemyCharacterView() - livingEnemyParty has no enemies in it.");
            NPC1 = null;
            NPC2 = null;
            NPC3 = null;
            NPC4 = null;
            return;
        }
        NPC1 = null;
        NPC2 = null;
        NPC3 = null;
        NPC4 = null;
        //Debug.LogWarning("RefreshEnemyCharacterView() just ran");
        List<Character> characters = new List<Character>(MainData.livingEnemyParty);

        characters.Sort((x, y) => x.currentHealth.CompareTo(y.currentHealth));
        // ascending. swap y and x on the right side for descending. Yes, we are sorting by plain ol health without any ratio because it's better to hit the one with less life and not the 500hp behemoth who has only 100hp left and the game thinks it's equivalent to 25hp max100hp guy. also ratio.

        //StaticDataHolder.MainLoop.EventLoggingComponent.LogGray("There are " + characters.Count + " enemy characters.");
        switch (characters.Count)
        {
            case 0:
                //this should never happen 
                SetActiveEnemyTabs(0);
                break;
            case 1:
                NPC1 = characters[0].selfScriptRef;
                SetActiveEnemyTabs(1);
                break;
            case 2:
                SetActiveEnemyTabs(2);
                NPC1 = characters[0].selfScriptRef;
                NPC2 = characters[1].selfScriptRef;
                break;
            case 3:
                SetActiveEnemyTabs(3);
                NPC1 = characters[0].selfScriptRef;
                NPC2 = characters[1].selfScriptRef;
                NPC3 = characters[2].selfScriptRef;
                break;
            default: //any case other than 0 1 2 3 and 4 is automatically > 3 so yeah
                SetActiveEnemyTabs(4);
                NPC1 = characters[0].selfScriptRef;
                NPC2 = characters[1].selfScriptRef;
                NPC3 = characters[2].selfScriptRef;
                NPC4 = characters[3].selfScriptRef;
                break;
        }
        RefreshHealthBarEnemy();
    }
    private void SetActiveEnemyTabs(int amount)
    {
        //1 - mess with first one
        //2 - mess with first+second
        //3 - mess with 1,2,3
        //4 - mess with all 4


        switch (amount)
        {
            case 0:// hide all
                enemydata1.SetActive(false);
                enemydata2.SetActive(false);
                enemydata3.SetActive(false);
                enemydata4.SetActive(false);
                break;
            case 1://show first
                enemydata1.SetActive(true);
                enemydata2.SetActive(false);
                enemydata3.SetActive(false);
                enemydata4.SetActive(false);
                break;
            case 2://show 1+second
                enemydata1.SetActive(true);
                enemydata2.SetActive(true);
                enemydata3.SetActive(false);
                enemydata4.SetActive(false);
                break;
            case 3://show 1,2+third
                enemydata1.SetActive(true);
                enemydata2.SetActive(true);
                enemydata3.SetActive(true);
                enemydata4.SetActive(false);
                break;
            case 4://show 1,2,3+fourth
                enemydata1.SetActive(true);
                enemydata2.SetActive(true);
                enemydata3.SetActive(true);
                enemydata4.SetActive(true);
                break;

        }

    }
    public void RefreshViewEnemy()
    {//run this after every spawning or death of an enemy
        if (MainData.livingEnemyParty.Count == 0)
        {
            NPC1HPbar.transform.parent.gameObject.SetActive(false);
            NPC2HPbar.transform.parent.gameObject.SetActive(false);
            NPC3HPbar.transform.parent.gameObject.SetActive(false);
            NPC4HPbar.transform.parent.gameObject.SetActive(false);

            selectedEnemyCharName.text = "";
            selectedEnemyCharDescription.text = "";
            selectedCharAvatar.sprite = transparency;


            return;
        }
        ReferenceEnemiesForDisplay();
        if (NPC1 != null && NPC1.associatedCharacter != null)//checks if it exists
        {
            NPC1HPbar.transform.parent.gameObject.SetActive(true);
            NPC1Avatar.sprite = NPC1.associatedCharacter.charAvatar; //if it does, show it in the tabs
            NPC1Name.text = NPC1.associatedCharacter.charName;

        }
        else
        {
            NPC1HPbar.transform.parent.gameObject.SetActive(false);
        }
        if (NPC2 != null && NPC2.associatedCharacter != null)
        {
            NPC2HPbar.transform.parent.gameObject.SetActive(true);
            NPC2Avatar.sprite = NPC2.associatedCharacter.charAvatar;
            NPC2Name.text = NPC2.associatedCharacter.charName;
        }
        else
        {
            NPC2Avatar.gameObject.SetActive(false);
        }
        if (NPC3 != null && NPC3.associatedCharacter != null)
        {
            NPC3HPbar.transform.parent.gameObject.SetActive(true);
            NPC3Avatar.sprite = NPC3.associatedCharacter.charAvatar;
            NPC3Name.text = NPC3.associatedCharacter.charName;
        }
        else
        {
            NPC3HPbar.transform.parent.gameObject.SetActive(false);
        }
        if (NPC4 != null && NPC4.associatedCharacter != null)
        {
            NPC4HPbar.transform.parent.gameObject.SetActive(true);
            NPC4Avatar.sprite = NPC4.associatedCharacter.charAvatar;
            NPC4Name.text = NPC4.associatedCharacter.charName;

        }
        else
        {
            NPC4HPbar.transform.parent.gameObject.SetActive(false);
        }

        RefreshHealthBarEnemy();

    }
    private void RefreshViewPlayer()
    {//run this after any trait change, death, etc.
        if (PC1 != null && PC1.associatedCharacter != null)
        {
            firstCharAvatar.sprite = PC1.associatedCharacter.charAvatar;
            firstCharName.text = PC1.associatedCharacter.charName;
            PC1.associatedCharacter.HealthBar = firstHealthBar;
            pc1Miniview.associatedCharacter = PC1.associatedCharacter;
            if (PC1.associatedCharacter.charTrait != null)
            {
                firstCharTrait.text = PC1.associatedCharacter.charTrait.adjective;
            }
            else
            {
                firstCharTrait.text = "";
            }

        }
        if (PC2 != null && PC2.associatedCharacter != null)
        {
            secondCharAvatar.sprite = PC2.associatedCharacter.charAvatar;
            secondCharName.text = PC2.associatedCharacter.charName;
            PC2.associatedCharacter.HealthBar = secondHealthBar;
            pc2Miniview.associatedCharacter = PC2.associatedCharacter;
            if (PC2.associatedCharacter.charTrait != null)
            {
                secondCharTrait.text = PC2.associatedCharacter.charTrait.adjective;
            }
            else
            {
                secondCharTrait.text = "";
            }
        }
        if (PC3 != null && PC3.associatedCharacter != null)
        {
            thirdCharAvatar.sprite = PC3.associatedCharacter.charAvatar;
            thirdCharName.text = PC3.associatedCharacter.charName;
            PC3.associatedCharacter.HealthBar = thirdHealthBar;
            pc3Miniview.associatedCharacter = PC3.associatedCharacter;
            if (PC3.associatedCharacter.charTrait != null)
            {
                thirdCharTrait.text = PC3.associatedCharacter.charTrait.adjective;
            }
            else
            {
                thirdCharTrait.text = "";
            }
        }
        if (PC4 != null && PC4.associatedCharacter != null)
        {
            fourthCharAvatar.sprite = PC4.associatedCharacter.charAvatar;
            fourthCharName.text = PC4.associatedCharacter.charName;
            PC4.associatedCharacter.HealthBar = fourthHealthBar;
            pc4Miniview.associatedCharacter = PC4.associatedCharacter;
            if (PC4.associatedCharacter.charTrait != null)
            {
                fourthCharTrait.text = PC4.associatedCharacter.charTrait.adjective;
            }
            else
            {
                fourthCharTrait.text = "";
            }
        }


        RefreshHealthManaBarsPlayer();
        RefreshPlayerDeathStatus();




    }
    public void RefreshHealthBarEnemy()
    {//this is small enough and used enough we shouldn't run the whole refresh thing if possible
        //this is only used when we get a new character to display or a character dies.
        //the health bar value is changed when hit, in the Character class' TakeDamageFromCharacter
        foreach (Character item in MainData.livingEnemyParty)
        {
            item.HealthBar = null;
        }//we do this so we don't have unwanted references if we somehow switch back and forth from a character due to changed hp

        //basically the character displayed in a "view" can change from time to time depending on health so we don't want to retain the reference to healthbar

        if (NPC1 != null)
        {
            if (NPC1.associatedCharacter != null)
            {
                if (!NPC1.associatedCharacter.isDead)
                {


                    NPC1.associatedCharacter.HealthBar = NPC1HPbar;
                    NPC1HPbar.maxValue = NPC1.associatedCharacter.maxHealth;
                    NPC1HPbar.value = NPC1.associatedCharacter.currentHealth;
                    string currHP = (NPC1.associatedCharacter.currentHealth < 0) ? "0" : NPC1.associatedCharacter.currentHealth.ToString();
                    NPC1nmbr.text = currHP + "/" + NPC1.associatedCharacter.maxHealth;
                }
                else
                {
                    NPC1HPbar.value = 0f;
                }
            }
            else
            {
                NPC1HPbar.value = 0f;
            }


        }

        if (NPC2 != null)
        {
            if (NPC2.associatedCharacter != null)
            {
                if (!NPC2.associatedCharacter.isDead)
                {
                    NPC2.associatedCharacter.HealthBar = NPC2HPbar;
                    NPC2HPbar.maxValue = NPC2.associatedCharacter.maxHealth;
                    NPC2HPbar.value = NPC2.associatedCharacter.currentHealth;
                    string currHP = (NPC2.associatedCharacter.currentHealth < 0) ? "0" : NPC2.associatedCharacter.currentHealth.ToString();
                    NPC2nmbr.text = currHP + "/" + NPC2.associatedCharacter.maxHealth;
                }
                else
                {
                    NPC2HPbar.value = 0f;
                }
            }
            else
            {
                NPC2HPbar.value = 0f;
            }


        }
        if (NPC3 != null)
        {
            if (NPC3.associatedCharacter != null)
            {
                if (!NPC3.associatedCharacter.isDead)
                {
                    NPC3.associatedCharacter.HealthBar = NPC3HPbar;
                    NPC3HPbar.maxValue = NPC3.associatedCharacter.maxHealth;
                    NPC3HPbar.value = NPC3.associatedCharacter.currentHealth;
                    string currHP = (NPC3.associatedCharacter.currentHealth < 0) ? "0" : NPC3.associatedCharacter.currentHealth.ToString();
                    NPC3nmbr.text = currHP + "/" + NPC3.associatedCharacter.maxHealth;
                }
                else
                {
                    NPC3HPbar.value = 0f;
                }
            }
            else
            {
                NPC3HPbar.value = 0f;
            }


        }
        if (NPC4 != null)
        {
            if (NPC4.associatedCharacter != null)
            {
                if (!NPC4.associatedCharacter.isDead)
                {
                    NPC4.associatedCharacter.HealthBar = NPC4HPbar;
                    NPC4HPbar.maxValue = NPC4.associatedCharacter.maxHealth;
                    NPC4HPbar.value = NPC4.associatedCharacter.currentHealth;
                    string currHP = (NPC4.associatedCharacter.currentHealth < 0) ? "0" : NPC4.associatedCharacter.currentHealth.ToString();
                    NPC4nmbr.text = currHP + "/" + NPC4.associatedCharacter.maxHealth;
                }
                else
                {
                    NPC4HPbar.value = 0f;
                }

            }
            else
            {
                NPC4HPbar.value = 0f;
            }


        }
    }
    public void RefreshHealthManaBarsPlayer()
    {

        if (PC1 != null && PC1.associatedCharacter != null && !PC1.associatedCharacter.isDead)
        {
            
            PC1.associatedCharacter.HealthBar = firstHealthBar;
            firstHealthBar.gameObject.SetActive(true);
            firstManaBar.gameObject.SetActive(true);
            firstHealthBar.maxValue = PC1.associatedCharacter.maxHealth;
            firstHealthBar.value = PC1.associatedCharacter.currentHealth;
            string currHP = (PC1.associatedCharacter.currentHealth < 0) ? "0" : PC1.associatedCharacter.currentHealth.ToString();
            PC1nmbr.text = currHP + "/" + PC1.associatedCharacter.maxHealth;

            PC1.associatedCharacter.ManaBar = firstManaBar;
            firstManaBar.maxValue = 100;
            firstManaBar.value = PC1.associatedCharacter.manaTotal;
            PC1nmbrMana.text = PC1.associatedCharacter.manaTotal + "/" + "100";
        }
        else
        {
            Debug.Log("disabled p1 healthbar");
            firstHealthBar.gameObject.SetActive(false);
            firstManaBar.gameObject.SetActive(false);
        }
        //
        if (PC2 != null && PC2.associatedCharacter != null && !PC2.associatedCharacter.isDead)
        {

            PC2.associatedCharacter.HealthBar = secondHealthBar;
            secondHealthBar.gameObject.SetActive(true);
            secondManaBar.gameObject.SetActive(true);
            secondHealthBar.maxValue = PC2.associatedCharacter.maxHealth;
            secondHealthBar.value = PC2.associatedCharacter.currentHealth;
            string currHP = (PC2.associatedCharacter.currentHealth < 0) ? "0" : PC2.associatedCharacter.currentHealth.ToString();
            PC2nmbr.text = currHP + "/" + PC2.associatedCharacter.maxHealth;

            PC2.associatedCharacter.ManaBar = secondManaBar;
            secondManaBar.maxValue = 100;
            secondManaBar.value = PC2.associatedCharacter.manaTotal;
            PC2nmbrMana.text = PC2.associatedCharacter.manaTotal + "/" + "100";
        }
        else
        {
            secondHealthBar.gameObject.SetActive(false);
            secondManaBar.gameObject.SetActive(false);
        }
        //
        if (PC3 != null && PC3.associatedCharacter != null && !PC3.associatedCharacter.isDead)
        {
            PC3.associatedCharacter.HealthBar = thirdHealthBar;
            thirdHealthBar.gameObject.SetActive(true);
            thirdManaBar.gameObject.SetActive(true);
            thirdHealthBar.maxValue = PC3.associatedCharacter.maxHealth;
            thirdHealthBar.value = PC3.associatedCharacter.currentHealth;
            string currHP = (PC3.associatedCharacter.currentHealth < 0) ? "0" : PC3.associatedCharacter.currentHealth.ToString();
            PC3nmbr.text = currHP + "/" + PC3.associatedCharacter.maxHealth;

            PC3.associatedCharacter.ManaBar = thirdManaBar;
            thirdManaBar.maxValue = 100;
            thirdManaBar.value = PC3.associatedCharacter.manaTotal;
            PC3nmbrMana.text = PC3.associatedCharacter.manaTotal + "/" + "100";
        }
        else
        {
            thirdHealthBar.gameObject.SetActive(false);
            thirdManaBar.gameObject.SetActive(false);
        }
        //
        if (PC4 != null && PC4.associatedCharacter != null && !PC4.associatedCharacter.isDead)
        {
            PC4.associatedCharacter.HealthBar = fourthHealthBar;
            fourthHealthBar.gameObject.SetActive(true);
            fourthManaBar.gameObject.SetActive(true);
            fourthHealthBar.maxValue = PC4.associatedCharacter.maxHealth;
            fourthHealthBar.value = PC4.associatedCharacter.currentHealth;
            string currHP = (PC4.associatedCharacter.currentHealth < 0) ? "0" : PC4.associatedCharacter.currentHealth.ToString();
            PC4nmbr.text = currHP + "/" + PC4.associatedCharacter.maxHealth;

            PC4.associatedCharacter.ManaBar = fourthManaBar;
            fourthManaBar.maxValue = 100;
            fourthManaBar.value = PC4.associatedCharacter.manaTotal;
            PC4nmbrMana.text = PC4.associatedCharacter.manaTotal + "/" + "100";
        }
        else
        {
            fourthHealthBar.gameObject.SetActive(false);
            fourthManaBar.gameObject.SetActive(false);
        }
        //



        RefreshPlayerDeathStatus();
    }
    private void RefreshPlayerDeathStatus()
    {//checks wether the player is dead so it can hide or show the death/inactive overlay
        if (PC1.associatedCharacter == null)
        {
            PCDead1.SetActive(true);
            firstHealthBar.gameObject.SetActive(false);
        }
        else
        {
            firstHealthBar.gameObject.SetActive(true);
            PCDead1.SetActive(false);
        }
        if (PC2.associatedCharacter == null)
        {
            PCDead2.SetActive(true);
            secondHealthBar.gameObject.SetActive(false);
        }
        else
        {
            secondHealthBar.gameObject.SetActive(true); 
            PCDead2.SetActive(false);
        }
        if (PC3.associatedCharacter == null)
        {
            PCDead3.SetActive(true);
            thirdHealthBar.gameObject.SetActive(false);
        }
        else
        {
            thirdHealthBar.gameObject.SetActive(true);
            PCDead3.SetActive(false);
        }
        if (PC4.associatedCharacter == null)
        {
            PCDead4.SetActive(true);
            fourthHealthBar.gameObject.SetActive(false);
        }
        else
        {
            fourthHealthBar.gameObject.SetActive(true);
            PCDead4.SetActive(false);
        }

    }
    //char UI stuff done

    public void ClickConsumableSlotButton(GameObject source)
    {
        //just checking if target is valid first.
        if (MainData.MainLoop.CombatHelperComponent.activeTarget == null)
        {
            return;
        }
        if (MainData.MainLoop.CombatHelperComponent.activeTarget.associatedCharacter == null)
        {
            MainData.MainLoop.EventLoggingComponent.LogGray("'It is unwise to give something to the nothingness, for occasionally, it gives back.'");
            return;
        }
        MainData.MainLoop.EntityDefComponent.UseConsumable(source.GetComponent<consumableSlotScript>().itemContent, MainData.MainLoop.CombatHelperComponent.activeTarget.associatedCharacter);
        if (source.GetComponent<consumableSlotScript>().itemContent != null)
        {
            source.GetComponentInChildren<Text>().text = source.GetComponent<consumableSlotScript>().itemContent.itemQuantity.ToString();
        }
        
    }
    public void ClickSendPause()
    {
        MainMenuBack.SetActive(true);
        MenuCanvas.SetActive(true);
        GameUI.SetActive(false);
        MainData.SoundManagerRef.PlayClickSound();

    }
    public void ClickStartGame()
    {
        //StaticDataHolder.MainLoop.SoundManagerComponent.PlayClickSound();
        if (!MainData.MainLoop.gameStarted)
        {

            //StaticDataHolder.MainLoop.SoundManagerComponent.ChangeSoundtrack(StaticDataHolder.SoundManagerRef.MainTheme);
        }
        MainMenuStart.GetComponentInChildren<TextMeshProUGUI>().text = "Continue";
        MenuCanvas.SetActive(false);
        MainMenuBack.SetActive(false);
        GameUI.SetActive(true);
        MainData.MainLoop.gameStarted = true;
    }
    public void ClickExitGame()
    {
        MainData.SoundManagerRef.PlayClickSound();
        MenuCanvas.SetActive(false);

        ExitConfirmationCanvas.SetActive(true);
    }
    public void ClickExitYes()
    {
        MainData.SoundManagerRef.PlayClickSound();
        Application.Quit();
    }
    public void ClickExitNo()
    {
        MainData.SoundManagerRef.PlayClickSound();
        ExitConfirmationCanvas.SetActive(false);
        MenuCanvas.SetActive(true);



    }
    public void ClickMenuSettings()
    {
        MenuCanvas.SetActive(false);
        SettingsCanvas.SetActive(true);

    }
    public void ClickSettingsBack()
    {
        MenuCanvas.SetActive(true);
        SettingsCanvas.SetActive(false);



    }
    public void ClickSettingsParallax()
    {
        if (MainData.MainLoop.BackgroundParallaxObject.ParallaxSetting)
        {
            MainData.MainLoop.BackgroundParallaxObject.ParallaxSetting = false;
            SettingsParallaxButton.GetComponentInChildren<TextMeshProUGUI>().text = "Parallax - Off";
        }
        else
        {
            MainData.MainLoop.BackgroundParallaxObject.ParallaxSetting = true;
            SettingsParallaxButton.GetComponentInChildren<TextMeshProUGUI>().text = "Parallax - On";
        }



    }
    /// <summary>
    /// in-game buttons
    /// </summary>
    public void ClickTesting()
    {
        Debug.Log("I HAVE BEEN CLICKED. WHO DARES?");
    }
    public void ClickOvermapLevel(MapLevel clickyyy)
    {
        if (clickyyy != MainData.currentLevel)
        {//we go there if possible
            if (MainData.currentLevel.nextLevels.Contains(clickyyy))
            {//oh yeeeeee

                MainData.SoundManagerRef.PlayClickSound();
            }
            else
            {
                //failure
                //StaticDataHolder.SoundManagerRef.PlayFailureSound();
            }
        }

    }
    public void ClickMapClose()
    {

        WorldMapCanvas.SetActive(false);
        Debug.Log("CLICKED closeOVERMAP BUTTON.");
        MainData.SoundManagerRef.PlayClickSound();
    }
    public void ClickMapOpen()
    {

        WorldMapCanvas.SetActive(true);
        Debug.Log("CLICKED OVERMAP-open BUTTON.");
        MainData.SoundManagerRef.PlayClickSound();
    }
    //NOTE - combat buttons are handled in CombatHelper.cs
    IEnumerator darksequence()
    {
        float transparency = 0f;
        while (transparency <= 100f)//quickly fade the loading stuff into view
        {
            transparency += transparencyIncrement / 100;
            Vector4 b = new Vector4(0, 0, 0, transparency);
            darkText.GetComponent<Image>().color = b;
            yield return new WaitForSecondsRealtime(travelMicroDelay);
        }
        yield return new WaitForSecondsRealtime(2f);
        while (transparency >= 0f)
        {
            transparency -= transparencyIncrement / 100;
            Vector4 b = new Vector4(0, 0, 0, transparency);
            darkText.GetComponent<Image>().color = b;
            yield return new WaitForSecondsRealtime(travelMicroDelay);
        }
    }

    public void TravelLoadingSequence()
    {


        StartCoroutine(darksequence());

    }
}
