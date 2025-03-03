using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class SaveDataManager : DontDestroySingleton<FlagManager>
{
    //セーブ内容：インベントリ内容、現在のシーン、現在のDive世界で何を拾ったか
    [SerializeField] private string inventorySavePath = "InventorySaveData";
    [SerializeField] private string currentSceneSavePath = "CurrentSceneSaveData";
    private List<ItemEntry> inventoryItems = new();
    public void Load()
    {
        LoadInventory();
    }

    private bool LoadInventory()
    {
        // try
        // {
            string inventoryJsonText = Resources.Load<TextAsset>(inventorySavePath).text;
            Logger.Log("inventoryJsonText", inventoryJsonText);
            inventoryItems = JsonUtility.FromJson<InventoryConverter>(inventoryJsonText).ToList();
            Logger.LogElements("inventoryItems", inventoryItems.Select(e => e.ItemWord.ToString()));
            Logger.LogElements("inventoryItems", inventoryItems.Select(e => e.IsUsed));
            return true;
        // }
        // catch (Exception e)
        // {
        //     Debug.Log(e);
        //     return false;
        // }
    }
}
