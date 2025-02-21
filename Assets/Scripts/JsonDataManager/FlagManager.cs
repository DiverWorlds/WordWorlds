using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class FlagManager : JsonDataManager
{
    //TODO: デバッグ用のフラグファイルを読み込めるように、SerializeFieldでファイルパスを指定できるようにする。
    [SerializeField] private string InitialFlagsPath = "InitialFlags";
    [SerializeField] private string SavedFlagsPath = "SavedFlags";
    private HashSet<Flag> data = new();
    public HashSet<Flag> Data => data;

    void Start()
    {
        loadFilePath = InitialFlagsPath;
        saveFilePath = SavedFlagsPath;
        Load();
    }

    public override bool Load()
    {
        try
        {
            string flagJsonText = Resources.Load<TextAsset>(loadFilePath).text;
            Logger.Log("flagJsonText", flagJsonText);
            data = JsonUtility.FromJson<FlagData>(flagJsonText).ToHashSet();
            Logger.LogElements("data", data.Select(f => f.ToString()));
            return true;
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return false;
        }
    }

    public bool Get(string key)
    {
        // data
        return true;
    }

    public override void Change(string name, bool value)
    {
        // data[name] = value;
    }

    public override bool Save()
    {
        try
        {
            Logger.LogElements("data", data.Select(f => f.ToString()));
            string flagJsonText = JsonUtility.ToJson(data);
            Logger.Log("flagJsonText", flagJsonText);
            File.WriteAllText(saveFilePath+".json", flagJsonText);
            return true;
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return false;
        }
    }

    public override void Clear()
    {
        data = new();
    }

    public override void Log()
    {
        // string result = $"{name} Log\n";
        // foreach (var record in data)
        // {
        //     result += $"{record.Key}: {record.Value}\n";
        // }
        // Logger.Log(result);
    }
}