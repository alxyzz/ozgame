using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class charDisplayStatisticsMouseDetection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        MainData.MainLoop.InventoryHelperComponent.PopUpCharInfo.SetActive(true);
        MainData.MainLoop.InventoryHelperComponent.RefreshCharacterStatistics();

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MainData.MainLoop.InventoryHelperComponent.PopUpCharInfo.SetActive(false);

    }
}
