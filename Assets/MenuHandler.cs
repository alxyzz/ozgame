﻿using System.Collections;
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
        initialized = true;
    }

    private void Update()
    {
        if (initialized)
        {
            float distanceMoved = (Time.time - startTime) * speed;

            float fracOfJourney = distanceMoved / journeyLength;

            camHolder.transform.position = Vector3.Lerp(start.position, end[camSpot].position, fracOfJourney);
            camHolder.transform.rotation = Quaternion.Lerp(start.rotation, end[camSpot].rotation, fracOfJourney);

            if (playCutscene)
            {
                var tempColor = Fade.GetComponent<Image>().color;
                tempColor.a = fracOfJourney * 60;
                Fade.GetComponent<Image>().color = tempColor;
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
        speed = 0.3f;
    }

    public void showHandbook()
    {
        mainMenuHolder.SetActive(false);
        handbookHolder.SetActive(true);
        camSpot = 1;
        start = camHolder.transform;
        startTime = Time.time;
        journeyLength = Vector3.Distance(start.position, end[camSpot].position);
        speed = 0.3f;
    }

    public void LoadGame()
    {
        mainMenuHolder.SetActive(false);
        handbookHolder.SetActive(false);
        camSpot = 2;
        start = camHolder.transform;
        startTime = Time.time;
        journeyLength = Vector3.Distance(start.position, end[camSpot].position);
        speed = 0.02f;
        Fade.SetActive(true);
        playCutscene = true;
        anim.GetComponent<Animation>().Play();
        //SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
}

