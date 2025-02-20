using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ItemWordInventory : DontDestroySingleton<ItemWordInventory>
{
    public class ItemEntry
    {
        public ItemWord ItemWord { get; set; }
        public bool IsUsed { get; set; }
        public ItemEntry(ItemWord itemWord)
        {
            ItemWord = itemWord;
            IsUsed = false;
        }
    }
    [SerializeField] private int maxSize = 15;
    [SerializeField] private SearchWorldDatabase searchWorldDatabase;
    private List<ItemEntry> inventory = new();
    public ReadOnlyCollection<ItemEntry> Inventory => inventory.AsReadOnly();

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
        SearchWorld searchWorld = searchWorldDatabase.GetRecalledWorld(itemWord1, itemWord2);
        if (searchWorld != null)
        {
            UseItemWord(itemWord1, itemWord2);

        }
        return searchWorld;
    }

    private void UseItemWord(params ItemWord[] itemWords)
    {
        int count = 0;
        foreach(ItemEntry itemEntry in inventory)
        {
            if (itemWords.Contains(itemEntry.ItemWord))
            {
                itemEntry.IsUsed = true;
                count++;
                if (count==itemWords.Length) return;
            }
        }
    }

    //TODO: セーブデータからInventory情報を読み取って再度入れるメソッドは、必要があれば後で行う
}
