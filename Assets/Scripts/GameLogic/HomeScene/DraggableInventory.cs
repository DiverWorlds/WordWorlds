using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DraggableInventory : MonoBehaviour
{
    [SerializeField] private GameObject DraggableWordUIPrefab;
    [SerializeField] private Canvas canvas;
    private ItemWordInventory itemWordInv;
    private Dictionary<string, ItemWordIcon> itemWordIconDict = new();
    private Dictionary<string, DraggableWordUI> draggableWordDict = new();

    void Start()
    {
        itemWordInv = ItemWordInventory.Instance;
        LoadInventoryData();
        ItemWordInventory.Instance.OnInventoryUpdated += LoadInventoryData;
    }
    void OnDestroy()
    {
        ItemWordInventory.Instance.OnInventoryUpdated -= LoadInventoryData;
    }

    public void LoadInventoryData()
    {
        int i = 0;
        foreach (ItemEntry itemEntry in itemWordInv.Inventory)
        {
            if (draggableWordDict.ContainsKey(itemEntry.ItemWord.Word)) continue;
            GameObject instance = Instantiate(DraggableWordUIPrefab, canvas.GetComponent<RectTransform>());
            DraggableWordUI draggableWord = instance.GetComponent<DraggableWordUI>();
            draggableWordDict.Add(itemEntry.ItemWord.Word, draggableWord);
            draggableWord.Initialize(new Vector2(Mathf.Cos(360 / itemWordInv.Inventory.Count * i) * 200, Mathf.Sin(360 / itemWordInv.Inventory.Count * i) * 100), itemEntry);
            i++;
        }
    }

}
