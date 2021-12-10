using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static LevelHelper;

public class MapIconScript : MonoBehaviour
{
    public List<MapIconScript> moveableToIcons = new List<MapIconScript>();
    private MapLevel relatedMapLevel;
    bool clickedOnce;

    public Color SelectedColor;
    Color normalColor;

    // Start is called before the first frame update
    void Start()
    {
        normalColor = this.gameObject.GetComponent<Image>().color;
        GameManager.mapIcons.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator delayLoading()
    {

            yield return new WaitForSecondsRealtime(2f);

            
    }
    IEnumerator unclick()
    {

        yield return new WaitForSecondsRealtime(2f);
        clickedOnce = false;
        gameObject.GetComponent<Image>().color = normalColor;

    }

    private void OnMouseDown()
    {
        if (GameManager.currentMapIcon.moveableToIcons.Contains(this))
        {
            if (!clickedOnce) //so doubleclick to travel
            {
                clickedOnce = true;
                gameObject.GetComponent<Image>().color = SelectedColor;
                StartCoroutine(unclick());
                //make it change color
            }
            else
            {
                MoveToIcon();
            }
            //move there
        }
    }


    private void MoveToIcon()
    {//we assume everything in order and proper checks have been previously made
        GameManager.currentMapIcon = this;
        GameManager.Travel(relatedMapLevel);
        GameManager.ControlsHelperRef.CloseOvermapButton();
        GameManager.uiMan.TravelLoadingSequence();
    }

    public MapLevel LevelFromTemplate(MapLevel p)
    {
        MapLevel johnnyLevel = new MapLevel(p.levelName, p.levelBlurb, p.levelDescription, p.roomCount, p.EnemyType, p.startingDifficulty, p.difficultyIncreasePerRoom, p.levelBackgroundMaterial, p.levelSoundtrack, p.isCampfire);
        if (p.localMerchant != null)
        {
            johnnyLevel.localMerchant = p.localMerchant;
        }
        else
        {
            Merchant newmerc = new Merchant();
            johnnyLevel.localMerchant = newmerc;
            johnnyLevel.localMerchant.GenerateStock();

        }

        return johnnyLevel;
    }

    private void FindClosest()
    {
        //find all icons in range by tag "mapIcon"
        
    }

    private void LinkToClosest(List<GameObject> closestIcons)
    {
        //draw a line to the closest ones and allow moving towards them. do not allow moving towards ones on the same x level or ones already visited.
    }
}
