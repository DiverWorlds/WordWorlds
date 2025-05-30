using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemWordDropArea : MonoBehaviour
{
    [SerializeField] private SearchWorldDatabase searchWorldDatabase;
    private ItemWordInventory itemWordInventory;

    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private Image background;//ドロップ可能範囲はこの画像のサイズに依存
    [SerializeField] private TextMeshProUGUI tmp_word_1;
    [SerializeField] private TextMeshProUGUI tmp_word_2;
    [SerializeField] private Transform AppearancePivot;//世界の見た目が表示される場所
    private GameObject WorldAppearance = null;//世界の見た目の3Dオブジェクト
    [SerializeField] private DraggableWordUI[] draggableWordUIs = new DraggableWordUI[2];//UIのリスト
    private SearchWorld predictWorld;//予測した生成先世界の保存のための変数
    [SerializeField] private PredictCanvas predictCanvas;
    [SerializeField] private DraggableInventory draggableInventory;


    private void Start()
    {
        countText.text = "0 / 2";
        tmp_word_1.text = "";
        tmp_word_2.text = "";
        itemWordInventory = ItemWordInventory.Instance;
    }
    public void AddItemWord(DraggableWordUI draggableWordUI)
    {
        Logger.Log("ワード：" + draggableWordUI.ItemEntry.ItemWord.name);
        if (!this.draggableWordUIs[0])
        {
            draggableWordUI.gameObject.SetActive(false);//ドロップされたワードを非表示に
            this.draggableWordUIs[0] = draggableWordUI;
            countText.text = "1 / 2";
            tmp_word_1.text = this.draggableWordUIs[0].ItemEntry.ItemWord.Word;
        }
        else if (!this.draggableWordUIs[1] && draggableWordUI != draggableWordUIs[0])
        {
            draggableWordUI.gameObject.SetActive(false);//ドロップされたワードを非表示に
            this.draggableWordUIs[1] = draggableWordUI;
            countText.text = "2 / 2";
            tmp_word_2.text = this.draggableWordUIs[1].ItemEntry.ItemWord.Word;
            PredictResult();
        }
        else
        {
            Logger.Log("同じものを選んでいるか、容量オーバーです");
        }
    }

    public void PredictResult()
    {
        predictWorld = searchWorldDatabase.GetRecalledWorld(draggableWordUIs[0].ItemEntry.ItemWord, draggableWordUIs[1].ItemEntry.ItemWord);
        if (predictWorld)
        {
            background.gameObject.SetActive(false);
            WorldAppearance = Instantiate(predictWorld.WorldAppearance, AppearancePivot);
            predictCanvas.ShowPredicion(predictWorld.name);
            Logger.Log(predictWorld.name);
        }
        else
        {
            ResetWordsList();
        }
    }

    public void RecallSearchWorld()
    {
        itemWordInventory.RecallWorld(draggableWordUIs[0].ItemEntry.ItemWord, draggableWordUIs[1].ItemEntry.ItemWord);
        predictCanvas.gameObject.SetActive(false);
        tmp_word_1.text = "";
        tmp_word_2.text = "";
        draggableWordUIs[0] = null;
        draggableWordUIs[1] = null;
        draggableInventory.HideInventory();
    }

    public void OnClickCancel() //予測表示後にキャンセルが押される時
    {
        ResetWordsList();
        background.gameObject.SetActive(true);
        Destroy(WorldAppearance);
    }

    private void ResetWordsList()
    {
        Logger.Log("Reset!!!");
        tmp_word_1.text = "";
        tmp_word_2.text = "";
        draggableWordUIs[0].gameObject.SetActive(true);
        draggableWordUIs[0] = null;
        draggableWordUIs[1].gameObject.SetActive(true);
        draggableWordUIs[1] = null;
        countText.text = "0 / 2";
    }
}
