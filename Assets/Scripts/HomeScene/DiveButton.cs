using UnityEngine;

public class DiveButton : MonoBehaviour
{
    public void OnClick()
    {
        if (HomeManager.Instance.CurrentSearchWorld)
        {
            HomeManager.Instance.DiveToSearchWorld();
        }
        else
        {
            Logger.Log("世界がありません...");
        }
    }
}
