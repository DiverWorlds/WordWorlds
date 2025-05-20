using UnityEngine;

public class SwitchViewPointEffect : MonoBehaviour, IEventEffect
{
    [SerializeField] private ViewPoint targetViewPoint;
    private SearchWorldManager searchWorldManager;
    void Start()
    {
        searchWorldManager = SearchWorldManager.Instance;
    }
    public void EventEffect()
    {
        Logger.Log($"{gameObject.name}: EventEffect()");
        if (searchWorldManager.CurrentViewPoint.MovableViewPoints.Contains(targetViewPoint))
        {
            searchWorldManager.SwitchViewPoint(targetViewPoint);
        }
        else
        {
            Logger.Log($"{targetViewPoint.name}は{gameObject.name}の移動先に設定されていません．");
        }
    }
}