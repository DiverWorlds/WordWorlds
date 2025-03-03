using UnityEngine;

public class DiveButton : MonoBehaviour
{
    [SerializeField] HomeManager homeManager;
    public void OnClick()
    {
        if (homeManager.CurrentSearchWorld) Logger.Log(homeManager.CurrentSearchWorld.WorldName + "にダイブ!!");
        else Logger.Log("世界がありません...");
    }
}
