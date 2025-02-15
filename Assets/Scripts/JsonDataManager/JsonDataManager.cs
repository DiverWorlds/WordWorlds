using System.Collections.Generic;
using UnityEngine;

public abstract class JsonDataManager : DontDestroySingleton<JsonDataManager>
{
    protected string filePath;
    public abstract bool Load();
    public abstract void Change(string name, bool value);
    public abstract bool Save();

    #if UNITY_EDITOR
    public void Log(Dictionary<string, bool> datas)
    {
        string result = $"{name} Log\n";
        foreach (var record in datas)
        {
            result += $"{record.Key}: {record.Value}\n";
        }
        Logger.Log(result);
    }
    #endif
}