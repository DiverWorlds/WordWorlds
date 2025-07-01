using UnityEngine;

/// <summary>
/// Awake()はItemWordInventoryより前に実行する．
/// </summary>
[DefaultExecutionOrder(-100)]
public class GlobalDB : DontDestroySingleton<GlobalDB>
{
    [SerializeField] private ItemWordDatabase itemWordDatabase;
    [SerializeField] private SearchWorldDatabase searchWorldDatabase;
    public ItemWordDatabase ItemWordDB => itemWordDatabase;
    public SearchWorldDatabase SearchWorldDB => searchWorldDatabase;

    public override void Awake()
    {
        base.Awake();
        itemWordDatabase.Initialize();
        searchWorldDatabase.Initialize();
    }

    public override void OnApplicationQuit()
    {
        itemWordDatabase = null;
        searchWorldDatabase = null;
        base.OnApplicationQuit();
    }
}