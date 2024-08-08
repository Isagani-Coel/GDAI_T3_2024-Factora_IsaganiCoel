using UnityEngine;

public enum Owner { DEFAULT, PLAYER, ENEMY }

/* -BULLET-
    - fires a straight line
    - dies if its anything
    - dies after 3s if it doesn't hit anything
*/

public class Bullet : MonoBehaviour {

    public float speed;
    public int dmg;
    const int MAX_DMG = 5;
    public Owner owner;
    
    void Start() { 
        if (dmg > MAX_DMG) dmg = MAX_DMG;

        Destroy(gameObject, 3f); 
    }
    void LateUpdate() { transform.Translate(Vector3.forward * speed * Time.deltaTime); }

    void OnTriggerEnter(Collider other) {
        if (owner == Owner.DEFAULT) return;

        if (other.gameObject.CompareTag("Player") && owner == Owner.ENEMY)
            other.gameObject.GetComponent<Player>().TakeDamage(dmg);

        if (other.gameObject.CompareTag("Enemy") && owner == Owner.PLAYER)
            other.gameObject.GetComponent<Enemy>().TakeDamage(dmg);

        Destroy(gameObject);
    }
}

/* -HOW COLLISION WORKS-

    1. Collider needs a collider and IsTrigger = true
    2. Collider needs OnTriggerEnter
    3. Target needs rb with a specific tag

 */