using UnityEngine;

/* -NOTES- (07.24.24)
    TANKS
    [/] add 2 other Tank AIs
    [/] Player must shoot boolets as well
    [/] add an HP component to you and AI tanks 
    [/] Destory tanks if HP == 0

    FSM
    [/] add a FLEE state
    [/] AIs must FLEE when HP < 20% of max
 */

public class Player : MonoBehaviour {

    [Header("Player Settings")]
    [SerializeField] int maxHP, currHP;
    [SerializeField] GameObject bullet, turret;
 	[SerializeField] float speed = 10f, rotationSpeed = 100f;

    bool isAlive = true;

    public int GetHP() { return currHP; }
    public int GetMaxHP() { return maxHP; }

    void Update() {
        if (!isAlive) return;

        Attack();
        Movement();
        // ViewHP();
	}

    void Movement() {
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        transform.Translate(0f, 0f, translation * Time.deltaTime);
        transform.Rotate(0f, rotation * Time.deltaTime, 0f);
    }

    void Attack() {
        if (Input.GetMouseButtonDown(0)) {
            GameObject b = Instantiate(bullet, turret.transform.position, turret.transform.rotation);
            b.GetComponent<Rigidbody>().AddForce(turret.transform.forward * 500f);
            b.GetComponent<Bullet>().owner = Owner.PLAYER;
        }
    }

    public void TakeDamage() { 
        currHP--;

        if (currHP <= 0) 
            Die();
    }
    void Die(){
        Debug.LogWarning("You are dead");
        Destroy(gameObject);
    }

    void ViewHP() { Debug.Log(name + " HP: " + currHP + "/" + maxHP); }
}
