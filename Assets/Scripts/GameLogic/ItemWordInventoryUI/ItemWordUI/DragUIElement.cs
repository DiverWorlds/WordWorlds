using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragUIElement : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private MonoBehaviour coreComponent;
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

    public MonoBehaviour CoreComponent => coreComponent;
    public event Action OnDroppedInArea { add => onDroppedInArea += value; remove => onDroppedInArea -= value; }
    public event Action OnDroppedOutArea { add => onDroppedOutArea += value; remove => onDroppedOutArea -= value; }

    public void Initialize(MonoBehaviour coreComponent, bool isDraggable, Vector2 initialPosition, DropArea dropArea)
    {
        this.coreComponent = coreComponent;
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

        Rect uiElementRect = WorldRectGetter.Get(rectTransform);
        Rect dropAreaRect = WorldRectGetter.Get(dropAreaRectTransform);
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