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
        isPlacedInDropArea = false;
        this.initialPosition = initialPosition;
        lastPosition = initialPosition;
        rectTransform = transform.GetComponent<RectTransform>();
        SetPosition(initialPosition);
        parentRectTransform = transform.parent.GetComponent<RectTransform>();
        this.dropArea = dropArea;
        dropAreaRectTransform = dropArea.GetComponent<RectTransform>();
    }
    public void SetPosition(Vector2 position)
    {
        rectTransform.anchoredPosition = position;
    }
    public void ResetPosition()
    {
        SetPosition(initialPosition);
        isPlacedInDropArea = false;
        dropArea.HandleRemove(this);
        onDroppedOutArea?.Invoke();
    }
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
    /// <summary>
    /// このUI要素のドラッグ操作が終了した際の処理を行います。
    /// ドロップ位置に応じて、要素が有効なドロップエリアに配置されたか、ドロップエリアから外されたか、
    /// またはドロップがキャンセルされたかを判定します。
    /// 要素の状態や位置を更新し、適切なイベントを呼び出します。
    /// </summary>
    public void OnEndDrag(PointerEventData eventData)
    {
        if (isDraggable)
        {
            if (IsDroppedOutToInArea())
            {
                Logger.Log("ドロップエリアに入った", gameObject.name);
                isPlacedInDropArea = true;
                onDroppedInArea.Invoke();
                dropArea.HandleDrop(this);
            }
            else if (IsDroppedInToOutArea())
            {
                Logger.Log("ドロップエリアから出た", gameObject.name);
                isPlacedInDropArea = false;
                SetPosition(initialPosition);
                onDroppedOutArea.Invoke();
                dropArea.HandleRemove(this);
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
    private Vector2 GetLocalPosition(Vector2 screenPosition)
    {
        Vector2 result;
        var cam = parentRectTransform != null && parentRectTransform.GetComponent<Canvas>().renderMode != RenderMode.ScreenSpaceOverlay
            ? Camera.main
            : null;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, screenPosition, cam, out result);
        return result;
    }
    private bool IsDroppedOutToInArea()
    {
        return IsOverDropArea() && !isPlacedInDropArea;
    }
    private bool IsDroppedInToOutArea()
    {
        return !IsOverDropArea() && isPlacedInDropArea;
    }
    private bool IsDropCanceledOutArea()
    {
        return !IsOverDropArea() && !isPlacedInDropArea;
    }
    private bool IsDropCanceledInArea()
    {
        return IsOverDropArea() && isPlacedInDropArea;
    }
}