using UnityEngine;
using UnityEditor;
using UnityEngine.EventSystems;
using System.Linq;

public class AddRemoveEventComponents : MonoBehaviour
{
    private const string Rooms = "Rooms";
    private const string EventObjects = "EventObjects";

    // Whole----------------------------------------------------------------------------------------
    // RoomsのMenuItemで，全てのEventObjectにEventTrigger, EventTargetCondition, MeshColliderを追加
    [MenuItem("GameObject/Event Components (Whole)/Add", false, 10)]
    private static void AddComponentsToWhole()
    {
        GameObject activeGameObject = Selection.activeGameObject;
        if (activeGameObject == null) return;

        foreach (Transform searchRoom in activeGameObject.transform)
        {
            foreach (Transform child in searchRoom)
            {
                if (child.CompareTag(EventObjects))
                {
                    foreach (Transform eventObject in child)
                    {
                        AddComponents(eventObject.gameObject);
                    }
                }
            }
        }
        Logger.Log("全てのEventObjectsに Component をアタッチしました。");
        Logger.Log($"<b><color=green>[アナウンス]</color></b>  各 EventObject に適切な EventEffect をアタッチしてください。");
        Logger.Log($"<b><color=green>[アナウンス]</color></b>  各 ViewPoint の ActiveEvents に適切な EventObject をアタッチしてください。");
    }

    // RoomsのMenuItemで，全てのEventObjectのEventTrigger, EventTargetCondition, IEventEffect，MeshColliderを取り除く
    [MenuItem("GameObject/Event Components (Whole)/Remove", false, 11)]
    private static void RemoveComponentsFromWhole()
    {
        GameObject activeGameObject = Selection.activeGameObject;
        if (activeGameObject == null) return;

        foreach (Transform searchRoom in activeGameObject.transform)
        {
            foreach (Transform child in searchRoom)
            {
                if (child.CompareTag(EventObjects))
                {
                    foreach (Transform eventObject in child)
                    {
                        RemoveComponents(eventObject.gameObject);
                    }
                }
            }
        }
        Logger.Log("全てのEventObjectsの Component をデタッチしました。");
    }

    [MenuItem("GameObject/Event Components (Whole)/Add", true, 10)]
    [MenuItem("GameObject/Event Components (Whole)/Remove", true, 11)]
    private static bool AddRemoveComponentsWholeValidation()
    {
        return Selection.activeGameObject.CompareTag(Rooms);
    }
    // ---------------------------------------------------------------------------------------------
    // One------------------------------------------------------------------------------------------
    [MenuItem("GameObject/Event Components/Add", false, 20)]
    private static void AddComponentsToSelection()
    {
        var obj = Selection.activeGameObject;
        AddComponents(obj);
        Logger.Log($"<b><color=green>[アナウンス]</color></b>  {obj.name} に適切な EventEffect をアタッチしてください。");
        Logger.Log($"<b><color=green>[アナウンス]</color></b>  適切な ViewPoint の ActiveEvents に {obj.name} をアタッチしてください。");
    }

    [MenuItem("GameObject/Event Components/Remove", false, 21)]
    private static void RemoveComponentsFromSelection()
    {
        RemoveComponents(Selection.activeGameObject);
    }

    [MenuItem("GameObject/Event Components/Add", true, 20)]
    [MenuItem("GameObject/Event Components/Remove", true, 21)]
    private static bool AddRemoveComponentsValidation()
    {
        return Selection.activeGameObject.transform.parent.CompareTag(EventObjects);
    }
    // ---------------------------------------------------------------------------------------------
    // 内部処理--------------------------------------------------------------------------------------
    private static void AddComponents(GameObject eventObject)
    {
        MeshCollider meshCollider = EnsureComponent<MeshCollider>(eventObject);
        EventTrigger eventTrigger = EnsureComponent<EventTrigger>(eventObject);
        EventTargetCondition eventTargetCondition = EnsureComponent<EventTargetCondition>(eventObject);

        if (eventTrigger != null && eventTargetCondition != null)
        {
            eventTrigger.triggers.Clear();
            EventTrigger.Entry entry = new()
            {
                eventID = EventTriggerType.PointerClick
            };
            EventTrigger.TriggerEvent call = new();
            UnityEditor.Events.UnityEventTools.AddPersistentListener(call, eventTargetCondition.OnEventTriggered);
            entry.callback = call;
            eventTrigger.triggers.Add(entry);
        }
    }
    private static void RemoveComponents(GameObject eventObject)
    {
        RemoveComponentsIfExists<Collider>(eventObject);
        RemoveComponentsIfExists<EventTrigger>(eventObject);
        RemoveComponentsIfExists<EventTargetCondition>(eventObject);
        RemoveImplementingComponents<IEventEffect>(eventObject);
    }

    private static T EnsureComponent<T>(GameObject gameObject) where T : Component
    {
        T component = gameObject.GetComponent<T>();
        if (typeof(Collider).IsAssignableFrom(typeof(T)))
        {
            Collider collider = gameObject.GetComponent<Collider>();
            if (collider == null)
            {
                Logger.Log($"{gameObject.name} に {typeof(T).Name} をアタッチしました。");
                return gameObject.AddComponent<T>();
            }
            else
            {
                if (collider is T tCollider)
                {
                    Logger.Log($"{gameObject.name} は {collider.GetType().Name} が既にアタッチされています。");
                    return tCollider;
                }
                else
                {
                    // 1つ前のコミットでの実装だと，returnでエラーが発生していた．l.131のように条件式内の変数から型を取得するとエラーが起きなかった．
                    // このセクションに実行が移ることはないと思うが，メソッドの仕様上nullを返すようにする
                    //TODO: ここの実装を改善
                    return null;
                }
            }
        }
        else
        {
            if (component == null)
            {
                Logger.Log($"{gameObject.name} に {typeof(T).Name} をアタッチしました。");
                return gameObject.AddComponent<T>();
            }
            else
            {
                Logger.Log($"{gameObject.name} は {typeof(T).Name} が既にアタッチされています。");
                return component;
            }

        }
    }
    private static void RemoveComponentsIfExists<T>(GameObject gameObject) where T : Component
    {
        T[] components = gameObject.GetComponents<T>();
        if (components.Length > 0)
        {
            foreach (T component in components)
            {
                DestroyImmediate(component);
            }
            Logger.Log($"{gameObject.name} から {typeof(T).Name} を {components.Length} 個デタッチしました。");
        }
        else
        {
            Logger.Log($"{gameObject.name} には {typeof(T).Name} がありません。");
        }
    }

    private static void RemoveImplementingComponents<TInterface>(GameObject gameObject) where TInterface : class
    {
        MonoBehaviour[] components = gameObject.GetComponents<MonoBehaviour>();
        int removedCount = 0;

        foreach (MonoBehaviour component in components)
        {
            if (component != null && component.GetType().GetInterfaces().Any(i => i == typeof(TInterface)))
            {
                DestroyImmediate(component);
                removedCount++;
            }
        }

        if (removedCount > 0)
        {
            Debug.Log($"{gameObject.name} から {typeof(TInterface).Name} を実装している Script を {removedCount} 個デタッチしました。");
        }
        else
        {
            Debug.Log($"{gameObject.name} には {typeof(TInterface).Name} を実装している Script はアタッチされていませんでした。");
        }
    }
    // ---------------------------------------------------------------------------------------------
}