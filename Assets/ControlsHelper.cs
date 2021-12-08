using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LevelHelper;

public class ControlsHelper : MonoBehaviour
{
    // Start is called before the first frame update


    private void Start()
    {
        GameManager.ControlsHelperRef = this;
    }

    //sends the pass turn button click to GameMaster, which is a static function & can't be chosen for the button dropdown function call thing coz you need it attached to an object.
    public void SendPlayClick()
    {
        GameManager.PassTurn();
        //add a click sound here


    }


    public void SendPauseClick()
    {

        GameManager.SoundManagerRef.PlaySoundByName("clickButton");
    }




    public void OvermapButton()
    {

        GameManager.SoundManagerRef.PlaySoundByName("clickButton");
    }

    public void ClickedLevel(MapLevel clickyyy)
    {
        if (clickyyy != GameManager.currentLevel)
        {//we go there if possible
            if (GameManager.currentLevel.nextLevels.Contains(clickyyy))
            {//oh yeeeeee

                GameManager.SoundManagerRef.PlaySoundByName("clickButton");
            }
            else
            {
                //failure
                GameManager.SoundManagerRef.PlaySoundByName("failure");
            }
        }

    }




}
