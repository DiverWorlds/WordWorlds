using System;
using UnityEngine;

[Serializable]
public class ItemEntry
{
    [SerializeField] private string itemWord;
    [SerializeField] private bool isUsed;
    public ItemWord ItemWord 
    {
        get { return GlobalDB.Instance.ItemWordDB.GetItemWord(itemWord); }
        set { itemWord = value.Word; }
    }
    public bool IsUsed 
    {
        get { return isUsed; }
        set { isUsed = value; }
    }
    public ItemEntry(ItemWord itemWord)
    {
        ItemWord = itemWord;
        IsUsed = false;
    }
}