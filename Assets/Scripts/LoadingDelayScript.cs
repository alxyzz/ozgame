using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingDelayScript : MonoBehaviour
{
    public List<string> loadingFlufflist = new List<string>(); //strings for loading

    public GameObject TextObj;
    private TextMeshProUGUI TextComponent;

    public float loadingSpeed;
    private float loadedPercent = 0f;

    private float texttime = 0;

    public Slider loadingBarObj;




    // Start is called before the first frame update
    void Start()
    {
        TextComponent = TextObj.GetComponent<TextMeshProUGUI>();


    }


    IEnumerator WaitForLoadingStuff()
    {

        yield return new WaitForSecondsRealtime(3f);

    }

    // Update is called once per frame
    void Update()
    {

        loadedPercent += (loadingSpeed * Time.deltaTime);
        loadingBarObj.value = (loadedPercent/100f);


        if (loadedPercent >= 100f)
        {
            gameObject.SetActive(false);
        }


        texttime += Time.deltaTime;
        if (texttime >= 3)
        {
            texttime = 0.0f;
            SwitchLoadingBlurb();
        }
    }



    void SwitchLoadingBlurb()
    {
        TextComponent.text = loadingFlufflist[Random.Range(0, loadingFlufflist.Count - 1)];


    }

}