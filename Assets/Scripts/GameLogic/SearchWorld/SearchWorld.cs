using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[Serializable]
[CreateAssetMenu(fileName = "SearchWorld", menuName = "ScriptableObject/SearchWorld")]

public class SearchWorld : ScriptableObject
{
    [SerializeField] private string id;
    [SerializeField] private string worldName;
    [SerializeField] private GameObject worldPreview;
    /// <summary>
    /// csvに記述された、省略された名前。
    /// この値はScene名と完全に一致している必要があります。
    /// </summary>
    public string Id => id;
    public string WorldName => worldName; //正式な日本語の名前
    public GameObject WorldPreview => worldPreview; //世界のミニチュアのオブジェクトデータ

}