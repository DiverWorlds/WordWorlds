using UnityEngine;

public abstract class JsonDataManager : MonoBehaviour
{
    private string filePath;

    public abstract bool Save();
    public abstract bool Load();
}