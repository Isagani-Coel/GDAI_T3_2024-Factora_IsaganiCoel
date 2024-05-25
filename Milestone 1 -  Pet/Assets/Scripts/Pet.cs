using UnityEngine;

public class PetMovement : MonoBehaviour {

    public Transform goal;
    float movSpeed = 5f, rotSpeed = 4f;
    
    void LateUpdate() { 
        Vector3 lookAtGoal = new Vector3(goal.position.x, transform.position.y, goal.position.z);
        transform.LookAt(lookAtGoal);

        // smooth rotation
        Vector3 direction = lookAtGoal - transform.position;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, 
                                                   Quaternion.LookRotation(direction), 
                                                   Time.deltaTime * rotSpeed);

        // travelling to the goal (ignores Y-axis) 
        if (Vector3.Distance(lookAtGoal, transform.position) > 1) {
            Vector3.Lerp(transform.position, goal.position, movSpeed * Time.deltaTime); // smooth acceleration & decelration
            transform.Translate(0, 0, movSpeed * Time.deltaTime);
        }
    }
}