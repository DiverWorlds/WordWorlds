using UnityEngine;

public abstract class JsonDataManager : MonoBehaviour
{
    protected string filePath;

    public abstract bool Save();
    public abstract bool Load();
}