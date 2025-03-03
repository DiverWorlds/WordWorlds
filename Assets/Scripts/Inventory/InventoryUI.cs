using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject itemWordButtonPrefab;
    [SerializeField] private VerticalLayoutGroup inventoryGroup;

    void Start()
    {
        LoadInventoryData();
    }

    public void LoadInventoryData()
    {
        foreach (ItemEntry item in ItemWordInventory.Instance.Inventory)
        {
            GameObject instance = Instantiate(itemWordButtonPrefab, inventoryGroup.gameObject.transform);
            ItemWordButton ItemWordButton = instance.GetComponent<ItemWordButton>();
            ItemWordButton.Initialize(item);
        }
    }
}