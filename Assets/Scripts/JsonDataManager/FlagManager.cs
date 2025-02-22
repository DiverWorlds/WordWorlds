using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class FlagManager : JsonDataManager
{
    [SerializeField] private string InitialFlagsPath = "InitialFlags";
    [SerializeField] private string SavedFlagsPath = "SavedFlags";
    private Dictionary<string, bool> flags = new();

    void Start()
    {
        loadFilePath = InitialFlagsPath;
        saveFilePath = SavedFlagsPath;
        Load();
    }

    public override bool Load()
    {
        // try
        // {
            Logger.Log("Load()");
            string flagJsonText = Resources.Load<TextAsset>(loadFilePath).text;
            Logger.Log("flagJsonText", flagJsonText);
            flags = JsonUtility.FromJson<FlagData>(flagJsonText).ToDictionary();
            Logger.LogElements("flags", flags.Select(f => $"{f.Key}: {f.Value}"));
            return true;
        // }
        // catch (Exception e)
        // {
        //     Debug.Log(e);
        //     return false;
        // }
    }

    public bool Get(string key)
    {
        return flags[key];
    }

    public override void Change(string key, bool value)
    {
        flags[key] = value;
    }

    public override bool Save()
    {
        // try
        // {
            Logger.Log("Save()");
            string flagJsonText = JsonUtility.ToJson(new FlagData(flags), true);
            Logger.Log("flagJsonText", flagJsonText);
            File.WriteAllText($"Assets/Resources/{saveFilePath}.json", flagJsonText);
            return true;
        // }
        // catch (Exception e)
        // {
        //     Debug.Log(e);
        //     return false;
        // }
    }

    public override void Clear()
    {
        flags = new();
        // ここでセーブまでした方がいい？
    }

    public override void Log()
    {
        string result = $"{name} Log\n";
        foreach (var flag in flags)
        {
            result += $"{flag.Key}: {flag.Value}\n";
        }
        Logger.Log(result);
    }
}