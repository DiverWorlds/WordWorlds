using UnityEngine;

public class DiveButton : MonoBehaviour
{
    private SearchWorld currentSearchWorld;
    public SearchWorld CurrentSearchWorld { get; set; }
    public void OnClick()
    {
        if (currentSearchWorld) Debug.Log(currentSearchWorld.WorldName + "にダイブ!!");
        else Debug.Log("世界がありません...");
    }
}
