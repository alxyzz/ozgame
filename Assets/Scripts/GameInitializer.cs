using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{

    
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(initMap());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator initMap()
    {
       
        GameManager.Travel(GameManager.startingLevel);
        yield return new WaitForSecondsRealtime(3f);
    }
}
