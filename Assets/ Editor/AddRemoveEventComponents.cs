using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.Events;

public class AddRemoveEventComponents : MonoBehaviour
{
    private const string Rooms = "Rooms";
    private const string EventObjects = "EventObjects";

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
        Logger.Log("全てのEventObjectsにComponentをアタッチする作業が終了しました。");
    }

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
        Logger.Log("全てのEventObjectsの、Componentを取り除く作業が終了しました。");
    }

    [MenuItem("GameObject/Event Components (Whole)/Add", true, 10)]
    [MenuItem("GameObject/Event Components (Whole)/Remove", true, 11)]
    private static bool AddRemoveComponentsWholeValidation()
    {
        return Selection.activeGameObject.CompareTag(Rooms);
    }

    [MenuItem("GameObject/Event Components/Add", false, 20)]
    private static void AddComponentsToSelection()
    {
        AddComponents(Selection.activeGameObject);
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

    private static void AddComponents(GameObject eventObject)
    {
        EnsureComponent<MeshCollider>(eventObject.gameObject);
        EventTrigger eventTrigger = EnsureComponent<EventTrigger>(eventObject.gameObject);
        EventTargetCondition eventTargetCondition = EnsureComponent<EventTargetCondition>(eventObject.gameObject);

        eventTrigger.triggers = new List<EventTrigger.Entry>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        EventTrigger.TriggerEvent  call = new EventTrigger.TriggerEvent();
        UnityEditor.Events.UnityEventTools.AddPersistentListener(call);
        entry.callback = call;
        eventTrigger.triggers.Add(entry);
        Logger.LogElements("triggers", eventTrigger.triggers);
        Logger.Log("eventID", eventTrigger.triggers[0].eventID);
    }
    private static void RemoveComponents(GameObject eventObject)
    {
        RemoveComponents<Collider>(eventObject);
        RemoveComponents<EventTrigger>(eventObject);
        RemoveComponents<EventTargetCondition>(eventObject);
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
                Logger.Log($"{gameObject.name} は {collider.GetType().Name} が既にアタッチされています。");
                return null;
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
                return null;
            }

        }
    }
    private static void RemoveComponents<T>(GameObject gameObject) where T : Component
    {
        T[] components = gameObject.GetComponents<T>();
        if (components.Length > 0)
        {
            foreach (T component in components)
            {
                DestroyImmediate(component);
            }
            Logger.Log($"{gameObject.name} から {typeof(T).Name} を {components.Length} 個取り除きました。");
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
            Debug.Log($"{gameObject.name} から {typeof(TInterface).Name} を実装している Script を {removedCount} 個取り除きました。");
        }
        else
        {
            Debug.Log($"{gameObject.name} には {typeof(TInterface).Name} を実装している Script はアタッチされていませんでした。");
        }
    }
}