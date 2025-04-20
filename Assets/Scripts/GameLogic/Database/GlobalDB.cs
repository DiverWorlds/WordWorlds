using UnityEngine;

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
}