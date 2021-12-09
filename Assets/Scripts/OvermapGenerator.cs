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
        OverMap world = new OverMap();
        world.GenerateOvermap();
        GameManager.gameOvermap = world;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public class MapRow
    {//a singular row in the overmap. should have between 3-5 levels.
        public MapLevel[] levels;


        public void Initialize(int lvlCount)
        {
            levels = new MapLevel[lvlCount];
        }


    }



    public class OverMap
    {//overmap class.
        public MapRow[] Rows;//i am surprised this works



        //todo  - put starting and ending level at required places
        public void GenerateOvermap()
        {//.Rank for the number of dimensions. In the case this is 2, .GetLength(0) for the number of rows, .GetLength(1) for the number of columns. 
            bool RowCountAlternatorBool = true;
            for (int i = 0; i < 3; i++)
            {//loops through each level in each rank and chooses a random template type to apply
                if (Rows[i] != null)
                {
                    Rows[i] = new MapRow();
                    if (RowCountAlternatorBool)
                    {
                        Rows[i].Initialize(3);
                        RowCountAlternatorBool = false;
                    }
                    else
                    {
                        Rows[i].Initialize(4);
                        RowCountAlternatorBool = true;
                    }
                    
                    for (int y = 0; y < Rows[i].levels.Length-1; y++) //todo - check if zero based so it doesnt crash when it reaches the last index - done
                    {
                        int listIndex = Random.Range(0, GameManager.levelTemplates.Count + 1); //0-based list? some find it quite cringe.
                        Rows[i].levels[y] = LevelFromTemplate(GameManager.levelTemplates[listIndex]);//rolls a random template for the level
                    }
                }
                
               
            }
            LinkMaps();
        }


        public void LinkMaps()
        {
            if (GameManager.currentLevel == null)
            {
                Debug.Log("GameManager's current level is null...");
            }
            foreach (MapLevel item in Rows[0].levels)
            {//links the starting level
                GameManager.startingLevel.nextLevels.Add(item);
                item.previousLevels.Add(GameManager.startingLevel);
            }

            foreach (MapLevel item in Rows[Rows.Length-1].levels)//0 based
            {//links ending level
                GameManager.finalLevel.previousLevels.Add(item); ;
                item.nextLevels.Add(GameManager.finalLevel);
            }
            
            for (int RowIndex = 0; RowIndex < Rows.Length-1; RowIndex++)
            {//pass through all levels, link current level to next two closest ones
                //link x level to next rank x and x+1 level if it's a small rank
                //link backwards if it's a big rank
                for (int levelIndex = 0; levelIndex < Rows[RowIndex].levels.Length-2; levelIndex++)//exclude the last rank because it is linked to the ending level
                {
                    if (Rows[RowIndex].levels.Length == 3)
                    {
                        Rows[RowIndex].levels[levelIndex].nextLevels.Add(Rows[RowIndex + 1].levels[levelIndex]);//tells this level it leads to the level on the same index ON the next rank.
                        Rows[RowIndex + 1].levels[levelIndex].previousLevels.Add(Rows[RowIndex].levels[levelIndex]); //tells the level on the same index ON the next rank, that it leads to this level

                        Rows[RowIndex].levels[levelIndex].nextLevels.Add(Rows[RowIndex + 1].levels[levelIndex+1]);//tells this level it leads to the level on the same index +1 ON the next rank.
                        Rows[RowIndex + 1].levels[levelIndex+1].previousLevels.Add(Rows[RowIndex].levels[levelIndex]);//tells the level on the same index+1 ON the next rank, that it leads to this level.
                    }
                    else if(Rows[RowIndex].levels.Length == 4)
                    {
                        if (levelIndex != Rows[RowIndex].levels.Length-1)//exclude last level so we don't get an object not found exception
                        {//always link same index together
                            Rows[RowIndex].levels[levelIndex].nextLevels.Add(Rows[RowIndex + 1].levels[levelIndex]);
                            Rows[RowIndex + 1].levels[levelIndex].previousLevels.Add(Rows[RowIndex].levels[levelIndex]);
                        }
                        if (levelIndex != 0)//exclude first level so we don't get an object not found exception
                        {//links each level with the index-1 equivalent on next rank
                            Rows[RowIndex].levels[levelIndex].nextLevels.Add(Rows[RowIndex + 1].levels[levelIndex-1]);
                            Rows[RowIndex + 1].levels[levelIndex-1].previousLevels.Add(Rows[RowIndex].levels[levelIndex]);
                        }//ok i checked this, all is well
                    }
                }
            }

        }


        public MapLevel LevelFromTemplate(MapLevel p)
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
