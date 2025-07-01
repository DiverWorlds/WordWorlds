using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DropArea : MonoBehaviour
{
    private const int MAX_VALUE = 2;
    [SerializeField] private List<Transform> dropSlots;
    [SerializeField] private CounterTextController counterTextController;
    private DragUIElement[] placedUIElements = new DragUIElement[MAX_VALUE];
    private Action onAreaFullFilled;

    // 配列のコピーを返すことで外部からの書き換えを防ぐ
    public DragUIElement[] PlacedUIElements => placedUIElements.ToArray();

    public event Action OnAreaFullFilled { add => onAreaFullFilled += value; remove => onAreaFullFilled -= value; }

    public void HandleDrop(DragUIElement droppedElement)
    {
        // 先着順でdropSlotにdroppedElementを配置し，placedUIElementsに記録する．
        for (int i = 0; i < MAX_VALUE; i++)
        {
            if (placedUIElements[i] == null)
            {
                placedUIElements[i] = droppedElement;
                droppedElement.transform.localPosition = dropSlots[i].localPosition;
                break;
            }
        }

        int droppedObjectsCount = placedUIElements.Where(w => w != null).Count();
        counterTextController.SetCounter(droppedObjectsCount);

        if (droppedObjectsCount == MAX_VALUE)
        {
            onAreaFullFilled?.Invoke();
        }
    }
    public void HandleRemove(DragUIElement removedElement)
    {
        for (int i = 0; i < MAX_VALUE; i++)
        {
            if (placedUIElements[i] != null && placedUIElements[i].Equals(removedElement))
            {
                placedUIElements[i] = null;
                break;
            }
        }

        int droppedObjectsCount = placedUIElements.Where(w => w != null).Count();
        counterTextController.SetCounter(droppedObjectsCount);
    }
    public void ResetPlacedElements()
    {
        for (int i = 0; i < MAX_VALUE; i++)
        {
            if (placedUIElements[i] != null)
            {
                placedUIElements[i].ResetPosition();
                placedUIElements[i] = null;
            }
        }
        counterTextController.SetCounter(0);
    }
}