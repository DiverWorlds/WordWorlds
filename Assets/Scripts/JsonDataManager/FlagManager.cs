using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FlagManager : JsonDataManager
{
    //TODO: デバッグ用のフラグファイルを読み込めるように、SerializeFieldでファイルパスを指定できるようにする。
    [SerializeField] private string InitialFlagsPath = "InitialFlags";
    [SerializeField] private string SavedFlagsPath = "SavedFlags";
    private Dictionary<string, bool> data = new();
    public Dictionary<string, bool> Data => data;

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
            // data = JsonUtility.FromJson<FlagData>(flagJsonText).ToTupleList();
            Logger.LogElements("data Keys", data.Keys);
            Logger.Log("DummyA", data["DummyA"]);
            Logger.Log("DummyB", data["DummyB"]);
            Logger.Log("DummyC", data["DummyC"]);
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
        data[name] = value;
    }

    public override bool Save()
    {
        try
        {
            // string flagJsonText = JsonUtility.ToJson(data, true);
            string flagJsonText = JsonUtility.ToJson(new FlagData(new(){new("DummyA", true),new("DummyB", false)}));
            Logger.Log("ToJson; flagJsonText", flagJsonText);
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
        string result = $"{name} Log\n";
        foreach (var record in data)
        {
            result += $"{record.Key}: {record.Value}\n";
        }
        Logger.Log(result);
    }
}