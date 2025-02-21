using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

[Serializable]
public class FlagData
{
    [SerializeField] List<Flag> flags = new();
    public HashSet<Flag> ToHashSet()
    { 
        Logger.LogElements("ToDictionary(); flags", flags.Select(f => f.ToString()));
        return flags.ToHashSet();
    }

    public FlagData(HashSet<Flag> flags)
    {
        this.flags = flags.ToList();
    }
}