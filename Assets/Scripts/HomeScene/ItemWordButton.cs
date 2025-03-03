using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemWordButton : MonoBehaviour
{
    private ItemEntry item;
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] private Button button;
    public void Initialize(ItemEntry item)
    {
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
        HomeManager.Instance.SelectWord(this);
    }

    public ItemEntry GetItemEntry()
    {
        return item;
    }
}
