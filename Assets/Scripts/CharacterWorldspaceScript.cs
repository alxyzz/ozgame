
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static EntityDefiner;
using static TraitHelper;

public class CharacterWorldspaceScript : MonoBehaviour
{
    [HideInInspector]
    public Character associatedCharacter;
    public float expandOnMouseOver = 0.01f;
    public SpriteRenderer spriteRenderer;
    public bool isEnemyCharacter;
    // Start is called before the first frame update
    void Start()
    {
        if (associatedCharacter != null)
        {
            associatedCharacter.selfScriptRef = this;
        }
    }

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
        associatedCharacter.attackAnimation = template.attackAnimation;
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
        associatedCharacter.valueBounty = template.valueBounty;
        associatedCharacter.InitialPosition = this.transform.position;

        if (associatedCharacter.standingSprite == null)
        {
            associatedCharacter.standingSprite = associatedCharacter.attackAnimation[0];
        }
        spriteRenderer.sprite = associatedCharacter.standingSprite;

        if (isEnemyCharacter)
        {
            // Debug.Log("Added a new enemy character - " + associatedCharacter.charName);
            // spriteRenderer.flipX = true;
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


    [HideInInspector]
    public bool isWalking = false;
    public void StartWalk()
    {
        Debug.Log("Starting to walk.");
        StartCoroutine(WalkAnim());
    }


    public void GotHurt()
    {
        MainData.MainLoop.EventLoggingComponent.Log("Got hurt, playing animation. At " + associatedCharacter.charName + ".");

        if (associatedCharacter.hurtSprites != null)
        {
            StartCoroutine(HurtAnim());
        }

    }


    public System.Collections.IEnumerator HurtAnim()
    {
        for (int i = 0; i < associatedCharacter.WalkSprites.Length-1; i++)
        {
            spriteRenderer.sprite = associatedCharacter.hurtSprites[i];
            yield return new WaitForSeconds(0.01f);
        }
    }



    public System.Collections.IEnumerator WalkAnim()
    {
        
            for (int i = 0; i < associatedCharacter.WalkSprites.Length-1; i++)
            {
                spriteRenderer.sprite = associatedCharacter.WalkSprites[i];
                yield return new WaitForSeconds(0.04f);
            }
        

    }


    public void StopWalk()
    {
        spriteRenderer.sprite = associatedCharacter.standingSprite;
        Debug.Log("Stopped walking.");
        isWalking = false;
        StopCoroutine(WalkAnim());
    }











}
