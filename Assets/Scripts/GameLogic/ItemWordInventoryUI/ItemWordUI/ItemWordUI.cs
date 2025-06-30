using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class ItemWordUI : MonoBehaviour
{
    private const float DROP_AREA_SCALE_MULTIPLIER = 1.7f;
    [SerializeField] private Material unusedMaterial;
    [SerializeField] private Material usedMaterial;
    [SerializeField] private DragUIElement dragUIElement;
    private ItemEntry itemEntry;
    private ItemWordInventoryUI itemWordInventoryUI;
    private Vector3 initialScale;
    private Vector3 dropAreaScale;


    public void Initialize(ItemEntry itemEntry, ItemWordInventoryUI itemWordInventoryUI)
    {
        RawImage rawImage = GetComponent<RawImage>();
        TextMeshProUGUI textMeshPro = GetComponent<TextMeshProUGUI>();

        // ItemWordが画像を持っていたらそれを表示し，無ければTextを表示
        if (itemEntry.ItemWord.WordImage != null)
        {
            textMeshPro.enabled = false;
            rawImage.enabled = true;
            rawImage.texture = itemEntry.ItemWord.WordImage;
            rawImage.material = itemEntry.IsUsed ? usedMaterial : unusedMaterial;
        }
        else
        {
            textMeshPro.enabled = true;
            rawImage.enabled = false;
            textMeshPro.text = itemEntry.ItemWord.Word;
        }

        dragUIElement.IsDraggable = IsDraggable();
        dragUIElement.OnDroppedInArea += () => { ChangeScale(dropAreaScale); };
        dragUIElement.OnDroppedOutArea += () => { ChangeScale(initialScale); };
        this.itemEntry = itemEntry;
        this.itemWordInventoryUI = itemWordInventoryUI;
        initialScale = transform.localScale;
        dropAreaScale = initialScale * DROP_AREA_SCALE_MULTIPLIER;
    }
    private bool IsDraggable()
    {
        return /*itemWordInventoryUI.IsInteractable &&*/ !itemEntry.IsUsed;
    }
    private void ChangeScale(Vector3 scale)
    {
        transform.localScale = scale;
    }
}