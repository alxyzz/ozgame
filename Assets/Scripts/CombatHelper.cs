using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static EntitiesDefinition;

public class CombatHelper : MonoBehaviour
{

    public GameObject TextObject;
    public float textFloatingDuration;
    public float textFloatSpeed;
    public float textOffset;
    [Space(15)]
    public float closenessMargin;
    public float activationMovingSpeed;
    public float delayBetweenMovement;
    [Space(15)]
    private Vector3 InitialActiveCharacterPositionCoordinates;//initial coordinates of character, to be moved back to after their action.
    public GameObject ActiveCharSpot; //where the character that is attacking will move.
    [Space(15)]
    private bool DoneWithThisTurn = false; // this is so the foreach loop waits for the player, or AI, to finish a singular miniturn in combat.
    private bool someoneIsMoving; //so we don't get people clicking two characters quickly and bugging stuff out
    public bool allHaveActed = true;






    [HideInInspector]
    public CharacterScript CurrentlyActiveChar; //each enemy on the level is composed of the game object, holding the script, associated with a Character instance that holds stats, names, descriptions etc
    [HideInInspector]
    public CharacterScript activeTarget; //player's target

    public void DisplayFloatingDamageNumbers(int damage, Character target)
    {
        TextMeshProUGUI ourtext = TextObject.GetComponent<TextMeshProUGUI>();
        ourtext.text = damage.ToString();
        if (damage > 50)
        {
            ourtext.text += "!";
            ourtext.color = Color.red;
            //switch color to red and add a !
        }

        TextObject.transform.position = Camera.main.WorldToScreenPoint(target.selfScriptRef.transform.position);
        StartCoroutine(FloatingTextVisuals(target));
    }

    IEnumerator FloatingTextVisuals(Character target)
    {
        TextObject.SetActive(true);
        for (int i = 0; i < 100; i++)
        {
            if (target.isPlayerPartyMember)
            {
                TextObject.transform.position = Vector3.MoveTowards(TextObject.transform.position, new Vector3(TextObject.transform.position.x + textOffset, TextObject.transform.position.y + textOffset, TextObject.transform.position.z), textFloatSpeed);
            }
            else
            {
                TextObject.transform.position = Vector3.MoveTowards(TextObject.transform.position, new Vector3(TextObject.transform.position.x - textOffset, TextObject.transform.position.y + textOffset, TextObject.transform.position.z), textFloatSpeed);
            }

            yield return new WaitForSeconds(0.02f); //should be enough for a nice, smooth movement
        }
        TextObject.SetActive(false);

        TextObject.GetComponent<TextMeshProUGUI>().color = Color.white;
    }


    // Update is called once per frame

    IEnumerator DoPatientCombatRound(List<Character> combatants)
    {//it waits for the current round to end, before it gives the next combatant the opportunity to fight.
        Debug.LogWarning("Now doing combat turn. Patiently.");
        for (int i = 0; i < combatants.Count - 1; i++)
        {
            if (i > 1)
            {//waits for the previous one to finish their turn before actually doing anything. We are polite, after all.
                yield return new WaitUntil(() => combatants[i - 1].hasActedThisTurn == true);
            }

            if (combatants[i].CheckIfCanAct())
            {
                Debug.LogWarning("Turn of - " + combatants[i].selfScriptRef.name);
                //MainData.MainLoop.SoundManagerComponent.sfxSource.PlayOneShot(item.turnSound); //plays the character specific noise/vocalization
                ///we do what is to be done
                MoveToActiveSpot(combatants[i].selfScriptRef);

                if (combatants[i].isPlayerPartyMember)
                {
                    DoPlayerCharacterTurn(combatants[i]);
                }
                else
                {
                    DoEnemyCharacterTurn(combatants[i]);
                }

                yield return new WaitUntil(() => DoneWithThisTurn == true);
                DoneWithThisTurn = false;

            }

        }
        allHaveActed = true;
        Debug.Log("Finished a combat round.");
        foreach (Character item in combatants)
        {//refreshes that boolean so we can act again next turn
            item.hasActedThisTurn = false;
        }
        MainData.MainLoop.PassTurn();
    }

