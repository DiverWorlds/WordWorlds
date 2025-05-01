using UnityEngine;

public class DatabaseTest : MonoBehaviour
{
    private ItemWordDatabase itemWordDB;
    private SearchWorldDatabase searchWorldDB;
    void Start()
    {
        itemWordDB = GlobalDB.Instance.ItemWordDB;
        searchWorldDB = GlobalDB.Instance.SearchWorldDB;
    }
    public void GetWord_a()
    {
        Logger.Log($"Generated: {itemWordDB.GetItemWord("a")}");
    }
    public void GetWord_A()
    {
        itemWordDB.GetItemWord("A");
    }
    public void Recall_a_b_A()
    {
        Logger.Log($"Succeeded Recall: Word 'a' and Word 'b' to World \"{searchWorldDB.GetRecalledWorld(itemWordDB.GetItemWord("a"), itemWordDB.GetItemWord("b")).name}\"");
    }
}