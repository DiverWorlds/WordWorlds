using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//TODO: ItemWordDropAreaとDraggableItemWord間の依存関係の解消を検討する．
public class ItemWordDropArea : MonoBehaviour
{
    [SerializeField] private Transform AppearancePivot;//世界の見た目が表示される場所
    [SerializeField] private ItemWordInventoryUI draggableInventory;
    [SerializeField] private TextMeshProUGUI dropCounterText;
    [SerializeField] private Image background;//ドロップ可能範囲はこの画像のサイズに依存
    [SerializeField] private List<Transform> dropPoints;
    [SerializeField] private SearchWorldDatabase searchWorldDatabase;
    private SearchWorld predictedWorld;//予測した生成先世界の保存のための変数
    private GameObject WorldAppearance;//世界の見た目の3Dオブジェクト
    private DraggableItemWord[] droppedItemWords = new DraggableItemWord[2];
    private ItemWordInventory itemWordInventory;
    private int DropCount => droppedItemWords.Count(x => x != null);

    private void Start()
    {
        SetDropCounterText(0);
        itemWordInventory = ItemWordInventory.Instance;
    }
    public bool HandleItemWordDrop(DraggableItemWord droppedItemWord)
    {
        Logger.Log("ドロップされたアイテムワード: " + droppedItemWord.ItemEntry.ItemWord.Word);
        bool hasEmptySlot = false;
        if (droppedItemWords[0] == null)
        {
            Logger.Log("1つ目をセット");
            droppedItemWords[0] = droppedItemWord;
            droppedItemWord.SetPosition(dropPoints[0].position);
            hasEmptySlot = true;
        }
        else if (droppedItemWords[1] == null)
        {
            Logger.Log("2つ目をセット");
            droppedItemWords[1] = droppedItemWord;
            droppedItemWord.SetPosition(dropPoints[1].position);
            hasEmptySlot = true;
        }
        else
        {
            Logger.Log("ドロップエリアにアイテムワードが2つあります。");
            return false;
        }

        if (hasEmptySlot)
        {
            SetDropCounterText(DropCount);
        }

        if (DropCount == 2)
        {
            bool isSuccessPredicting = PredictResult();
            if (isSuccessPredicting)
            {
                HomeManager.Instance.PredictCanvas.ShowPrediction(predictedWorld.name);
            }
            else
            {
                ResetWordsList();
            }
        }

        return true;
    }
    public void HandleItemWordRemove(DraggableItemWord removedItemWord)
    {
        Logger.Log("ドロップエリアからアイテムワードが削除された: " + removedItemWord.ItemEntry.ItemWord.Word);
        int index = System.Array.IndexOf(droppedItemWords, removedItemWord);
        droppedItemWords[index] = null;
        SetDropCounterText(DropCount);
        removedItemWord.AutoMoveToInitialPosition();
    }
    private bool PredictResult()
    {
        Logger.Log("PrediqtResultを実行します。");
        Logger.Log("Word0: " + droppedItemWords[0].ItemEntry.ItemWord.Word);
        Logger.Log("Word1: " + droppedItemWords[1].ItemEntry.ItemWord.Word);
        predictedWorld = searchWorldDatabase.PeekRecalledWorld(droppedItemWords[0].ItemEntry.ItemWord, droppedItemWords[1].ItemEntry.ItemWord);
        if (predictedWorld is not null)
        {
            background.gameObject.SetActive(false);
            WorldAppearance = Instantiate(predictedWorld.WorldAppearance, AppearancePivot);
            Logger.Log("予測された世界: ", predictedWorld.name);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void RecallSearchWorld()
    {
        SearchWorld searchWorld = itemWordInventory.RecallWorld(droppedItemWords[0].ItemEntry.ItemWord, droppedItemWords[1].ItemEntry.ItemWord);
        //TODO: 今後，遷移先のSearchWorld系Sceneを作成したら、以下のコメントアウトを外す
        // SceneManager.LoadScene(searchWorld.Id, LoadSceneMode.Single);
        Logger.Log($"{searchWorld.WorldName}のシーンに遷移します。");
    }

    public void CancelRecalling() //予測表示後にキャンセルが押される時
    {
        ResetWordsList();
        background.gameObject.SetActive(true);
        Destroy(WorldAppearance);
        Logger.Log($"worldAppearance is {(WorldAppearance == null ? "null" : "not null")}");
    }

    private void ResetWordsList()
    {
        Logger.Log("ドロップされた2つのItemWordをリセットします。");
        Logger.LogElements("droppedItemWords", droppedItemWords.Select(x => x?.ItemEntry.ItemWord.Word).ToArray());
        foreach (var draggableItemWord in droppedItemWords)
        {
            if (draggableItemWord == null) continue; // nullチェックを追加
            draggableItemWord.SetInitialScale();
            draggableItemWord.AutoMoveToInitialPosition();
            Logger.Log("ItemWordを戻した", draggableItemWord.ItemEntry.ItemWord.Word);
        }
        droppedItemWords = new DraggableItemWord[2];
        SetDropCounterText(0);
    }
    private void SetDropCounterText(int count)
    {
        dropCounterText.text = $"{count} / 2";
    }
}
