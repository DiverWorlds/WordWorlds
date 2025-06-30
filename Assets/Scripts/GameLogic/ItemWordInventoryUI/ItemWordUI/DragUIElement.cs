using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragUIElement : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private bool isDraggable;
    private bool isPlacedInDropArea;
    private Action onDroppedInArea;
    private Action onDroppedOutArea;
    private Vector2 initialPosition;
    private Vector2 lastPosition;
    private RectTransform rectTransform;
    private RectTransform parentRectTransform;
    private DropArea dropArea;
    private RectTransform dropAreaRectTransform;
    public event Action OnDroppedInArea { add => onDroppedInArea += value; remove => onDroppedInArea -= value; }
    public event Action OnDroppedOutArea { add => onDroppedOutArea += value; remove => onDroppedOutArea -= value; }

    public void Initialize(bool isDraggable, Vector2 initialPosition, DropArea dropArea)
    {
        this.isDraggable = isDraggable;
        this.initialPosition = initialPosition;
        lastPosition = initialPosition;
        rectTransform = transform.GetComponent<RectTransform>();
        parentRectTransform = transform.parent.GetComponent<RectTransform>();
        this.dropArea = dropArea;
        dropAreaRectTransform = dropArea.GetComponent<RectTransform>();
    }

    /// <summary>
    /// このUI要素のドラッグ操作が終了した際の処理を行います。
    /// ドロップ位置に応じて、要素が有効なドロップエリアに配置されたか、ドロップエリアから外されたか、
    /// またはドロップがキャンセルされたかを判定します。
    /// 要素の状態や位置を更新し、適切なイベントを呼び出します。
    /// </summary>
    private bool IsOverDropArea()
    {
        if (rectTransform == null)
        {
            Debug.LogError("DragUIElementのRectTransformを取得できていません。");
        }
        if (dropAreaRectTransform == null)
        {
            Debug.LogError("DropAreaのRectTransformを取得できていません。");
        }

        Rect uiElementRect = GetWorldRect(rectTransform);
        Rect dropAreaRect = GetWorldRect(dropAreaRectTransform);
        if (uiElementRect.Overlaps(dropAreaRect))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDraggable) SetPosition(GetLocalPosition(eventData.position));
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (isDraggable)
        {
            if (IsDroppedOutToInArea())
            {
                Logger.Log("ドロップエリアに入った", gameObject.name);
                dropArea.HandleDrop(this);
                isPlacedInDropArea = true;
                onDroppedInArea.Invoke();
            }
            else if (IsDroppedInToOutArea())
            {
                Logger.Log("ドロップエリアから出た", gameObject.name);
                dropArea.HandleRemove(this);
                isPlacedInDropArea = false;
                onDroppedOutArea.Invoke();
            }
            else if (IsDropCanceledOutArea())
            {
                SetPosition(initialPosition);
            }
            else if (IsDropCanceledInArea())
            {
                SetPosition(lastPosition);
            }
            lastPosition = transform.localPosition;
        }
    }
    private void SetPosition(Vector2 position)
    {
        rectTransform.anchoredPosition = position;
    }
    private Vector2 GetLocalPosition(Vector2 screenPosition)
    {
        Vector2 result = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, screenPosition, Camera.main, out result);
        return result;
    }
    private Rect GetWorldRect(RectTransform rectTransform)
    {
        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);

        // 左下（最小点）と右上（最大点）を計算
        Vector2 min = corners[0]; // 左下
        Vector2 max = corners[2]; // 右上

        // 幅と高さを計算
        float width = max.x - min.x;
        float height = max.y - min.y;

        return new Rect(min.x, min.y, width, height);
    }
    private bool IsDroppedOutToInArea()
    {
        return IsOverDropArea() && isPlacedInDropArea;
    }
    private bool IsDroppedInToOutArea()
    {
        return IsOverDropArea() && !isPlacedInDropArea;
    }
    private bool IsDropCanceledOutArea()
    {
        return !IsOverDropArea() && !isPlacedInDropArea;
    }
    private bool IsDropCanceledInArea()
    {
        return !IsOverDropArea() && isPlacedInDropArea;
    }
}