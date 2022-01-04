using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundLayerMovementParallax : MonoBehaviour
{
    public GameObject firstPart, midPart, lastPart, spawnSpot;
    public float movementAmount;
    private Vector3 InitialPosition, endPosition;
    [HideInInspector]
    public bool? moveDir;
    // Start is called before the first frame update
    void Start()
    {
        AcquireStartingPositions();

    }


    private void AcquireStartingPositions()
    {
        InitialPosition = firstPart.transform.position;
        //endPosition = ;


        //Debug.Log(firstPart.GetComponent<Image>().sprite.textureRect.size.x + " is the length of firstpart.");
       // Debug.Log(midPart.GetComponent<Image>().sprite.textureRect.size.x + " is the length of midPart.");
        //Debug.Log(lastPart.GetComponent<Image>().sprite.textureRect.size.x + " is the length of lastPart.");

    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            switch (moveDir)
            {
                case null: // we dont do anything
                    break;
                case false: //we go backwards, player party thus goes forwards
                    StaticDataHolder.MainLoop.LevelHelperComponent.distanceWalked += 0.01f;
                    HandleObject(firstPart, lastPart, midPart, false);
                    HandleObject(midPart, firstPart, lastPart, false);
                    HandleObject(lastPart, midPart, firstPart, false);
                    break;
                case true: //we go forwards, player party thus goes backwards
                    StaticDataHolder.MainLoop.LevelHelperComponent.distanceWalked -= 0.01f;
                    HandleObject(firstPart, midPart, lastPart, true);
                    HandleObject(midPart, lastPart, firstPart, true);
                    HandleObject(lastPart, firstPart, midPart, true);
                    break;
            }
        }
        catch (System.ArgumentNullException)
        {
            Debug.LogWarning("ArgumentNullException in BackgroundLayerMovementParallax.cs - Update()");
            AcquireStartingPositions();
        }

    }




    public void ChangeDirection(bool? moveDirection)
    {
        moveDir = moveDirection;
    }
    





    /// <summary>
    /// moves the layer/object in the current direction then from the front to the back of the queue.
    /// </summary>
    /// <param name="b"> layer slice to be moved. </param>
    /// <param name="dir"> direction. false is backwards, true is forwards. Note: This moves the background. So realistically, the background is moving backwards when YOU as the party are moving forwards.</param>
    private void HandleObject(GameObject b, GameObject previousLayer, GameObject nextlayer, bool dir)
    {
        if (dir)
        {
            if (b.transform.position.x > spawnSpot.transform.position.x -2)
            {
                b.transform.position = InitialPosition;
            }
            b.transform.position = new Vector3(b.transform.position.x + (movementAmount * Time.deltaTime), b.transform.position.y, b.transform.position.z);
        }
        else
        {
            if (b.transform.position.x < InitialPosition.x +2)
            {
                b.transform.position = spawnSpot.transform.position;
            }
            b.transform.position = new Vector3(b.transform.position.x - (movementAmount * Time.deltaTime), b.transform.position.y, b.transform.position.z);
        }
        
        
    }
}
