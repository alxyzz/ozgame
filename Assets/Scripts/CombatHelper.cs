using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static EntityDefiner;

public class CombatHelper : MonoBehaviour
{
    // private bool endNext = false;
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








    /// <summary>
    /// 
    /// </summary>
    /// <param name="target">target from where the indicator floats</param>
    /// <param name="message"> custom message for various things</param>
    /// <param name="damage"> damage if relevant</param>
    /// <param name="heal"> wether it's damage or healing. affects color</param>
    public void DisplayFloatingDamageNumbers(Character target, string message = null, int damage = 0, bool heal = false)
    {

        GameObject TextObject = ObjectPooling.Instance.SpawnFromPool("damage_indicator", Camera.main.WorldToScreenPoint(target.selfScriptRef.transform.position), Quaternion.identity);

        TextObject.transform.SetParent(DamageIndicatorCanvas.transform);
        TextMeshProUGUI ourtext = TextObject.GetComponent<TextMeshProUGUI>();
        if (message != null)
        {
            ourtext.color = Color.white;
            ourtext.text = message;
            if (damage != 0)
            {//if there's damage too, put it on the next line
                ourtext.text += "\n" + damage;
            }
            if (heal)
            {
                ourtext.color = new Color(0.701f, 1f, 0.745f);
            }
            else
            {
                ourtext.color = new Color(0.996f, 0.380f, 0.345f);
            }
        }
        else
        {
            if (heal)
            {
                ourtext.color = new Color(0.701f, 1f, 0.745f);
            }
            else
            {
                ourtext.color = new Color(0.996f, 0.380f, 0.345f);
            }
            ourtext.text = damage.ToString();
            if (damage > 50 && !heal)
            {
                ourtext.text += "!";
                ourtext.color = Color.red;
                //switch color to red and add a !
            }
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
        if (AreEnemiesDead())
        {
            EndCombat();
        }

        for (int i = 0; i < combatants.Count - 1; i++)
        {
            if (MainData.livingEnemyParty.Count != 1 && !MainData.livingEnemyParty[0].isDead)
            {
                if (combatants[i].charTrait != null)
                {
                    MainData.MainLoop.EventLoggingComponent.LogDanger("It is now " + combatants[i].charName + " the " + combatants[i].charTrait.adjective + "'s turn!");
                }
                else
                {
                    MainData.MainLoop.EventLoggingComponent.LogDanger("It is now " + combatants[i].charName + "'s turn!");
                }
            }




            if (i > 0)
            {//waits for the previous one to finish their turn before actually doing anything. We are polite, after all.
                yield return new WaitUntil(() => combatants[i - 1].hasActedThisTurn == true);
            }


            

            if (combatants[i].isDead || !combatants[i].canAct || MainData.livingEnemyParty.Count < 1)
            {//

                allHaveActed = true;
                ToggleCombatButtomVisibility(false);
                EndCurrentTurn();
                foreach (Character item in combatants)
                {//refreshes that boolean so we can act again next turn
                    item.hasActedThisTurn = false;
                    item.selfScriptRef.transform.position = item.InitialPosition; // JUST in case we get a straggler, resets their position to where they should be. This works because we don't actually move in the environment, we just move the world around us like a boss.
                }
                MainData.MainLoop.PassTurn();
                yield break;
            }//
            if (combatants[i].canAct || !combatants[i].isDead)
            {

                //StaticDataHolder.MainLoop.SoundManagerComponent.sfxSource.PlayOneShot(item.turnSound); //plays the character specific noise/vocalization
                ///we do what is to be done

                activeCharacterWorldspaceObject = combatants[i].selfScriptRef;
                MoveToActiveSpot(activeCharacterWorldspaceObject);

                List<Character> results = new List<Character>();
                results = MainData.livingEnemyParty.FindAll(x => x.isDead == false);
                //if (results.Count == 1)
                //{
                //    endNext = true;
                //}
                if (combatants[i].isPlayerPartyMember)
                {
                    if (results.Count > 0)
                    {
                        DoPlayerCharacterTurn(combatants[i]);
                    }
                    else
                    {


                        allHaveActed = true;
                        EndCurrentTurn();

                    }
                }
                else
                {
                    DoEnemyCharacterTurn(combatants[i]);
                }

                if (combatants[i].isDead || !combatants[i].canAct)
                {
                    combatants[i].hasActedThisTurn = true;
                    continue;
                }
                yield return new WaitUntil(() => combatants[i].hasActedThisTurn == true || combatants[i] == null);
                //disable controls here


                yield return new WaitForSecondsRealtime(0.1f);
            }
            else
            {
                EndCombat();
            }





            //MainData.MainLoop.EventLoggingComponent.LogDanger("we're here!");
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
        if (MainData.livingPlayerParty.Count == 0)
        {
            MainData.MainLoop.LostTheGame();
            return;
        }
        //foreach (Character item in MainData.livingPlayerParty)
        //{ //cleans up dead ppl
        //    item.HandleListsUponDeath();
        //}



        MainData.MainLoop.LevelHelperComponent.MoveStop();
        List<Character> combatants = MainData.allChars;
        foreach (Character item in MainData.livingPlayerParty)
        {
            item.RecalculateStatsFromTraits();
            //item stats are dealt with on being equipped anyways
            item.RegenerateMana();
            item.RecalculateThreatFromStats();
        }
        MainData.MainLoop.UserInterfaceHelperComponent.RefreshCharacterTabs();
        string b = "";
        combatants.Sort((x, y) => y.GetCompoundSpeed().CompareTo(x.GetCompoundSpeed()));
        foreach (Character item in combatants)
        {
            b += item.charName + "\n";
        }
        Debug.LogWarning(b);
        StartCoroutine(DoPatientCombatRound(combatants));
    }


    private bool AreEnemiesDead()
    {

        foreach (Character item in MainData.livingEnemyParty)
        {
            if (!item.isDead)
            {
                return false;
            }
        }
        return true;


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
            MainData.MainLoop.EventLoggingComponent.LogGray("Turn in progress. Active char is " + activeCharacterWorldspaceObject.associatedCharacter.charName);
            if (activeCharacterWorldspaceObject.associatedCharacter.isPlayerPartyMember)
            {
                DoPlayerCharacterTurn(activeCharacterWorldspaceObject.associatedCharacter);
            }
            else
            {
                DoEnemyCharacterTurn(activeCharacterWorldspaceObject.associatedCharacter);
            }
            MainData.MainLoop.UserInterfaceHelperComponent.ToggleFightButtonVisiblity(false);
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
        Debug.LogError("the active target is " + activeTarget.associatedCharacter.charName);
        GameManager gameloop = MainData.MainLoop;
        Character Caster = activeCharacterWorldspaceObject.associatedCharacter;
        if (!Caster.charTrait.forceCooldown) //Check if cooldown is forced
        {
            if (Caster.manaTotal < Caster.charTrait.manaCost)// we check wether we have enough mana here.
            {
                MainData.MainLoop.EventLoggingComponent.Log(Caster.charName + " does not have enough mana to express their " + Caster.charTrait.traitName + "!");
                //a failure sound, here
                return;
            }

            switch (Caster.charTrait.identifier)
            {
                case "caring"://so yeah this is where active traits go
                    //heal target. allied target.
                    if (activeTarget == null)
                    {
                        Debug.LogError("Target was null. Can't use caring.");
                        return;
                    }
                    if (activeTarget.associatedCharacter == null)
                    {
                        Debug.LogError("Target's assoc char was null. Can't use caring.");
                        return;
                    }



                    if (activeTarget.associatedCharacter.isPlayerPartyMember)
                    {

                        if (activeTarget.associatedCharacter.currentHealth >= activeTarget.associatedCharacter.maxHealth)
                        {
                            gameloop.EventLoggingComponent.Log(Caster.charName + "'s hugs " + activeTarget.associatedCharacter.charName + "! " + activeTarget.associatedCharacter.charName + " feels much better!");
                            activeTarget.associatedCharacter.GainHealth(0);
                            EndCurrentTurn();

                        }
                        else
                        {//heals for a percentage of max health
                            int healing = (activeTarget.associatedCharacter.maxHealth / 100) * (MainData.MainLoop.TweakingComponent.caringActiveHealing);
                            Debug.LogError("healing is " + healing.ToString());
                            gameloop.EventLoggingComponent.Log(activeCharacterWorldspaceObject.associatedCharacter.charName + "'s caring nature mends " + activeTarget.associatedCharacter.charName + "'s wounds for " + healing + " health!");
                            activeTarget.associatedCharacter.GainHealth(healing);
                            Caster.manaTotal -= Caster.charTrait.manaCost;
                            EndCurrentTurn();
                        }

                    }
                    else
                    {
                        MainData.MainLoop.EventLoggingComponent.LogGray("Even " + Caster.charName + " is wise enough not to waste their power on an enemy.");
                        return;
                    }
                    break;

                case "greed":
                    //sell off HP for gold
                    if (Caster.currentHealth > gameloop.TweakingComponent.greedActiveSelfDamage)
                    {

                        gameloop.EventLoggingComponent.Log(Caster.charName + "'s sells off their vital energy for " + gameloop.TweakingComponent.greedActiveRevenue + " coins!");
                        Caster.TakeDamage(gameloop.TweakingComponent.greedActiveSelfDamage);
                        gameloop.Currency += MainData.MainLoop.TweakingComponent.greedActiveRevenue;
                        gameloop.UserInterfaceHelperComponent.UpdateCurrencyCounter();
                        Caster.manaTotal -= Caster.charTrait.manaCost;
                        EndCurrentTurn();
                    }
                    else
                    {
                        gameloop.EventLoggingComponent.Log("Too wounded! " + Caster.charName + " is still not ready to sell their soul completely.");

                        EndCurrentTurn();
                    }
                    break;

                case "angry":
                    //Lash out for massive damage.
                    if (activeTarget == null) { return; }
                    if (activeTarget.associatedCharacter == null) { return; }
                    Caster.damageMax -= gameloop.TweakingComponent.angryActiveDamageMalus;
                    Caster.damageMin -= gameloop.TweakingComponent.angryActiveDamageMalus;
                    gameloop.EventLoggingComponent.Log(Caster.charName + " lashes out! " + activeTarget.associatedCharacter.charName + " takes " + (gameloop.TweakingComponent.angryActivePowerDamage + (Caster.damageMax * 2)) + " damage!");
                    activeTarget.associatedCharacter.TakeDamage(gameloop.TweakingComponent.angryActivePowerDamage + (Caster.damageMax * 2));
                    Caster.manaTotal -= Caster.charTrait.manaCost;
                    EndCurrentTurn();

                    break;
                case "wrath":
                    //double attack
                    if (activeTarget == null) { return; }
                    if (activeTarget.associatedCharacter == null) { return; }
                    //activeCharacterWorldspaceObject.associatedCharacter.damageMax -= gameloop.TweakingComponent.angryActiveDamageMalus; //permanent damage loss
                    //activeCharacterWorldspaceObject.associatedCharacter.damageMin -= gameloop.TweakingComponent.angryActiveDamageMalus;
                    gameloop.EventLoggingComponent.Log(activeCharacterWorldspaceObject.associatedCharacter.charName + " wrathful nature provokes a double attack!");
                    activeTarget.associatedCharacter.TakeDamageFromCharacter(activeCharacterWorldspaceObject.associatedCharacter);
                    if (MainData.livingEnemyParty.Count > 0)
                    {//in case the enemy just gets killed immediately
                        if (activeTarget != null)
                        {
                            if (activeTarget.associatedCharacter != null)
                            {
                                activeTarget.associatedCharacter.TakeDamageFromCharacter(activeCharacterWorldspaceObject.associatedCharacter);
                            }

                        }

                    }
                    activeCharacterWorldspaceObject.associatedCharacter.manaTotal -= activeCharacterWorldspaceObject.associatedCharacter.charTrait.manaCost;
                    EndCurrentTurn();

                    break;

                case "bulwark":
                    //sell off HP for gold
                        Caster.defense += 1;
                        Caster.threatBonus = 999;
                        Debug.Log(Caster.threatBonus + " THREAT BONUS");
                        gameloop.EventLoggingComponent.Log(Caster.charName + " taunts the enemies and prepares for combat!");
                        Caster.manaTotal -= Caster.charTrait.manaCost;
                        EndCurrentTurn();
                    break;

                case "nurturing"://so yeah this is where active traits go
                    //heal target. allied target.
                    if (activeTarget == null)
                    {
                        Debug.LogError("Target was null. Can't use caring.");
                        return;
                    }
                    if (activeTarget.associatedCharacter == null)
                    {
                        Debug.LogError("Target's assoc char was null. Can't use caring.");
                        return;
                    }

                    if (activeTarget.associatedCharacter.isPlayerPartyMember)
                    {
                        //heals for a percentage of max health
                            int healing = MainData.MainLoop.TweakingComponent.nurtureActiveHealing;
                            Debug.LogError("healing is " + healing.ToString());
                            gameloop.EventLoggingComponent.Log(activeCharacterWorldspaceObject.associatedCharacter.charName + "'s inspiration helps " + activeTarget.associatedCharacter.charName + ", healing them for " + healing + " health!");
                            activeTarget.associatedCharacter.GainHealth(healing);
                            activeCharacterWorldspaceObject.associatedCharacter.GainHealth(healing);
                            Caster.manaTotal -= Caster.charTrait.manaCost;
                            EndCurrentTurn();
                    }
                    else
                    {
                        MainData.MainLoop.EventLoggingComponent.LogGray(Caster.charName + " can't inspire something without a mind.");
                        return;
                    }
                    break;

                default:
                    break;
            }
            //take the mana for the trait use.
            MainData.MainLoop.UserInterfaceHelperComponent.RefreshCharacterTabs();
        }

        EndCurrentTurn();
    }
    public void EndCombat()
    {
        MainData.MainLoop.EventLoggingComponent.Log("Combat is over.");
        MainData.MainLoop.LevelHelperComponent.ButtonMoveOn.SetActive(true);
        List<Character> clone = new List<Character>(MainData.allChars);
        foreach (Character item in clone)
        {
            item.HandleListsUponDeath();
        }

        PurgeAllStatusEffects();
        MainData.MainLoop.UserInterfaceHelperComponent.ToggleFightButtonVisiblity(false);
        MainData.MainLoop.inCombat = false;
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
        ReturnFromActiveSpot(); //we send the character back in this moment.
        foreach (Character item in MainData.allChars)
        {
            item.selfScriptRef.transform.position = item.InitialPosition;
        }

        if (activeCharacterWorldspaceObject != null)
        {
            if (activeCharacterWorldspaceObject.associatedCharacter != null)
            {
                activeCharacterWorldspaceObject.associatedCharacter.hasActedThisTurn = true;
            }

        }
        if (activeCharacterWorldspaceObject != null)
        {
            activeCharacterWorldspaceObject.associatedCharacter.hasActedThisTurn = true;
        }
        ToggleCombatButtomVisibility(false);
        TargetSelectionCheck();

        activeCharacterWorldspaceObject = null;
        DecayThreat();


        if (MainData.livingEnemyParty.Count >= 1)
        {

        }



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
        activeCharacterWorldspaceObject.ToggleIdle(false);
        //CurrentlyActiveChar.transform.position = ActiveCharSpot.transform.position; //MOVE CHAR TO SPOT

        Character Fool = activeTarget.associatedCharacter;

        Debug.Log("attacking enemy. Currently active character is " + activeCharacterWorldspaceObject.associatedCharacter.charName);
        //animation

        for (int i = 0; i < activeCharacterWorldspaceObject.associatedCharacter.attackAnimation.Length; i++)
        {
            activeCharacterWorldspaceObject.spriteRenderer.sprite = activeCharacterWorldspaceObject.associatedCharacter.attackAnimation[i];
            yield return new WaitForSecondsRealtime(0.04f);
        }
        if (activeCharacterWorldspaceObject.associatedCharacter.standingSprite != null)
        {
            activeCharacterWorldspaceObject.spriteRenderer.sprite = activeCharacterWorldspaceObject.associatedCharacter.standingSprite;

        }
        else
        {
            activeCharacterWorldspaceObject.spriteRenderer.sprite = activeCharacterWorldspaceObject.associatedCharacter.attackAnimation[0];
        }

        //animation
        Fool.TakeDamageFromCharacter(activeCharacterWorldspaceObject.associatedCharacter);//this also handles damage indicator

        yield return new WaitForSeconds(0.05f);

        ReturnFromActiveSpot();
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
    //public IEnumerator HitKnockback(CharacterWorldspaceScript toAnimate)
    //{

    //    Vector3 Initial = toAnimate.transform.position;

    //    if (toAnimate.associatedCharacter.isPlayerPartyMember)
    //    {
    //        Vector3 final = new Vector3(Initial.x - 1f, Initial.y, Initial.z);
    //        while (Vector3.Distance(toAnimate.transform.position, final) > 0.05f)
    //        {
    //            toAnimate.transform.position = Vector3.Lerp(toAnimate.transform.position, final, 0.5f * Time.deltaTime);
    //        }

    //        yield return new WaitForSecondsRealtime(0.3f);
    //        while (Vector3.Distance(toAnimate.transform.position, Initial) > 0.05f)
    //        {
    //            toAnimate.transform.position = Vector3.Lerp(toAnimate.transform.position, Initial, 0.5f * Time.deltaTime);
    //        }
    //    }
    //    else
    //    {
    //        Vector3 final = new Vector3(Initial.x + 1f, Initial.y, Initial.z);
    //        while (Vector3.Distance(toAnimate.transform.position, final) > 0.05f)
    //        {
    //            toAnimate.transform.position = Vector3.Lerp(toAnimate.transform.position, final, 0.5f * Time.deltaTime);
    //        }
    //        Character poorFool = MainData.livingPlayerParty[Random.Range(0, MainData.livingPlayerParty.Count + 1)];

    //        yield return new WaitForSecondsRealtime(0.3f);
    //        while (Vector3.Distance(toAnimate.transform.position, Initial) > 0.05f)
    //        {
    //            toAnimate.transform.position = Vector3.Lerp(toAnimate.transform.position, Initial, 0.5f * Time.deltaTime);
    //        }
    //    }


    //}
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
            //Debug.LogWarning("Could not move " + chara.gameObject.name + ", CurrentlyActiveChar is not null");
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
        GameObject highli = MainData.MainLoop.UserInterfaceHelperComponent.CombatHighlightObject;
        if (activeTarget == null)
        {
            highli.SetActive(false);
            return;
        }
        if (activeTarget.associatedCharacter == null)
        {
            highli.SetActive(false);
            return;
        }


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
        if (activeCharacterWorldspaceObject == null)
        {
            return;
        }

        activeCharacterWorldspaceObject.transform.position = activeCharacterWorldspaceObject.associatedCharacter.InitialPosition;//yaaaay
        //Debug.Log("Just returned from active spot to coordinates " + activeCharacterWorldspaceObject.associatedCharacter.InitialPosition.ToString());



        //switch (activeCharsacterWorldspaceObject.associatedCharacter.charType)
        //{
        //    case "lion":
        //        activeCharacterWorldspaceObject.spriteRenderer.sprite = MainData.MainLoop.EntityDefComponent.lionStanding;
        //        break;
        //    case "dorothy":
        //        activeCharacterWorldspaceObject.spriteRenderer.sprite = MainData.MainLoop.EntityDefComponent.dorothyStanding;
        //        break;
        //    case "tin_man":
        //        activeCharacterWorldspaceObject.spriteRenderer.sprite = MainData.MainLoop.EntityDefComponent.tinmanStanding;
        //        break;
        //    case "scarecrow":
        //        activeCharacterWorldspaceObject.spriteRenderer.sprite = MainData.MainLoop.EntityDefComponent.scarecrowStanding;
        //        break;
        //}
        activeCharacterWorldspaceObject.ToggleIdle(true);

    }
    public void DoEnemyCharacterTurn(Character npc)
    {
        ToggleCombatButtomVisibility(false);
        //Character toBeAttacked = StaticDataHolder.playerParty[Random.Range(0, StaticDataHolder.playerParty.Count + 1)];
        //StartCoroutine(SlideTo(npc.selfScriptRef, ActiveCharSpot.transform.position, activationMovingSpeed));

        //StartCoroutine(SlideTo(CurrentlyActiveChar, ActiveCharSpot.transform.position, activationMovingSpeed, false));
        npc.selfScriptRef.transform.position = ActiveCharSpot.transform.position;

        StartCoroutine(AttackPlayerPartyBasedOnThreat(npc.selfScriptRef));


    }
    public IEnumerator AttackPlayerPartyBasedOnThreat(CharacterWorldspaceScript chara)
    {
        chara.ToggleIdle(false);
        List<Character> results = MainData.livingPlayerParty.FindAll(x => x.isDead == false);
        results.Sort((x, y) => (y.Threat + y.threatFromStats).CompareTo(x.Threat + x.threatFromStats));
        Character Fool = results[0];

        //Debug.Log("attacking player at playerParty[" + b.ToString() + "]!");
        chara.associatedCharacter.summoningCurrentDelay++;
        if (chara.associatedCharacter.Summoner && chara.associatedCharacter.summoningCurrentDelay >= chara.associatedCharacter.summoningInterval && MainData.freeEnemyPartyMemberObjects.Count > 0)
        {

            int x = UnityEngine.Random.Range(0, MainData.freeEnemyPartyMemberObjects.Count);
            GameObject f = MainData.freeEnemyPartyMemberObjects[x];
            MainData.freeEnemyPartyMemberObjects.RemoveAt(x); //we remove the spot from the inactive/free enemy spot list
            MainData.usedEnemyPartyMemberObjects.Add(f); //track usage...
            MainData.MainLoop.EventLoggingComponent.LogGray(chara.associatedCharacter.charName + " starts focusing ambient mana. The air thrums with potential.");
            yield return new WaitForSeconds(0.3f);
            MainData.MainLoop.EventLoggingComponent.LogGray(chara.associatedCharacter.charName + " summons a " + MainData.characterTypes[chara.associatedCharacter.summonedEnemy].charName + " from thin air!");
            f.SetActive(true);//we turn the spot on on
            CharacterWorldspaceScript d = f.GetComponent<CharacterWorldspaceScript>();//get the Cscript reference
            d.SetupCharacterByTemplate(MainData.characterTypes[chara.associatedCharacter.summonedEnemy]); //assign and set up an enemy template to the spot

            chara.associatedCharacter.summoningCurrentDelay = 0;


        }
        else
        {
            if (chara.associatedCharacter != null)
            {
                Fool.TakeDamageFromCharacter(chara.associatedCharacter);//this also handles the damage indicator 
            }


            for (int i = 0; i < activeCharacterWorldspaceObject.associatedCharacter.attackAnimation.Length; i++)
            {
                activeCharacterWorldspaceObject.spriteRenderer.sprite = activeCharacterWorldspaceObject.associatedCharacter.attackAnimation[i];
                yield return new WaitForSecondsRealtime(0.06f);
            }
            if (activeCharacterWorldspaceObject.associatedCharacter.standingSprite != null)
            {
                activeCharacterWorldspaceObject.spriteRenderer.sprite = activeCharacterWorldspaceObject.associatedCharacter.standingSprite;
            }
            else
            {
                activeCharacterWorldspaceObject.spriteRenderer.sprite = activeCharacterWorldspaceObject.associatedCharacter.attackAnimation[0];
            }
        }






        //play attack animation here
        yield return new WaitForSeconds(0.3f);

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
