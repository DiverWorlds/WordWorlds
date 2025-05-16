using System.Linq;

public class ViewPointSwitchButton : ViewPointSwitchBase
{
    void Start()
    {
        searchWorldManager.OnViewPointSwitched += OnViewPointSwitched;
    }
    public void OnClick()
    {
        if (searchWorldManager.CurrentViewPoint.Type == ViewPointType.Zoom)
        {
            searchWorldManager.SwitchViewPoint(targetViewPoint);
        }
    }
    // ズーム中にさらに画面クリックで移動することはしない想定
    // 一旦，PlayerState.Normalで，UIでカメラ移動はしない想定
    private void OnViewPointSwitched()
    {
        targetViewPoint = searchWorldManager.CurrentViewPoint.MovableViewPoints.First();
    }
}