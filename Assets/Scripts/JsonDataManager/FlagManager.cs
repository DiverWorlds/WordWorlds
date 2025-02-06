using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class FlagManager : JsonDataManager
{
    private string filePath = "Flags";
    private Flags data;
    public Flags Data => data;

    public override bool Load()
    {
        try
        {
            string flagJsonText = Resources.Load<TextAsset>(filePath).text;
            data = JsonUtility.FromJson<Flags>(flagJsonText);
            return true;
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return false;
        }
    }

    public override bool Save()
    {
        try
        {
            string flagJsonText = JsonUtility.ToJson(data, true);
            File.WriteAllText(filePath, flagJsonText);
            return true;
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return false;
        }
    }
}