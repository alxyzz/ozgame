
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
            StaticDataHolder.freeEnemyPartyMemberObjects.Add(this.gameObject);
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
        spriteRenderer.sprite = associatedCharacter.charSprite;
        if (isEnemyCharacter)
        {
            // Debug.Log("Added a new enemy character - " + associatedCharacter.charName);
            spriteRenderer.flipX = true;
            StaticDataHolder.livingEnemyParty.Add(associatedCharacter);
        }
        else
        {
            // Debug.Log("Added a new player character - " + associatedCharacter.charName);
            StaticDataHolder.livingPlayerParty.Add(associatedCharacter);
        }
        StaticDataHolder.allChars.Add(associatedCharacter);
    }






    public void GotClicked()
    {
        if (!this.associatedCharacter.isPlayerPartyMember)
        {//if it's not a party member, we select it as a target so we can attack it.
            if (StaticDataHolder.MainLoop.CombatHelperComponent.activeTarget == this)
            {
                StaticDataHolder.MainLoop.CombatHelperComponent.activeTarget = null;
                StaticDataHolder.MainLoop.UserInterfaceHelperComponent.DisplayTargetedEnemyInfo(null);
            }
            else
            {
                Debug.Log(this.associatedCharacter.charName + " got clicked and was selected during combat.");
                StaticDataHolder.MainLoop.CombatHelperComponent.activeTarget = this;
                StaticDataHolder.MainLoop.UserInterfaceHelperComponent.DisplayTargetedEnemyInfo(this);

            }

        }
        StaticDataHolder.MainLoop.CombatHelperComponent.HighlightCheck();
    }

    void RefreshCharacterScript(bool show)
    {
        if (associatedCharacter == null)
        {
            // Debug.Log(this.name + "Associated character null at RefreshCharacterScript()");
            return;
        }
        TextMeshProUGUI charactDescript = StaticDataHolder.MainLoop.UserInterfaceHelperComponent.selectedCharDescription.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI charactName = StaticDataHolder.MainLoop.UserInterfaceHelperComponent.selectedCharName.GetComponent<TextMeshProUGUI>();
        Image charactAvatar = StaticDataHolder.MainLoop.UserInterfaceHelperComponent.selectedCharAvatar.GetComponent<Image>();
        TextMeshProUGUI charactTitle = StaticDataHolder.MainLoop.UserInterfaceHelperComponent.selectedCharTraitDesc.GetComponent<TextMeshProUGUI>();
        Image charactTraitIcon = StaticDataHolder.MainLoop.UserInterfaceHelperComponent.selectedCharTraitIcon.GetComponent<Image>();
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
