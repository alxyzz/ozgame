using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Entity EntityLoader;
    public LevelHelper LevelLoader;
    public SoundManager SoundLoader;
    public RefHolder RefStore;

    // Start is called before the first frame update
    void StartGame()
    {
        Storagestuff.MainLoop = this;
        SoundLoader = gameObject.GetComponent<SoundManager>();
        LevelLoader = gameObject.GetComponent<LevelHelper>();
        RefStore = gameObject.GetComponent<RefHolder>();

        //call things from here from now on
        //StartLoading(); //blacks screen out



        LevelLoader.GenerateLevels(); //set up templates
        EntityLoader.GenerateTraits();
        EntityLoader.GenerateParty(); //set up characters
        EntityLoader.GenerateMonsters();
        EntityLoader.GenerateConsumables();
        //compile map data
        //setup overmap icons
        //compile 
        //finish up
        //StopLoading(); //restores vision


    }

    // Update is called once per frame
    void Update()
    {

    }
}
