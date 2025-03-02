using UnityEngine;
using UnityEngine.UI;

public class HomeManager : MonoBehaviour
{
    //＊注意：HomeSceneでのみ扱われるクラスとする。基本的に他シーンとの依存関係は排除する

    private ItemWordInventory itemWordInventory;
    [SerializeField] private ItemWordDatabase itemWordDatabase;
    //シーン内のオブジェクト(UI)をSerializeFieldとして格納
    [SerializeField] private GameObject ItemWordButtonPrefab;//ワードのUIPrefab
    [SerializeField] private GameObject inventoryScrollContent;//インベントリのスクロールビュー
    [SerializeField] private RecallButton recallButton;//リコールボタン

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

        foreach (ItemEntry item in itemWordInventory.Inventory)
        {
            GameObject instance = Instantiate(ItemWordButtonPrefab, inventoryScrollContent.transform);
            ItemWordButton ItemWordButton = instance.GetComponent<ItemWordButton>();
            ItemWordButton.Initialize(recallButton, item);
        }
    }
}
