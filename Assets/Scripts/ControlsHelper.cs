using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LevelHelper;

public class ControlsHelper : MonoBehaviour
{

    public GameObject WorldMapCanvas; //for activating it on click
    public GameObject WorldCanvasLevelPrefab;
    public GameObject SelectedChar;
    // Start is called before the first frame update


    private void Start()
    {
        DataHolder.ControlsHelperRef = this;
    }

    //sends the pass turn button click to GameMaster, which is a static function & can't be chosen for the button dropdown function call thing coz you need it attached to an object.
    public void SendPlayClick()
    {
        DataHolder.PassTurn();
        //add a click sound here


    }



    public void BuildWorldCanvas()
    {


    }




    public void SendPauseClick()
    {

        DataHolder.SoundManagerRef.PlaySoundByName("clickButton");
    }




    public void OpenOvermapButton()
    {

        WorldMapCanvas.SetActive(true);
        //Debug.Log("CLICKED OVERMAP BUTTON.");
        DataHolder.SoundManagerRef.PlaySoundByName("clickButton");
    }

    public void CloseOvermapButton()
    {

        WorldMapCanvas.SetActive(false);
       // Debug.Log("CLICKED closeOVERMAP BUTTON.");
        DataHolder.SoundManagerRef.PlaySoundByName("clickButton");
    }

    public void ClickedLevel(MapLevel clickyyy)
    {
        if (clickyyy != DataHolder.currentLevel)
        {//we go there if possible
            if (DataHolder.currentLevel.nextLevels.Contains(clickyyy))
            {//oh yeeeeee

                DataHolder.SoundManagerRef.PlaySoundByName("clickButton");
            }
            else
            {
                //failure
                DataHolder.SoundManagerRef.PlaySoundByName("failure");
            }
        }

    }




}
