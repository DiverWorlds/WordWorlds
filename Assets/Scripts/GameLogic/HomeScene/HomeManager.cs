using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
//TODO: Scene実行を終了すると以下が起こる問題を直す
/*
Some objects were not cleaned up when closing the scene. (Did you spawn new GameObjects from OnDestroy?)
The following scene GameObjects were found:
GlobalDB
ItemWordInventory
*/
public class HomeManager : Singleton<HomeManager>
{
    [SerializeField] private ItemWordDatabase itemWordDatabase;
    private ItemWordInventory itemWordInventory;
    private FlagManager flagManager;//FlagManagerはSingletonのInstanceから取得

    //データ類
    public SearchWorld CurrentSearchWorld { get; set; }
    private ItemWordButton elemItemWord1;//?ボタン？押せないやつじゃない？
    private ItemWordButton elemItemWord2;
    public ItemWordButton ElemItemWord1
    {
        get { return elemItemWord1; }
        set
        {
            elemItemWord1 = value;
            LogDebugText();
        }
    }
    public ItemWordButton ElemItemWord2
    {
        get { return elemItemWord2; }
        set
        {
            elemItemWord2 = value;
            LogDebugText();
        }
    }

    void Start()
    {
        flagManager = FlagManager.Instance;
        itemWordInventory = ItemWordInventory.Instance;
    }
    public void SelectItemWord(ItemWordButton itemWordButton)//UIから合成するワードを選択する
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

    public void DiveToSearchWorld()
    {
        if (CurrentSearchWorld == null) return;
        if (SceneManager.GetActiveScene().name.Contains("Dev_"))
        {
            SceneManager.LoadScene($"Dev_{CurrentSearchWorld.name}SearchWorld");
        }
        else
        {
            SceneManager.LoadScene($"{CurrentSearchWorld.name}SearchWorld");
        }
    }

    private void LogDebugText()
    {
        string text = "";
        if (ElemItemWord1) text += "選択: " + ElemItemWord1.GetComponent<ItemWordButton>().GetItemEntry().ItemWord.Word + "\n";
        if (ElemItemWord2) text += "選択: " + ElemItemWord2.GetComponent<ItemWordButton>().GetItemEntry().ItemWord.Word + "\n";
        if (CurrentSearchWorld) text += "現在の世界" + CurrentSearchWorld.WorldName + "\n";
        Logger.Log(text);
    }
}
