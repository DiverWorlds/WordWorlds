using UnityEngine;
using UnityEngine.EventSystems;

public class CameraSwitchTrigger : CameraSwitchBase, IPointerClickHandler
{

    public void OnPointerClick(PointerEventData eventData)
    {
        Logger.Log("Clicked; cameraName", targetCamera.name);
        if (searchWorldManager.CurrentCamera.MovableCameras.Contains(targetCamera))
        {
            searchWorldManager.SwitchCamera(targetCamera);
        }
    }
}