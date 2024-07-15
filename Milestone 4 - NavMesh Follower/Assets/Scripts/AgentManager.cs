using UnityEngine;

public class AgentManager : MonoBehaviour {

    GameObject[] agents;
    
    void Start() {
        agents = GameObject.FindGameObjectsWithTag("AI");
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000))
                foreach (GameObject ai in agents)
                    ai.GetComponent<AIController>().agent.SetDestination(hit.point);
        }
    }
}
