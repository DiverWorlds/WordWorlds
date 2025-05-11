using UnityEngine;
using UnityEngine.InputSystem;

public class ZoomedView : MonoBehaviour
{
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