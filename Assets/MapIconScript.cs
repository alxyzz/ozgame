using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static LevelHelper;

public class MapIconScript : MonoBehaviour
{

    public bool isStart;
    public bool isEnd;

    public List<MapIconScript> moveableToIcons = new List<MapIconScript>();
    private MapLevel relatedMapLevel;
    bool clickedOnce;

    public string description;
    public string nameee;

    public Color SelectedColor;
    Color normalColor;

    public float MaxDistanceToLink;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.mapIcons.Add(this);
        if (isStart)
        {
            GameManager.currentMapIcon = this;
        }
        normalColor = this.gameObject.GetComponent<Image>().color;
        FindClosestAbove();
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

    public void OnClick()
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

    private void OnMouseEnter()
    {
        GameManager.uiMan.worldmapName.GetComponent<TextMeshProUGUI>().text = nameee;
        GameManager.uiMan.worldmapDescription.GetComponent<TextMeshProUGUI>().text = description;
    }

    private void OnMouseExit()
    {
        GameManager.uiMan.worldmapName.GetComponent<TextMeshProUGUI>().text = "";
        GameManager.uiMan.worldmapDescription.GetComponent<TextMeshProUGUI>().text = "";
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

    private void FindClosestAbove()
    {
        //find all icons in range by tag "mapIcon"
        foreach (MapIconScript item in GameManager.mapIcons)
        {
            if ((item.transform.position.y > this.transform.position.y) && (item.transform.position.x != this.transform.position.x))
            {

            }
        }
    }

    private void LinkToClosest(List<GameObject> closestIcons)
    {
        //draw a line to the closest ones and allow moving towards them. do not allow moving towards ones on the same x level or ones already visited.
    }
}
