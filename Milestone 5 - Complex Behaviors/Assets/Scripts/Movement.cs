using UnityEngine;

public class Movement : MonoBehaviour {

    public float moveSpeed = 10f, rotSpeed = 200f, currSpeed = 0f;

    void LateUpdate() {
        float translation = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        float rotation = Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;

        transform.Translate(0f, 0f, translation);
        currSpeed = translation;

        transform.Rotate(0f, rotation, 0f);
    }
}
