using UnityEngine;

public class Player : MonoBehaviour {

    Vector2 turn;
    float speed = 10f;

    void LateUpdate() {
        // KEYBOARD MOVEMENT
        if (Input.GetKey(KeyCode.W)) transform.Translate(Vector3.forward.normalized * speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.A)) transform.Translate(Vector3.left.normalized * speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.S)) transform.Translate(Vector3.back.normalized * speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.D)) transform.Translate(Vector3.right.normalized * speed * Time.deltaTime);
        
        // MOUSE MOVEMENT
        turn.x = Input.GetAxis("Mouse X");
        turn.y = Input.GetAxis("Mouse Y");
        transform.position += new Vector3(turn.x, 0, turn.y).normalized;
    }
}
