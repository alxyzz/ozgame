using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyInfo : MonoBehaviour
{
    public string Name;
    public int[] Stats;
    public string[] Lore;
    public Image Splash;

    public Text tname;
    public Text tstats;
    public Text tlore;
    public Image SplashArt;


    public void HandbookTime()
    {
        tname.text = Name;
        tstats.text = "HP: " + Stats[0] + System.Environment.NewLine + "Damage: " + Stats[1] + System.Environment.NewLine + "Defense: " + Stats[2] + System.Environment.NewLine + "Speed: " + Stats[3] + System.Environment.NewLine;
        tlore.text = Lore[0] + System.Environment.NewLine + Lore[1] + System.Environment.NewLine + Lore[2] + System.Environment.NewLine + Lore[3] + System.Environment.NewLine + Lore[4] + System.Environment.NewLine;
        SplashArt.sprite = Splash.sprite;
    }
    
}
