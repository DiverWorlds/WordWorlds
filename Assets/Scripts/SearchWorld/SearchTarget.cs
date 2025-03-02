using System.Linq;
using TMPro;
using UnityEngine;

public class SearchTarget : MonoBehaviour
{
    [SerializeField] private string onClickText;
    [SerializeField] private ItemWord foundItemWord;
    [SerializeField] private TextMeshProUGUI textBox;

    public void OnClick()
    {
        DisplayText();
        ItemWordInventory wordInv = ItemWordInventory.Instance;
        wordInv.AddItemWord(foundItemWord);
        Logger.LogElements("Inventory", wordInv.Inventory.Select(w => w.ItemWord.Word));
    }

    private void DisplayText()
    {
        // 将来的には会話ウィンドウを呼び出すはず
        textBox.text = onClickText;
    }
}