using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class InventoryConverter
{
    [SerializeField] private List<ItemEntry> inventory = new();

    public List<ItemEntry> ToList()
    {
        return new(inventory);
    }

    public InventoryConverter(List<ItemEntry> itemEntries)
    {
        inventory = itemEntries;
    }
}