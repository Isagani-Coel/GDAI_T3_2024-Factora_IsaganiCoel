using UnityEngine;

public class Shell : MonoBehaviour {

    float speed;
    int dmg;
    public Owner owner;

    void Start() {
        speed = 20f;
        dmg = 1;
        owner = Owner.ENEMY;

        Destroy(gameObject, 3f); 
    }

    void LateUpdate() { transform.Translate(Vector3.right * speed * Time.deltaTime); }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player") && owner == Owner.ENEMY)
            other.gameObject.GetComponent<Player>().TakeDamage(dmg);

        Destroy(gameObject);
    }
}