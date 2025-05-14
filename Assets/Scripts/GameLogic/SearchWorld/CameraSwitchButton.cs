using System.Linq;

public class CameraSwitchButton : CameraSwitchBase
{
    void Start()
    {
        searchWorldManager.OnCameraSwitched += OnCameraSwitched;
    }
    public void OnClick()
    {
        if (searchWorldManager.CurrentCamera.CameraType == CameraType.Zoom)
        {
            searchWorldManager.SwitchCamera(targetCamera);
        }
    }
    // ズーム中にさらに画面クリックで移動することはしない想定
    // 一旦，PlayerState.Normalで，UIでカメラ移動はしない想定
    private void OnCameraSwitched()
    {
        targetCamera = searchWorldManager.CurrentCamera.MovableCameras.First();
    }
}