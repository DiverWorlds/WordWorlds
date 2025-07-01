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
    private Vector3 initialScale;
    private Vector3 dropAreaScale;
    public ItemEntry ItemEntry => itemEntry;


    public void Initialize(ItemEntry itemEntry, bool isInteractable, Vector2 initialPosition, DropArea dropArea)
    {
        this.itemEntry = itemEntry;
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

        dragUIElement.Initialize(this, IsDraggable(isInteractable, itemEntry.IsUsed), initialPosition, dropArea);
        dragUIElement.OnDroppedInArea += () => { ChangeScale(dropAreaScale); };
        dragUIElement.OnDroppedOutArea += () => { ChangeScale(initialScale); };
        initialScale = transform.localScale;
        dropAreaScale = initialScale * DROP_AREA_SCALE_MULTIPLIER;
        gameObject.name = gameObject.name + "_" + itemEntry.ItemWord.Word;
    }

    /// <summary>
    /// アイテムがドラッグ可能かどうかを判定します。
    /// </summary>
    /// <param name="isInteractable">アイテムが操作可能かどうか。</param>
    /// <param name="isUsed">アイテムが既に使用済みかどうか。</param>
    /// <returns>
    /// アイテムが操作可能かつ未使用の場合は <c>true</c>、それ以外は <c>false</c> を返します。
    /// </returns>
    private bool IsDraggable(bool isInteractable, bool isUsed)
    {
        return isInteractable && !isUsed;
    }
    private void ChangeScale(Vector3 scale)
    {
        Logger.Log($"ItemWordUI{gameObject.name}のスケールを変更: {scale}");
        transform.localScale = scale;
    }
}