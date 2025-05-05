using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class DraggableWordUI : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private RawImage image;
    private Vector2 prevPos; //保存しておく初期position
    private RectTransform rectTransform; // 移動したいオブジェクトのRectTransform
    private RectTransform parentRectTransform; // 移動したいオブジェクトの親(Panel)のRectTransform
    private ItemEntry itemEntry;
    public ItemEntry ItemEntry
    {
        get { return itemEntry; }
    }
    private WordDropper wordDropper;
    public WordDropper WordDropper
    {
        get { return wordDropper; }
        set
        {
            wordDropper = value;
            if (wordDropper)
            {
                this.textMeshProUGUI.color = new Color(1, 1, 1, 1);
            }
            else
            {
                this.textMeshProUGUI.color = new Color(1, 0, 0, 1);
            }
        }
    }
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;

    [SerializeField] private Material unUsedMaterial;
    [SerializeField] private Material isUsedMaterial;

    public void Initialize(Vector2 prevPos, ItemEntry itemEntry)
    {
        rectTransform = GetComponent<RectTransform>();
        parentRectTransform = rectTransform.parent as RectTransform;
        this.prevPos = prevPos;
        rectTransform.anchoredPosition = prevPos;
        this.itemEntry = itemEntry;
        if (ItemEntry.ItemWord.WordImage)
        {
            this.textMeshProUGUI.gameObject.SetActive(false);
            this.image.texture = ItemEntry.ItemWord.WordImage;
            if (itemEntry.IsUsed) image.material = isUsedMaterial;
            else image.material = unUsedMaterial;
        }
        else
        {
            //デバッグ用、画像
            this.image.gameObject.SetActive(false);
            this.textMeshProUGUI.text = itemEntry.ItemWord.Word;
        }
    }

    // ドラッグ開始時の処理
    public void OnBeginDrag(PointerEventData eventData)
    {
        // ドラッグ前の位置を記憶しておく
        // RectTransformの場合はpositionではなくanchoredPositionを使う
        if (!ItemEntry.IsUsed) prevPos = rectTransform.anchoredPosition;

    }

    // ドラッグ中の処理
    public void OnDrag(PointerEventData eventData)
    {
        if (!ItemEntry.IsUsed)
        {
            // eventData.positionから、親に従うlocalPositionへの変換を行う
            // オブジェクトの位置をlocalPositionに変更する
            Vector2 localPosition = GetLocalPosition(eventData.position);
            rectTransform.anchoredPosition = localPosition;
        }
    }

    // ドラッグ終了時の処理
    public void OnEndDrag(PointerEventData eventData)
    {
        if (!ItemEntry.IsUsed)
        {
            // オブジェクトをドラッグ前の位置に戻す
            rectTransform.anchoredPosition = prevPos;

            if (WordDropper)
            {
                WordDropper.AddItemWord(this);
            }
        }
    }

    // ScreenPositionからlocalPositionへの変換関数
    private Vector2 GetLocalPosition(Vector2 screenPosition)
    {
        Vector2 result = Vector2.zero;

        // screenPositionを親の座標系(parentRectTransform)に対応するよう変換する.
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, screenPosition, Camera.main, out result);

        return result;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        WordDropper = collision.gameObject.GetComponent<WordDropper>();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        WordDropper = null;
    }
}
