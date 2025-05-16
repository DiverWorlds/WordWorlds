using UnityEngine;

abstract public class ViewPointSwitchBase : MonoBehaviour
{
    [SerializeField] protected ViewPoint targetViewPoint;
    [SerializeField] protected SearchWorldManager searchWorldManager;
    public ViewPoint TargetViewPoint
    {
        get { return targetViewPoint; }
    }
}