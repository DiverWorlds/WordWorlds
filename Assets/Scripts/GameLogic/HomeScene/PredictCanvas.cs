using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PredictCanvas : MonoBehaviour
{
    [SerializeField] private ItemWordDropArea wordDropper;
    [SerializeField] private TextMeshProUGUI worldName;
    public void ShowPrediction(string worldName)
    {
        this.worldName.text = worldName;
        this.gameObject.SetActive(true);
    }

    public void OnClickDecide()
    {
        wordDropper.RecallSearchWorld();
    }

    public void OnClickCancel()
    {
        this.gameObject.SetActive(false);
    }
}
