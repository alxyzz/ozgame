using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DetectHoverConsumables : MonoBehaviour/*, IPointerEnterHandler, IPointerExitHandler*/
{
    public PopUpWhenMouseNearby popupScriptRef;
    public RectTransform imgRectTransform;


    bool mouse_over = false;


    private void Update()
    {
        Vector2 localMousePosition = imgRectTransform.InverseTransformPoint(Input.mousePosition);
        if (imgRectTransform.rect.Contains(localMousePosition))
        {
            popupScriptRef.gameObject.SetActive(true);
        }
        else
        {
            popupScriptRef.gameObject.SetActive(false);
        }
    }

    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    if (!mouse_over)
    //    {
    //        popupScriptRef.gameObject.SetActive(true);
    //    }

    //}

    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    mouse_over = false;
    //    popupScriptRef.gameObject.SetActive(false);
    //}
}
