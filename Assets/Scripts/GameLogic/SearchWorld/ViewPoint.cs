using System.Collections.Generic;
using UnityEngine;

public class ViewPoint : MonoBehaviour
{
    //TODO: located RoomをEditor拡張で自動アタッチ
    [SerializeField] private new Camera camera;
    [SerializeField] private ViewPointType type;
    [SerializeField] private SearchRoom locatedRoom;
    [SerializeField] private List<EventTargetCondition> activeEvents;
    public Camera Camera => camera;
    public ViewPointType Type => type;
    public SearchRoom LocatedRoom => locatedRoom;
    public List<EventTargetCondition> ActiveEvents => activeEvents;
}