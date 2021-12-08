using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LevelHelper;

public class OvermapGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.overmapGeneratorRef = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public class MapRank
    {
        public MapLevel[] levels;


    }



    public class OverMap
    {//overmap class.
        public MapRank[] ranks = new MapRank[GameManager.RankAmount];//i am surprised this works




        public void GenerateOvermap()
        {//.Rank for the number of dimensions. In the case this is 2, .GetLength(0) for the number of rows, .GetLength(1) for the number of columns. 
            string debuglog = "";

            for (int i = 0; i < GameManager.RankAmount; i++)
            {
                for (int y = 0; y < GameManager.maxLevelsAmount; y++)
                {

                }
            }

                    //int listIndex = Random.Range(0, GameManager.levelTemplates.Count + 1); //rolls a random template for the level

            


        }


        public static MapLevel LevelFromTemplate(MapLevel p) // i could use a structure but this is just to create a new copy of the level instead of passing a reference.
        {//keep in mind this does not clone class instances, the same process has to be done to those...
            MapLevel temp = new MapLevel(p.levelName, p.levelBlurb, p.levelDescription, p.roomCount, p.EnemyType, p.startingDifficulty, p.difficultyIncreasePerRoom, p.levelBackground, p.levelSoundtrack, p.isCampfire);
            if (p.localMerchant != null)
            {
                temp.localMerchant = p.localMerchant;
            }
            else
            {
                Merchant newmerc = new Merchant();
                newmerc.GenerateStock();
                temp.localMerchant = newmerc;

            }
            
            return temp;
        }




    }



}
