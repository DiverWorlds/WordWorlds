using System.Collections.Generic;
using UnityEngine;

public class ViewPoint : MonoBehaviour
{
    [SerializeField] private new Camera camera;
    [SerializeField] private ViewPointType type;
    [SerializeField] private SearchRoom locatedRoom;
    [SerializeField] private List<ViewPoint> movableViewPoints;
    public Camera Camera => camera;
    public ViewPointType Type => type;
    public SearchRoom LocatedRoom => locatedRoom;
    public List<ViewPoint> MovableViewPoints => movableViewPoints;
}