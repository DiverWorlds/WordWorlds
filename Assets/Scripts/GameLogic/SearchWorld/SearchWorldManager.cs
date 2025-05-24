using System;
using System.Collections.Generic;
using UnityEngine;

public class SearchWorldManager : Singleton<SearchWorldManager>
{
    [SerializeField] private List<ViewPoint> viewPointList = new();
    [SerializeField] private ViewPointSwitchButton viewPointBackButton;
    private ViewPoint currentViewPoint;
    private PlayerState playerState = PlayerState.Normal;
    private Action onViewPointSwitched;
    public ViewPoint CurrentViewPoint => currentViewPoint;
    public PlayerState PlayerState => playerState;
    public event Action OnViewPointSwitched { add => onViewPointSwitched += value; remove => onViewPointSwitched -= value; }

    void Start()
    {
        currentViewPoint = viewPointList[0];
        for (int i = 0; i < viewPointList.Count; i++)
        {
            if (i == 0)
            {
                viewPointList[i].gameObject.SetActive(true);
            }
            else
            {
                viewPointList[i].gameObject.SetActive(false);
            }
        }
        onViewPointSwitched?.Invoke();
    }
    public void SwitchViewPoint(ViewPoint viewPoint)
    {
        ViewPoint nextViewPoint = viewPoint;
        if (nextViewPoint.Type == ViewPointType.Zoom)
        {
            viewPointBackButton.gameObject.SetActive(true);
            viewPointBackButton.TargetViewPoint = currentViewPoint;
        }
        else if (nextViewPoint.Type == ViewPointType.Main)
        {
            viewPointBackButton.gameObject.SetActive(false);
        }
        currentViewPoint.gameObject.SetActive(false);
        nextViewPoint.gameObject.SetActive(true);
        currentViewPoint = nextViewPoint;
        onViewPointSwitched?.Invoke();
    }
}