using UnityEngine;

public class CameraSwitchButton : MonoBehaviour
{
    [SerializeField] private string cameraName;
    [SerializeField] private SearchWorldManager searchWorldManager;

    public void OnClick()
    {
        if (gameObject.name.Contains("Back"))
        {
            if (searchWorldManager.CurrentCameraName.Contains("Zoom"))
            {
                searchWorldManager.SwitchCamera(cameraName);
            }
        }
        else
        {
            searchWorldManager.SwitchCamera(cameraName);
        }
    }
}