using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class SaveData
{
    [SerializeField] private List<ItemEntry> inventory = new();
    [SerializeField] private string lastScene = "";

    public List<ItemEntry> Inventory
    {
        get { return new(inventory);}
    }
    public string LastSceneName => lastScene;

    public SaveData(IEnumerable<ItemEntry> itemEntries, string lastScene)
    {
        inventory = itemEntries.ToList();
        this.lastScene = lastScene;

    }
}