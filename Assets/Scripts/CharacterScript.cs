
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static EntitiesDefinition;
using static TraitHelper;

public class CharacterScript : MonoBehaviour
{
    [HideInInspector]
    public Character associatedCharacter;
    public float expandOnMouseOver = 0.01f;
    public SpriteRenderer spriteRenderer;
    public bool isEnemyCharacter;
    // Start is called before the first frame update
    void Start()
    {
        //spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (associatedCharacter != null)
        {
            associatedCharacter.selfScriptRef = this;
        }

        //if (!isEnemyCharacter)
        //{
        //    StaticDataHolder.playerPartyMemberObjects.Add(gameObject);
        //}
        //else
        //{
        //    StaticDataHolder.enemyPartyMemberObjects.Add(gameObject);
        //}
    }



    //make a different button for this

    public void Die() //visually show character has died
    {
        associatedCharacter = null;
        spriteRenderer.sprite = null;
        if (isEnemyCharacter)
        {
            MainData.freeEnemyPartyMemberObjects.Add(this.gameObject);
        }
        gameObject.SetActive(false);
    }

    public void SetupCharacterByTemplate(Character template)
    {
        associatedCharacter = Character.CreateInstance<Character>();
        associatedCharacter.attackverb = template.attackverb;
        associatedCharacter.baseDamageMin = template.baseDamageMin;
        associatedCharacter.maxHealth = template.maxHealth;
        associatedCharacter.baseSpeed = template.baseSpeed;
        associatedCharacter.charAvatar = template.charAvatar;
        associatedCharacter.charName = template.charName;
        associatedCharacter.charSprite = template.charSprite;
        associatedCharacter.charTrait = template.charTrait;
        associatedCharacter.charType = template.charType;
        associatedCharacter.selfScriptRef = this;
        associatedCharacter.currentHealth = template.currentHealth;
        associatedCharacter.damageMin = template.damageMin;
        associatedCharacter.defense = template.defense;
        associatedCharacter.entityDescription = template.entityDescription;
        associatedCharacter.isPlayerPartyMember = !isEnemyCharacter;
        associatedCharacter.luck = template.luck;
        associatedCharacter.mana = template.mana;
        associatedCharacter.turnSound = template.turnSound;
        associatedCharacter.speed = template.speed;
        associatedCharacter.InitialPosition = this.transform.position;
        spriteRenderer.sprite = associatedCharacter.charSprite[0];
        if (isEnemyCharacter)
        {
            // Debug.Log("Added a new enemy character - " + associatedCharacter.charName);
            // spriteRenderer.flipX = true;
            string newname;
            switch (UnityEngine.Random.Range(1, 7))
            {
                case 1:
                    newname = "Billy";
                    break;
                case 2:
                    newname = "John";
                    break;
                case 3:
                    newname = "Maria";
                    break;
                case 4:
                    newname = "Hans";
                    break;
                case 5:
                    newname = "Harry Potter";
                    break;
                default:
                    newname = "aasfasfasfasf";
                    break;

            }
            associatedCharacter.charName = newname;
            MainData.livingEnemyParty.Add(associatedCharacter);
        }
        else
        {
            // Debug.Log("Added a new player character - " + associatedCharacter.charName);
            MainData.livingPlayerParty.Add(associatedCharacter);
        }
        MainData.allChars.Add(associatedCharacter);
    }






    public void GotClicked()
    {

        //if it's not a party member, we select it as a target so we can attack it.
        if (MainData.MainLoop.CombatHelperComponent.activeTarget == this)
        {
            MainData.MainLoop.CombatHelperComponent.activeTarget = null;
            MainData.MainLoop.UserInterfaceHelperComponent.DisplayTargetedCharacterInfo(null);
        }
        else
        {
            Debug.Log(this.associatedCharacter.charName + " got clicked and was selected during combat.");
            MainData.MainLoop.CombatHelperComponent.activeTarget = this;
            if (!this.isEnemyCharacter)
            {
                MainData.MainLoop.CombatHelperComponent.isTargetFriendly = true;
            }
            else
            {
                MainData.MainLoop.CombatHelperComponent.isTargetFriendly = false;
            }

            MainData.MainLoop.UserInterfaceHelperComponent.DisplayTargetedCharacterInfo(this);

        }



        MainData.MainLoop.CombatHelperComponent.TargetSelectionCheck();
    }

    void RefreshCharacterScript(bool show)
    {
        if (associatedCharacter == null)
        {
            Debug.Log(this.name + "Associated character null at RefreshCharacterScript()");
            return;
        }
        TextMeshProUGUI charactDescript = MainData.MainLoop.UserInterfaceHelperComponent.selectedCharDescription.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI charactName = MainData.MainLoop.UserInterfaceHelperComponent.selectedCharName.GetComponent<TextMeshProUGUI>();
        Image charactAvatar = MainData.MainLoop.UserInterfaceHelperComponent.selectedCharAvatar.GetComponent<Image>();
        TextMeshProUGUI charactTitle = MainData.MainLoop.UserInterfaceHelperComponent.selectedCharTraitDesc.GetComponent<TextMeshProUGUI>();
        Image charactTraitIcon = MainData.MainLoop.UserInterfaceHelperComponent.selectedCharTraitIcon.GetComponent<Image>();
        if (show)
        {
            charactDescript.gameObject.SetActive(true);
            charactName.gameObject.SetActive(true);


            charactDescript.text = associatedCharacter.entityDescription;
            charactName.text = associatedCharacter.charName;
            if (associatedCharacter.charTrait != null)
            {
                charactTitle.gameObject.SetActive(true);
                charactTitle.text = associatedCharacter.charTrait.traitName;
            }
            if (associatedCharacter.charAvatar != null)
            {
                charactAvatar.gameObject.SetActive(true);
                charactAvatar.sprite = associatedCharacter.charAvatar;
            }
            if (associatedCharacter.charTrait != null)
            {
                if (associatedCharacter.charTrait.traitSprite != null)
                {
                    charactTraitIcon.gameObject.SetActive(true);
                    charactTraitIcon.sprite = associatedCharacter.charTrait.traitSprite;
                }
            }
            else
            {
                charactTraitIcon.gameObject.SetActive(false);
            }

        }
        else
        {
            charactDescript.gameObject.SetActive(false);
            charactName.gameObject.SetActive(false);
            charactTitle.gameObject.SetActive(false);
            charactAvatar.gameObject.SetActive(false);
            charactDescript.text = "";
            charactName.text = "";
            charactTitle.text = "";
            charactAvatar.sprite = null;
        }
    }
    void OnMouseEnter()
    {//shows the details of hovered character
        RefreshCharacterScript(true);
        transform.localScale = new Vector3(1.1f, 1.1f, 1f);
    }


    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) { GotClicked(); }
    }

    void OnMouseExit()
    {
        RefreshCharacterScript(false);
        transform.localScale = new Vector3(1f, 1f, 1f);

    }















}
