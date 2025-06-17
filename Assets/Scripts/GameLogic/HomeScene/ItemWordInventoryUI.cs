using UnityEngine;
using System.Collections.Generic;

public class ItemWordInventoryUI : MonoBehaviour
{
    //インベントリというより、ワードをインベントリの中身に従って配置するクラス
    [SerializeField] private DraggableItemWord draggableItemWordPrefab;
    [SerializeField] private Transform draggableItemWordParent;
    [SerializeField] private ItemWordDropArea itemWordDropArea;
    // 楕円の周上に配置するDraggableItemWordを配置する
    [SerializeField] private float width;
    [SerializeField] private float height;
    private ItemWordInventory itemWordInv;
    private Dictionary<string, DraggableItemWord> draggableWordDict = new();

    void Start()
    {
        itemWordInv = ItemWordInventory.Instance;
        LoadInventoryData();
        itemWordInv.OnInventoryUpdated += LoadInventoryData;
    }
    void OnDestroy()
    {
        itemWordInv.OnInventoryUpdated -= LoadInventoryData;
    }

    // DraggableItemWordを楕円形に配置する
    public void LoadInventoryData()
    {
        var inv = itemWordInv.Inventory;
        for (int i = 0; i < itemWordInv.Inventory.Count; i++)
        {
            if (draggableWordDict.ContainsKey(inv[i].ItemWord.Word))
            {
                continue;
            }
            DraggableItemWord draggableItemWord = Instantiate(draggableItemWordPrefab, draggableItemWordParent);
            draggableWordDict.Add(inv[i].ItemWord.Word, draggableItemWord);
            Vector2 aroundEclipsePos = new(Mathf.Cos(2 * Mathf.PI / itemWordInv.Inventory.Count * i) * width, Mathf.Sin(2 * Mathf.PI / itemWordInv.Inventory.Count * i) * height);
            draggableItemWord.Initialize(aroundEclipsePos, inv[i], itemWordDropArea);
        }
    }

    public void HideInventory()
    {
        foreach (DraggableItemWord draggableWordUI in draggableWordDict.Values)
        {
            Destroy(draggableWordUI.gameObject);
        }
        draggableWordDict.Clear();
    }

}
