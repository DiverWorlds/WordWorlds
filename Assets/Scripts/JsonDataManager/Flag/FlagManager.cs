using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class FlagManager : DontDestroySingleton<FlagManager>
{
    [SerializeField] private string initialFilePath = "InitialFlags";
    [SerializeField] private string saveFilePath = "SavedFlags";
    private Dictionary<string, bool> flags = new();

    void Start()
    {
        LoadInitialFlags();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.F))
        {
            LogAllFlags();
        }
    }

    public bool LoadInitialFlags()
    {
        return Load(initialFilePath);
    }
    public bool LoadSavedFlags()
    {
        return Load(saveFilePath);
    }
    private bool Load(string filePath)
    {
        try
        {
            string flagJsonText = Resources.Load<TextAsset>(filePath).text;
            flags = JsonUtility.FromJson<FlagData>(flagJsonText).ToDictionary();
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
        return flags[key];
    }

    public void Change(string key, bool value)
    {
        flags[key] = value;
    }

    public bool Save()
    {
        try
        {
            string flagJsonText = JsonUtility.ToJson(new FlagData(flags), true);
            File.WriteAllText($"Assets/Resources/{saveFilePath}.json", flagJsonText);
            return true;
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return false;
        }
    }

    public void LogAllFlags()
    {
        string result = $"{name} Log\n";
        foreach (var flag in flags)
        {
            result += $"{flag.Key}: {flag.Value}\n";
        }
        Logger.Log(result);
    }
}