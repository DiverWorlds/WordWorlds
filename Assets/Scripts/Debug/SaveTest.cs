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
        Logger.Log("RecalledWorld", ItemWordInventory.Instance.RecallWorld(GlobalDB.Instance.ItemWordDB.GetItemWord("a"), GlobalDB.Instance.ItemWordDB.GetItemWord("b")).WorldName);
        ItemWordInventory.Instance.Log();
    }
    public void SaveInventory()
    {
        SaveDataManager.Instance.Save();
        Logger.Log("セーブしました。");
    }
}