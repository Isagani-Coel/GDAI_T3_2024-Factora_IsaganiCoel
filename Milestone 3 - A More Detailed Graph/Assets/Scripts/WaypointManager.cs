using UnityEngine;

public class WaypointManager : MonoBehaviour {

    public GameObject[] waypoints;
    public Graph graph = new Graph();
    public Link[] links;

    void Start() {
        if (waypoints.Length > 0) {
            foreach(GameObject wp in waypoints)
                graph.AddNode(wp);

            foreach (Link l in links) {
                graph.AddEdge(l.node1, l.node2);

                if (l.dir == Link.direction.BI)
                    graph.AddEdge(l.node1, l.node2);
            }
        }
    }

    void Update() {
        graph.debugDraw();
    }
}

[System.Serializable]
public struct Link {
    public enum direction { UNI, BI };
    public GameObject node1, node2;
    public direction dir;
}