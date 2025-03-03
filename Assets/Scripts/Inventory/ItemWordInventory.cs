using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ItemWordInventory : DontDestroySingleton<ItemWordInventory>
{
    [SerializeField] private int maxSize = 15;
    private SearchWorldDatabase searchWorldDB;
    private List<ItemEntry> inventory = new();
    public ReadOnlyCollection<ItemEntry> Inventory => inventory.AsReadOnly();

    void Start()
    {
        searchWorldDB = GlobalDB.Instance.SearchWorldDB;
    }

    public bool AddItemWord(ItemWord itemWord)
    {
        if (maxSize > inventory.Count() && !inventory.Any(w => w.ItemWord.Equals(itemWord)))
        {
            inventory.Add(new(itemWord));
            return true;
        }
        else
        {
            return false;
        }
    }

    public SearchWorld RecallWorld(ItemWord itemWord1, ItemWord itemWord2)
    {
        SearchWorld searchWorld = searchWorldDB.GetRecalledWorld(itemWord1, itemWord2);
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
