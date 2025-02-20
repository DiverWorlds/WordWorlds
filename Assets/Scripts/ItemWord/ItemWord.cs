using System;
using UnityEngine;
[Serializable]
[CreateAssetMenu(fileName = "ItemWord", menuName = "ScriptableObject/ItemWord")]

public class ItemWord : ScriptableObject
{
    [SerializeField] private string word;
    public string Word => word;
}