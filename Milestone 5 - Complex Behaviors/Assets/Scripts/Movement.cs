using UnityEngine;

public class Movement : MonoBehaviour { 
    public float speed = 10;
    public float rotationSpeed = 200;
    public float currentSpeed = 0;

    void LateUpdate() {
        float translation = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;

        transform.Translate(0, 0, translation);
        currentSpeed = translation;

        transform.Rotate(0, rotation, 0);
    }
}
