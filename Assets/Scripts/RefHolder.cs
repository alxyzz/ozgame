using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefHolder : MonoBehaviour
{
    // public GameObject 
    public GameObject PartySlot1;
    public GameObject PartySlot2;
    public GameObject PartySlot3;
    public GameObject PartySlot4;

    public GameObject ConsumableSlot1;
    public GameObject ConsumableSlot2;
    public GameObject ConsumableSlot3;


    public GameObject EnemySpawnBoundaryLeft;//enemies spawn between these
    public GameObject EnemySpawnBoundaryRight;


    void Start()
    {
        Storagestuff.references = this;
    }







}
