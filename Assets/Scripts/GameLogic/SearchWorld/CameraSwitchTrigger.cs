using UnityEngine;
using UnityEngine.EventSystems;

public class CameraSwitchTrigger : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private string cameraName;
    [SerializeField] private SearchWorldManager searchWorldManager;
    public void OnPointerClick(PointerEventData eventData)
    {
        Logger.Log("Clicked; cameraName", cameraName);
        searchWorldManager.SwitchCamera(cameraName);
    }
}