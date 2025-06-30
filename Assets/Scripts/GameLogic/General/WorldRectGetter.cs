using UnityEngine;

public static class WorldRectGetter
{
    /// <summary>
    /// 指定されたRectTransformのワールド座標を取得します。
    /// </summary>
    /// <param name="rectTransform">対象のRectTransform</param>
    /// <returns>ワールド座標のRect</returns>
    public static Rect Get(RectTransform rectTransform)
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

}