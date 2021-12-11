using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundObjectScript : MonoBehaviour
{
    //simply designates the object it is attached to as the background object, on which level aesthetics are placed
    void Start()
    {
        MainData.backgroundObject = this.gameObject;   
    }

}
