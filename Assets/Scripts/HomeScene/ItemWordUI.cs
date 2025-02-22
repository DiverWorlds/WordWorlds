using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemWordUI : MonoBehaviour
{
    private HomeManager homeManager;
    private ItemWordInventory.ItemEntry item;
    [SerializeField] private Text text;
    [SerializeField] private Button button;
    public void Initialize(HomeManager homeManager, ItemWordInventory.ItemEntry item)
    {
        this.homeManager = homeManager;
        this.item = item;
        text.text = item.ItemWord.name;//名前表示
    }

    public void CheckIsUsed()
    {
        item.IsUsed = true;
        button.interactable = !item.IsUsed;
    }

    public void SendWordData()
    {
        homeManager.SendMessage("SelectMixWord", this.gameObject);
    }

    public ItemWordInventory.ItemEntry GetItem()
    {
        return item;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
