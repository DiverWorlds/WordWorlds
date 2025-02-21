using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[Serializable]
public class FlagData : ISerializationCallbackReceiver
{
    // [SerializeField]
    // List<string> keys;
    // [SerializeField]
    // List<bool> values;

    [SerializeField]
    List<Tuple<string, bool>> flags;
    public List<Tuple<string, bool>> ToTupleList() { 
        Logger.LogElements("ToDictionary(); flags", flags);
        return flags; 
    }

    public FlagData(List<Tuple<string, bool>> flags)
    {
        this.flags = flags;
        Logger.LogElements("flags", flags);
    }

    public void OnBeforeSerialize()
    {
        // keys = new List<string>(flags.Keys);
        // values = new List<bool>(flags.Values);
        // Logger.Log("OnBeforeSerialize()");
        // Logger.LogElements("keys", keys);
        // Logger.LogElements("values", values);
    }

    public void OnAfterDeserialize()
    {
        // Logger.Log("OnAfterDeserialize()");
        // var count = Math.Min(keys.Count, values.Count);
        // flags = new Dictionary<string, bool>(count);
        // for (var i = 0; i < count; ++i)
        // {
        //     flags.Add(keys[i], values[i]);
        //     Logger.Log("Added key", keys[i]);
        //     Logger.Log("Added value", values[i]);
        // }
        // Logger.LogElements("flags", flags);
    }
}