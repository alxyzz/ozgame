using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static EntityDefiner;

public class CombatHelper : MonoBehaviour
{

    public float textFloatingDuration;
    public float textFloatSpeed;
    public float textOffset;
    [Space(15)]
    public float closenessMargin;
    public float activationMovingSpeed;
    public float delayBetweenMovement;
    [Space(15)]
    public GameObject ActiveCharSpot; //where the character that is attacking will move.
    [Space(15)]
    public bool allHaveActed = true;
    private Color friendlyColor = new Color(0f, 1f, 0.145f, 0.5f); //for targeting
    private Color enemyColor = new Color(1f, 0f, 0f, 0.5f);
    public float threatDecayPerTurn;

    [Header("Animation stuff.")]
    public float animationDuration;



    [HideInInspector]
    public CharacterWorldspaceScript activeCharacterWorldspaceObject; //each enemy on the level is composed of the game object, holding the script, associated with a Character instance that holds stats, names, descriptions etc
    [HideInInspector]
    public CharacterWorldspaceScript activeTarget; //player's target
    [HideInInspector]
    public bool isTargetFriendly;







    public GameObject DamageIndicatorCanvas;

    public void DisplayFloatingDamageNumbers(int damage, Character target, bool heal)
    {

        GameObject TextObject = ObjectPooling.Instance.SpawnFromPool("damage_indicator", Camera.main.WorldToScreenPoint(target.selfScriptRef.transform.position), Quaternion.identity);

        TextObject.transform.SetParent(DamageIndicatorCanvas.transform);
        TextMeshProUGUI ourtext = TextObject.GetComponent<TextMeshProUGUI>();
        if (heal)
        {
            ourtext.color = new Color(0.701f, 1f, 0.745f);
        }
        else
        {
            ourtext.color = new Color(0.996f, 0.380f, 0.345f);
        }
        ourtext.text = damage.ToString();
        if (damage > 50)
        {
            ourtext.text += "!";
            ourtext.color = Color.red;
            //switch color to red and add a !
        }

        StartCoroutine(FloatingTextVisuals(target, TextObject));
    }

