using System.Linq;
using UnityEngine;

public class ViewPointSwitchButton : MonoBehaviour
{
    // targetはボタン活性化の際にsetする．
    private ViewPoint targetViewPoint;
    private SearchWorldManager searchWorldManager;
    public ViewPoint TargetViewPoint
    {
        get { return targetViewPoint; }
        set { targetViewPoint = value; }
    }

    void Awake()
    {
        searchWorldManager = SearchWorldManager.Instance;
    }
    public void OnClick()
    {
        if (searchWorldManager.CurrentViewPoint.Type == ViewPointType.Zoom)
        {
            searchWorldManager.SwitchViewPoint(targetViewPoint);
        }
    }
}