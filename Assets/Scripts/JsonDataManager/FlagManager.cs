using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FlagManager : JsonDataManager
{
    //TODO: デバッグ用のフラグファイルを読み込めるように、SerializeFieldでファイルパスを指定できるようにする。
    private FlagData data;
    public FlagData Data => data;

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
            data = JsonUtility.FromJson<FlagData>(flagJsonText);
            return true;
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return false;
        }
    }

    public override void Change(string name, bool value)
    {
        data.Flags[name] = value;
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