using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WordDropper : MonoBehaviour
{
    [SerializeField] private SearchWorldDatabase searchWorldDatabase;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private HomeManager homeManager;
    [SerializeField] private TextMeshProUGUI tmp_word_1;
    [SerializeField] private TextMeshProUGUI tmp_word_2;
    private DraggableWordUI[] draggableWordUIs = new DraggableWordUI[2];
    private SearchWorld predictWorld;


    private void Start()
    {
        countText.text = "0 / 2";
        tmp_word_1.text = "";
        tmp_word_2.text = "";
    }
    public void AddItemWord(DraggableWordUI draggableWordUI)
    {
        Logger.Log("ワード：" + draggableWordUI.ItemEntry.ItemWord.name);
        if (!this.draggableWordUIs[0])
        {
            this.draggableWordUIs[0] = draggableWordUI;
            countText.text = "1 / 2";
            tmp_word_1.text = this.draggableWordUIs[0].ItemEntry.ItemWord.Word;
        }
        else if (!this.draggableWordUIs[1] && draggableWordUI != draggableWordUIs[0])
        {
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
        Logger.Log(predictWorld.name);
    }
}
