using UnityEngine;
using UnityEditor;

public class AddRemoveClickDetection : MonoBehaviour
{
    //TODO: アタッチ忘れ検出機能作る
    // [MenuItem("GameObject/Click Detection/Add")]
    private static void AddComponents(MenuCommand command)
    {
        GameObject selectedObject = command.context as GameObject;

        if (selectedObject != null)
        {
            if (selectedObject.GetComponent<BoxCollider>() == null)
            {
                BoxCollider boxCollider = selectedObject.AddComponent<BoxCollider>();
                boxCollider.size = new(boxCollider.size.x, boxCollider.size.y, 0.05f);
            }
            else
            {
                Logger.Log(selectedObject.name + " は既に BoxCollider を持っています。");
            }

            if (selectedObject.GetComponent<CameraSwitchTrigger>() == null)
            {
                selectedObject.AddComponent<CameraSwitchTrigger>();
            }
            else
            {
                Logger.Log(selectedObject.name + " は既に CameraSwitchTrigger スクリプトを持っています。");
            }
        }
    }

    // [MenuItem("GameObject/Click Detection/Remove")]
    private static void RemoveComponents(MenuCommand command)
    {
        GameObject selectedObject = command.context as GameObject;

        if (selectedObject != null)
        {
            var boxCollider = selectedObject.GetComponent<BoxCollider>();
            if (boxCollider != null)
            {
                DestroyImmediate(boxCollider);
            }
            else
            {
                Logger.Log(selectedObject.name + " は BoxCollider を持っていません。");
            }

            var cameraSwitchTrigger = selectedObject.GetComponent<CameraSwitchTrigger>();
            if (cameraSwitchTrigger != null)
            {
                DestroyImmediate(cameraSwitchTrigger);
            }
            else
            {
                Logger.Log(selectedObject.name + " は CameraSwitchTrigger を持っていません。");
            }
        }
    }
}