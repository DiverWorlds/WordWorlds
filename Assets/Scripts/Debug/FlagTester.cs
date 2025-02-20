using System;
using System.IO;
using UnityEngine;

public class FlagTester : MonoBehaviour
{
    public void Load()
    {
        FlagManager.Instance.Load();
        FlagManager.Instance.Log();
    }

    public void ChangeAToTrue()
    {
        FlagManager.Instance.Change("DummyA", true);
        FlagManager.Instance.Log();
    }

    public void ChangeBToFalse()
    {
        FlagManager.Instance.Change("DummyB", false);
        FlagManager.Instance.Log();
    }

    public void Save()
    {
        FlagManager.Instance.Save();
        FlagManager.Instance.Log();
    }
}