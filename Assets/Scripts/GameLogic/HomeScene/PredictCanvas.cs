using TMPro;
using UnityEngine;

public class PredictCanvas : MonoBehaviour
{
    [SerializeField] private ItemWordDropArea wordDropper;
    [SerializeField] private TextMeshProUGUI worldName;
    public void ShowPrediction(string worldName)
    {
        this.worldName.text = worldName;
        gameObject.SetActive(true);
    }

    public void OnClickDecide()
    {
        wordDropper.RecallSearchWorld();
    }

    public void OnClickCancel()
    {
        gameObject.SetActive(false);
    }
}
