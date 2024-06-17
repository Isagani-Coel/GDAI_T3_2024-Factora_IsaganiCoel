using UnityEngine;

public class FollowPath : MonoBehaviour {

    // OBJECT VARIABLES
    Transform goal;
    public GameObject wpManager;
    public float movSpeed, accuracy, rotSpeed;

    // GRAPH VARIABLES
    Graph graph;
    GameObject[] wps;
    GameObject currNode;
    int currWPindex = 0;

    void Start() {
        wps = wpManager.GetComponent<WaypointManager>().waypoints;
        graph = wpManager.GetComponent<WaypointManager>().graph;
        currNode = wps[0];
    }

    void LateUpdate() {
        if (graph.getPathLength() == 0 || currWPindex == graph.getPathLength()) return;

        // moves on to the next node if it's close enough
        if (Vector3.Distance(graph.getPathPoint(currWPindex).transform.position, transform.position) < accuracy)
            currWPindex++;

        // if we're not at the end of the path
        if (currWPindex < graph.getPathLength()) {
            goal = graph.getPathPoint(currWPindex).transform;

            Vector3 lookAtGoal = new Vector3(goal.position.x, transform.position.y, goal.transform.position.z);
            Vector3 direction = lookAtGoal - transform.position;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed);
            transform.Translate(0, 0, movSpeed * Time.deltaTime);
        }
    }

    public void GoToMountains() {
        graph.AStar(currNode, wps[2]);
        currWPindex = 0;
    }

    public void GoToBarracks() {
        graph.AStar(currNode, wps[12]);
        currWPindex = 0;
    }

    public void GoToCommandCenter() {
        graph.AStar(currNode, wps[6]);
        currWPindex = 0;
    }

    public void GoToRefinery() {
        graph.AStar(currNode, wps[9]);
        currWPindex = 0;    
    }

    public void GoToTankers() {
        graph.AStar(currNode, wps[11]);
        currWPindex = 0;
    }

    public void GoToRadar() { 
        graph.AStar(currNode, wps[5]);
        currWPindex = 0;
    }

    public void GoToCommandPost() {
        graph.AStar(currNode, wps[4]);
        currWPindex = 0;
    }

    public void GoToCenter() {
        graph.AStar(currNode, wps[13]);
        currWPindex = 0;
    }
}
