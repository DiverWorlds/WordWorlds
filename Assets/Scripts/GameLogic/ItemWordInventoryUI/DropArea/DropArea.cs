using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DropArea : MonoBehaviour
{
    private const int MAX_VALUE = 2;
    [SerializeField] private List<Transform> dropSlots;
    [SerializeField] private CounterTextController counterTextController;
    private DragUIElement[] placedItemWords = new DragUIElement[MAX_VALUE];
    private Action onAreaFullFilled;
    public DragUIElement[] PlacedItemWords => placedItemWords;
    public event Action OnAreaFullFilled { add => onAreaFullFilled += value; remove => onAreaFullFilled -= value; }

    public void HandleDrop(DragUIElement droppedElement)
    {
        // 先着順でdropSlotにdroppedObjectを配置し，placedItemWordsに記録する．
        for (int i = 0; i < MAX_VALUE; i++)
        {
            if (placedItemWords[i] == null)
            {
                placedItemWords[i] = droppedElement;
                droppedElement.transform.localPosition = dropSlots[i].localPosition;
                break;
            }
        }

        int droppedObjectsCount = placedItemWords.Where(w => w != null).Count();
        counterTextController.SetCounter(droppedObjectsCount);

        if (droppedObjectsCount == MAX_VALUE)
        {
            onAreaFullFilled.Invoke();
        }
    }

    public void HandleRemove(DragUIElement removedElement)
    {
        for (int i = 0; i < MAX_VALUE; i++)
        {
            if (placedItemWords[i].Equals(removedElement))
            {
                placedItemWords[i] = null;
            }
        }
        
        int droppedObjectsCount = placedItemWords.Where(w => w != null).Count();
        counterTextController.SetCounter(droppedObjectsCount);
    }
}