using UnityEngine;

public class Turret : MonoBehaviour {

    [SerializeField] UnityStandardAssets.Utility.WaypointCircuit circuit;
    [SerializeField] GameObject shellPrefab;
    [SerializeField] Transform nozzle;

    public int currIndex = 0;
    float movSpeed, rotSpeed, distance, attackInterval;

    void Start() {
        movSpeed = 10f;
        rotSpeed = 5f;
        distance = 1f;

        attackInterval = Random.Range(1f, 1.5f);
        InvokeRepeating("Attack", 1f, attackInterval);
    }

    void LateUpdate() {
        if (circuit.Waypoints.Length == 0) return;

        GameObject currWaypoint = circuit.Waypoints[currIndex].gameObject;
        Vector3 lookAtGoal = new Vector3(currWaypoint.transform.position.x,
                                         transform.position.y,
                                         currWaypoint.transform.position.z);

        Vector3 direction = lookAtGoal - transform.position;
        if (direction.magnitude < distance) {
            currIndex++;

            if (currIndex >= circuit.Waypoints.Length)
                currIndex = 0;
        }

        transform.rotation = Quaternion.Slerp(transform.rotation,
                                              Quaternion.LookRotation(direction),
                                              rotSpeed * Time.deltaTime);

        transform.Translate(0f, 0f, movSpeed * Time.deltaTime);
    }

    void Attack() {
        Instantiate(shellPrefab, nozzle.position, nozzle.rotation);
        SoundManager.Instance.Play("player fire", 1);
    }
}
