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
        searchWorldManager.SwitchViewPoint(targetViewPoint);
    }
}