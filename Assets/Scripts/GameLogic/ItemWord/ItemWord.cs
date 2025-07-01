using System;
using UnityEngine;
using UnityEngine.UI;
[Serializable]
[CreateAssetMenu(fileName = "ItemWord", menuName = "ScriptableObject/ItemWord")]

public class ItemWord : ScriptableObject
{
    [SerializeField] private string word;
    public string Word => word;

    [SerializeField] private Texture wordImage;
    public Texture WordImage => wordImage;
}