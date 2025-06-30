using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragUIElement : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private MonoBehaviour contentData;
    private bool isDraggable;
    private bool isPlacedInDropArea;
    private bool isOverDropArea;
    private Vector2 initialPosition;
    private Vector2 lastPosition;
    private Action onDroppedInArea;
    private Action onDroppedOutArea;
    private RectTransform rectTransform;
    private RectTransform parentRectTransform;
    private DropArea dropArea;
    private RectTransform dropAreaRectTransform;
    public bool IsDraggable
    {
        set { isDraggable = value; }
    }
    public event Action OnDroppedInArea { add => onDroppedInArea += value; remove => onDroppedInArea -= value; }
    public event Action OnDroppedOutArea { add => onDroppedOutArea += value; remove => onDroppedOutArea -= value; }

    void Update()
    {
        if (dropAreaRectTransform == null)
        {
            Debug.LogError("DropAreaのRectTransformが設定されていません。");
            return;
        }
        // DragUIElementとDropAreaが同じオブジェクトを親に持つことが前提の実装であるので注意．
        Rect uiElementRect = GetWorldRect(rectTransform);
        Rect dropAreaRect = GetWorldRect(dropAreaRectTransform);
        if (uiElementRect.Overlaps(dropAreaRect))
        {
            if (!isOverDropArea)
            {
                isOverDropArea = true;
                Logger.Log("ドロップエリアに重なった", contentData.gameObject.name);
            }
        }
        else
        {
            if (isOverDropArea)
            {
                isOverDropArea = false;
                Logger.Log("ドロップエリアから離れた", contentData.gameObject.name);
            }
        }
    }
    public void Initialize(Vector2 initialPosition, DropArea dropArea)
    {
        this.initialPosition = initialPosition;
        lastPosition = initialPosition;
        rectTransform = transform.GetComponent<RectTransform>();
        parentRectTransform = transform.parent.GetComponent<RectTransform>();
        this.dropArea = dropArea;
        dropAreaRectTransform = dropArea.GetComponent<RectTransform>();
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
                Logger.Log("ドロップエリアに入った", contentData.gameObject.name);
                dropArea.HandleDrop(this);
                isPlacedInDropArea = true;
                onDroppedInArea.Invoke();
            }
            else if (IsDroppedInToOutArea())
            {
                Logger.Log("ドロップエリアから出た", contentData.gameObject.name);
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
    private void SetPosition(Vector2 position)
    {
        rectTransform.anchoredPosition = position;
    }
    private bool IsDroppedOutToInArea()
    {
        return isOverDropArea && isPlacedInDropArea;
    }
    private bool IsDroppedInToOutArea()
    {
        return isOverDropArea && !isPlacedInDropArea;
    }
    private bool IsDropCanceledOutArea()
    {
        return !isOverDropArea && !isPlacedInDropArea;
    }
    private bool IsDropCanceledInArea()
    {
        return !isOverDropArea && isPlacedInDropArea;
    }
}