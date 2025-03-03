using UnityEngine;

public class CancelButton : MonoBehaviour
{
    [SerializeField] HomeManager homeManager;

    public void OnClick()
    {
        homeManager.ResetSelect();
    }
}
