using System.Diagnostics;
using System.Collections;
using Unity.VisualScripting;
public static class Logger
{
    [Conditional("UNITY_EDITOR")]
    public static void Log(this object o)
    {
        UnityEngine.Debug.Log(o);
    }
    
    [Conditional("UNITY_EDITOR")]
    public static void Log(string tabMessage)
    {
        UnityEngine.Debug.Log(tabMessage);
    }
    public static void Log(string label, object o)
    {
        UnityEngine.Debug.Log($"{label}: {o}");
    }
    
    [Conditional("UNITY_EDITOR")]
    public static void LogElements(string collectionName, IEnumerable collection)
    {
        string collectionValues = collection.ToCommaSeparatedString();
        UnityEngine.Debug.Log($"{collectionName}: {collectionValues}");
    }  

    [Conditional("UNITY_EDITOR")]
    public static void LogElements(IEnumerable collection)
    {
        string collectionValues = collection.ToCommaSeparatedString();
        UnityEngine.Debug.Log($"{collectionValues}");
    }  
}