using UnityEngine;
using UnityEngine.UI;

public class ItemWordButton : MonoBehaviour
{
    private RecallButton recallButton;
    private HomeManager homeManager;
    private ItemEntry item;
    [SerializeField] private Text text;
    [SerializeField] private Button button;
    public void Initialize(HomeManager homeManager, ItemEntry item)
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

    public void OnClick()
    {
        homeManager.SelectWord(this);
    }

    public ItemEntry GetItem()
    {
        return item;
    }
}
