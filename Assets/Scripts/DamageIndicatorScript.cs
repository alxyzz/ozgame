using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageIndicatorScript : MonoBehaviour, IPooledObject
{
    // Start is called before the first frame update
    public void OnObjectSpawn()//does not actually do anything coz we don't need this to do anything on start. it's dealt with on initializing it from the object pool in CombatHelper.cs
    {

    }
}
