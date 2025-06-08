using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.XR;
using NUnit.Framework;
public class DraggableItemWord : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] private RawImage rawImage;
    [SerializeField] private TextMeshProUGUI textMeshProUGUI; // imageにアタッチする画像がまだ用意されてない時はTextを表示する．
    [SerializeField] private Material unusedMaterial;  // 未使用の際に適用されるマテリアル
    [SerializeField] private Material usedMaterial; // 使用済みの際に適用されるマテリアル
    private RectTransform parentRectTransform;
    private RectTransform rectTransform;
    private HomeManager homeManager; // HomeManagerの参照を保持する
    private Vector2 initialPos; // ドラッグ前の初期position
    private Vector3 initialScale;
    private float scaleOnDropAreaMultiplier = 1.7f;
    private Vector3 scaleOnDropArea;
    private Vector2 lastPosition;
    private ItemEntry itemEntry;
    private ItemWordDropArea itemWordDropArea;
    private bool isOverDropArea = false; // ドラッグ中にDropArea内に侵入したかどうか
    private bool isPlacedOnDropArea = false; // DropAreaと初期位置のどちらに配置された状態か．HandleItemWordDrop/Remove時とAutoMoveToInitialPosition時に更新する
    private Dictionary<DropPoints, Vector2> dropPointsDict = new();
    public ItemEntry ItemEntry
    {
        get { return itemEntry; }
    }

    public void Initialize(Vector2 initialPos, ItemEntry itemEntry, ItemWordDropArea itemWordDropArea)
    {
        //　GetComponentがどれほど処理に影響があるか試すため，試験的にGetComponentを使用する
        rectTransform = GetComponent<RectTransform>();
        parentRectTransform = transform.parent.GetComponent<RectTransform>();
        homeManager = HomeManager.Instance;
        this.initialPos = initialPos;
        lastPosition = initialPos;
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ItemWordDropArea>() != null)
        {
            isOverDropArea = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<ItemWordDropArea>() != null)
        {
            isOverDropArea = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (IsDragAvailable()) SetPosition(GetLocalPosition(eventData.position));
    }

    // ドラッグ終了時の処理
    public void OnEndDrag(PointerEventData eventData)
    {
        if (IsDragAvailable())
        {
            if (isPlacedOnDropArea)
            {
                if (isOverDropArea)
                {
                    Logger.Log("ドロップエリアにいるが、すでに配置済み: " + ItemEntry.ItemWord.Word);
                    SetPosition(lastPosition);
                }
                else
                {
                    Logger.Log("ドロップエリアから出た: " + ItemEntry.ItemWord.Word);
                    itemWordDropArea.HandleItemWordRemove(this);
                    SetInitialScale();
                    isPlacedOnDropArea = false;
                }
            }
            else
            {
                if (isOverDropArea)
                {
                    Logger.Log("ドロップエリアにいる: " + ItemEntry.ItemWord.Word);
                    // ドロップエリアにいる場合はドロップ処理を行う
                    bool isSuccessMoving = itemWordDropArea.HandleItemWordDrop(this);
                    if (isSuccessMoving)
                    {
                        SetScaleOnDropArea();
                        isPlacedOnDropArea = true;
                    }
                    else
                    {
                        SetPosition(lastPosition);
                    }
                }
                else
                {
                    Logger.Log("ドロップエリアにいない: " + ItemEntry.ItemWord.Word);
                    // ドロップエリアにいない場合は初期位置に戻す
                    SetPosition(lastPosition);
                }
            }
        }
        lastPosition = rectTransform.anchoredPosition; // ドラッグ終了時の位置を保存
    }
    public void AutoMoveToInitialPosition()
    {
        // ドラッグ前の位置に戻す
        SetPosition(initialPos);
        isPlacedOnDropArea = false;
    }
    public void SetInitialScale()
    {
        // 初期スケールに戻す
        Logger.Log($"{itemEntry.ItemWord.Word}: 初期スケールに戻す前のスケール: " + transform.localScale);
        transform.localScale = initialScale;
        Logger.Log($"{itemEntry.ItemWord.Word}: 初期スケールに戻した後のスケール: " + transform.localScale);
    }
    private void SetScaleOnDropArea()
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
    public void SetPosition(Vector2 position)
    {
        rectTransform.anchoredPosition = position;
    }
    private bool IsDragAvailable()
    {
        return !itemEntry.IsUsed && !homeManager.PredictCanvas.gameObject.activeSelf;
    }
}
