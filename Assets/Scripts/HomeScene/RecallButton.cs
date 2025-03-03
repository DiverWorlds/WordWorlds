using TMPro;
using UnityEngine;

public class RecallButton : MonoBehaviour
{
    [SerializeField] private HomeManager homeManager;

    public void OnClick()
    {
        HomeManager.Instance.CombineItemWord();
    }
}
