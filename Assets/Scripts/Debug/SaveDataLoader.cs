using UnityEngine;

public class SaveDataLoader : MonoBehaviour
{
    public void OnClick()
    {
        SaveDataManager.Instance.Load();
    }
}