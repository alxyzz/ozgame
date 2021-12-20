using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DetectHoverConsumables : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public PopUpWhenMouseNearby popupScriptRef;



    // Start is called before the first frame update
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("entered rect");
        popupScriptRef.mouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("left rect");
        popupScriptRef.mouseOver = false;
    }
}
