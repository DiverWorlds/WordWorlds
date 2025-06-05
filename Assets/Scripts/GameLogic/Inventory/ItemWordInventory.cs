using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Awake()はGlobalDataより後に実行する．
/// </summary>
[DefaultExecutionOrder(-99)]
public class ItemWordInventory : DontDestroySingleton<ItemWordInventory>
{
    //TODO: Homeに戻る機能作る
    //TODO: Inventoryの中にHomeに戻るボタンを置く
    [SerializeField] private int maxSize = 15;
    private SearchWorldDatabase searchWorldDB;
    private List<ItemEntry> inventory = new();
    public ReadOnlyCollection<ItemEntry> Inventory => inventory.AsReadOnly();
    private Action onInventoryUpdated;
    public event Action OnInventoryUpdated { add => onInventoryUpdated += value; remove => onInventoryUpdated -= value; }

    public override void Awake()
    {
        base.Awake();
        searchWorldDB = GlobalDB.Instance.SearchWorldDB;
        Logger.LogElements("ItemWordInventory", inventory.Select(e => e.ItemWord.Word));
    }

    public bool AddItemWord(ItemWord itemWord)
    {
        if (maxSize > inventory.Count() && !inventory.Any(w => w.ItemWord.Equals(itemWord)))
        {
            inventory.Add(new(itemWord));
            onInventoryUpdated.Invoke();
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsContains(ItemWord itemWord)
    {
        return inventory.Select(e => e.ItemWord).Contains(itemWord);
    }

    public SearchWorld RecallWorld(ItemWord itemWord1, ItemWord itemWord2)
    {
        SearchWorld searchWorld = searchWorldDB.PeekRecalledWorld(itemWord1, itemWord2);
        if (searchWorld != null)
        {
            UseItemWord(itemWord1, itemWord2);

        }
        return searchWorld;
    }

    private void UseItemWord(params ItemWord[] itemWords)
    {
        int count = 0;
        foreach (ItemEntry itemEntry in inventory)
        {
            if (itemWords.Contains(itemEntry.ItemWord))
            {
                itemEntry.IsUsed = true;
                count++;
                if (count == itemWords.Length) return;
            }
        }
    }

    public void LoadSaveData(List<ItemEntry> loadedInventory)
    {
        inventory = new(loadedInventory);
    }

    public void Log()
    {
        Logger.LogElements("inventory", ItemWordInventory.Instance.Inventory.Select(e => $"{e.ItemWord.Word}: {e.IsUsed}"));
    }
}
