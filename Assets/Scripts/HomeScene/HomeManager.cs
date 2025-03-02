using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class HomeManager : MonoBehaviour
{
    //＊注意：HomeSceneでのみ扱われるクラスとする。基本的に他シーンとの依存関係は排除する

    private ItemWordInventory itemWordInventory;
    [SerializeField] ItemWordDatabase itemWordDatabase;
    [SerializeField] private SearchWorld initialWorld;
    private SearchWorld currentWorld = null;
    private SearchWorld nextWorld = null;
    private GameObject mixWord1, mixWord2;

    //シーン内のオブジェクト(UI)をSerializeFieldとして格納
    [SerializeField] Text debugText;
    [SerializeField] GameObject currentWorldImage;//世界の外観
    [SerializeField] GameObject ItemWordButtonPrefab;//ワードのUIPrefab
    [SerializeField] GameObject inventoryScrollContent;//インベントリのスクロールビュー
    private List<GameObject> uiInventory = new List<GameObject>();
    void Awake()
    {
        //一旦initialWorldをcurrentWorldへ代入
        currentWorld = initialWorld;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //シーン開始

        //ItemWordInventoryを開発用に調整
        itemWordInventory = ItemWordInventory.Instance;
        Logger.LogElements($"Before Inventory", itemWordInventory.Inventory);
        Logger.Log("Add Word \"a\". Result", itemWordInventory.AddItemWord(itemWordDatabase.GetItemWord("a")));
        Logger.Log("Add Word \"b\". Result", itemWordInventory.AddItemWord(itemWordDatabase.GetItemWord("b")));
        Logger.Log("Add Word \"c\". Result", itemWordInventory.AddItemWord(itemWordDatabase.GetItemWord("c")));
        Logger.Log("Add Word \"d\". Result", itemWordInventory.AddItemWord(itemWordDatabase.GetItemWord("d")));
        Logger.LogElements($"After Inventory", itemWordInventory.Inventory);

        foreach (ItemWordInventory.ItemEntry item in itemWordInventory.Inventory)
        {
            GameObject uiObject = Instantiate(ItemWordButtonPrefab, inventoryScrollContent.transform);
            ItemWordButton ItemWordButton = uiObject.GetComponent<ItemWordButton>();
            ItemWordButton.Initialize(this, item);
            uiInventory.Add(uiObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        ShowDebugState();//開発用、現在の状態をテキストで表示
    }

    public void ShowDebugState()
    {
        string text = "";
        if (currentWorld) text += "現在の世界" + currentWorld.name + "\n";
        if (mixWord1) text += "合成用ワード1:" + mixWord1.GetComponent<ItemWordButton>().GetItem().ItemWord.name + "\n";
        if (mixWord2) text += "合成用ワード2:" + mixWord2.GetComponent<ItemWordButton>().GetItem().ItemWord.name + "\n";
        if (nextWorld) text += "生まれる世界:" + nextWorld.name + "\n";
        text += "所持ワード\n";
        foreach (ItemWordInventory.ItemEntry item in itemWordInventory.Inventory)
        {
            text += item.ItemWord.name + " isUsed: " + item.IsUsed + "\n";
        }
        debugText.text = text;
    }

    public void SelectMixWord(GameObject uiItemWord)//UIから合成するワードを選択する
    {
        if (!mixWord1) mixWord1 = uiItemWord;
        else if (mixWord1 == uiItemWord)
        {
            Debug.Log("同じワードを選ぶことはできません");
        }
        else if (!mixWord2)
        {
            mixWord2 = uiItemWord;
        }
        else Debug.Log("すでに2つのワードが選択されています");
    }

    public void MixWord(GameObject word1, GameObject word2)//実際にワードをミックス、世界を生成する
    {
        ItemWordButton ItemWordButton1 = word1.GetComponent<ItemWordButton>();
        ItemWordInventory.ItemEntry itemEntry1 = ItemWordButton1.GetItem();
        ItemWord itemWord1 = itemEntry1.ItemWord;

        ItemWordButton ItemWordButton2 = word2.GetComponent<ItemWordButton>();
        ItemWordInventory.ItemEntry itemEntry2 = ItemWordButton2.GetItem();
        ItemWord itemWord2 = itemEntry2.ItemWord;

        nextWorld = itemWordInventory.RecallWorld(itemWord1, itemWord2);//ItemWord二つからSearchWorld一つを生成}
    }
    public void ChangeWorld()//世界を生成したものに変更する
    {
        MixWord(mixWord1, mixWord2);
        mixWord1.GetComponent<ItemWordButton>().CheckIsUsed();
        mixWord2.GetComponent<ItemWordButton>().CheckIsUsed();
        currentWorld = nextWorld;//今の世界を次の世界に置き換える

        //リセット
        Reset();
    }

    public void DiveWorld()
    {
        //現在設定されているSearchWorldを参照してシーン遷移
        if (currentWorld) Debug.Log(currentWorld.name + "にダイブ!!");
        else Debug.Log("世界がありません...");
    }

    public void Reset()
    {
        //リセット
        mixWord1 = null;
        mixWord2 = null;
        nextWorld = null;
    }
}
