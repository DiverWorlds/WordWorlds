using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.XR;
public class DraggableItemWord : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] private RawImage rawImage;
    [SerializeField] private TextMeshProUGUI textMeshProUGUI; // imageにアタッチする画像がまだ用意されてない時はTextを表示する．
    [SerializeField] private Material unusedMaterial;  // 未使用の際に適用されるマテリアル
    [SerializeField] private Material usedMaterial; // 使用済みの際に適用されるマテリアル
    private RectTransform parentRectTransform;
    private RectTransform rectTransform;
    private Vector2 initialPos; // ドラッグ前の初期position
    private Vector3 initialScale;
    private float scaleOnDropAreaMultiplier = 1.7f;
    private Vector3 scaleOnDropArea;
    private ItemEntry itemEntry;
    private ItemWordDropArea itemWordDropArea;
    private bool isOverDropArea = false; // ドラッグ中にDropArea内に侵入したかどうか
    private bool isPlacedOnDropArea = false; // DropAreaと初期位置のどちらに配置された状態か

    public ItemEntry ItemEntry
    {
        get { return itemEntry; }
    }
    public bool IsInDropArea
    {
        set { isOverDropArea = value; }
    }

    public void Initialize(Vector2 initialPos, ItemEntry itemEntry, ItemWordDropArea itemWordDropArea)
    {
        //　GetComponentがどれほど処理に影響があるか試すため，試験的にGetComponentを使用する
        rectTransform = GetComponent<RectTransform>();
        parentRectTransform = transform.parent.GetComponent<RectTransform>();
        this.initialPos = initialPos;
        SetPosition(initialPos);
        initialScale = transform.localScale;
        scaleOnDropArea = initialScale * scaleOnDropAreaMultiplier;
        this.itemEntry = itemEntry;

        // ItemWordが画像を持っていたらそれを表示し，無ければTextを表示
        if (ItemEntry.ItemWord.WordImage)
        {
            textMeshProUGUI.gameObject.SetActive(false);
            rawImage.texture = ItemEntry.ItemWord.WordImage;
            rawImage.material = itemEntry.IsUsed ? usedMaterial : unusedMaterial;
        }
        else
        {
            rawImage.gameObject.SetActive(false);
            textMeshProUGUI.text = itemEntry.ItemWord.Word;
        }
        this.itemWordDropArea = itemWordDropArea;
    }

    // ドラッグ中の処理
    public void OnDrag(PointerEventData eventData)
    {
        if (!itemWordDropArea.IsPredictCanvasActive)
        {
            Vector2 localPosition = GetLocalPosition(eventData.position);
            SetPosition(localPosition);
        }
    }

    // ドラッグ終了時の処理
    public void OnEndDrag(PointerEventData eventData)
    {
        if (!itemWordDropArea.IsPredictCanvasActive)
        {
            Logger.Log("ドロップ処理開始: " + ItemEntry.ItemWord.Word);
            if (!isPlacedOnDropArea)
            {
                if (isOverDropArea)
                {
                    Logger.Log("ドロップエリアにいる: " + ItemEntry.ItemWord.Word);
                    // ドロップエリアにいる場合はドロップ処理を行う
                    itemWordDropArea.HandleItemWordDrop(this);
                    isPlacedOnDropArea = true;
                }
                else
                {
                    Logger.Log("ドロップエリアにいない: " + ItemEntry.ItemWord.Word);
                    // ドロップエリアにいない場合は初期位置に戻す
                    ResetPosition();
                }
            }
            else
            {
                if (isOverDropArea)
                {
                    Logger.Log("ドロップエリアにいるが、すでに配置済み: " + ItemEntry.ItemWord.Word);
                    itemWordDropArea.RepositionItemWord(this);
                }
                else
                {
                    Logger.Log("ドロップエリアから出た: " + ItemEntry.ItemWord.Word);
                    itemWordDropArea.HandleItemWordRemove(this);
                    isPlacedOnDropArea = false;
                }
            }
        }
    }

    public void ResetPosition()
    {
        // ドラッグ前の位置に戻す
        SetPosition(initialPos);
    }
    public void SetInitialScale()
    {
        // 初期スケールに戻す
        transform.localScale = initialScale;
    }
    public void SetScaleOnDropArea()
    {
        // ドロップエリアにいるときのスケールに変更
        transform.localScale = scaleOnDropArea;
    }

    // ScreenPositionからlocalPositionへの変換関数
    private Vector2 GetLocalPosition(Vector2 screenPosition)
    {
        Vector2 result = Vector2.zero;

        // screenPositionを親の座標系(parentRectTransform)に対応するよう変換する.
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, screenPosition, Camera.main, out result);

        return result;
    }
    private void SetPosition(Vector2 position)
    {
        rectTransform.anchoredPosition = position;
    }
    
}
