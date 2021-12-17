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

        MainData.mapIcons.Add(this);
        //Randomize();
        if (isStart)
        {
            MainData.currentMapIcon = this;
        }
        normalColor = this.gameObject.GetComponent<Image>().color;
        FindClosestAbove();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //void Randomize()
    //{
    //    MapLevel mappyTheLevel;
    //    try
    //    {
    //        mappyTheLevel = LevelFromTemplate(DataHolder.levelTemplates[Random.Range(1, DataHolder.levelTemplates.Count + 1)]);
    //        relatedMapLevel = mappyTheLevel;
    //    }
    //    catch (System.Exception)
    //    {
    //        mappyTheLevel = new MapLevel("Weird Place", "A plane of nothingness.", "Something is wrong.", 3, "Gods", 1f, 1f, null, null, false);
    //        relatedMapLevel = mappyTheLevel;
    //        throw;
    //    }
        
       
    //    //this must: grab a template from the list, apply it
    //    //randomize name, enemy amount, get description based on that


    //}

    IEnumerator unclick()
    {

        yield return new WaitForSecondsRealtime(2f);
        clickedOnce = false;
        gameObject.GetComponent<Image>().color = normalColor;

    }

    public void OnClick()
    {
        if (MainData.currentMapIcon.moveableToIcons.Contains(this))
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
        MainData.MainLoop.UserInterfaceHelperComponent.worldmapName.GetComponent<TextMeshProUGUI>().text = nameee;
        MainData.MainLoop.UserInterfaceHelperComponent.worldmapDescription.GetComponent<TextMeshProUGUI>().text = description;
    }

    private void OnMouseExit()
    {
        MainData.MainLoop.UserInterfaceHelperComponent.worldmapName.GetComponent<TextMeshProUGUI>().text = "";
        MainData.MainLoop.UserInterfaceHelperComponent.worldmapDescription.GetComponent<TextMeshProUGUI>().text = "";
    }


    private void MoveToIcon()
    {//we assume everything in order and proper checks have been previously made
        MainData.currentMapIcon = this;
        MainData.MainLoop.Travel(relatedMapLevel);
        MainData.MainLoop.UserInterfaceHelperComponent.ClickMapClose();
        MainData.MainLoop.UserInterfaceHelperComponent.TravelLoadingSequence();
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
        foreach (MapIconScript item in MainData.mapIcons)
        {
            if ((item.transform.position.y > this.transform.position.y) && (item.transform.position.x != this.transform.position.x))
            {
                if ((item.transform.position.y - this.transform.position.y) <= MaxDistanceToLink)
                {
                    StartCoroutine(delayLoading(item));
                }
                
            }
        }
    }

    private void LinkTo(MapIconScript closestIcon)
    {
        moveableToIcons.Add(closestIcon);
        relatedMapLevel.nextLevels.Add(closestIcon.relatedMapLevel);
        //draw a line to the closest ones and allow moving towards them. do not allow moving towards ones on the same x level or ones already visited.
        var go = new GameObject();
        var lr = go.AddComponent<LineRenderer>();

        lr.SetPosition(0, this.transform.position);
        lr.SetPosition(1, closestIcon.transform.position);
        lr.startWidth = 5f;
        lr.endWidth = 5f;
    }

    IEnumerator delayLoading(MapIconScript item)
    {

        yield return new WaitForSecondsRealtime(1f);
        LinkTo(item);

    }
}
