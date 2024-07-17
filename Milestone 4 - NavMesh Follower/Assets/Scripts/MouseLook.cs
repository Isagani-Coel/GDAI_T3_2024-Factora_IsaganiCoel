using UnityEngine;

public class MouseLook : MonoBehaviour {

    [Header("Player Settings")]
    [SerializeField] Transform playerBody;

    [Header("Mouse Settings")]
    [SerializeField] float sensitivity = 100f;
    float xRotation = 0f;

    void Start(){
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate() {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY; 
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // rotation doesn't go too far

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
