using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[Serializable]
[CreateAssetMenu(fileName = "ItemWordDatabase", menuName = "ScriptableObject/ItemWordDatabase")]

public class ItemWordDatabase : ScriptableObject
{
    [SerializeField] private List<ItemWord> itemWordList;
    private HashSet<ItemWord> itemWords;
    public HashSet<ItemWord> ItemWords => itemWords;

    public void Initialize()
    {
        itemWords = new(itemWordList);
    }

    public ItemWord GetItemWord(string word)
    {
        ItemWord foundItemWord = itemWords.FirstOrDefault(w => w.Word.Equals(word));
        if (foundItemWord != null)
        {
            return foundItemWord;
        }
        else
        {
            Logger.Log($"存在しないItemWordです。: {word}");
            return null;
        }
    }
}