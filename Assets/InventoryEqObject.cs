using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryEqObject : MonoBehaviour, IPointerClickHandler
{
    public EntityDefiner.Item associatedItem;
    public Image selfImage;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
        MainData.MainLoop.InventoryHelperComponent.ClickedSlot(CharItem: this);
    }
}
