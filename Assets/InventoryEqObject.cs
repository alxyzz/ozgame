using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryEqObject : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler
{
    public EntityDefiner.Item associatedItem;
    public Image selfImage;
    public Image background;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (associatedItem != null && !MainData.MainLoop.InventoryHelperComponent.coolDown)
        {
            Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
            MainData.MainLoop.InventoryHelperComponent.ClickedSlot(CharItem: this);
        }

    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (associatedItem != null)
        {
            MainData.MainLoop.InventoryHelperComponent.RefreshItemDescription(associatedItem);
        }
    }
}
