using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

public class AddRemoveTriggersCollider : MonoBehaviour
{
    private const string Rooms = "Rooms";
    private const string EventTriggers = "EventTriggers";

    [MenuItem("GameObject/Triggers Collider/Add")]
    private static void AddComponents()
    {
        GameObject activeGameObject = Selection.activeGameObject;
        if (activeGameObject == null) return;

        foreach (Transform searchRoom in activeGameObject.transform)
        {
            foreach (Transform child in searchRoom)
            {
                if (child.CompareTag(EventTriggers))
                {
                    foreach (Transform eventTrigger in child)
                    {
                        if (eventTrigger.GetComponent<Collider>() == null)
                        {
                            eventTrigger.AddComponent<MeshCollider>();
                        }
                        else
                        {
                            Logger.Log(eventTrigger.gameObject.name + " は既に Collider を持っています。");
                        }
                    }
                }
            }
        }
        Logger.Log("Adding Collider To All EventTriggers Finished.");
    }

    [MenuItem("GameObject/Triggers Collider/Remove")]
    private static void RemoveComponents()
    {
        GameObject activeGameObject = Selection.activeGameObject;
        if (activeGameObject == null) return;

        foreach (Transform searchRoom in activeGameObject.transform)
        {
            foreach (Transform child in searchRoom)
            {
                if (child.CompareTag(EventTriggers))
                {
                    foreach (Transform eventTrigger in child)
                    {
                        Collider coll = eventTrigger.GetComponent<Collider>();
                        if (coll != null)
                        {
                            DestroyImmediate(coll);
                        }
                        else
                        {
                            Logger.Log(eventTrigger.gameObject.name + " は Collider を持っていません。");
                        }
                    }
                }
            }
        }
        Logger.Log("Removing Collider To All EventTriggers Finished.");
    }

    [MenuItem("GameObject/Triggers Collider/Add", true)]
    [MenuItem("GameObject/Triggers Collider/Remove", true)]
    private static bool AddRemoveComponentsValidation()
    {
        return Selection.activeGameObject.CompareTag(Rooms);
    }

}