using UnityEngine;

public class InventoryTest : MonoBehaviour
{
    private ItemWordDatabase itemWordDB;
    private ItemWordInventory itemWordInv;
    void Start()
    {
        itemWordDB = GlobalDB.Instance.ItemWordDB;
        itemWordInv = ItemWordInventory.Instance;
    }

    public void AddItemWord_a()
    {
        Logger.LogElements($"Before Inventory", itemWordInv.Inventory);
        itemWordInv.AddItemWord(itemWordDB.GetItemWord("a"));
        Logger.LogElements($"After Inventory", itemWordInv.Inventory);
    }

    public void AddItemWordsUntilLimit()
    {
        // maxは現在3つにしてある
        Logger.LogElements($"Before Inventory", itemWordInv.Inventory);
        Logger.Log("Add Word \"a\". Result", itemWordInv.AddItemWord(itemWordDB.GetItemWord("a")));
        Logger.Log("Add Word \"b\". Result", itemWordInv.AddItemWord(itemWordDB.GetItemWord("b")));
        Logger.Log("Add Word \"c\". Result", itemWordInv.AddItemWord(itemWordDB.GetItemWord("c")));
        Logger.Log("Add Word \"d\". Result", itemWordInv.AddItemWord(itemWordDB.GetItemWord("d")));
        Logger.LogElements($"After Inventory", itemWordInv.Inventory);
    }

    public void RecallWorld()
    {
        Logger.Log("Word \"a\" and Word \"b\" Recall. Result", itemWordInv.RecallWorld(itemWordDB.GetItemWord("a"), itemWordDB.GetItemWord("b")).WorldName);
    }
}