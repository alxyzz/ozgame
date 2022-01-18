using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryBagObject : MonoBehaviour, IPointerClickHandler
{
    public EntityDefiner.Item associatedItem;
    public Image selfImage; //this button is not very confident...



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }





    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
        MainData.MainLoop.InventoryHelperComponent.ClickedSlot(BagItem: this);
    }

}
