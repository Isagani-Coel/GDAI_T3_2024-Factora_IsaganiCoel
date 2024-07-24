using UnityEngine;

public class Patrol : NPCBaseFSM {

    GameObject[] waypoints;
    int currWaypoint;

    void Awake() {
        waypoints = GameObject.FindGameObjectsWithTag("waypoint");
    }

    // Called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        currWaypoint = 0;
    }

    // Called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if (waypoints.Length == 0) return;

        // traverses the entire graph
        if (Vector3.Distance(waypoints[currWaypoint].transform.position, NPC.transform.position) < accuracy) {
            currWaypoint++;

            // goes back to the start of the graph
            if (currWaypoint >= waypoints.Length)
                currWaypoint = 0;
        }

        // rotate
        var direction = waypoints[currWaypoint].transform.position - NPC.transform.position;
        NPC.transform.rotation = Quaternion.Slerp(NPC.transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);

        // move
        NPC.transform.Translate(0f, 0f, Time.deltaTime * movSpeed);
    }

    // Called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateExit(animator, stateInfo, layerIndex);
    }
}
