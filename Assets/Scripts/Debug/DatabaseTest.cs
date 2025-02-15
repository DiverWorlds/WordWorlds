using UnityEngine;

public class DatabaseTest : MonoBehaviour
{
    [SerializeField] private ItemWordDatabase itemWordDatabase;
    [SerializeField] private SearchWorldDatabase searchWorldDatabase;
    public void GetWord_a()
    {
        Logger.Log($"Generated: {itemWordDatabase.GetItemWord("a")}");
    }
    public void GetWord_A()
    {
        itemWordDatabase.GetItemWord("A");
    }
    public void Recall_a_b_A()
    {
        Logger.Log($"Succeeded Recall: Word 'a' and Word 'b' to World \"{searchWorldDatabase.GetRecalledWorld(itemWordDatabase.GetItemWord("a"), itemWordDatabase.GetItemWord("b")).name}\"");
    }
}