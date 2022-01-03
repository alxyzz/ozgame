using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendorScript : MonoBehaviour
{


    [Space(5)]
    public GameObject Destination;
    public GameObject Startination;
    public GameObject Cart;
    [Space(5)]
    public GameObject GameUIReference; //we disable this so we don't do bad stuff.
    public GameObject VendorUIReference;

    [Space(5)]
    private bool encountering = false; //if this is on, we start the animation
    // Start is called before the first frame update
    [Space(5)]
    [Header("These are related to the movement of the vendor.")]
    [Space(2)]
    [Header("Minimum distance from the destination at which the cart can stop moving towards the party.")]
    public float minDistance;
    [Header("Speed at which the cart moves.")]
    public float speed;
    [Header("This should be around 0.01f to be smooth...")]
    public float moveInterval;





    IEnumerator EncounterAnimation()
    {
        while (Vector3.Distance(Cart.transform.position, Destination.transform.position) > 0.1f)
        {
            Cart.transform.position = Vector3.MoveTowards(Cart.transform.position, Destination.transform.position, speed * Time.deltaTime);
            yield return new WaitForSecondsRealtime(moveInterval);
        }



    }


    //click the vendor
    //this should play a short nice animation
    public void OpenVendorMenu()
    {
        GameUIReference.SetActive(false);
        VendorUIReference.SetActive(true);
    }





    

    public void ButtonClickReturn()
    {//click back button in shop
        GameUIReference.SetActive(true);
        VendorUIReference.SetActive(false);
    }





    public void GenerateMerchantInventory()
    {

    }

    public void EncounterMerchant()
    {
        StartCoroutine(EncounterAnimation());
    }

}
