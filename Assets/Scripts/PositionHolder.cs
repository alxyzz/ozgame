using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionHolder : MonoBehaviour
{
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

    public GameObject CampfireSpot1;
    public GameObject CampfireSpot2;
    public GameObject CampfireSpot3;
    public GameObject CampfireSpot4;


    public GameObject EnemySpawnBoundaryLeft;//enemies spawn between these
    public GameObject EnemySpawnBoundaryRight;


    void Start()
    {




    MainData.references = this;
    }



    public void PrepPartyPlaces()
    {

        PartySlot1InitialPos = PartySlot1.transform.position;
        PartySlot1InitialPos = PartySlot1.transform.position;
        PartySlot1InitialPos = PartySlot1.transform.position;
        PartySlot1InitialPos = PartySlot1.transform.position;
    }



}
