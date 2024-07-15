using UnityEngine;

/* -NOTES- (07.15.24)
    - Make this script have a camera as well
    - All agents will follow this object once established
*/

public class Movement : MonoBehaviour {

    public float speed = 10, rotSpeed = 200, currSpeed = 0;

    void LateUpdate() {
        float translation = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float rotation = Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;

        transform.Translate(0, 0, translation);
        currSpeed = translation;

        transform.Rotate(0, rotation, 0);
    }
}
