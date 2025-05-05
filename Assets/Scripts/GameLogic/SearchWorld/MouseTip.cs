using UnityEngine;

public class MouseTip : MonoBehaviour
{
    private Camera mainCamera;
    private Vector2 leftDown;
    private Vector2 rightUp;

    void Start()
    {
        mainCamera = Camera.main;
        leftDown = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, -mainCamera.transform.position.z));
        rightUp = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, -mainCamera.transform.position.z));
    }

    void Update()
    {
        Vector3 currentMousePosition = new(Input.mousePosition.x, Input.mousePosition.y, -mainCamera.transform.position.z);
        Vector3 cursorTipPosition = mainCamera.ScreenToWorldPoint(currentMousePosition);
        transform.position = ClampCursorTipPosition(cursorTipPosition);
    }

    private Vector3 ClampCursorTipPosition(Vector3 newCursorTipPosition)
    {
        float xPosition = Mathf.Clamp(newCursorTipPosition.x, leftDown.x, rightUp.x);
        float yPosition = Mathf.Clamp(newCursorTipPosition.y, leftDown.y, rightUp.y);
        return new Vector3(xPosition, yPosition, 0);
    }
}