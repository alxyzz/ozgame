using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LevelHelper;
using static OvermapGenerator;

public class OvermapIconInitiator : MonoBehaviour
{

    //this script should allow hovering over the icon to show the description and name of the level on the world map.
    public List<GameObject> PreviousLevels = new List<GameObject>();
    public List<GameObject> NextLevels = new List<GameObject>();
    public MapLevel levelRef;
    public GameObject MapIconPrefab; //makes these above the starting level at set distance
    [Space(10)]
    public float verticalDistance;
    public float horizontalSpace;
    public GameObject leftbot;
    public GameObject topright;

    void Start()
    {
        GenerateMapIcons();
        //levelRef = GameManager.startingLevel;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateMapIcons()
    {
        //int listIndex = Random.Range(0, GameManager.levelTemplates.Count + 1); //0-based list? some find it quite cringe.= 
        //MapLevel bobbyTheLevel = LevelFromTemplate(GameManager.levelTemplates[listIndex]);//rolls a random template for the level
    }

    //generate layers above


}
