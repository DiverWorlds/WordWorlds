using System;
using UnityEngine;
[Serializable]
public class ExampleEffect : MonoBehaviour, IEffectable
{
    //この関数でズーム処理やワード入手処理を行う
    public void PlayEffect()
    {
        Logger.Log("くまちゃんだ。");
    }
}
