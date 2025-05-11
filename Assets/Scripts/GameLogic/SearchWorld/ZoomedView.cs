using UnityEngine;
using UnityEngine.InputSystem;

public class ZoomedView : MonoBehaviour
{
    //TODO: 複数カメラの切り替えで見た目を変更するようにする
    //TODO: WorldView移動機能
    [SerializeField] private InputActionReference cancelAction;

    private InputAction action;

    void Awake()
    {
        action = cancelAction.action;
        if (action == null)
        {
            Debug.LogError("Cancel Actionが設定されていません！");
            enabled = false;
        }
    }

    void OnEnable()
    {
        action.Enable();
        action.performed += OnCancel;
    }

    void OnDisable()
    {
        action.performed -= OnCancel;
        action.Disable();
    }

    void OnCancel(InputAction.CallbackContext context)
    {
        gameObject.SetActive(false);
    }
}