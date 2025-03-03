using UnityEngine;

public class DiveButton : MonoBehaviour
{
    public void OnClick()
    {
        if (HomeManager.Instance.CurrentSearchWorld) Logger.Log(HomeManager.Instance.CurrentSearchWorld.WorldName + "にダイブ!!");
        else Logger.Log("世界がありません...");
    }
}
