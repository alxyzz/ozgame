using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryEqObject : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public EntityDefiner.Item associatedItem;
    public Image selfImage;
    public Image background;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (associatedItem != null && !MainData.MainLoop.InventoryHelperComponent.coolDown)
        {
            //Debug.LogError("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
            MainData.MainLoop.InventoryHelperComponent.ClickedSlot(CharItem: this);
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (associatedItem != null)
        {
            //Debug.LogWarning("entered mouse on item");
            MainData.MainLoop.InventoryHelperComponent.RefreshItemDescription(associatedItem);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (associatedItem != null)
        {
            //Debug.LogWarning("exited mouse on item ");
            MainData.MainLoop.InventoryHelperComponent.RefreshItemDescription();
        }
    }
}
