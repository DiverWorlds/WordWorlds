using System.Linq;
using UnityEngine;

public class SaveTest : MonoBehaviour
{
    public void LoadInventory()
    {
        SaveDataManager.Instance.Load();
        ItemWordInventory.Instance.Log();
    }
    public void RecallItemWordA_B()
    {
        ItemWordInventory.Instance.RecallWorld(GlobalDB.Instance.ItemWordDB.GetItemWord("a"), GlobalDB.Instance.ItemWordDB.GetItemWord("b"));
    }
}