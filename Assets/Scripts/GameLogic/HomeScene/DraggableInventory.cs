using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class DraggableInventory : MonoBehaviour
{
    [SerializeField] private GameObject DraggableWordUIPrefab;
    [SerializeField] private Canvas canvas;
    [SerializeField] private float width;
    [SerializeField] private float height;
    private ItemWordInventory itemWordInv;
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
            draggableWord.Initialize(new Vector2(Mathf.Cos(2 * Mathf.PI / itemWordInv.Inventory.Count * i) * width, Mathf.Sin(2 * Mathf.PI / itemWordInv.Inventory.Count * i) * height), itemEntry);
            i++;
        }
    }

    public void HideInventory()
    {
        foreach (DraggableWordUI draggableWordUI in draggableWordDict.Values)
        {
            Destroy(draggableWordUI.gameObject);
        }
        draggableWordDict.Clear();
    }

}
