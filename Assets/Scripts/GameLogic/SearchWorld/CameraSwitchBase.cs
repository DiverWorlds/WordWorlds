using UnityEngine;

abstract public class CameraSwitchBase : MonoBehaviour
{
    [SerializeField] protected MyCamera targetCamera;
    [SerializeField] protected SearchWorldManager searchWorldManager;
    public MyCamera TargetCamera
    {
        get { return targetCamera; }
    }
}