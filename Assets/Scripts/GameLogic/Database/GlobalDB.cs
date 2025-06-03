using UnityEngine;

public class GlobalDB : DontDestroySingleton<GlobalDB>
{
    [SerializeField] private ItemWordDatabase itemWordDatabase;
    [SerializeField] private SearchWorldDatabase searchWorldDatabase;
    public ItemWordDatabase ItemWordDB => itemWordDatabase;
    public SearchWorldDatabase SearchWorldDB => searchWorldDatabase;
    /// <summary>
    /// AwakeControllerから呼び出してデータベースを初期化する。
    /// </summary>
    public void OnAwake()
    {
        itemWordDatabase.Initialize();
        searchWorldDatabase.Initialize();
    }
}