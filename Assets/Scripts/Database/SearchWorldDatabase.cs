using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class SearchWorldDatabase : ScriptableObject
{
    [SerializeField] private List<SearchWorld> searchWorldList;
    private HashSet<SearchWorld> searchWorlds;
    public HashSet<SearchWorld> SearchWorlds => searchWorlds;
    private List<string[]> recipeData = new();
    private List<string> rowHeadWords = new();
    private List<string> columnHeadWords = new();


    public void Initialize()
    {
        searchWorlds = new (searchWorldList);

        TextAsset recipeCSV = Resources.Load("recipe.csv") as TextAsset;
        StreamReader reader = new(recipeCSV.text);
        for (int i=0; reader.Peek() != -1; i++)
        {
            string[] recipeDataLine = reader.ReadLine().Split(',');
            if (i==0)
            {
                columnHeadWords = new(recipeDataLine[1..]);
            }
            else
            {
                rowHeadWords.Add(recipeDataLine[0]);
                recipeData.Add(recipeDataLine[1..]);
            }
        }
    }

    public SearchWorld GetRecalledWorld(ItemWord itemWordA, ItemWord itemWordB)
    {
        int indexA = rowHeadWords.IndexOf(itemWordA.Word);
        int indexB = columnHeadWords.IndexOf(itemWordB.Word);
        if (indexA==indexB)
        {
            Logger.Log($"同じItemWordが選択されました。: {itemWordA.Word}");
            return null;
        }
        if (LogUndefinedItemWord(itemWordA.Word, indexA) || LogUndefinedItemWord(itemWordB.Word, indexB))
        {
            return null;
        }
        string recipeResultId = !recipeData[indexA][indexB].Equals('-') ? recipeData[indexA][indexB] : recipeData[indexB][indexA];
        return (SearchWorld)searchWorlds.Where(w => w.Id.Equals(recipeResultId));
    }

    private bool LogUndefinedItemWord(string word, int index)
    {
        if (index==-1)
        {
            Logger.Log($"次のItemWordはレシピに含まれていません。: {word}");
            return true;
        }
        return false;
    }
}