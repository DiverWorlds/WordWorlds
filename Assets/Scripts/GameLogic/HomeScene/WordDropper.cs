using System.Collections.Generic;
using UnityEngine;

public class WordDropper : MonoBehaviour
{
    [SerializeField] private SearchWorldDatabase searchWorldDatabase;
    private DraggableWordUI[] draggableWordUIs = new DraggableWordUI[2];
    private SearchWorld predictWorld;
    public void AddItemWord(DraggableWordUI draggableWordUI)
    {
        Logger.Log("ワード：" + draggableWordUI.ItemEntry.ItemWord.name);
        if (!this.draggableWordUIs[0])
        {
            this.draggableWordUIs[0] = draggableWordUI;
        }
        else if (!this.draggableWordUIs[1] && draggableWordUI != draggableWordUIs[0])
        {
            this.draggableWordUIs[1] = draggableWordUI;
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
