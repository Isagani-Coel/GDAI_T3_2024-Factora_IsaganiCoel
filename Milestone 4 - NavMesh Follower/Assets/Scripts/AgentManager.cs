using UnityEngine;
public class AgentManager : MonoBehaviour {

    [SerializeField] GameObject target; // the player
    [SerializeField] GameObject[] agents;
    
    void Start() {
        agents = GameObject.FindGameObjectsWithTag("AI");
    }

    void Update() {
        foreach (GameObject ai in agents)
            ai.GetComponent<AIController>().agent.SetDestination(target.transform.position);
    }
}
