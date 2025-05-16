using UnityEngine;

abstract public class ViewPointSwitchBase : MonoBehaviour
{
    [SerializeField] protected ViewPoint targetViewPoint;
    protected SearchWorldManager searchWorldManager;
    public ViewPoint TargetViewPoint
    {
        get { return targetViewPoint; }
    }

    protected void OnStart()
    {
        searchWorldManager = SearchWorldManager.Instance;
    }
}