using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LevelHelper;

public class OvermapGenerator : MonoBehaviour
{

    public GameObject WorldmapScreen;
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
    {//a singular row in the overmap. should have between 3-5 levels.
        public MapLevel[] levels;


        public void Initialize()
        {
            if (Random.Range(1,3) == 1)
            {//sometimes it's 2 levels wide, sometimes 3.
                levels = new MapLevel[2];
            }
            else
            {
                levels = new MapLevel[3];
            }
            
        }


    }



    public class OverMap
    {//overmap class.
        public MapRank[] ranks = new MapRank[GameManager.RankAmount];//i am surprised this works




        public void GenerateOvermap()
        {//.Rank for the number of dimensions. In the case this is 2, .GetLength(0) for the number of rows, .GetLength(1) for the number of columns. 
            string debuglog = "";

            for (int i = 0; i < GameManager.RankAmount; i++)
            {//loops through each level in each rank and chooses a random template type to apply
                if (ranks[i] != null)
                {
                    ranks[i].Initialize();
                    for (int y = 0; y < ranks[i].levels.Length-1; y++) //todo - check if zero based so it doesnt crash when it reaches the last index - done
                    {
                        int listIndex = Random.Range(0, GameManager.levelTemplates.Count + 1); //0-based list? some find it quite cringe.
                        ranks[i].levels[y] = LevelFromTemplate(GameManager.levelTemplates[listIndex]);//rolls a random template for the level
                    }
                }
                
               
            }
        }


        public void LinkMaps()
        {

            for (int i = 0; i < GameManager.RankAmount; i++)
            {//pass through all levels, link them together
                for (int b = 0; b < ranks[i].levels.Length; b++)
                {

                    if (ranks[i-1] == null)
                    {
                        //link em all to the starting level
                    }
                    if (ranks[i+1] != null)
                    {
                        for (int x = 0; x < ranks[i + 1].levels.Length; x++)
                        {

                        }
                    }
                    else
                    {
                        //link em all to the final level
                    }
                    
                }
            }

        }


        public static MapLevel LevelFromTemplate(MapLevel p)
        { 
            MapLevel temp = new MapLevel(p.levelName, p.levelBlurb, p.levelDescription, p.roomCount, p.EnemyType, p.startingDifficulty, p.difficultyIncreasePerRoom, p.levelBackgroundMaterial, p.levelSoundtrack, p.isCampfire);
            if (p.localMerchant != null)
            {
                temp.localMerchant = p.localMerchant;
            }
            else
            {
                Merchant newmerc = new Merchant();
                temp.localMerchant = newmerc;
                temp.localMerchant.GenerateStock();

            }
            
            return temp;
        }




    }



}
