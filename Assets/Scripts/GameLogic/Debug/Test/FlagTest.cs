using System;
using System.IO;
using UnityEngine;

public class FlagTester : MonoBehaviour
{
    public void LoadInitialFlags()
    {
        FlagManager.Instance.LoadInitialFlags();
        FlagManager.Instance.LogAllFlags();
    }
    public void LoadSavedFlags()
    {
        FlagManager.Instance.LoadSavedFlags();
        FlagManager.Instance.LogAllFlags();
    }

    public void ChangeAToTrue()
    {
        FlagManager.Instance.Change("DummyA", true);
        FlagManager.Instance.LogAllFlags();
    }

    public void ChangeBToFalse()
    {
        FlagManager.Instance.Change("DummyB", false);
        FlagManager.Instance.LogAllFlags();
    }
    public void GetABC()
    {
        Logger.Log("DummyA", FlagManager.Instance.Get("DummyA"));
        Logger.Log("DummyB", FlagManager.Instance.Get("DummyB"));
        Logger.Log("DummyC", FlagManager.Instance.Get("DummyC"));
    }

    public void Save()
    {
        FlagManager.Instance.Save();
        FlagManager.Instance.LogAllFlags();
    }
}