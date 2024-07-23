using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

/* -NOTES (07.22.24)-
    
    NavMesh Obstacle
        - dynamic mesh changing
        - very expensive if used multiple times
        - if it doens't need to move, just rebake the NavMesh

    To Do:
        - Create another kind of NavMeshObstacle object 
        - Use right mouse click to spawn the new obsrtacle
        - the crowd must flock to that object once spawned
*/

public class AIControl : MonoBehaviour {

    public List<GameObject> goals = new List<GameObject>();
    NavMeshAgent agent;
    Animator animator;

    float speedMultiplier, detectionRadius = 5f, fleeRadius = 10f;

    void Start() {
        GameObject[] foundGoals = GameObject.FindGameObjectsWithTag("goal");
        
        // clears the initial list before filing it in
        goals.Clear();
        foreach (GameObject obj in foundGoals)
            goals.Add(obj);

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // RANDOM WALK ANIMATION START
        agent.SetDestination(goals[Random.Range(0, goals.Count)].transform.position);
        animator.SetTrigger("isWalking");
        animator.SetFloat("wOffset", Random.Range(0.1f, 1f)); // desyncing the animations start times to feel more natural

        ResetAgent();
    }

    void Update() {
        if (agent.remainingDistance < 1f)
            agent.SetDestination(goals[Random.Range(0, goals.Count)].transform.position);
    }

    public void ResetAgent() {
        speedMultiplier = Random.Range(0.1f, 1.5f);
        agent.speed = 2 * speedMultiplier;
        agent.angularSpeed = 120;
        animator.SetFloat("speedMultiplier", speedMultiplier);
        agent.ResetPath();
    }

    public void DetectNewObstacle(Vector3 location) {
        if (Vector3.Distance(location, transform.position) > detectionRadius) return;

        Vector3 fleeDirection = (transform.position - location).normalized;
        Vector3 newGoal = transform.position + fleeDirection * fleeRadius;
        NavMeshPath path = new NavMeshPath(); // makes a new path to aim for the corners

        agent.CalculatePath(newGoal, path);

        if (path.status != NavMeshPathStatus.PathInvalid) {
            agent.SetDestination(path.corners[path.corners.Length - 1]);
            animator.SetTrigger("isRunning");
            agent.speed = 10;
            agent.angularSpeed = 500; // turning direction (like slerp before)
        }
        /* -NO GUARD CLAUSE-
        if (Vector3.Distance(location, transform.position) < detectionRadius) {
            Vector3 fleeDirection = (transform.position - location).normalized;
            Vector3 newGoal = transform.position + fleeDirection * fleeRadius;
            NavMeshPath path = new NavMeshPath(); // makes a new path to aim for the corners

            agent.CalculatePath(newGoal, path);

            if (path.status != NavMeshPathStatus.PathInvalid) {
                agent.SetDestination(path.corners[path.corners.Length - 1]);
                animator.SetTrigger("isRunning");
                agent.speed = 10;
                agent.angularSpeed = 500; // turning direction (like slerp before)
            }
        } // */
    }

    public void DetectNewGoal(GameObject goal) {
        goals.Add(goal);
    }
}
