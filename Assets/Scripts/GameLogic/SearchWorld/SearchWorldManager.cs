using System;
using System.Collections.Generic;
using UnityEngine;

public class SearchWorldManager : MonoBehaviour
{
    [SerializeField] private List<MyCamera> cameraList = new();
    private MyCamera currentCamera;
    private PlayerState playerState = PlayerState.Normal;
    private Action onCameraSwitched;
    public MyCamera CurrentCamera => currentCamera;
    public PlayerState PlayerState => playerState;
    public event Action OnCameraSwitched { add => onCameraSwitched += value; remove => onCameraSwitched -= value; }

    void Start()
    {
        currentCamera = cameraList[0];
        for (int i=0; i<cameraList.Count; i++)
        {
            if (i==0)
            {
                cameraList[i].gameObject.SetActive(true);
            }
            else
            {
                cameraList[i].gameObject.SetActive(false);
            }
        }
    }
    public void SwitchCamera(MyCamera myCamera)
    {
        MyCamera nextCamera = myCamera;
        currentCamera.gameObject.SetActive(false);
        currentCamera = nextCamera;
        nextCamera.gameObject.SetActive(true);
        onCameraSwitched.Invoke();
    }
}