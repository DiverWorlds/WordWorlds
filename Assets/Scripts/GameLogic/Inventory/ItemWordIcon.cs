using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemWordIcon : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI textMeshProUGUI;
    public virtual void Initialize(ItemEntry itemEntry)
    {
        textMeshProUGUI.text = itemEntry.ItemWord.Word;
    }

}
