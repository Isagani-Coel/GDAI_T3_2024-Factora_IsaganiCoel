using UnityEngine;

public class MonsterSpawn : MonoBehaviour {

    public GameObject[] obstacles;
    GameObject[] agents;

    void Start() {
        agents = GameObject.FindGameObjectsWithTag("Agent");
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) { // SPAWN MONSTER
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray.origin, ray.direction, out hit)) {
                Instantiate(obstacles[0], hit.point, obstacles[0].transform.rotation);

                foreach (GameObject a in agents)
                    a.GetComponent<AIControl>().DetectNewObstacle(hit.point);
            }
        }
        if (Input.GetMouseButtonDown(1)) { // SPAWN GOLD BAR
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray.origin, ray.direction, out hit)) {
                GameObject newGoal = Instantiate(obstacles[1], hit.point, obstacles[1].transform.rotation) as GameObject;

                foreach (GameObject a in agents) {
                    a.GetComponent<AIControl>().DetectNewGoal(newGoal);
                    a.GetComponent<AIControl>().ResetAgent();
                }
            }
        }
    }
}
