using TMPro;
using UnityEngine;

public class RecallButton : MonoBehaviour
{
    [SerializeField] private ItemWordDatabase itemWordDatabase;
    [SerializeField] private DiveButton diveButton;
    private ItemWordInventory itemWordInventory;
    private GameObject elemItemWord1;
    public GameObject ElemItemWord1
    {
        set
        {
            elemItemWord1 = value;
            ShowDebugText();
        }
        get { return elemItemWord1; }
    }

    private GameObject elemItemWord2;
    public GameObject ElemItemWord2
    {
        set
        {
            elemItemWord2 = value;
            ShowDebugText();
        }
        get { return elemItemWord2; }
    }

    //開発用
    [SerializeField] private TextMeshProUGUI debugText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //ItemWordInventoryを開発用に調整
        itemWordInventory = ItemWordInventory.Instance;
        Logger.LogElements($"Before Inventory", itemWordInventory.Inventory);
        Logger.Log("Add Word \"a\". Result", itemWordInventory.AddItemWord(itemWordDatabase.GetItemWord("a")));
        Logger.Log("Add Word \"b\". Result", itemWordInventory.AddItemWord(itemWordDatabase.GetItemWord("b")));
        Logger.Log("Add Word \"c\". Result", itemWordInventory.AddItemWord(itemWordDatabase.GetItemWord("c")));
        Logger.Log("Add Word \"d\". Result", itemWordInventory.AddItemWord(itemWordDatabase.GetItemWord("d")));
        Logger.LogElements($"After Inventory", itemWordInventory.Inventory);
    }

    public void OnClick()
    {
        if (ElemItemWord1 && ElemItemWord2) diveButton.CurrentSearchWorld = CombineItemWord(ElemItemWord1, ElemItemWord2);
        ResetSelect();
    }

    public void SelectWord(GameObject uiItemWord)//UIから合成するワードを選択する
    {
        if (!ElemItemWord1) ElemItemWord1 = uiItemWord;
        else if (ElemItemWord1 == uiItemWord)
        {
            Debug.Log("同じワードを選ぶことはできません");
        }
        else if (!ElemItemWord2)
        {
            ElemItemWord2 = uiItemWord;
        }
        else Debug.Log("すでに2つのワードが選択されています");
    }

    public SearchWorld CombineItemWord(GameObject word1, GameObject word2)//実際にワードをミックス、世界を生成する
    {
        ItemWordButton ItemWordButton1 = word1.GetComponent<ItemWordButton>();
        ItemEntry itemEntry1 = ItemWordButton1.GetItem();
        ItemWord itemWord1 = itemEntry1.ItemWord;

        ItemWordButton ItemWordButton2 = word2.GetComponent<ItemWordButton>();
        ItemEntry itemEntry2 = ItemWordButton2.GetItem();
        ItemWord itemWord2 = itemEntry2.ItemWord;
        ElemItemWord1.GetComponent<ItemWordButton>().CheckIsUsed();
        ElemItemWord2.GetComponent<ItemWordButton>().CheckIsUsed();

        return itemWordInventory.RecallWorld(itemWord1, itemWord2);//ItemWord二つからSearchWorld一つを生成}
    }

    public void ResetSelect()
    {
        //リセット
        ElemItemWord1 = null;
        ElemItemWord2 = null;
    }

    public void ShowDebugText()
    {
        string text = "";
        if (ElemItemWord1) text += "選択: " + ElemItemWord1.GetComponent<ItemWordButton>().GetItem().ItemWord.Word + "\n";
        if (ElemItemWord2) text += "選択: " + ElemItemWord2.GetComponent<ItemWordButton>().GetItem().ItemWord.Word + "\n";
        if (diveButton.CurrentSearchWorld) text += "現在の世界" + diveButton.CurrentSearchWorld.WorldName + "\n";
        debugText.text = text;
    }
}
