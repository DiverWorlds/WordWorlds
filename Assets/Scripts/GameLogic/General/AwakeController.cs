using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Awakeで呼びたい処理に、スクリプトごとに順序をつけて実行したいときに使用するコンポーネントです。
/// UnityEventで実行順を柔軟に制御できます。
/// </summary>
public class AwakeController : MonoBehaviour
{
    [SerializeField] private UnityEvent onAwake;
    void Awake()
    {
        onAwake?.Invoke();
    }
}