    IEnumerator FloatingTextVisuals(Character target, GameObject TextObject)
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
    IEnumerator DoPatientCombatRound(List<Character> combatants)
    {//it waits for the current round to end, before it gives the next combatant the opportunity to fight.
        Debug.LogWarning("Now doing combat turn. Patiently.");
        for (int i = 0; i < combatants.Count - 1; i++)
        {
            MainData.MainLoop.EventLoggingComponent.LogDanger("It is now " + combatants[i].charName + "'s turn!");


            if (i > 0)
            {//waits for the previous one to finish their turn before actually doing anything. We are polite, after all.
                yield return new WaitUntil(() => combatants[i - 1].hasActedThisTurn == true);
            }

            if (MainData.livingEnemyParty.Count < 1)
            {//just an additional check for enemies so we don't waste player's time
                allHaveActed = true;
                Debug.Log("Finished a combat round.");
                ToggleCombatButtomVisibility(false);
                foreach (Character item in combatants)
                {//refreshes that boolean so we can act again next turn
                    item.hasActedThisTurn = false;
                    item.selfScriptRef.transform.position = item.InitialPosition; // JUST in case we get a straggler, resets their position to where they should be. This works because we don't actually move in the environment, we just move the world around us like a boss. solipsism vibe?
                }
                MainData.MainLoop.PassTurn();
                yield break;
            }
            if (combatants[i].canAct)
            {

                //StaticDataHolder.MainLoop.SoundManagerComponent.sfxSource.PlayOneShot(item.turnSound); //plays the character specific noise/vocalization
                ///we do what is to be done

                activeCharacterWorldspaceObject = combatants[i].selfScriptRef;
                MoveToActiveSpot(activeCharacterWorldspaceObject);

                Debug.LogWarning("Moving to active spot - " + activeCharacterWorldspaceObject.associatedCharacter.charName);

                if (combatants[i].isPlayerPartyMember)
                {
                    //enable controls here
                    DoPlayerCharacterTurn(combatants[i]);
                }
                else
                {
                    DoEnemyCharacterTurn(combatants[i]);
                }


                yield return new WaitUntil(() => combatants[i].hasActedThisTurn == true);
                //disable controls here


                yield return new WaitForSecondsRealtime(0.4f);


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
        MainData.MainLoop.LevelHelperComponent.MoveStop();

        List<Character> combatants = MainData.allChars;
        //todo - what happens if someone dies during this? can foreach cope with it?
        //follow-up - ok, so you shouldn't modify a collection during the foreach. dying characters just get flagged, hidden and cleaned up behind the scene after the speed dependant turns

        foreach (Character item in MainData.livingPlayerParty)
        {
            item.RecalculateStatsFromTraits();
            item.RecalculateThreatFromStats();
            
        }
        combatants.Sort((x, y) => y.speed.CompareTo(x.speed)); // descending. swap y and x on the right side for ascending.

        StartCoroutine(DoPatientCombatRound(combatants));
    }

    public void ClickPlayButton()
    {
        if (MainData.livingEnemyParty.Count < 1)
        {
            //play a tick sound here
            MainData.MainLoop.EventLoggingComponent.LogGray("You're facing no enemies at the moment.");
            return;
        }
        if (activeCharacterWorldspaceObject != null)//if a turn is still in progress, what should this button do? - answer - nothing.
        {

            //play a tick sound here
            foreach (Character item in MainData.livingEnemyParty)
            {
                MainData.MainLoop.EventLoggingComponent.LogGray(item.charName + " is still alive.");
            }
            MainData.MainLoop.EventLoggingComponent.LogGray("Turn in progress.");
            return;
        }
        if (MainData.MainLoop.CombatHelperComponent.allHaveActed)
        {

            MainData.MainLoop.PassTurn();
        }
        else
        {
            MainData.MainLoop.EventLoggingComponent.Log("Current turn not finished.");
        }

        //add a click sound here
    }






    public void ClickTraitAbility()
    {

        GameManager gameloop = MainData.MainLoop;

        if (!activeCharacterWorldspaceObject.associatedCharacter.charTrait.forceCooldown && (activeCharacterWorldspaceObject.associatedCharacter.mana >= activeCharacterWorldspaceObject.associatedCharacter.charTrait.manaCost))
        {
            switch (activeCharacterWorldspaceObject.associatedCharacter.charTrait.traitName)
            {
                case "caring"://so yeah this is where active traits go
                    //heal target. allied target.
                    if (activeTarget = null) { return; }
                    if (activeTarget.associatedCharacter = null) { return; }

                    if (activeTarget.associatedCharacter.isPlayerPartyMember)
                    {

                        if (activeTarget.associatedCharacter.currentHealth == activeTarget.associatedCharacter.maxHealth)
                        {
                            gameloop.EventLoggingComponent.Log(activeCharacterWorldspaceObject.associatedCharacter.charName + "'s hugs " + activeTarget.associatedCharacter.charName + "! " + activeTarget.associatedCharacter.charName + " feels much better!");
                            activeTarget.associatedCharacter.GainHealth(0);
                            EndCurrentTurn();

                        }
                        else
                        {
                            activeTarget.associatedCharacter.GainHealth(gameloop.DesignerEasyTweakProPremium.caringActiveHealing);
                            gameloop.EventLoggingComponent.Log(activeCharacterWorldspaceObject.associatedCharacter.charName + "'s caring nature mends " + activeTarget.associatedCharacter.charName + "'s wounds!");
                            activeCharacterWorldspaceObject.associatedCharacter.mana -= activeCharacterWorldspaceObject.associatedCharacter.charTrait.manaCost;
                            EndCurrentTurn();
                        }

                    }
                    else
                    {
                        MainData.MainLoop.EventLoggingComponent.LogGray("Even " + activeCharacterWorldspaceObject.associatedCharacter.charName + " is wise enough not to waste their power on an enemy.");
                        return;
                    }
                    break;

                case "greed":
                    //sell off HP for gold
                    if (activeCharacterWorldspaceObject.associatedCharacter.currentHealth > gameloop.DesignerEasyTweakProPremium.greedActiveSelfDamage)
                    {

                        gameloop.EventLoggingComponent.Log(activeCharacterWorldspaceObject.associatedCharacter.charName + "'s sells off their vital energy for " + gameloop.DesignerEasyTweakProPremium.greedActiveRevenue + " coins!");
                        activeCharacterWorldspaceObject.associatedCharacter.TakeDamage(gameloop.DesignerEasyTweakProPremium.greedActiveSelfDamage);
                        gameloop.Currency += MainData.MainLoop.DesignerEasyTweakProPremium.greedActiveRevenue;
                        gameloop.UserInterfaceHelperComponent.UpdateCurrencyCounter();
                        EndCurrentTurn();
                    }
                    else
                    {
                        gameloop.EventLoggingComponent.Log("Too wounded! " + activeCharacterWorldspaceObject.associatedCharacter.charName + " is still not ready to sell their soul completely.");
                        EndCurrentTurn();
                    }
                    break;

                case "angry":
                    //Lash out for massive damage.
                    if (activeTarget = null) { return; }
                    if (activeTarget.associatedCharacter = null) { return; }
                    activeCharacterWorldspaceObject.associatedCharacter.damageMax -= gameloop.DesignerEasyTweakProPremium.angryActiveDamageMalus;
                    activeCharacterWorldspaceObject.associatedCharacter.damageMin -= gameloop.DesignerEasyTweakProPremium.angryActiveDamageMalus;

                    activeTarget.associatedCharacter.TakeDamage(gameloop.DesignerEasyTweakProPremium.angryActivePowerDamage);
                    EndCurrentTurn();

                    break;



                default:
                    break;
            }
        }

        EndCurrentTurn();
    }





    public void EndCombat()
    {
        MainData.MainLoop.EventLoggingComponent.Log("Combat is over.");
        MainData.MainLoop.LevelHelperComponent.ButtonMoveOn.SetActive(true);
        PurgeAllStatusEffects();
        MainData.MainLoop.UserInterfaceHelperComponent.ToggleFightButtonVisiblity(false);
    }

    /// <summary>
    /// purges every player party member of their status effects after combat according to the current design
    /// </summary>
    private void PurgeAllStatusEffects()
    {


        foreach (Character item in MainData.allChars)
        {
            item.currentStatusEffects.Clear();
        }
    }
    /// <summary>
    /// this occurs after the active character, enemy or player, has acted.
    /// </summary>
    public void EndCurrentTurn()
    {
        activeCharacterWorldspaceObject.associatedCharacter.hasActedThisTurn = true;
        ReturnFromActiveSpot(); //we send the character back in this moment.

        if (MainData.livingEnemyParty.Count < 1)
        {
            EndCombat();
        }

        if (activeCharacterWorldspaceObject.isEnemyCharacter)
        {
            activeTarget = null;
            ToggleCombatButtomVisibility(false);
        }
        TargetSelectionCheck();
        activeCharacterWorldspaceObject.associatedCharacter.hasActedThisTurn = true;
        activeCharacterWorldspaceObject = null;
        DecayThreat();

    }


    /// <summary>
    /// decays threat for each player party member.
    /// </summary>
    private void DecayThreat()
    {
        foreach (Character item in MainData.livingPlayerParty)
        {
            item.Threat -= threatDecayPerTurn;
            if (item.Threat < 0)
            {
                item.Threat = 0;
            }
        }

    }

    public void ClickNormalAttack()
    {
        if (activeCharacterWorldspaceObject == null)
        {//this shouldn't happen because the game itself picks a character to move
            MainData.MainLoop.EventLoggingComponent.LogDanger("currently active char is null");
            return;
        }
        if (activeCharacterWorldspaceObject.associatedCharacter.isPlayerPartyMember)
        {
            if (activeTarget != null)
            {
                //StartCoroutine(AttackVisuals(activeTarget)); //does a nice attack effect, either hitting or knockback
                //now we will check if somehow the target is dead.
                if (activeTarget.associatedCharacter == null || activeTarget.associatedCharacter.isDead || activeTarget.associatedCharacter.isPlayerPartyMember)
                {
                    activeTarget = null;
                    return;
                }
                ToggleCombatButtomVisibility(false);
                StartCoroutine(AttackTargetedEnemy());

            }
            else
            {
                MainData.MainLoop.EventLoggingComponent.LogGray("You haven't selected a valid target!");
            }
        }
        else
        {
            MainData.MainLoop.EventLoggingComponent.LogDanger("It's not your turn!");
        }
    }



    public IEnumerator AttackTargetedEnemy()
    {

        //CurrentlyActiveChar.transform.position = ActiveCharSpot.transform.position; //MOVE CHAR TO SPOT

        Character Fool = activeTarget.associatedCharacter;

        Debug.Log("attacking enemy. Currently active character is " + activeCharacterWorldspaceObject.associatedCharacter.charName);

        for (int i = 0; i < activeCharacterWorldspaceObject.associatedCharacter.charSprite.Length; i++)
        {
            activeCharacterWorldspaceObject.spriteRenderer.sprite = activeCharacterWorldspaceObject.associatedCharacter.charSprite[i];
            yield return new WaitForSecondsRealtime(0.04f);
        }
        activeCharacterWorldspaceObject.spriteRenderer.sprite = activeCharacterWorldspaceObject.associatedCharacter.charSprite[0];
        //play attack animation here


        Fool.TakeDamageFromCharacter(activeCharacterWorldspaceObject.associatedCharacter);//this also handles damage indicator



        yield return new WaitForSeconds(0.5f);

        ReturnFromActiveSpot(); //return him duh sillypants
        activeCharacterWorldspaceObject.associatedCharacter.hasActedThisTurn = true;
        EndCurrentTurn();

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

    public IEnumerator HitKnockback(CharacterWorldspaceScript toAnimate)
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
    public void DoPlayerCharacterTurn(Character pc)
    {
        activeCharacterWorldspaceObject.transform.position = ActiveCharSpot.transform.position; //MOVE CHAR TO SPOT
        ToggleCombatButtomVisibility(true);
        MainData.controlsEnabled = true;
    }


    public void MoveToActiveSpot(CharacterWorldspaceScript chara)
    {
        if (activeCharacterWorldspaceObject != null)
        {
            Debug.LogWarning("Could not move " + chara.gameObject.name + ", CurrentlyActiveChar is not null");
            return;
        }

        chara.transform.position = ActiveCharSpot.transform.position; //replace this with the sliding thing

        //StartCoroutine(SlideTo(chara, ActiveCharSpot.transform.position, activationMovingSpeed, false));
        if (!activeCharacterWorldspaceObject.isEnemyCharacter)
        {
            ToggleCombatButtomVisibility(true);
        }
    }


    public void TargetSelectionCheck()
    {
        if (activeTarget == null)
        {
            return;
        }
        if (activeTarget.associatedCharacter == null)
        {
            return;
        }

        GameObject highli = MainData.MainLoop.UserInterfaceHelperComponent.CombatHighlightObject;
        //highlights the current target, checks if there is no target in which case it hides the highlight
        if (!activeTarget.associatedCharacter.isDead || activeTarget.associatedCharacter.canAct)
        {


            highli.transform.position = activeTarget.transform.position;
            highli.SetActive(true);

        }
        else
        {
            highli.SetActive(false);
            return;
        }
        SpriteRenderer targeterSprite = highli.GetComponent<SpriteRenderer>();
        if (!activeTarget.associatedCharacter.isPlayerPartyMember)
        {
            targeterSprite.color = enemyColor;
            Debug.Log("Color modified.Probably.");
        }
        else
        {
            targeterSprite.color = friendlyColor;
        }

    }


    public IEnumerator SlideTo(CharacterWorldspaceScript Chara, Vector3 Destination, float speed, bool returning)
    {

        while (Vector3.Distance(Chara.transform.position, Destination) > closenessMargin)
        {
            Chara.transform.position = Vector3.Lerp(Chara.transform.position, Destination, speed * Time.deltaTime);
            yield return new WaitForSecondsRealtime(delayBetweenMovement);
        }
        if (returning) //
        {//so it does these AFTER it finishes moving, coz its async
            TargetSelectionCheck();
            activeCharacterWorldspaceObject.associatedCharacter.hasActedThisTurn = true;
            activeCharacterWorldspaceObject = null;
        }

    }


    public void ReturnFromActiveSpot()
    { //this does not need an argument, since it always works with the currently active character
        activeCharacterWorldspaceObject.transform.position = activeCharacterWorldspaceObject.associatedCharacter.InitialPosition;//yaaaay
        Debug.Log("Just returned from active spot to coordinates " + activeCharacterWorldspaceObject.associatedCharacter.InitialPosition.ToString());
    }






    public void DoEnemyCharacterTurn(Character npc)
    {
        ToggleCombatButtomVisibility(false);
        //Character toBeAttacked = StaticDataHolder.playerParty[Random.Range(0, StaticDataHolder.playerParty.Count + 1)];
        //StartCoroutine(SlideTo(npc.selfScriptRef, ActiveCharSpot.transform.position, activationMovingSpeed));

        //StartCoroutine(SlideTo(CurrentlyActiveChar, ActiveCharSpot.transform.position, activationMovingSpeed, false));
        npc.selfScriptRef.transform.position = ActiveCharSpot.transform.position;

        StartCoroutine(AttackRandomEnemy(npc.selfScriptRef));


    }

    public IEnumerator AttackRandomEnemy(CharacterWorldspaceScript chara)
    {
        int b = Random.Range(0, MainData.livingPlayerParty.Count);
        Character Fool = MainData.livingPlayerParty[b];

        Debug.Log("attacking player at playerParty[" + b.ToString() + "]!");

        Fool.TakeDamageFromCharacter(chara.associatedCharacter);//this also handles the damage indicator 
        for (int i = 0; i < activeCharacterWorldspaceObject.associatedCharacter.charSprite.Length; i++)
        {
            activeCharacterWorldspaceObject.spriteRenderer.sprite = activeCharacterWorldspaceObject.associatedCharacter.charSprite[i];
            yield return new WaitForSecondsRealtime(0.06f);
        }
        activeCharacterWorldspaceObject.spriteRenderer.sprite = activeCharacterWorldspaceObject.associatedCharacter.charSprite[0];
        //play attack animation here
        yield return new WaitForSeconds(1f);

        ReturnFromActiveSpot();
        activeCharacterWorldspaceObject.associatedCharacter.hasActedThisTurn = true;


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
