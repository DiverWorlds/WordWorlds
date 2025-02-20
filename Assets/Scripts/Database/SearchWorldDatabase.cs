using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
[Serializable]
[CreateAssetMenu(fileName = "SearchWorldDatabase", menuName = "ScriptableObject/SearchWorldDatabase")]
public class SearchWorldDatabase : ScriptableObject
{
    [SerializeField] private ItemWordDatabase wordDB;
    [SerializeField] private List<SearchWorld> searchWorldList;
    private HashSet<SearchWorld> searchWorlds;
    public HashSet<SearchWorld> SearchWorlds => searchWorlds;
    private List<string[]> recipeData = new();
    private List<string> headWords = new();

    public void Initialize()
    {
        searchWorlds = new (searchWorldList);

        TextAsset recipeCSV = Resources.Load<TextAsset>("searchWorldRecipe");
        string[] recipeDataLines = recipeCSV.text.Replace("\r\n", "\n").Split("\n");
        for (int i=0; i<recipeDataLines.Length; i++)
        {
            string[] recipeDataLine = recipeDataLines[i].Split(",");
            if (i==0)
            {
                headWords = new(recipeDataLine[1..]);
            }
            else
            {
                recipeData.Add(recipeDataLine[1..]);
            }
        }
    }

    public SearchWorld GetRecalledWorld(ItemWord itemWordA, ItemWord itemWordB)
    {
        int indexA = headWords.IndexOf(itemWordA.Word);
        int indexB = headWords.IndexOf(itemWordB.Word);
        
        if (indexA==indexB)
        {
            Logger.Log($"同じItemWordが選択されました。: {itemWordA.Word}");
            return null;
        }
        if (LogNotExistInRecipe(itemWordA.Word, indexA) || LogNotExistInRecipe(itemWordB.Word, indexB))
        {
            return null;
        }

        string recipeResultId = !recipeData[indexA][indexB].Equals("-") ? recipeData[indexA][indexB] : recipeData[indexB][indexA];
        if (!recipeData[indexA][indexB].Equals("-"))
        {
            recipeResultId = recipeData[indexA][indexB];
        }
        else if (!recipeData[indexB][indexA].Equals("-"))
        {
            recipeResultId = recipeData[indexB][indexA];
        }
        else
        {
            Logger.Log($"次のItemWordの組み合わせはsearchWorldRecipeに含まれていません。: ({itemWordA.Word}, {itemWordB.Word})");
            return null;
        }

        return searchWorlds.FirstOrDefault(w => w.Id.Equals(recipeResultId));
    }

    private bool LogNotExistInRecipe(string word, int index)
    {
        if (index==-1)
        {
            Logger.Log($"次のItemWordはsearchWorldRecipe内に存在しません。: {word}");
            return true;
        }
        return false;
    }
}