    public void InitiateCombatTurn()
    {
        List<Character> combatants = MainData.allChars;
        //todo - what happens if someone dies during this? can foreach cope with it?
        //follow-up - ok, so you shouldn't modify a collection during the foreach. dying characters just get hidden and cleaned up behind the scene after the speed dependant turns

        combatants.Sort((x, y) => y.speed.CompareTo(x.speed)); // descending. swap y and x on the right side for ascending.
        StartCoroutine(DoPatientCombatRound(combatants));




    }





    public void ClickTraitAbility()
    {

        switch (CurrentlyActiveChar.associatedCharacter.charTrait.traitName)
        {
            case "blahblah"://so yeah this is where active traits go
                break;
            default:
                break;
        }
        DoneWithThisTurn = true;
        CurrentlyActiveChar.associatedCharacter.hasActedThisTurn = true;
        ToggleCombatButtomVisibility(false);
    }

    public void ClickNormalAttack()
    {
        if (CurrentlyActiveChar.associatedCharacter.isPlayerPartyMember)
        {
            if (activeTarget != null)
            {
                //StartCoroutine(AttackVisuals(activeTarget)); //does a nice attack effect, either hitting or knockback
                activeTarget.associatedCharacter.TakeDamageFromCharacter(CurrentlyActiveChar.associatedCharacter);
                DisplayFloatingDamageNumbers(CurrentlyActiveChar.associatedCharacter.damage, activeTarget.associatedCharacter);
                DoneWithThisTurn = true;
                CurrentlyActiveChar.associatedCharacter.hasActedThisTurn = true;//so the next character can begin their turn
                ToggleCombatButtomVisibility(false);

            }
            else
            {
                MainData.MainLoop.EventLoggingComponent.LogGray("You haven't selected a target!");
            }

        }
        else
        {
            MainData.MainLoop.EventLoggingComponent.LogDanger("It's not your turn!");
        }
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
            Character poorFool = MainData.livingPlayerParty[Random.Range(0, MainData.livingPlayerParty.Count + 1)];

            yield return new WaitForSecondsRealtime(0.3f);
            while (Vector3.Distance(toAnimate.transform.position, Initial) > 0.05f)
            {
                toAnimate.transform.position = Vector3.Lerp(toAnimate.transform.position, Initial, 0.5f * Time.deltaTime);
            }
        }


    }

    //public IEnumerator AttackVisuals(CharacterScript target)
    //{

    //    Vector3 Initial = CurrentlyActiveChar.transform.position;

    //    if (CurrentlyActiveChar.associatedCharacter.isPlayerPartyMember)
    //    {

    //    }
    //    else
    //    {
    //        Vector3 final = new Vector3(Initial.x - 3f, Initial.y, Initial.z);
    //        while (Vector3.Distance(CurrentlyActiveChar.transform.position, final) > 0.05f)
    //        {
    //            CurrentlyActiveChar.transform.position = Vector3.Lerp(CurrentlyActiveChar.transform.position, final, 0.5f * Time.deltaTime);
    //        }
    //        Character poorFool = MainData.playerParty[Random.Range(0, MainData.playerParty.Count + 1)];
    //        poorFool.TakeDamageFromCharacter(CurrentlyActiveChar.associatedCharacter); // for now, just a random attack
    //        StartCoroutine(HitKnockback(poorFool.selfScriptRef));
    //        //play some kinda sprite animation + sound effect here, perhaps
    //        yield return new WaitForSecondsRealtime(0.3f);

    //    }


    //}

    public void DoPlayerCharacterTurn(Character pc)
    {

        ToggleCombatButtomVisibility(true);
        MainData.controlsEnabled = true;
    }


    public void MoveToActiveSpot(CharacterScript chara)
    {
        if (CurrentlyActiveChar != null)
        {
            Debug.LogWarning("Could not move " + chara.gameObject.name + ", CurrentlyActiveChar is not null");
        }

        Debug.Log("MOVING " + chara.associatedCharacter.charName + " TO ACTIVE SPOT");
        MainData.MainLoop.CombatHelperComponent.CurrentlyActiveChar = chara;
        InitialActiveCharacterPositionCoordinates = chara.transform.position;
        //chara.transform.position = ActiveCharSpot.transform.position; //replace this with the sliding thing
        StartCoroutine(SlideTo(chara, ActiveCharSpot.transform.position, activationMovingSpeed));
        CurrentlyActiveChar = chara;




        if (!CurrentlyActiveChar.isEnemyCharacter)
        {
            ToggleCombatButtomVisibility(true);
        }
    }


    public void HighlightCheck()
    {
        //highlights the current target, checks if there is no target in which case it hides the highlight
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


    public IEnumerator SlideTo(CharacterScript Chara, Vector3 Destination, float speed)
    {//currently just moves you there for simplicity sake, later will make this animate the moving character and lerp/movetoward him there
        //yield return new WaitUntil(() => someoneIsMoving == false);
        //someoneIsMoving = true;
        while (Vector3.Distance(Chara.transform.position, Destination) > closenessMargin)
        {
            Chara.transform.position = Vector3.MoveTowards(Chara.transform.position, Destination, speed * Time.deltaTime);
            yield return new WaitForSecondsRealtime(delayBetweenMovement);
        }
        //someoneIsMoving = false;
    }



    //IEnumerator QueueSliding(CharacterScript Chara, Vector3 Destination, float speed)
    //{

    //    yield return new WaitUntil(() => someoneIsMoving == false);
    //    SlideTo(Chara, Destination, speed);
    //}


    public void ReturnFromActiveSpot()
    { //this does not need an argument, since it always works with the currently active character
        //if (!someoneIsMoving)
        //{
        //    someoneIsMoving = true;
        StartCoroutine(SlideTo(CurrentlyActiveChar, InitialActiveCharacterPositionCoordinates, activationMovingSpeed));
        // CurrentlyActiveChar.transform.position = InitialActiveCharacterPositionCoordinates;
        if (!CurrentlyActiveChar.isEnemyCharacter)
        {
            activeTarget = null;
            ToggleCombatButtomVisibility(false);
        }
        CurrentlyActiveChar = null;
        HighlightCheck();
        //someoneIsMoving = false;
        //}
    }

    public void DoEnemyCharacterTurn(Character npc)
    {
        ToggleCombatButtomVisibility(false);
        //Character toBeAttacked = MainData.playerParty[Random.Range(0, MainData.playerParty.Count + 1)];
        //StartCoroutine(SlideTo(npc.selfScriptRef, ActiveCharSpot.transform.position, activationMovingSpeed));

        StartCoroutine(SlideTo(CurrentlyActiveChar, ActiveCharSpot.transform.position, activationMovingSpeed));
        //npc.selfScriptRef.transform.position = ActiveCharSpot.transform.position;

        StartCoroutine(AttackRandomEnemy(npc.selfScriptRef));


    }

    public IEnumerator AttackRandomEnemy(CharacterScript chara)
    {
        int b = Random.Range(0, MainData.livingPlayerParty.Count);
        Character Fool = MainData.livingPlayerParty[b];
        Debug.Log("attacking player at playerParty[" + b.ToString() + "]!");
        Character currentChar = CurrentlyActiveChar.associatedCharacter;
        Fool.TakeDamageFromCharacter(chara.associatedCharacter);
        yield return new WaitForSeconds(1f);
        ReturnFromActiveSpot();
        DoneWithThisTurn = true;
        DisplayFloatingDamageNumbers(chara.associatedCharacter.damage, Fool);

    }

    //public void EnemyAttack(Character fool)
    //{
    //    Character currentChar = CurrentlyActiveChar.associatedCharacter;
    //    Debug.Log(currentChar.charName + " has attacked " + fool.charName + " for " + currentChar.damage + " damage.");
    //    fool.TakeDamageFromCharacter(currentChar);
    //    DisplayFloatingDamageNumbers(currentChar.damage, fool);
    //    ReturnFromActiveSpot();
    //    DoneWithThisTurn = true;
    //}


}
