using UnityEngine;

public enum Owner { PLAYER, ENEMY }

public class Bullet : MonoBehaviour {

	public GameObject explosion;
    public Owner owner;
    
	void OnCollisionEnter(Collision other) {
        GameObject e = Instantiate(explosion, transform.position, Quaternion.identity);

        if (other.gameObject.CompareTag("Enemy") && owner == Owner.PLAYER) 
            other.gameObject.GetComponent<TankAI>().TakeDamage();

        if (other.gameObject.CompareTag("Player") && owner == Owner.ENEMY)
            other.gameObject.GetComponent<Player>().TakeDamage();

        Destroy(e, 1.5f);
        Destroy(gameObject);
    }
}
