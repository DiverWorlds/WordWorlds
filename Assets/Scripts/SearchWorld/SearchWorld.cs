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
    [SerializeField] private Sprite sprite;
    // SearchWorldの各シーンでSearchPointオブジェクトを配置するから、SearchWorldがitemWordを一括管理する必要はないかも
    [SerializeField] private List<ItemWord> itemWords;
    public string Id => id; //csvに記述された、省略された名前
    public string WorldName => worldName; //正式な日本語の名前
    public Sprite Sprite => sprite;
    public List<ItemWord> ItemWords => itemWords;
}