using UnityEngine;
using UnityEngine.EventSystems;

public class ViewPointSwitchTrigger : ViewPointSwitchBase, IPointerClickHandler
{
    //TODO: Itemスクリプトを作る
    public void OnPointerClick(PointerEventData eventData)
    {
        Logger.Log("Clicked; viewPointName", targetViewPoint.name);
        if (searchWorldManager.CurrentViewPoint.MovableViewPoints.Contains(targetViewPoint))
        {
            searchWorldManager.SwitchViewPoint(targetViewPoint);
        }
    }
}