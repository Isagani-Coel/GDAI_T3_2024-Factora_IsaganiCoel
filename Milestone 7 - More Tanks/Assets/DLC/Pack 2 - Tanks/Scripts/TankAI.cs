using UnityEngine;

public class TankAI : MonoBehaviour {

    public GameObject player, bullet, turret;
    [SerializeField] int currHP, maxHP;
    
    Animator animator;
    bool isAlive = true;

    public GameObject GetPlayer() { return player; }
    public int GetHP() { return currHP; }
    public int GetMaxHP() { return maxHP; }

    void Start() {
        animator = GetComponent<Animator>();
    }

    void Update() {
        if (!isAlive) return;

        animator.SetFloat("distance", Vector3.Distance(transform.position, player.transform.position));
        animator.SetInteger("HP", currHP);

        ViewHP();
    }

    void Fire() {
        GameObject b = Instantiate(bullet, turret.transform.position, turret.transform.rotation);
        b.GetComponent<Rigidbody>().AddForce(turret.transform.forward * 500f);
        b.GetComponent<Bullet>().owner = Owner.ENEMY;
    }

    public void StartFiring() { InvokeRepeating("Fire", 0.5f, 0.5f); }
    public void StopFiring() { CancelInvoke("Fire"); }

    public void TakeDamage() { 
        currHP--;

        if (currHP <= 0)
            Die();
    }

    void Die() {
        Debug.LogWarning("Enemy has been killed");
        Destroy(gameObject);
    }

    void ViewHP() { Debug.Log(name + " HP: " + currHP + "/" + maxHP); }

}
