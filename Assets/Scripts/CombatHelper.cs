using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EntitiesDefinition;

public class CombatHelper : MonoBehaviour
{
    public float closenessMargin;
    public float activationMovingSpeed;

    public CharacterScript CurrentActiveCharacter; //each enemy on the level is composed of the game object, holding the script, associated with a Character instance that holds stats, names, descriptions etc

    public CharacterScript activeTarget; //player's target

    private Vector3 InitialActiveCharacterPositionCoordinates;//initial coordinates of character, to be moved back to after their action.
    public GameObject ActiveCharSpot; //where the character that is attacking will move.

    // Update is called once per frame



    public void InitiateCombatTurn()
    {
        List<Character> combatants = MainData.allChars;
        //todo - what happens if someone dies during this? can foreach cope with it?

        combatants.Sort((x, y) => y.speed.CompareTo(x.speed)); // descending. swap y and x on the right side for ascending.

        foreach (Character item in combatants)//goes from 0 to length-1, always
        {
            if (item.CheckIfCanAct())
            {
                
                    MainData.MainLoop.SoundManagerComponent.sfxSource.PlayOneShot(item.turnSound); //plays the character specific noise/vocalization
                MoveToActiveSpot(item);
                if (item.isPlayerPartyMember)
                    {
                        DoPlayerCharacterTurn(item);
                    }
                    else
                    {
                        DoEnemyCharacterTurn(item);
                    }
                
               
            }
        }
        foreach (Character item in MainData.casualties)
        {
            item.DeleteTheVanquished(); //deletes the vanquished and unreferences them from objects, coz we don't mess with a collection during the foreach loop
            MainData.casualties.Clear();
            Debug.Log("Deleted corpse of character - " + item.charName + ".");
        }
        

    }





    public void ClickTraitAbility()
    {

        switch (CurrentActiveCharacter.associatedCharacter.charTrait.traitName)
        {
            case "blahblah"://so yeah this is where active traits go
                break;
            default:
                break;
        }

    }

    public void ClickNormalAttack()
    {
        
        if (activeTarget == null)
        {
            //MainData.MainLoop.SoundManagerComponent.PlayFailureSound();
            return;
        }
        StartCoroutine(AttackVisuals(CurrentActiveCharacter));

    }


    private void ToggleCombatButtomVisibility(bool togg)
    {
        if (togg)
        {
            MainData.MainLoop.UserInterfaceHelperComponent.AbilityButton.SetActive(true);
            MainData.MainLoop.UserInterfaceHelperComponent.AttackButton.SetActive(true);
        }
        else
        {
            MainData.MainLoop.UserInterfaceHelperComponent.AbilityButton.SetActive(false);
            MainData.MainLoop.UserInterfaceHelperComponent.AttackButton.SetActive(false);
        }


    }

    public IEnumerator HitKnockback(CharacterScript toAnimate)
    {

        Vector3 Initial = toAnimate.transform.position;

        if (toAnimate.associatedCharacter.isPlayerPartyMember)
        {
            Vector3 final = new Vector3(Initial.x - 1f, Initial.y, Initial.z);
            while (Vector3.Distance(toAnimate.transform.position, final) > 0.05f)
            {
                toAnimate.transform.position = Vector3.Lerp(toAnimate.transform.position, final, 0.5f * Time.deltaTime);
            }

            yield return new WaitForSecondsRealtime(0.3f);
            while (Vector3.Distance(toAnimate.transform.position, Initial) > 0.05f)
            {
                toAnimate.transform.position = Vector3.Lerp(toAnimate.transform.position, Initial, 0.5f * Time.deltaTime);
            }
        }
        else
        {
            Vector3 final = new Vector3(Initial.x + 1f, Initial.y, Initial.z);
            while (Vector3.Distance(toAnimate.transform.position, final) > 0.05f)
            {
                toAnimate.transform.position = Vector3.Lerp(toAnimate.transform.position, final, 0.5f * Time.deltaTime);
            }
            Character poorFool = MainData.playerParty[Random.Range(0, MainData.playerParty.Count + 1)];

            yield return new WaitForSecondsRealtime(0.3f);
            while (Vector3.Distance(toAnimate.transform.position, Initial) > 0.05f)
            {
                toAnimate.transform.position = Vector3.Lerp(toAnimate.transform.position, Initial, 0.5f * Time.deltaTime);
            }
        }


    }





