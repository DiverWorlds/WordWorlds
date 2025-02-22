using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class FlagData
{
    [SerializeField] List<Flag> flagList = new();
    public Dictionary<string, bool> ToDictionary()
    {
        Logger.Log("ToDictionary()");
        Logger.LogElements("flagList", flagList);
        Dictionary<string, bool> flagDict = new();
        foreach(Flag flag in flagList)
        {
            flagDict.Add(flag.Key, flag.Value);
        }
        Logger.LogElements("flagDict", flagDict);
        return flagDict;
    }

    public FlagData(Dictionary<string, bool> flagDict)
    {
        Logger.Log("FlagData() constructor");
        Logger.LogElements("flagDict", flagDict);
        flagList = flagDict.Select(f => new Flag(f.Key, f.Value)).ToList();
        Logger.LogElements("flagList", flagList.Select(f => f.ToString()));
    }
}