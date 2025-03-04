using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    // HomeSceneではButton、DiveSceneではIconをアタッチする。
    [SerializeField] private GameObject itemWordIconPrefab;
    [SerializeField] private VerticalLayoutGroup inventoryGroup;
    private ItemWordInventory itemWordInv;
    private Dictionary<string, ItemWordIcon> itemWordIconDict = new();

    void Start()
    {
        itemWordInv = ItemWordInventory.Instance;
        LoadInventoryData();
    }

    public void LoadInventoryData()
    {
        foreach (ItemEntry itemEntry in itemWordInv.Inventory)
        {
            if(itemWordIconDict.ContainsKey(itemEntry.ItemWord.Word)) continue;
            GameObject instance = Instantiate(itemWordIconPrefab, inventoryGroup.gameObject.transform);
            ItemWordIcon itemWordIcon = instance.GetComponent<ItemWordIcon>();
            itemWordIconDict.Add(itemEntry.ItemWord.Word, itemWordIcon);
            itemWordIcon.Initialize(itemEntry);
        }
    }

    public void AddItemWord(ItemWord itemWord)
    {
        itemWordInv.AddItemWord(itemWord);
    }
}