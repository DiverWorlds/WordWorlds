using TMPro;
using UnityEngine;

public class CounterTextController : MonoBehaviour
{
    private const int MAX_VALUE = 2;
    [SerializeField] private TextMeshProUGUI counterText;

    private void Start()
    {
        SetCounter(0);
    }
    public void SetCounter(int count)
    {
        counterText.text = $"{count} / {MAX_VALUE}";
    }
}