    public IEnumerator AttackVisuals(CharacterScript toAnimate)
    {
        
        Vector3 Initial = toAnimate.transform.position;
       
        if (toAnimate.associatedCharacter.isPlayerPartyMember)
        {
            Vector3 final = new Vector3(Initial.x+3f, Initial.y, Initial.z);
            while (Vector3.Distance(toAnimate.transform.position, final) > 0.05f)
            {
                toAnimate.transform.position = Vector3.Lerp(toAnimate.transform.position, final, 0.5f*Time.deltaTime);
            }
            activeTarget.associatedCharacter.TakeDamageFromCharacter(toAnimate.associatedCharacter);
            StartCoroutine(HitKnockback(activeTarget));
            //currently active character attacks the character you clicked
            //play some kinda sprite animation + sound effect here, perhaps
            yield return new WaitForSecondsRealtime(0.3f);
            while (Vector3.Distance(toAnimate.transform.position, Initial) > 0.05f)
            {
                toAnimate.transform.position = Vector3.Lerp(toAnimate.transform.position, Initial, 0.5f * Time.deltaTime);
            }
            ReturnFromActiveSpot(toAnimate.associatedCharacter);
        }
        else
        {
            Vector3 final = new Vector3(Initial.x-3f, Initial.y, Initial.z);
            while (Vector3.Distance(toAnimate.transform.position, final) > 0.05f)
            {
                toAnimate.transform.position = Vector3.Lerp(toAnimate.transform.position, final, 0.5f * Time.deltaTime);
            }
            Character poorFool = MainData.playerParty[Random.Range(0, MainData.playerParty.Count + 1)];
            poorFool.TakeDamageFromCharacter(toAnimate.associatedCharacter); // for now, just a random attack
            StartCoroutine(HitKnockback(poorFool.selfScriptRef));
            //play some kinda sprite animation + sound effect here, perhaps
            yield return new WaitForSecondsRealtime(0.3f);
            while (Vector3.Distance(toAnimate.transform.position, Initial) > 0.05f)
            {
                toAnimate.transform.position = Vector3.Lerp(toAnimate.transform.position, Initial, 0.5f * Time.deltaTime);
            }
            ReturnFromActiveSpot(toAnimate.associatedCharacter);
        }
       

    }

    public void DoPlayerCharacterTurn(Character pc)
    {
        
        //what do we do here?
        //first off, move the player character to the designated spot
       
        //then, pop up an icon with either attacking or using the trait from the character
        /////pc.selfScriptRef.transform.position
        //PopUpActionButtons(); //pop up the action menu
        //enable controls over items and such
        MainData.controlsEnabled = true;

        //player can click an enemy to highlight it

    }


    public void MoveToActiveSpot(Character chara)
    {
        Debug.Log("MOVING "  +chara.charName +  " TO ACTIVE SPOT");
        CurrentActiveCharacter = chara.selfScriptRef; //stores the reference to character's physical form
        InitialActiveCharacterPositionCoordinates = chara.selfScriptRef.transform.position;
        //while (Vector3.Distance(chara.selfScriptRef.transform.position, ActiveCharSpot.transform.position) > closenessMargin)
        //{
        //    chara.selfScriptRef.transform.position = Vector3.MoveTowards(chara.selfScriptRef.transform.position, ActiveCharSpot.transform.position, activationMovingSpeed * Time.deltaTime);
        //}
        StartCoroutine(movetospot(chara));
        
        
    }


    public void HighlightCheck()
    {
        //highlights the current target
        if (activeTarget != null)
        {
            MainData.MainLoop.UserInterfaceHelperComponent.CombatHighlightObject.transform.position = activeTarget.transform.position;
            MainData.MainLoop.UserInterfaceHelperComponent.CombatHighlightObject.SetActive(true);
        }
        else
        {
            MainData.MainLoop.UserInterfaceHelperComponent.CombatHighlightObject.SetActive(false);
        }
       
    }

    private bool someoneIsMoving; //so we don't get people clicking two characters quickly and bugging stuff out
    public float delayBetweenMovement;

    IEnumerator movetospot(Character Chara)
    {
        if (!someoneIsMoving && CurrentActiveCharacter == null)
        {
            someoneIsMoving = true;
            while (Vector3.Distance(Chara.selfScriptRef.transform.position, ActiveCharSpot.transform.position) > closenessMargin)
            {
                Chara.selfScriptRef.transform.position = Vector3.MoveTowards(Chara.selfScriptRef.transform.position, ActiveCharSpot.transform.position, activationMovingSpeed * Time.deltaTime);
                yield return new WaitForSecondsRealtime(delayBetweenMovement);
            }
            if (!CurrentActiveCharacter.isEnemyCharacter)
            {
                ToggleCombatButtomVisibility(true);
            }
            someoneIsMoving = false;
        }
        
        
        
    }

    public void ReturnFromActiveSpot(Character chara)
    {
        HighlightCheck();
        while (Vector3.Distance(chara.selfScriptRef.transform.position, InitialActiveCharacterPositionCoordinates) > closenessMargin)
        {
            chara.selfScriptRef.transform.position = Vector3.MoveTowards(chara.selfScriptRef.transform.position, InitialActiveCharacterPositionCoordinates, activationMovingSpeed * Time.deltaTime);
        }
        if (!CurrentActiveCharacter.isEnemyCharacter)
        {
            ToggleCombatButtomVisibility(false);
        }
        CurrentActiveCharacter = null;


    }

    public void DoEnemyCharacterTurn(Character npc)
    {

        //Character toBeAttacked = MainData.playerParty[Random.Range(0, MainData.playerParty.Count + 1)];
        MoveToActiveSpot(npc);
        StartCoroutine(AttackVisuals(npc.selfScriptRef));



    }
}
