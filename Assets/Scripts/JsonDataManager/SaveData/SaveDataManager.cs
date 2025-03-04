using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveDataManager : DontDestroySingleton<SaveDataManager>
{
    //セーブ内容：インベントリ内容、現在のシーン、現在のDive世界で何を拾ったか
    [SerializeField] private string SaveDataPath = "SaveData";
    private List<ItemEntry> inventoryItems = new();
    private string lastSceneName = "";

    void Start()
    {
        Load();
    }

    public void Load()
    {
        try
        {
            string inventoryJsonText = Resources.Load<TextAsset>(SaveDataPath).text;
            SaveData saveData = JsonUtility.FromJson<SaveData>(inventoryJsonText);
            inventoryItems = saveData.Inventory;
            lastSceneName = saveData.LastSceneName;
            ItemWordInventory.Instance.LoadSaveData(inventoryItems);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }
    public void LoadLastScene()
    {
        SceneManager.LoadScene(lastSceneName);
    }
    public void Save()
    {
        try
        {
            lastSceneName = SceneManager.GetActiveScene().name;
            string inventoryJsonText = JsonUtility.ToJson(new SaveData(ItemWordInventory.Instance.Inventory, lastSceneName), true);
            Logger.Log("inventoryJsonText", inventoryJsonText);
            File.WriteAllText($"Assets/Resources/{SaveDataPath}.json", inventoryJsonText);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }
}
