using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class DraggableInventory : MonoBehaviour
{
    //インベントリというより、ワードをインベントリの中身に従って配置するクラス
    [SerializeField] private GameObject DraggableWordUIPrefab;
    [SerializeField] private Canvas canvas;
    [SerializeField] private float width;//どれだけの横幅でワードを配置するか
    [SerializeField] private float height;//どれだけの縦幅でワードを配置するか
    private ItemWordInventory itemWordInv;
    private Dictionary<string, DraggableItemWord> draggableWordDict = new();

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

    //ドラッグ可能ワードUIのロードと楕円形配置
    public void LoadInventoryData()
    {
        int i = 0;
        foreach (ItemEntry itemEntry in itemWordInv.Inventory)
        {
            if (draggableWordDict.ContainsKey(itemEntry.ItemWord.Word)) continue;
            GameObject instance = Instantiate(DraggableWordUIPrefab, canvas.GetComponent<RectTransform>());
            DraggableItemWord draggableWord = instance.GetComponent<DraggableItemWord>();
            draggableWordDict.Add(itemEntry.ItemWord.Word, draggableWord);
            draggableWord.Initialize(new Vector2(Mathf.Cos(2 * Mathf.PI / itemWordInv.Inventory.Count * i) * width, Mathf.Sin(2 * Mathf.PI / itemWordInv.Inventory.Count * i) * height), itemEntry);
            i++;
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
