using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    public GameObject camHolder;
    public GameObject mainMenuHolder;
    public GameObject handbookHolder;
    public GameObject Fade;
    public GameObject anim;

    float speed = 0.3f;
    int camSpot;
    bool initialized;
    bool playCutscene;

    private float startTime;
    private float journeyLength;

    Transform start;
    public Transform[] end;

    private void Start()
    {
        start = camHolder.transform;
        journeyLength = Vector3.Distance(start.position, end[camSpot].position);
        camHolder.transform.position = new Vector3(9.28f, 3.12f, -3.45f);
        initialized = true;
    }

    private void Update()
    {
        if (initialized)
        {
            float distanceMoved = (Time.time - startTime) * speed;

            float fracOfJourney = distanceMoved / journeyLength;

            start.position = camHolder.transform.position;
            start.rotation = camHolder.transform.rotation;

            camHolder.transform.position = Vector3.Lerp(start.position, end[camSpot].position, fracOfJourney);
            camHolder.transform.rotation = Quaternion.Lerp(start.rotation, end[camSpot].rotation, fracOfJourney);

            if (playCutscene)
            {
                var tempColor = Fade.GetComponent<Image>().color;
                tempColor.a = (fracOfJourney * 30);
                Debug.Log(fracOfJourney);
                Fade.GetComponent<Image>().color = tempColor;
                if (fracOfJourney >= .04f)
                {
                    SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
                }
            }
        }

    }

    public void showMainMenu()
    {
        mainMenuHolder.SetActive(true);
        handbookHolder.SetActive(false);
        camSpot = 0;
        start = camHolder.transform;
        startTime = Time.time;
        journeyLength = Vector3.Distance(start.position, end[camSpot].position);
        speed = 0.2f;
    }

    public void showHandbook()
    {
        mainMenuHolder.SetActive(false);
        handbookHolder.SetActive(true);
        camSpot = 1;
        start = camHolder.transform;
        startTime = Time.time;
        journeyLength = Vector3.Distance(start.position, end[camSpot].position);
        speed = 0.2f;
    }

    public void LoadGame()
    {
        mainMenuHolder.SetActive(false);
        handbookHolder.SetActive(false);
        camSpot = 2;
        start = camHolder.transform;
        startTime = Time.time;
        journeyLength = Vector3.Distance(start.position, end[camSpot].position);
        speed = 0.04f;
        Fade.SetActive(true);
        playCutscene = true;
        anim.GetComponent<Animation>().Play();
    }
}

