using UnityEngine;

public class CarMovement : MonoBehaviour { 

    public Transform goal;

    [Header("Speed")]
    public float currSpeed;
    public float minSpeed, maxSpeed, rotSpeed;

    [Header("Acceleration")]
    public float brakeAngle;
    public float acceleration, deceleration;

    void LateUpdate() {
        Vector3 lookAtGoal = new Vector3(goal.transform.position.x, transform.position.y, goal.transform.position.z);
        Vector3 direction = lookAtGoal - transform.position;

        // acceleration & deceleration
        if (Vector3.Angle(goal.forward, transform.forward) > brakeAngle && currSpeed > 2f)
            currSpeed = Mathf.Clamp(currSpeed - (deceleration * Time.deltaTime), minSpeed, maxSpeed);
        else currSpeed = Mathf.Clamp(currSpeed + (acceleration * Time.deltaTime), minSpeed, maxSpeed);

        // rotation & movement
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed);
        transform.Translate(0, 0, currSpeed);
    }
}
