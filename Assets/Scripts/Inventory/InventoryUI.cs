using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    // HomeSceneではButton、DiveSceneではIconをアタッチする。
    [SerializeField] private GameObject itemWordUIPrefab;
    [SerializeField] private VerticalLayoutGroup inventoryGroup;
    private ItemWordInventory itemWordInv;

    void Start()
    {
        itemWordInv = ItemWordInventory.Instance;
        LoadInventoryData();
    }

    public void LoadInventoryData()
    {
        foreach (ItemEntry item in itemWordInv.Inventory)
        {
            GameObject instance = Instantiate(itemWordUIPrefab, inventoryGroup.gameObject.transform);
            ItemWordIcon itemWordIcon = instance.GetComponent<ItemWordIcon>(); //Buttonとアイコンの親クラス作る
            itemWordIcon.Initialize(item);
        }
    }

    public void AddItemWord(ItemWord itemWord)
    {
        itemWordInv.AddItemWord(itemWord);
    }
}