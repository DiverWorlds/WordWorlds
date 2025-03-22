using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 400.0f;
    [SerializeField] private float clampAngle = 80.0f;

    private float rotY = 0.0f;
    private float rotX = 0.0f;

    void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
    }

    void Update()
    {
        Rotate();
        ResetAngle();
    }

    private void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");

        rotY += mouseX * mouseSensitivity * Time.deltaTime;
        rotX += mouseY * mouseSensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        transform.rotation = localRotation;
    }

    private void ResetAngle()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Quaternion resetRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            transform.rotation = resetRotation;
            rotX = 0;
            rotY = 0;
        }
    }
}