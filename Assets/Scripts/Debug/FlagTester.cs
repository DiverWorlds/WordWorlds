using System;
using System.IO;
using UnityEngine;

public class FlagTester : MonoBehaviour
{
    public void LoadInitialFlags()
    {
        FlagManager.Instance.LoadInitialFlags();
        FlagManager.Instance.LogAllObjects();
    }
    public void LoadSavedFlags()
    {
        FlagManager.Instance.LoadSavedFlags();
        FlagManager.Instance.LogAllObjects();
    }

    public void ChangeAToTrue()
    {
        FlagManager.Instance.Change("DummyA", true);
        FlagManager.Instance.LogAllObjects();
    }

    public void ChangeBToFalse()
    {
        FlagManager.Instance.Change("DummyB", false);
        FlagManager.Instance.LogAllObjects();
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
        FlagManager.Instance.LogAllObjects();
    }
}