
using System.Collections;
using System.Collections.Generic;
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
        ToggleIdle(false);
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
        associatedCharacter.SoundLibrary = template.SoundLibrary;

        associatedCharacter.attackAnimation = template.attackAnimation;
        associatedCharacter.idleSprite = template.idleSprite;
        associatedCharacter.hurtSprites = template.hurtSprites;
        associatedCharacter.WalkSprites = template.WalkSprites;

        associatedCharacter.charTrait = template.charTrait;
        associatedCharacter.charType = template.charType;
        associatedCharacter.selfScriptRef = this;
        associatedCharacter.currentHealth = template.currentHealth;
        associatedCharacter.damageMin = template.damageMin;
        associatedCharacter.defense = template.defense;
        associatedCharacter.entityDescription = template.entityDescription;
        associatedCharacter.isPlayerPartyMember = !isEnemyCharacter;
        MainData.MainLoop.EventLoggingComponent.Log(associatedCharacter.charName + " has luck " + template.luck);
        associatedCharacter.luck = template.luck;
        associatedCharacter.manaTotal = 100;
        associatedCharacter.manaRegeneration = template.manaRegeneration;
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



    public void SetupIdleAnimAndStart()
    {
        if (associatedCharacter.idleSprite == null)
        {
            return;
        }
        if (associatedCharacter.idleSprite.Length < 2)
        {
            return;
        }
        if (!this.isActiveAndEnabled)
        {
            Debug.LogWarning(associatedCharacter.charName + " was INACTIVE/DISABLED ON SetupIdleAnimAndStart()");
            return;
        }
        randomIdleness = Random.Range(0.00f, 1.2f);
        Debug.Log("random idle movement time variation for " + associatedCharacter.charName + " is " + randomIdleness);
        //randomIdleness = 0;
        StartCoroutine(InitIdle());
    }

    IEnumerator InitIdle()
    {

        yield return new WaitForSecondsRealtime(randomIdleness);
        idle = true;
        StartCoroutine(IdleAnimate());// THIS IS THE ONLY PLACE THIS COROUTINE SHOULD /EVER/ BE STARTED (excluding inside itself) lest we split the time continuum
    }



    private float randomIdleness;
    [HideInInspector]
    public bool idle = false;
    private int idleIndex = 0;
    IEnumerator IdleAnimate()
    {
        yield return new WaitUntil(() => idle == true);
        if (associatedCharacter.idleSprite == null)
        {
            StopCoroutine(IdleAnimate());
        }
        spriteRenderer.sprite = associatedCharacter.idleSprite[idleIndex];
        idleIndex++;
        if (idleIndex == associatedCharacter.idleSprite.Length - 1)
        {
            idleIndex = 0;
        }
        yield return new WaitForSecondsRealtime(0.12f);
        StartCoroutine(IdleAnimate());
    }



    public void ToggleIdle(bool tog)
    {
        idle = tog;
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
            //Debug.Log(this.associatedCharacter.charName + " got clicked and was selected during combat.");
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
    Coroutine co;

    [HideInInspector]
    public bool isWalking = false;
    public void StartWalk()
    {
        if (associatedCharacter == null)
        {
            Debug.Log("Associated character null.");
            return;
        }



        Debug.Log("Starting to walk.");
        isWalking = true;
        co = StartCoroutine(WalkAnim());
    }


    public void GotHurt()
    {
        if (associatedCharacter.hurtSprites != null)
        {
            StartCoroutine(HurtAnim());
        }
    }


    public System.Collections.IEnumerator HurtAnim()
    {
        Debug.Log(associatedCharacter.hurtSprites);
        ToggleIdle(false);

        for (int i = 0; i < associatedCharacter.hurtSprites.Length - 1; i++)
        {
            spriteRenderer.sprite = associatedCharacter.hurtSprites[i];
            yield return new WaitForSecondsRealtime(0.04f);
        }
        ToggleIdle(true);
    }

    public IEnumerator WalkAnim()
    {
        ToggleIdle(false);
        while (isWalking)
        {
            for (int i = 0; i < associatedCharacter.WalkSprites.Length - 1; i++)
            {
                spriteRenderer.sprite = associatedCharacter.WalkSprites[i];
                yield return new WaitForSecondsRealtime(0.08f);
            }
        }
        
    }


    public void StopWalk()
    {
        isWalking = false;
        StopCoroutine(co);
        ToggleIdle(true);
    }











}
