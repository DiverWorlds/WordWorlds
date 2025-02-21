using System.Collections.Generic;
using UnityEngine;

public abstract class JsonDataManager : DontDestroySingleton<JsonDataManager>
{
    protected string loadFilePath;
    protected string saveFilePath;
    public abstract bool Load();
    public abstract void Change(string name, bool value);
    public abstract bool Save();
    public abstract void Clear();
    public abstract void Log();
}