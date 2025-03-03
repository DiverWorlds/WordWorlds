using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Flag
{
    [SerializeField] private string key;
    [SerializeField] private bool value;
    public string Key
    {
        get { return key; }
        set { key = value; }
    }
    public bool Value
    {
        get { return value; }
        set { this.value = value; }
    }

    public Flag(string key, bool value)
    {
        this.key = key;
        this.value = value;
    }

    public override string ToString()
    {
        return $"{key}: {value}";
    }
}