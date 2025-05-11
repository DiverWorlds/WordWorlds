using System;
using System.Collections.Generic;
using UnityEngine;

public class SearchWorldManager : MonoBehaviour
{
    [Serializable]
    private class CameraData
    {
        public string name;
        public Camera camera;
    }

    [SerializeField] private List<CameraData> cameraList = new();
    private Dictionary<string, Camera> cameras = new();
    private string currentCameraName = "";
    public string CurrentCameraName => currentCameraName;

    void Start()
    {
        currentCameraName = cameraList[0].name;

        foreach (CameraData cameraData in cameraList)
        {
            cameras.Add(cameraData.name, cameraData.camera);
        }
    }
    public void SwitchCamera(string cameraName)
    {
        cameras[currentCameraName].gameObject.SetActive(false);
        currentCameraName = cameraName;
        cameras[currentCameraName].gameObject.SetActive(true);
    }
}