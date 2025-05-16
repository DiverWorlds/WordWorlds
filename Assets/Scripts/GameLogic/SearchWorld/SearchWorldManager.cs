using System;
using System.Collections.Generic;
using UnityEngine;

public class SearchWorldManager : MonoBehaviour
{
    //TODO: シングルトンにして，クラスからアクセスできるようにする
    [SerializeField] private List<ViewPoint> viewPointList = new();
    private ViewPoint currentViewPoint;
    private PlayerState playerState = PlayerState.Normal;
    private Action onViewPointSwitched;
    public ViewPoint CurrentViewPoint => currentViewPoint;
    public PlayerState PlayerState => playerState;
    public event Action OnViewPointSwitched { add => onViewPointSwitched += value; remove => onViewPointSwitched -= value; }

    void Start()
    {
        currentViewPoint = viewPointList[0];
        for (int i=0; i<viewPointList.Count; i++)
        {
            if (i==0)
            {
                viewPointList[i].gameObject.SetActive(true);
            }
            else
            {
                viewPointList[i].gameObject.SetActive(false);
            }
        }
    }
    public void SwitchViewPoint(ViewPoint viewPoint)
    {
        ViewPoint nextViewPoint = viewPoint;
        currentViewPoint.gameObject.SetActive(false);
        currentViewPoint = nextViewPoint;
        nextViewPoint.gameObject.SetActive(true);
        onViewPointSwitched.Invoke();
    }
}