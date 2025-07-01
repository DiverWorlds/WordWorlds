using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DiveController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI searchWorldNameText;
    [SerializeField] private Button diveButton;
    [SerializeField] private Button cancelButton;
    private SearchWorld predictedWorld;
    private Action onRecallingCanceled;
    public SearchWorld RecalledWorld
    { set { predictedWorld = value; } }
    public event Action OnRecallingCanceled { add => onRecallingCanceled += value; remove => onRecallingCanceled -= value; }

    void OnEnable()
    {
        if (predictedWorld != null)
        {
            searchWorldNameText.text = predictedWorld.WorldName;
        }
        else
        {
            Debug.LogError("recalledWorldがnullです．");
        }
    }
    void Start()
    {
        diveButton.onClick.AddListener(Dive);
        cancelButton.onClick.AddListener(CancelDiving);
    }
    public void Dive()
    {
        //TODO: 今後，遷移先のSearchWorld系Sceneを作成したら、以下のコメントアウトを外す
        // SceneManager.LoadScene(searchWorld.Id, LoadSceneMode.Single);
        Logger.Log($"{predictedWorld.WorldName}のシーンに遷移します。");
    }
    public void CancelDiving()
    {
        predictedWorld = null;
        searchWorldNameText.text = "-";
        onRecallingCanceled.Invoke();
        gameObject.SetActive(false);
    }
}