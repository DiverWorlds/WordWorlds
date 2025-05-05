using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WordDropper : MonoBehaviour
{
    [SerializeField] private SearchWorldDatabase searchWorldDatabase;
    private ItemWordInventory itemWordInventory;

    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private HomeManager homeManager;
    [SerializeField] private Image background;
    [SerializeField] private TextMeshProUGUI tmp_word_1;
    [SerializeField] private TextMeshProUGUI tmp_word_2;
    [SerializeField] private Transform AppearancePivot;
    [SerializeField] private RawImage defaultViewImage;
    private GameObject WorldAppearance = null;
    [SerializeField] private DraggableWordUI[] draggableWordUIs = new DraggableWordUI[2];
    private SearchWorld predictWorld;
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
