using TMPro;
using UnityEngine;

public class CounterTextController : MonoBehaviour
{
    private const int MAX_VALUE = 2;
    [SerializeField] private TextMeshProUGUI counterText;

    public void SetCounter(int count)
    {
        counterText.text = $"{count} / {MAX_VALUE}";
    }
}