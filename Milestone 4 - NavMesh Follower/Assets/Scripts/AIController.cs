using UnityEngine;

public class AIController : MonoBehaviour {

    public UnityEngine.AI.NavMeshAgent agent;

    void Start() {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }
}
