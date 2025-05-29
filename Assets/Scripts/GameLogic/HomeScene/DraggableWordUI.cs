using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class DraggableWordUI : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private RawImage image;//文字画像
    private Vector2 prevPos; //ドラッグ前の初期position
    private RectTransform rectTransform; // このオブジェクトのRectTransform
    [SerializeField] private RectTransform parentRectTransform; // 親のRectTransform(Canvas)
    private ItemEntry itemEntry;//ワードアイテムの実データ
    public ItemEntry ItemEntry
    {
        get { return itemEntry; }
    }
    private WordDropper wordDropper;//オブジェクトのドラッグ先
    public WordDropper WordDropper//ドラッグ先のプロパティ
    {
        get;
        set;
    }
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;//開発中、画像がない際にTMProを仮置きする際に使う

    [SerializeField] private Material unUsedMaterial;//未使用の際に適用されるマテリアル
    [SerializeField] private Material isUsedMaterial;//使用済みの際に適用されるマテリアル

    public void Initialize(Vector2 prevPos, ItemEntry itemEntry)
    {
        rectTransform = GetComponent<RectTransform>();
        parentRectTransform = rectTransform.parent as RectTransform;
        this.prevPos = prevPos;
        rectTransform.anchoredPosition = prevPos;
        this.itemEntry = itemEntry;
        if (ItemEntry.ItemWord.WordImage)
        {
            this.textMeshProUGUI.gameObject.SetActive(false);//TMProを無効に
            this.image.texture = ItemEntry.ItemWord.WordImage;//画像を有効に
            if (itemEntry.IsUsed) image.material = isUsedMaterial;
            else image.material = unUsedMaterial;
        }
        else
        {
            //開発中、画像が用意されていないワードに対して
            this.image.gameObject.SetActive(false);//画像を無効に
            this.textMeshProUGUI.text = itemEntry.ItemWord.Word;//TMProを有効に
        }
    }

    // ドラッグ開始時の処理
    public void OnBeginDrag(PointerEventData eventData)
    {
        // ドラッグ前の位置を記憶しておく
        if (!ItemEntry.IsUsed) prevPos = rectTransform.anchoredPosition;

    }

    // ドラッグ中の処理
    public void OnDrag(PointerEventData eventData)
    {
        if (!ItemEntry.IsUsed)
        {
            // eventData.positionから、親に従うlocalPositionへの変換を行う
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

            if (WordDropper)//ドロップ可能な場所でマウスが離れた時
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
        WordDropper = collision.gameObject.GetComponent<WordDropper>();//ドロップ可能な位置に来たらドロッパーを取得
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        WordDropper = null;//ドロップ可能な位置から離れたらドロッパーを忘れる
    }
}
