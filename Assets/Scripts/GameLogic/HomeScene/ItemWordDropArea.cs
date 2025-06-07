using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ItemWordDropArea : MonoBehaviour
{
    [SerializeField] private Transform AppearancePivot;//世界の見た目が表示される場所
    [SerializeField] private PredictCanvas predictCanvas;
    [SerializeField] private ItemWordInventoryUI draggableInventory;
    [SerializeField] private TextMeshProUGUI dropCounterText;
    [SerializeField] private Image background;//ドロップ可能範囲はこの画像のサイズに依存
    [SerializeField] private List<Transform> dropPoints;
    [SerializeField] private SearchWorldDatabase searchWorldDatabase;
    private SearchWorld predictedWorld;//予測した生成先世界の保存のための変数
    private GameObject WorldAppearance;//世界の見た目の3Dオブジェクト
    private DraggableItemWord[] droppedItemWords = new DraggableItemWord[2];
    private ItemWordInventory itemWordInventory;


    private void Start()
    {
        SetDropCounterText(0);
        itemWordInventory = ItemWordInventory.Instance;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DraggableItemWord draggableItemWord = collision.GetComponent<DraggableItemWord>();
        Logger.Log("ドロップエリアにアイテムワードが入った", draggableItemWord.ItemEntry.ItemWord.Word);
        if (draggableItemWord != null)
        {
            draggableItemWord.IsInDropArea = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        DraggableItemWord draggableItemWord = collision.GetComponent<DraggableItemWord>();
        Logger.Log("ドロップエリアからアイテムワードが出た", draggableItemWord.ItemEntry.ItemWord.Word);
        if (draggableItemWord != null)
        {
            draggableItemWord.IsInDropArea = false;
        }
    }
    public void HandleItemWordDrop(DraggableItemWord droppedItemWord)
    {
        Logger.Log("ドロップされたアイテムワード: " + droppedItemWord.ItemEntry.ItemWord.Word);
        int dropCount = droppedItemWords.Count(x => x != null);
        if (droppedItemWords[0] == null)
        {
            SetItemWordToArea(droppedItemWord, 0);
            SetDropCounterText(++dropCount);
            droppedItemWord.SetScaleOnDropArea();
        }
        else if (droppedItemWords[1] == null)
        {
            SetItemWordToArea(droppedItemWord, 1);
            SetDropCounterText(++dropCount);
            droppedItemWord.SetScaleOnDropArea();
        }
        else
        {
            Logger.Log("ドロップエリアにアイテムワードが2つあります。");
            return; // すでに2つのアイテムワードがある場合は何もしない
        }

        if (dropCount == 2)
        {
            PredictResult();
        }
    }
    private void SetItemWordToArea(DraggableItemWord droppedItemWord, int index)
    {
        droppedItemWords[index] = droppedItemWord;
        droppedItemWord.transform.position = dropPoints[index].position; // ドロップエリアの位置に移動
    }

    public void PredictResult()
    {
        predictedWorld = searchWorldDatabase.PeekRecalledWorld(droppedItemWords[0].ItemEntry.ItemWord, droppedItemWords[1].ItemEntry.ItemWord);
        if (predictedWorld is not null)
        {
            background.gameObject.SetActive(false);
            WorldAppearance = Instantiate(predictedWorld.WorldAppearance, AppearancePivot);
            predictCanvas.ShowPrediction(predictedWorld.name);
            Logger.Log("予測された世界: ", predictedWorld.name);
        }
        else
        {
            ResetWordsList();
        }
    }

    public void RecallSearchWorld()
    {
        SearchWorld searchWorld = itemWordInventory.RecallWorld(droppedItemWords[0].ItemEntry.ItemWord, droppedItemWords[1].ItemEntry.ItemWord);
        //TODO: 今後，遷移先のSearchWorld系Sceneを作成したら、以下のコメントアウトを外す
        // SceneManager.LoadScene(searchWorld.Id, LoadSceneMode.Single);
        Logger.Log($"{searchWorld.WorldName}のシーンに遷移します。");
    }

    public void OnClickCancel() //予測表示後にキャンセルが押される時
    {
        ResetWordsList();
        background.gameObject.SetActive(true);
        Destroy(WorldAppearance);
        Logger.Log($"worldAppearance is {(WorldAppearance == null ? "null" : "not null")}");
    }

    private void ResetWordsList()
    {
        Logger.Log("ドロップされたItemWordをリセットします。");
        foreach (var draggableItemWord in droppedItemWords)
        {
            draggableItemWord.ResetPosition();
        }
        SetDropCounterText(0);
    }
    private void SetDropCounterText(int count)
    {
        dropCounterText.text = $"{count} / 2";
    }
}
