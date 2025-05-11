using UnityEngine;

public class ZoomEffect : MonoBehaviour
{
    [SerializeField] private ZoomedView zoomedView;
    public void OnClick()
    {
        zoomedView.gameObject.SetActive(true);
    }
}