using System.Linq;
using TMPro;
using UnityEngine;

public class SearchTarget : MonoBehaviour
{
    [SerializeField] private string onClickText;
    [SerializeField] private ItemWord foundItemWord;
    [SerializeField] private TextMeshProUGUI textBox;
    private ItemWordInventory wordInv;

    void Start()
    {
        wordInv = ItemWordInventory.Instance;
    }
    public void OnClick()
    {
        if (!wordInv.IsContains(foundItemWord))
        {
            DisplayText();
            ItemWordInventory wordInv = ItemWordInventory.Instance;
            wordInv.AddItemWord(foundItemWord);
            // フラグ使用の例としてItemWordの入手をフラグで記録しているが、本来はInventoryにアクセスしてItemWordの入手を確認する。
            FlagManager.Instance.Change($"WordGet_{foundItemWord.Word}", true);
        }
    }

    private void DisplayText()
    {
        // 将来的には会話ウィンドウを呼び出すはず
        textBox.text = onClickText;
    }
}