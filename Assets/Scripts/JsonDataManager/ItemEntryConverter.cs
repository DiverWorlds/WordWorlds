using System;
using UnityEngine;

[Serializable]
public class ItemEntryConverter
{
    [SerializeField] private string itemWordText;
    [SerializeField] private bool isUsed;
    public string ItemWordText
    {
        get { return itemWordText; }
        set { itemWordText = value; }
    }
    public bool IsUsed
    {
        get { return isUsed; }
        set { isUsed = value; }
    }
    public ItemEntryConverter(string itemWordText, bool isUsed)
    {
        this.itemWordText = itemWordText;
        this.isUsed = isUsed;
    }
}