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
    public string Id => id; //csvに記述された、省略された名前
    public string WorldName => worldName; //正式な日本語の名前
}