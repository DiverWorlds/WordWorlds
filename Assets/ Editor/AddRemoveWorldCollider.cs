using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

public class AddRemoveWorldCollider : MonoBehaviour
{
    private const string SearchWorld = "SearchWorld";
    private const string SearchRoom = "SearchRoom";
    private const string RoomRoot = "RoomRoot";
    private const float ColliderThickness = 0.05f;

    [MenuItem("GameObject/World Collider/Add")]
    private static void AddComponents()
    {
        GameObject activeGameObject = Selection.activeGameObject;
        if (activeGameObject == null) return;

        foreach (Transform searchRoom in activeGameObject.transform)
        {
            if (searchRoom.CompareTag(SearchRoom))
            {
                foreach (Transform roomRoot in searchRoom)
                {
                    if (roomRoot.CompareTag(RoomRoot))
                    {
                        ReqAddComponents(roomRoot);
                    }
                }
            }
        }
        Logger.Log("Adding Collider To All World Structure Finished.");
    }
    private static void ReqAddComponents(Transform structureParent)
    {
        foreach (Transform roomStructurePart in structureParent)
        {
            if (roomStructurePart.childCount == 0)
            {
                if (roomStructurePart.GetComponent<MeshCollider>() == null)
                {
                    MeshCollider coll = roomStructurePart.AddComponent<MeshCollider>();
                    // coll.size = new(coll.size.x, coll.size.y, ColliderThickness);
                }
                else
                {
                    Logger.Log(roomStructurePart.gameObject.name + " は既に MeshCollider を持っています。");
                }
            }
            else
            {
                ReqAddComponents(roomStructurePart);
            }
        }
    }
    [MenuItem("GameObject/World Collider/Remove")]
    private static void RemoveComponents()
    {
        GameObject activeGameObject = Selection.activeGameObject;
        if (activeGameObject == null) return;

        foreach (Transform searchRoom in activeGameObject.transform)
        {
            if (searchRoom.CompareTag(SearchRoom))
            {
                foreach (Transform roomRoot in searchRoom)
                {
                    if (roomRoot.CompareTag(RoomRoot))
                    {
                        ReqRemoveComponents(roomRoot);
                    }
                }
            }
        }
        Logger.Log("Removing Collider To All World Structure Finished.");
    }
    private static void ReqRemoveComponents(Transform structureParent)
    {
        foreach (Transform roomStructurePart in structureParent)
        {
            if (roomStructurePart.childCount == 0)
            {
                var coll = roomStructurePart.GetComponent<MeshCollider>();
                if (coll != null)
                {
                    DestroyImmediate(coll);
                }
                else
                {
                    Logger.Log(roomStructurePart.gameObject.name + " は MeshCollider を持っていません。");
                }
            }
            else
            {
                ReqRemoveComponents(roomStructurePart);
            }
        }
    }

    [MenuItem("GameObject/World Collider/Add", true)]
    [MenuItem("GameObject/World Collider/Remove", true)]
    private static bool AddRemoveComponentsValidation()
    {
        return Selection.activeGameObject.CompareTag(SearchWorld);
    }

}