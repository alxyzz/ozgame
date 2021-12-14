using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EntitiesDefinition;

public class CombatHelper : MonoBehaviour
{
    public float closenessMargin;
    public float activationMovingSpeed;
    private CharacterScript CurrentActiveCharacter; //each enemy on the level is composed of the game object, holding the script, associated with a Character instance that holds stats, names, descriptions etc
    private Vector3 InitialActiveCharacterPositionCoordinates;//initial coordinates of character, to be moved back to after their action.
    public GameObject ActiveCharSpot; //where the character that is attacking will move.

    // Update is called once per frame
    void Update()
    {
        
    }


    public void InitiateCombatTurn()
    {
        List<Character> combatants = MainData.allChars;
        //todo - what happens if someone dies during this? can foreach cope with it?

        combatants.Sort((x, y) => y.speed.CompareTo(x.speed)); // descending. swap y and x on the right side for ascending.

        foreach (Character item in combatants)//goes from 0 to length-1, always
        {
            MainData.MainLoop.SoundManagerComponent.sfxSource.PlayOneShot(item.turnSound); //plays the character specific noise/vocalization
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


    public void DoPlayerCharacterTurn(Character pc)
    {
        //what do we do here?
        //first off, move the player character to the designated spot
        MoveToActiveSpot(pc);
        //then, pop up an icon with either attacking or using the trait from the character
        //PopUpActionButtons(); //pop up the action menu
        //enable controls over items and such
        MainData.controlsEnabled = true;
        //player can click an enemy to highlight it

    }

    private void MoveToActiveSpot(Character chara)
    {
        CurrentActiveCharacter = chara.selfScriptRef;
        InitialActiveCharacterPositionCoordinates = chara.selfScriptRef.transform.position;
        while (Vector3.Distance(chara.selfScriptRef.transform.position, ActiveCharSpot.transform.position) > closenessMargin)
        {
            chara.selfScriptRef.transform.position = Vector3.Lerp(chara.selfScriptRef.transform.position, ActiveCharSpot.transform.position, activationMovingSpeed * Time.deltaTime);
        }
    }

    private void ReturnFromActiveSpot(Character chara)
    {

        while (Vector3.Distance(chara.transform.position, InitialActiveCharacterPositionCoordinates) > closenessMargin)
        {
            chara.selfScriptRef.transform.position = Vector3.Lerp(chara.selfScriptRef.transform.position, InitialActiveCharacterPositionCoordinates, activationMovingSpeed * Time.deltaTime);
        }
        CurrentActiveCharacter = null;
    }

    public void DoEnemyCharacterTurn(Character npc)
    {
        //lock player controls/item usage
        //think on who to attack
        Character toBeAttacked = MainData.playerParty[Random.Range(0, MainData.playerParty.Count + 1)];




    }
}
