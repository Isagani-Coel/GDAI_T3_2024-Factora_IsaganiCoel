using UnityEngine;
using UnityEngine.AI;

/* -ENEMY-
    - has different behaviors based on HP & distance between the player
    - fires at a random time
    - has a FSM to determine it's behavior
    - the higher the tier, the better the score

   -TIERS- 
    - blue -> common
    - violer -> rare
    - red -> epic
    - yellow -> legend
*/

public enum Tier { DEFAULT, COMMON, RARE, EPIC, LEGEND }

public class Enemy : Leader {

    [Header("Enemy Settings")]
    [SerializeField] Tier tier;
    [SerializeField] int range, value;

    [Header("Components")]
    [SerializeField] GameObject target;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator animator;

    float startTime, repeatTime;

    public GameObject GetTarget() { return target; }
    public float GetMovSpeed() { return movSpeed; }
    public float GetRotSpeed() { return rotSpeed; }

    protected override void Start() {
        base.Start();

        name = "Enemy";
        startTime = Random.Range(0f, 1f);

        target = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        switch (tier) {
            case Tier.COMMON:
                value = 3;
                repeatTime = Random.Range(1.5f, 2f);
                break;
            case Tier.RARE:
                value = 5;
                repeatTime = Random.Range(1f, 1.3f);
                break;
            case Tier.EPIC:
                value = 10;
                repeatTime = Random.Range(0.8f, 1f);
                break;
            case Tier.LEGEND: 
                value = 20;
                Random.Range(0.6f, 0.8f);
                break;
            default: break;
        }
    }
    void Update() {
        if (!isAlive) {
            Destroy(gameObject);
            SpawnManager.Instance.SpawnOrb(gameObject, Random.Range(1, 4), value);
            SoundManager.Instance.Play("enemy death", 1);
            return;
        }

        animator.SetFloat("distance", Vector3.Distance(transform.position, target.transform.position));
        animator.SetInteger("HP", currHP);
    }

    public void Wander() {
        Vector3 wanderTarget = new Vector3();
        float wanderRadius = 10f, wanderDistance = 5f, wanderJitter = 1.5f;

        // randomly looks left/right
        wanderTarget += new Vector3(Random.Range(-2f, 2f) * wanderJitter, 0f, Random.Range(-2f, 2f) * wanderJitter);
        wanderTarget.Normalize();
        wanderTarget *= wanderRadius;

        // Sets the local target to go here
        Vector3 targetLocal = wanderTarget + new Vector3(0f, 0f, wanderDistance);
        Vector3 targetWorld = gameObject.transform.InverseTransformVector(targetLocal); // converts to world space

        agent.SetDestination(targetWorld);
    }
    public void Flee() { // rotates away from the player
        Vector3 direction = -(target.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation,
                                              Quaternion.LookRotation(direction),
                                              rotSpeed * Time.deltaTime);

        transform.Translate(0f, 0f, movSpeed * Time.deltaTime);
        // Debug.Log("Fleeing from " + target.name);
    }
    protected override void Attack() {
        GameObject b = Instantiate(bulletPrefab, nozzle.position, transform.rotation);
        SoundManager.Instance.Play("player fire", 1);

        b.GetComponent<Bullet>().owner = Owner.ENEMY;
        b.GetComponent<Bullet>().dmg = 1;
    }
    public void StartAttacking() { 
        if (CanSeeTarget()) 
            InvokeRepeating("Attack", startTime, repeatTime); 
    }
    public void StopAttacking() { CancelInvoke("Attack"); }
    bool CanSeeTarget() {
        RaycastHit hit;
        Vector3 rayToTarget = target.transform.position - transform.position;

        if (Physics.Raycast(transform.position, rayToTarget, out hit))
            return hit.transform.gameObject.tag == "Player";

        return false;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, range);
    }
    void OnDestroy() {
        SpawnManager.Instance.enemies.Remove(gameObject);
        GameManager.Instance.AddToScore(value);
    }
}
