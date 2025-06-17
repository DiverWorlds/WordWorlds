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
    [SerializeField] private List<RectTransform> dropPointObjects;
    [SerializeField] private SearchWorldDatabase searchWorldDatabase;
    private SearchWorld predictedWorld;//予測した生成先世界の保存のための変数
    private GameObject WorldAppearance;//世界の見た目の3Dオブジェクト
    private List<Vector2> dropPoints = new List<Vector2>();
    private DraggableItemWord[] droppedItemWords = new DraggableItemWord[2];
    private ItemWordInventory itemWordInventory;
    private int DropCount => droppedItemWords.Count(x => x != null);

    private void Start()
    {
        SetDropCounterText(0);
        foreach (var dropPointObject in dropPointObjects)
        {
            dropPoints.Add(dropPointObject.position);
        }
        itemWordInventory = ItemWordInventory.Instance;
    }
    /// <summary>
    /// ドロップされたアイテムワードを処理します。
    /// 「2つ目がドロップされ，それらがRecipeにない組み合わせの場合」にfalse，それ以外はtrueを返します。
    /// </summary>
    /// <param name="droppedItemWord"></param>
    /// <returns></returns>
    public bool HandleItemWordDrop(DraggableItemWord droppedItemWord)
    {
        bool hasEmptySlot = false;
        if (droppedItemWords[0] == null)
        {
            droppedItemWords[0] = droppedItemWord;
            //TODO: ItemWordの左上を基準にdropPointへ移動してしまう問題あり．
            droppedItemWord.SetAndRecordPosition(dropPoints[0]);
            hasEmptySlot = true;
        }
        else if (droppedItemWords[1] == null)
        {
            droppedItemWords[1] = droppedItemWord;
            droppedItemWord.SetAndRecordPosition(dropPoints[1]);
            hasEmptySlot = true;
        }
        else
        {
            Logger.Log("ドロップエリアにアイテムワードが2つあります。");
            Logger.Log("これは想定されていない状態です。");
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
                return false;
            }
        }

        return true;
    }
    public void HandleItemWordRemove(DraggableItemWord removedItemWord)
    {
        int index = System.Array.IndexOf(droppedItemWords, removedItemWord);
        droppedItemWords[index] = null;
        SetDropCounterText(DropCount);
        removedItemWord.AutoMoveToInitialPosition();
    }
    private bool PredictResult()
    {
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
    }

    private void ResetWordsList()
    {
        foreach (var draggableItemWord in droppedItemWords)
        {
            if (draggableItemWord == null) continue; // nullチェックを追加
            draggableItemWord.SetInitialScale();
            draggableItemWord.AutoMoveToInitialPosition();
        }
        droppedItemWords = new DraggableItemWord[2];
        SetDropCounterText(0);
    }
    private void SetDropCounterText(int count)
    {
        dropCounterText.text = $"{count} / 2";
    }
}
