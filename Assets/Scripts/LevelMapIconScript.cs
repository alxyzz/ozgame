using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LevelHelper;

public class LevelMapIconScript : MonoBehaviour
{

    //this script should allow hovering over the icon to show the description and name of the level on the world map.
    public MapLevel levelRef;

    void Start()
    {
        if (levelRef != null)
        {
            if (levelRef.visited)
            {
                //change color a bit
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
//    IEnumerator delayedCheck()
//    {
//        if (levelRef.visited)
//        {
////change color a bit
//        }
//        yield return new WaitForSecondsRealtime(0.5f);
//    }
}
