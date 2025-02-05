using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchWorld
{
    [SerializeField] private string id;
    [SerializeField] private string name;
    [SerializeField] private Image image;
    [SerializeField] private List<ItemWord> itemWords;
    public string Id => id; //csvに記述された、省略された名前
    public string Name => name; //正式な日本語の名前
    public Image Image => image;
    public List<ItemWord> ItemWords => itemWords;
}