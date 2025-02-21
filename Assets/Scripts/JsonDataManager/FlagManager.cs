using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FlagManager : JsonDataManager
{
    //TODO: デバッグ用のフラグファイルを読み込めるように、SerializeFieldでファイルパスを指定できるようにする。
    [SerializeField] private string InitialFlagsPath = "InitialFlags";
    [SerializeField] private string SavedFlagsPath = "SavedFlags";
    private FlagData data;
    public FlagData Data => data;

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
            data = JsonUtility.FromJson<FlagData>(flagJsonText);
            Logger.Log("data_A", data.Flags["DummyA"]);
            Logger.Log("DummyA", data.DummyA);
            Logger.Log("DummyB", data.DummyB);
            Logger.Log("DummyC", data.DummyC);
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
            File.WriteAllText(saveFilePath, flagJsonText);
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
        data.Flags = new();
    }

    public override void Log()
    {
        string result = $"{name} Log\n";
        foreach (var record in data.Flags)
        {
            result += $"{record.Key}: {record.Value}\n";
        }
        Logger.Log(result);
    }
}