using UnityEngine;
using UnityEngine.UI;

public class ItemWordButton : MonoBehaviour
{
    private RecallButton recallButton;
    private ItemEntry item;
    [SerializeField] private Text text;
    [SerializeField] private Button button;
    public void Initialize(RecallButton recallButton, ItemEntry item)
    {
        this.recallButton = recallButton;
        this.item = item;
        text.text = item.ItemWord.name;//名前表示
    }

    public void CheckIsUsed()
    {
        item.IsUsed = true;
        button.interactable = !item.IsUsed;
    }

    public void OnClick()
    {
        recallButton.SelectWord(this.gameObject);
    }

    public ItemEntry GetItem()
    {
        return item;
    }
}
