using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField] GameObject target;
    [SerializeField] float offset; // y-axis

    void Update() {
        transform.position = new Vector3(target.transform.position.x,
                                         target.transform.position.y + offset,
                                         target.transform.position.z
        );
    }
}
