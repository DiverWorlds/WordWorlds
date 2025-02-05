using UnityEngine;

public class DatabaseInitializer : MonoBehaviour
{
    [SerializeField] private ItemWordDatabase itemWordDatabase;
    [SerializeField] private SearchWorldDatabase searchWorldDatabase;

    void Start()
    {
        itemWordDatabase.Initialize();
        searchWorldDatabase.Initialize();
    }
}