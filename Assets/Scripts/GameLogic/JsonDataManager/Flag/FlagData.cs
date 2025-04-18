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
        Dictionary<string, bool> flagDict = new();
        foreach(Flag flag in flagList)
        {
            flagDict.Add(flag.Key, flag.Value);
        }
        return flagDict;
    }

    public FlagData(Dictionary<string, bool> flagDict)
    {
        flagList = flagDict.Select(f => new Flag(f.Key, f.Value)).ToList();
    }
}