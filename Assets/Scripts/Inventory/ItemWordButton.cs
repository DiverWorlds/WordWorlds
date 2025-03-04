using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemWordButton : ItemWordIcon
{
    private ItemEntry itemEntry;
    [SerializeField] private Button button;
    public override void Initialize(ItemEntry itemEntry)
    {
        base.Initialize(itemEntry);
        this.itemEntry = itemEntry;
    }

    public void CheckIsUsed()
    {
        itemEntry.IsUsed = true;
        button.interactable = !itemEntry.IsUsed;
    }

    public void OnClick()
    {
        HomeManager.Instance.SelectItemWord(this);
    }

    public ItemEntry GetItemEntry()
    {
        return itemEntry;
    }
}
