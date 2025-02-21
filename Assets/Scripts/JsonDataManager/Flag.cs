using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Flag
{
    [SerializeField] private string key;
    [SerializeField] private bool value;
    public string Key { get; set; }
    public bool Value { get; set; }

    public Flag(string key, bool value)
    {
        this.key = key;
        this.value = value;
    }

    public string ToString()
    {
        return $"{key}: {value}";
    }
}