using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SearchWorldManager : MonoBehaviour
{
    [SerializeField] private SearchWorld searchWorld;
    [SerializeField] private TextMeshProUGUI worldName;
    [SerializeField] private Image backgroundImage;

    void Start()
    {
        worldName.text = searchWorld.WorldName;
        backgroundImage.sprite = searchWorld.Sprite;
    }
}