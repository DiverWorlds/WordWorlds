using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class SaveDataManager : DontDestroySingleton<SaveDataManager>
{
    //セーブ内容：インベントリ内容、現在のシーン、現在のDive世界で何を拾ったか
    //最終的に１つのファイルでやりたい
    //TODO: Databaseの参照方法を全体で変える
    [SerializeField] private string inventorySavePath = "InventorySaveData";
    [SerializeField] private string currentSceneSavePath = "CurrentSceneSaveData";
    private List<ItemEntry> inventoryItems = new();
    public void Load()
    {
        LoadInventory();
    }

    public void Save()
    {
        SaveInventory();
    }

    private void LoadInventory()
    {
        try
        {
            string inventoryJsonText = Resources.Load<TextAsset>(inventorySavePath).text;
            inventoryItems = JsonUtility.FromJson<InventoryConverter>(inventoryJsonText).ToList();
            ItemWordInventory.Instance.LoadSaveData(inventoryItems);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    private void SaveInventory()
    {
        string inventoryJsonText = JsonUtility.ToJson(new InventoryConverter(ItemWordInventory.Instance.Inventory), true);
        Logger.Log("inventoryJsonText", inventoryJsonText);
        File.WriteAllText($"Assets/Resources/{inventorySavePath}.json", inventoryJsonText);
        
    }
}
