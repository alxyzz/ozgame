using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    public GameObject camHolder;

    public GameObject mainMenuHolder;
    public GameObject handbookHolder;

    public void showMainMenu()
    {
        mainMenuHolder.SetActive(true);
        handbookHolder.SetActive(false);
        Debug.Log("Main");
    }

    public void showHandbook()
    {
        mainMenuHolder.SetActive(false);
        handbookHolder.SetActive(true);
        Debug.Log("handbook");
    }
}

