using UnityEngine;

public class InventoryTest : MonoBehaviour
{
    [SerializeField] private ItemWordDatabase itemWordDatabase;
    [SerializeField] private ItemWordInventory itemWordInventory;

    public void AddItemWord_a()
    {
        Logger.LogElements($"Before Inventory", itemWordInventory.Inventory);
        itemWordInventory.AddItemWord(itemWordDatabase.GetItemWord("a"));
        Logger.LogElements($"After Inventory", itemWordInventory.Inventory);
    }

    public void AddItemWordsUntilLimit()
    {
        // maxは現在3つにしてある
        Logger.LogElements($"Before Inventory", itemWordInventory.Inventory);
        Logger.Log("Add Word \"a\". Result", itemWordInventory.AddItemWord(itemWordDatabase.GetItemWord("a")));
        Logger.Log("Add Word \"b\". Result", itemWordInventory.AddItemWord(itemWordDatabase.GetItemWord("b")));
        Logger.Log("Add Word \"c\". Result", itemWordInventory.AddItemWord(itemWordDatabase.GetItemWord("c")));
        Logger.Log("Add Word \"d\". Result", itemWordInventory.AddItemWord(itemWordDatabase.GetItemWord("d")));
        Logger.LogElements($"After Inventory", itemWordInventory.Inventory);
    }

    public void RecallWorld()
    {
        Logger.Log("Word \"a\" and Word \"b\" Recall. Result", itemWordInventory.RecallWorld(itemWordDatabase.GetItemWord("a"), itemWordDatabase.GetItemWord("b")).WorldName);
    }
}