using UnityEngine;
using UnityEngine.EventSystems;

public class MiniviewClickDetector : MonoBehaviour, IPointerClickHandler
    
{

    [HideInInspector]
    public EntityDefiner.Character associatedCharacter;

   
    public void OnPointerClick(PointerEventData eventData)
    {
        if (MainData.MainLoop.inCombat)
        {
            Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name + " but was in combat so we didn't enter inventory.");
            return;
        }

        Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
        MainData.MainLoop.UserInterfaceHelperComponent.TrinketScreenCharacter = associatedCharacter; //send them the char
        MainData.MainLoop.UserInterfaceHelperComponent.ToggleEquipmentInventory(); //show the screen
        
    }

}