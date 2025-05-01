using UnityEngine;

public class CancelButton : MonoBehaviour
{
    public void OnClick()
    {
        HomeManager.Instance.ResetSelect();
    }
}
