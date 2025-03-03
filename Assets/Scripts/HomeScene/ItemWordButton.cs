using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemWordButton : MonoBehaviour
{
    private HomeManager homeManager;
    private ItemEntry item;
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] private Button button;
    public void Initialize(HomeManager homeManager, ItemEntry item)
    {
        this.homeManager = homeManager;
        this.item = item;
        textMeshProUGUI.text = item.ItemWord.name;
    }

    public void CheckIsUsed()
    {
        item.IsUsed = true;
        button.interactable = !item.IsUsed;
    }

    public void OnClick()
    {
        homeManager.SelectWord(this);
    }

    public ItemEntry GetItemEntry()
    {
        return item;
    }
}
