using System.Collections.Generic;
using UnityEngine;

public class MyCamera : MonoBehaviour
{
    [SerializeField] private new Camera camera;
    [SerializeField] private CameraType cameraType;
    [SerializeField] private SearchRoom locatedRoom;
    [SerializeField] private List<MyCamera> movableCameras;
    public Camera Camera => camera;
    public CameraType CameraType => cameraType;
    public SearchRoom LocatedRoom => locatedRoom;
    public List<MyCamera> MovableCameras => movableCameras;
}