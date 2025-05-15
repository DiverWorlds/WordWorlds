using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

public class AddRemoveTriggersCollider : MonoBehaviour
{
    //TODO: Roomsクリックで全ての部屋に当たり判定つけられるようにする
    private const string EventTriggers = "EventTriggers";

    [MenuItem("GameObject/Triggers Collider/Add")]
    private static void AddComponents()
    {
        GameObject activeGameObject = Selection.activeGameObject;
        if (activeGameObject == null) return;

        foreach (Transform child in activeGameObject.transform)
        {
            if (child.GetComponent<Collider>() == null)
            {
                child.AddComponent<MeshCollider>();
            }
            else
            {
                Logger.Log(child.gameObject.name + " は既に Collider を持っています。");
            }

        }
        Logger.Log("Adding Collider To All EventTriggers Finished.");
    }

    [MenuItem("GameObject/Triggers Collider/Remove")]
    private static void RemoveComponents()
    {
        GameObject activeGameObject = Selection.activeGameObject;
        if (activeGameObject == null) return;

        foreach (Transform child in activeGameObject.transform)
        {
            Collider coll = child.GetComponent<Collider>();
            if (coll != null)
            {
                DestroyImmediate(coll);
            }
            else
            {
                Logger.Log(child.gameObject.name + " は Collider を持っていません。");
            }

        }
        Logger.Log("Removing Collider To All EventTriggers Finished.");
    }

    [MenuItem("GameObject/Triggers Collider/Add", true)]
    [MenuItem("GameObject/Triggers Collider/Remove", true)]
    private static bool AddRemoveComponentsValidation()
    {
        return Selection.activeGameObject.CompareTag(EventTriggers);
    }

}