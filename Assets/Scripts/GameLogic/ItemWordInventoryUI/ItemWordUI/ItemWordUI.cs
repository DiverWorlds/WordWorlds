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
    [SerializeField] private RawImage rawImage;
    [SerializeField] private TextMeshProUGUI textMeshPro;
    private ItemEntry itemEntry;
    private bool isInteractable;
    private Vector3 initialScale;
    private Vector3 dropAreaScale;
    public ItemEntry ItemEntry => itemEntry;


    public void Initialize(ItemEntry itemEntry, bool isInteractable, Vector2 initialPosition, DropArea dropArea)
    {
        // ItemWordが画像を持っていたらそれを表示し，無ければTextを表示
        if (itemEntry.ItemWord.WordImage != null)
        {
            rawImage.gameObject.SetActive(true);
            textMeshPro.gameObject.SetActive(false);
            rawImage.texture = itemEntry.ItemWord.WordImage;
            rawImage.material = itemEntry.IsUsed ? usedMaterial : unusedMaterial;
        }
        else
        {
            rawImage.gameObject.SetActive(false);
            textMeshPro.gameObject.SetActive(true);
            textMeshPro.text = itemEntry.ItemWord.Word;
        }

        dragUIElement.Initialize(this, IsDraggable(), initialPosition, dropArea);
        dragUIElement.OnDroppedInArea += () => { ChangeScale(dropAreaScale); };
        dragUIElement.OnDroppedOutArea += () => { ChangeScale(initialScale); };
        this.itemEntry = itemEntry;
        // this.isInteractable = isInteractable;
        initialScale = transform.localScale;
        dropAreaScale = initialScale * DROP_AREA_SCALE_MULTIPLIER;
    }
    private bool IsDraggable()
    {
        return isInteractable && !itemEntry.IsUsed;
    }
    private void ChangeScale(Vector3 scale)
    {
        transform.localScale = scale;
    }
}