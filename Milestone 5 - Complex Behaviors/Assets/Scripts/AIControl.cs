using UnityEngine;
using UnityEngine.AI;

public enum AgentType { PURSUER, HIDER, EVADER };

public class AIControl : MonoBehaviour {

    [SerializeField] GameObject target;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] AgentType type;

    float range = 7f, distanceOffset = 5f;
    Movement playerMovement;
    Vector3 wanderTarget;

    void Start() {
        agent = GetComponent<NavMeshAgent>();
        playerMovement = target.GetComponent<Movement>();
    }

    void Update() {
        if (IsWithinRange(target)) {
            switch (type) {
                case AgentType.PURSUER: Pursue();   break;
                case AgentType.HIDER: Camouflage(); break;
                case AgentType.EVADER: Evade();     break;
            }
        }
        else Wander();
    }

    // DIFFERENT AGENT BEHAVIORS 
    void Seek(Vector3 location) {
        agent.SetDestination(location);
    }
    void Flee(Vector3 location) {
        Vector3 fleeDirection = location - transform.position;
        agent.SetDestination(transform.position - fleeDirection);
    }
    void Pursue() { Debug.LogWarning(name + " is pursuing the player");
        Vector3 targetDirection = target.transform.position - transform.position;
        float lookAhead = targetDirection.magnitude / (agent.speed + playerMovement.currSpeed);

        Seek(target.transform.position + target.transform.forward * lookAhead);
    }
    void Evade() { Debug.LogWarning(name + " is evading the player");
        Vector3 targetDirection = target.transform.position - transform.position;
        float lookAhead = targetDirection.magnitude / (agent.speed + playerMovement.currSpeed);

        Flee(target.transform.position + target.transform.forward * lookAhead);
    }
    void Wander() {
        float wanderRadius = 10f, wanderDistance = 5f, wanderJitter = 1.5f;

        // it randomly looks left/right
        wanderTarget += new Vector3(Random.Range(-2f, 2f) * wanderJitter, 0, Random.Range(-2f, 2f) * wanderJitter);
        wanderTarget.Normalize();
        wanderTarget *= wanderRadius;

        // Sets the local target to go here
        Vector3 targetLocal = wanderTarget + new Vector3(0, 0, wanderDistance);
        Vector3 targetWorld = gameObject.transform.InverseTransformVector(targetLocal); // converts to world space

        Debug.Log(name + " is wandering");
        Seek(targetWorld);
    }
    void Hide() {
        Vector3 chosenSpot = Vector3.zero;
        float distance = Mathf.Infinity;
        int hidingSpotsCount = World.Instance.GetHidingSpots().Length;

        // finds the nearest place to hide and goes there
        for (int i = 0; i < hidingSpotsCount; i++) {
            Vector3 hideDirection = World.Instance.GetHidingSpots()[i].transform.position - target.transform.position;
            Vector3 hidePosition = World.Instance.GetHidingSpots()[i].transform.position + hideDirection.normalized * distanceOffset; // distance offset
            float spotDistance = Vector3.Distance(transform.position, hidePosition);

            if (spotDistance < distance) {
                chosenSpot = hidePosition;
                distance = spotDistance;
            }
        }

        Seek(chosenSpot);
    }
    void CleverHide() {
        Vector3 chosenSpot = Vector3.zero, chosenDirection = Vector3.zero;
        GameObject chosenGameObject = World.Instance.GetHidingSpots()[0];
        float distance = Mathf.Infinity;
        int hidingSpotsCount = World.Instance.GetHidingSpots().Length;

        // finds the nearest place to hide and goes there
        for (int i = 0; i < hidingSpotsCount; i++) {
            Vector3 hideDirection = World.Instance.GetHidingSpots()[i].transform.position - target.transform.position;
            Vector3 hidePosition = World.Instance.GetHidingSpots()[i].transform.position + hideDirection.normalized * distanceOffset;
            float spotDistance = Vector3.Distance(transform.position, hidePosition);

            if (spotDistance < distance) {
                chosenSpot = hidePosition;
                chosenDirection = hideDirection;
                chosenGameObject = World.Instance.GetHidingSpots()[i];
                distance = spotDistance;
            }
        }

        Collider hideCol = chosenGameObject.GetComponent<Collider>();
        Ray back = new Ray(chosenSpot, -chosenDirection.normalized);
        RaycastHit info;
        float rayDistance = 100f; // make sure this is bigger than the distance offset

        hideCol.Raycast(back, out info, rayDistance);

        Seek(info.point + chosenDirection.normalized * distanceOffset);
    }
    void Camouflage() { Debug.LogWarning(name + " is cleverly hiding");
        if (CanSeeTarget())
            CleverHide();
    }

    bool CanSeeTarget() {
        RaycastHit raycastInfo;
        Vector3 rayTotarget = target.transform.position - transform.position;

        if (Physics.Raycast(transform.position, rayTotarget, out raycastInfo)) {
            Debug.LogWarning(name + " can see the player");
            return raycastInfo.transform.gameObject.tag == "Player";
        }
        return false;
    }
    bool IsWithinRange(GameObject player) {
        if (Vector3.Distance(transform.position, player.transform.position) < range)
            return true;

        return false;
    }

    
    /* -VISUAL RANGE REPRESENTATION- */
    void OnDrawGizmos() {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, range);
    } // */
}