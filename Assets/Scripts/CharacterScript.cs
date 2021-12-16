
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
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (associatedCharacter != null)
        {
            associatedCharacter.selfScriptRef = this;
        }
       
        if (!isEnemyCharacter)
        {
            MainData.playerPartyMemberObjects.Add(gameObject);
        }
        else
        {
            MainData.enemyPartyMemberObjects.Add(gameObject);
        }
    }



    //make a different button for this
    
    public void Die() //visually show character has died
    {
        associatedCharacter = null;
        spriteRenderer.sprite = null;

    }


    public void Attack(CharacterScript target)
    {//add dodging
        target.associatedCharacter.TakeDamageFromCharacter(associatedCharacter);

    }
    public void SetupCharacterAfterTemplate(Character template)
    {
        associatedCharacter = new Character();
        associatedCharacter.attackverb = template.attackverb;
        associatedCharacter.baseDamage = template.baseDamage;
        associatedCharacter.baseHealth = template.baseHealth;
        associatedCharacter.baseSpeed = template.baseSpeed;
        associatedCharacter.charAvatar = template.charAvatar;
        associatedCharacter.charName = template.charName;
        associatedCharacter.charSprite = template.charSprite;
        associatedCharacter.charTrait = template.charTrait;
        associatedCharacter.charType = template.charType;
        associatedCharacter.selfScriptRef = this;
        associatedCharacter.currentHealth = template.currentHealth;
        associatedCharacter.damage = template.damage;
        associatedCharacter.defense = template.defense;
        associatedCharacter.entityDescription = template.attackverb;
        associatedCharacter.isPlayerPartyMember = !isEnemyCharacter;
        associatedCharacter.luck = template.luck;
        associatedCharacter.mana = template.mana;
        associatedCharacter.turnSound = template.turnSound;
        associatedCharacter.speed = template.speed;



        Debug.Log("Made a cope of " + template.charName);
        spriteRenderer.sprite = associatedCharacter.charSprite;
    }

    public void GotClicked()
    {
        //highlight this character
        //track selection
        Debug.Log(this.associatedCharacter.charName + "got clicked and was selected during combat.");
        MainData.MainLoop.CombatHelperComponent.activeTarget = this;



    }








    public void SwapWith(CharacterScript target)
    {
        if (target.associatedCharacter.isPlayerPartyMember)
        {
            Vector3 targetPosition = target.transform.position;
            target.transform.position = this.transform.position;
            this.transform.position = targetPosition;
        }
        
    }


    void RefreshCharacterScript(bool show)
    {
        TextMeshProUGUI charactDescript = MainData.uiMan.selectedCharDescription.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI charactName = MainData.uiMan.selectedCharName.GetComponent<TextMeshProUGUI>();
        Image charactAvatar = MainData.uiMan.selectedCharAvatar.GetComponent<Image>();
        TextMeshProUGUI charactTitle = MainData.uiMan.selectedCharTraitDesc.GetComponent<TextMeshProUGUI>();
        if (show)
        {
            charactDescript.text = associatedCharacter.entityDescription;
            charactName.text = associatedCharacter.charName;
            if (associatedCharacter.charTrait != null)
            {
                charactTitle.text = associatedCharacter.charTrait.traitName;
            }
            charactAvatar.sprite = associatedCharacter.charAvatar;
        }
        else
        {
            charactDescript.text = "";
            charactName.text = "";
            charactTitle.text = "";
            charactAvatar.sprite = null;
        }
    }





    void OnMouseEnter()
    {
        
            RefreshCharacterScript(true);
            transform.localScale = new Vector3(1.1f, 1.1f, 1f);
        
        
        //show text on bottom of screen
    }


    private void OnMouseOver()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {

            GotClicked();
        }
    }



    void OnMouseExit()
    {
        
            RefreshCharacterScript(false);
        transform.localScale = new Vector3(1f, 1f, 1f);
        
    }


        
        

    
   
        
            
        

        

    

}
