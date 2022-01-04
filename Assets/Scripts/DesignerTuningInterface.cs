using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesignerTuningInterface : MonoBehaviour
{
    //this script exists to make it easier for designers to tweak around the bonus/malus values of different items/trinkets.



    public int swordDamage;

























    // Start is called before the first frame update
    void Start()
    {
        StaticDataHolder.itemManager = this;
    }

}
