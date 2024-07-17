using UnityEngine;

/* -NOTES- (07.16.24)
    - only 1/4 agents follow the player (the type of agent was the issue)
    - duplicate the Luigi prefab for the others instead (it didn't work as the agent type was the problem)
    - 
*/

public class AgentManager : MonoBehaviour {

    [SerializeField] GameObject target;
    [SerializeField] GameObject[] agents;
    
    void Start() {
        agents = GameObject.FindGameObjectsWithTag("AI");
    }

    void Update() {
        foreach (GameObject ai in agents)
            ai.GetComponent<AIController>().agent.SetDestination(target.transform.position);
    }
}
