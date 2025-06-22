using TMPro;
using UnityEngine;

public class PredictCanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI worldName;
    private ItemWordDropArea itemWordDropArea;

    void Start()
    {
        itemWordDropArea = HomeManager.Instance.ItemWordDropArea;
    }
    public void ShowPrediction(string worldName)
    {
        this.worldName.text = worldName;
        gameObject.SetActive(true);
    }

    public void OnClickDecide()
    {
        itemWordDropArea.RecallSearchWorld();
    }

    public void OnClickCancel()
    {
        itemWordDropArea.CancelRecalling();
        gameObject.SetActive(false);
    }
}
