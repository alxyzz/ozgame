using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionHolder : MonoBehaviour
{


    //public GameObject ParentObjectForParties;
    

    [Space(15)]
    // public GameObject 
    public GameObject PartySlot1;
    public GameObject PartySlot2;
    public GameObject PartySlot3;
    public GameObject PartySlot4;

    [HideInInspector]
    public Vector3 PartySlot1InitialPos;
    [HideInInspector]
    public Vector3 PartySlot2InitialPos;
    [HideInInspector]
    public Vector3 PartySlot3InitialPos;
    [HideInInspector]
    public Vector3 PartySlot4InitialPos;
    [Space(15)]
    public GameObject PartyHolder;
    [Space(15)]
    public GameObject CampfireSpot1;
    public GameObject CampfireSpot2;
    public GameObject CampfireSpot3;
    public GameObject CampfireSpot4;
    [Space(15)]
    public GameObject EnemyCharacterSpot1;
    public GameObject EnemyCharacterSpot2;
    public GameObject EnemyCharacterSpot3;
    public GameObject EnemyCharacterSpot4;
    public GameObject EnemyCharacterSpot5;
    public GameObject EnemyCharacterSpot6;
    public GameObject EnemyCharacterSpot7;
    public GameObject EnemyCharacterSpot8;
    [Space(10)]

    public GameObject EnemySpawnBoundaryLeft;//enemies spawn between these
    public GameObject EnemySpawnBoundaryRight;


    void Start()
    {




    MainData.references = this;
    }




    public void RegisterEnemySpots()
    {
        MainData.freeEnemyPartyMemberObjects.Add(EnemyCharacterSpot1);
        MainData.freeEnemyPartyMemberObjects.Add(EnemyCharacterSpot2);
        MainData.freeEnemyPartyMemberObjects.Add(EnemyCharacterSpot3);
        MainData.freeEnemyPartyMemberObjects.Add(EnemyCharacterSpot4);
        MainData.freeEnemyPartyMemberObjects.Add(EnemyCharacterSpot5);
        MainData.freeEnemyPartyMemberObjects.Add(EnemyCharacterSpot6);
        MainData.freeEnemyPartyMemberObjects.Add(EnemyCharacterSpot7);
        MainData.freeEnemyPartyMemberObjects.Add(EnemyCharacterSpot8);
    }


    public void RegisterPlayerSpots()
    {
        MainData.playerPartyMemberObjects.Add(PartySlot1);
        MainData.playerPartyMemberObjects.Add(PartySlot2);
        MainData.playerPartyMemberObjects.Add(PartySlot3);
        MainData.playerPartyMemberObjects.Add(PartySlot4);

    }





    public void PrepPartyPlaces()
    {

        PartySlot1InitialPos = PartySlot1.transform.position;
        PartySlot1InitialPos = PartySlot1.transform.position;
        PartySlot1InitialPos = PartySlot1.transform.position;
        PartySlot1InitialPos = PartySlot1.transform.position;
    }



}
