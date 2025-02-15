

using System;
using System.IO;
using UnityEngine;

public class FlagManager : JsonDataManager
{
    //TODO: デバッグ用のフラグファイルを読み込めるように、SerializeFieldでファイルパスを指定できるようにする。
    private Flags data;
    public Flags Data => data;

    void Start()
    {
        filePath = "Flags";
        Load();
    }

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