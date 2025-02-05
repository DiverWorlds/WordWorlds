using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
        var found = itemWords.Where(w => w.Word.Equals(word));
        if (found.Count()>1)
        {
            Logger.Log($"指定されたItemWord: {word}が複数見つかりました。");
            return null;
        }
        return (ItemWord)found;
    }
}