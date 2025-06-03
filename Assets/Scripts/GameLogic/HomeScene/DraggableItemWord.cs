using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class DraggableItemWord : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private RawImage rawImage;
    [SerializeField] private TextMeshProUGUI textMeshProUGUI; // imageにアタッチする画像がまだ用意されてない時はTextを表示する．
    [SerializeField] private Material unusedMaterial;  // 未使用の際に適用されるマテリアル
    [SerializeField] private Material usedMaterial; // 使用済みの際に適用されるマテリアル
    private RectTransform parentRectTransform;
    private Vector2 initialPos; // ドラッグ前の初期position
    private RectTransform rectTransform; // このオブジェクトのRectTransform
    private ItemEntry itemEntry; // ItemWordの実データ
    private ItemWordDropArea itemWordDropArea;//オブジェクトのドラッグ先


    public ItemEntry ItemEntry
    {
        get { return itemEntry; }
    }
    public ItemWordDropArea ItemWordDropArea//ドラッグ先のプロパティ
    {
        get;
        set;
    }


    public void Initialize(Vector2 initialPos, ItemEntry itemEntry)
    {
        //　GetComponentがどれほど処理に影響があるか試すため，試験的にGetComponentを使用する
        rectTransform = GetComponent<RectTransform>();
        parentRectTransform = transform.parent.GetComponent<RectTransform>();
        this.initialPos = initialPos;
        SetPosition(initialPos);
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
    }

    // ドラッグ開始時の処理
    public void OnBeginDrag(PointerEventData eventData)
    {
        // ドラッグ前の位置を記憶しておく
        if (!ItemEntry.IsUsed) initialPos = rectTransform.anchoredPosition;
    }

    // ドラッグ中の処理
    public void OnDrag(PointerEventData eventData)
    {
        if (!ItemEntry.IsUsed)
        {
            // eventData.positionから、親に従うlocalPositionへの変換を行う
            Vector2 localPosition = GetLocalPosition(eventData.position);
            SetPosition(localPosition);
        }
    }

    // ドラッグ終了時の処理
    public void OnEndDrag(PointerEventData eventData)
    {
        if (!ItemEntry.IsUsed)
        {
            // オブジェクトをドラッグ前の位置に戻す
            SetPosition(initialPos);

            if (ItemWordDropArea)//ドロップ可能な場所でマウスが離れた時
            {
                ItemWordDropArea.AddItemWord(this);
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
        ItemWordDropArea = collision.gameObject.GetComponent<ItemWordDropArea>();//ドロップ可能な位置に来たらドロッパーを取得
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        ItemWordDropArea = null;//ドロップ可能な位置から離れたらドロッパーを忘れる
    }
    private void SetPosition(Vector2 position)
    {
        rectTransform.anchoredPosition = position;
    }
}
