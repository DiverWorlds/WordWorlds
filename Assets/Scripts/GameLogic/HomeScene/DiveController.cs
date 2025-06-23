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
    private SearchWorld recalledWorld;
    private Action onRecallingCanceled;
    public SearchWorld RecalledWorld
    { set { recalledWorld = value; } }
    public event Action OnRecallingCanceled { add => onRecallingCanceled += value; remove => onRecallingCanceled -= value; }

    void OnEnable()
    {
        if (recalledWorld != null)
        {
            searchWorldNameText.text = recalledWorld.WorldName;
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
    public void Hide()
    {
        searchWorldNameText.text = "-";
        recalledWorld = null;
    }
    public void Dive()
    {
        //TODO: 今後，遷移先のSearchWorld系Sceneを作成したら、以下のコメントアウトを外す
        // SceneManager.LoadScene(searchWorld.Id, LoadSceneMode.Single);
        Logger.Log($"{recalledWorld.WorldName}のシーンに遷移します。");
    }
    public void CancelDiving()
    {
        recalledWorld = null;
        onRecallingCanceled.Invoke();
        gameObject.SetActive(false);
    }
}