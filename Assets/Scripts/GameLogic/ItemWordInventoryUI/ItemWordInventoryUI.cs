using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemWordInventoryUI : MonoBehaviour
{
    [SerializeField] private ItemWordUI itemWordUIPrefab;
    [SerializeField] private SearchWorldDatabase searchWorldDatabase;
    [SerializeField] private DropArea dropArea;
    [SerializeField] private Canvas itemWordUIParent;
    private ItemWordInventory itemWordInv;
    private bool isInteractable;
    private Action<SearchWorld> onWorldPredicted;
    public event Action<SearchWorld> OnWorldPredicted { add => onWorldPredicted += value; remove => onWorldPredicted -= value; }

    public void Initialize(bool isInteractable)
    {
        itemWordInv = ItemWordInventory.Instance;
        this.isInteractable = isInteractable;
        dropArea.OnAreaFullFilled += () =>
        {
            SearchWorld predictedWorld = GetPredictedWorld();
            if (predictedWorld != null)
            {
                onWorldPredicted.Invoke(predictedWorld);
            }
            else
            {
                Logger.Log("予測されるSearchWorldがありません。");
            }
        };
        GenerateItemWordUIs();
    }
    private void GenerateItemWordUIs()
    {
        Rect dropAreaRect = WorldRectGetter.Get(dropArea.GetComponent<RectTransform>());
        for (int i = 0; i < itemWordInv.Inventory.Count; i++)
        {
            ItemWordUI itemWordUI = Instantiate(itemWordUIPrefab, itemWordUIParent.transform);
            Vector2 aroundEclipsePos = new(Mathf.Cos(2 * Mathf.PI / itemWordInv.Inventory.Count * i) * dropAreaRect.width, Mathf.Sin(2 * Mathf.PI / itemWordInv.Inventory.Count * i) * dropAreaRect.height);
            itemWordUI.Initialize(itemWordInv.Inventory[i], isInteractable, aroundEclipsePos, dropArea);
        }
    }
    public SearchWorld GetPredictedWorld()
    {
        List<ItemWord> placedItemWords = GetPlacedItemWords(dropArea.PlacedUIElements);
        SearchWorld predictedWorld = searchWorldDatabase.PeekRecalledWorld(placedItemWords[0], placedItemWords[1]);
        if (predictedWorld != null)
        {
            return predictedWorld;
        }
        return null;
    }
    private List<ItemWord> GetPlacedItemWords(DragUIElement[] placedUIElements)
    {
        return placedUIElements
            .Select(e => (e.CoreComponent as ItemWordUI)?.ItemEntry.ItemWord)
            .Where(itemWord => itemWord != null)
            .ToList();
    }
}