using UnityEngine;

/* -ORB- 
    - dropped by enemies
    - follows the player if it's within range

   -TYPES-
    - HP -> heals 2 HP to the player
    - XP -> its value changes when a certain enemy dies
*/

public enum OrbType { DEFAULT, XP, HP }

public class Orb : MonoBehaviour {

    [SerializeField] Transform target;
    [SerializeField] float speed, range;
  
    [HideInInspector]
    public int value;
    public OrbType ot;

    void Start() {
        if      (ot == OrbType.XP) name = "XP Orb";
        else if (ot == OrbType.XP) name = "HP Orb";

        target = GameObject.FindGameObjectWithTag("Player").transform;
        Destroy(gameObject, 20f);
    }
    void LateUpdate() {
        Vector3 lookAtGoal = new Vector3(target.position.x, 
                                         transform.position.y,
                                         target.position.z);
        transform.LookAt(lookAtGoal);

        // smooth rotation
        Vector3 direction = lookAtGoal - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation,
                                              Quaternion.LookRotation(direction),
                                              Time.deltaTime * 100f);

        // travels to the goal if player enters the range
        if (Vector3.Distance(lookAtGoal, transform.position) < range) {
            Vector3.Lerp(transform.position, target.position, speed * Time.deltaTime); // smooth acceleration & decelration
            transform.Translate(0f, 0f, speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {

            if (ot == OrbType.XP) {
                SoundManager.Instance.Play("gain xp", 0);
                GameManager.Instance.AddToScore(value);
                // Debug.Log("Gained XP");
            }
            else if (ot == OrbType.HP) {
                SoundManager.Instance.Play("gain hp", 0);
                target.GetComponent<Player>().TakeDamage(-value);
                // Debug.Log("Gained HP");
            }
            Destroy(gameObject);
        }
    }
    void OnDrawGizmos() {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, range);
    }
    void OnDestroy() { SpawnManager.Instance.orbs.Remove(gameObject); }
}
