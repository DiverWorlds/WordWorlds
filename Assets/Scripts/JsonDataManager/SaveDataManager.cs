using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class SaveDataManager : DontDestroySingleton<FlagManager>
{
    //セーブ内容：インベントリ内容、現在のシーン
    [SerializeField] private string saveFilePath = "SaveData";
    public bool Load()
    {
        try
        {
            
            return true;
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return false;
        }
    }
}
