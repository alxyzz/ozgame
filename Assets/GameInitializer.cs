using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Travel(GameManager.startingLevel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
