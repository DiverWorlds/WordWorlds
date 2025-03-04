using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HomeManager : Singleton<HomeManager>
{
    private ItemWordInventory itemWordInventory;
    [SerializeField] private ItemWordDatabase itemWordDatabase;

    //シーン内のオブジェクト(UI)をSerializeFieldとして格納
    [SerializeField] private WakeToEndingButton wakeToEndingButton;//エンディングへ向かうWakeボタン
    private FlagManager flagManager;//FlagManagerはSingletonのInstanceから取得

    //データ類
    public SearchWorld CurrentSearchWorld { get; set; }
    private ItemWordButton elemItemWord1;
    public ItemWordButton ElemItemWord1
    {
        set
        {
            elemItemWord1 = value;
            ShowDebugText();
        }
        get { return elemItemWord1; }
    }

    private ItemWordButton elemItemWord2;
    public ItemWordButton ElemItemWord2
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

    void Start()
    {
        flagManager = FlagManager.Instance;
        flagManager.LoadSavedFlags();

        //エンディングに迎えるならばエンディング用のWakeButtonを表示
        wakeToEndingButton.gameObject.SetActive(false);
        if (flagManager.Get("Dev_WordAGet")) wakeToEndingButton.gameObject.SetActive(true);

        itemWordInventory = ItemWordInventory.Instance;
    }

    public void SelectWord(ItemWordButton itemWordButton)//UIから合成するワードを選択する
    {
        if (ElemItemWord1 == null) ElemItemWord1 = itemWordButton;
        else if (ElemItemWord1 == itemWordButton) Logger.Log("同じワードを選ぶことはできません");
        else if (ElemItemWord2 == null) ElemItemWord2 = itemWordButton;
        else Logger.Log("すでに2つのワードが選択されています");
    }

    public void CombineItemWord()//実際にワードをミックス、世界を生成する
    {
        CurrentSearchWorld = itemWordInventory.RecallWorld(ElemItemWord1.GetItemEntry().ItemWord, ElemItemWord2.GetItemEntry().ItemWord);//ItemWord二つからSearchWorld一つを生成}
        ElemItemWord1.CheckIsUsed();
        ElemItemWord2.CheckIsUsed();
        ResetSelect();
    }

    public void ResetSelect()
    {
        ElemItemWord1 = null;
        ElemItemWord2 = null;
    }

    public void ShowDebugText()
    {
        string text = "";
        if (ElemItemWord1) text += "選択: " + ElemItemWord1.GetComponent<ItemWordButton>().GetItemEntry().ItemWord.Word + "\n";
        if (ElemItemWord2) text += "選択: " + ElemItemWord2.GetComponent<ItemWordButton>().GetItemEntry().ItemWord.Word + "\n";
        if (CurrentSearchWorld) text += "現在の世界" + CurrentSearchWorld.WorldName + "\n";
        debugText.text = text;
    }
}
