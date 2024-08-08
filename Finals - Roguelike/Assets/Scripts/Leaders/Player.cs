using UnityEngine;

/* -PLAYER-
    - W & S -> move fwd & back
    - A & D -> rotate camera
    - LMB -> attack
*/

/* -BUGS-
    [] when player collides with anything its rotation gets broken
*/

public class Player : Leader {

    int level = 1;
    const int MAX_LEVEL = 30, MAX_SPEED = 12, MAX_ROT_SPEED = 180;
    Vector3 startPos = new Vector3();
    Rigidbody rb;

    [HideInInspector]
    public bool isMaxed = false;

    public int GetLevel() { return level; } 
    public float GetMoveSpeed() { return movSpeed; }

    protected override void Start() {
        base.Start();

        name = "Player";
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        transform.position = new Vector3(0f, 1f, 0f);
    }
    void Update() {
        if (movSpeed > MAX_SPEED) movSpeed = MAX_SPEED;
        if (rotSpeed > MAX_ROT_SPEED) rotSpeed = MAX_ROT_SPEED;

        if (!isAlive) return;

        Reposition();
        Attack();
    }
    void LateUpdate() {
        if (!isAlive) return;

        float v = Input.GetAxis("Vertical") * movSpeed * Time.deltaTime;
        float h = Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;

        transform.Translate(0f, 0f, v);
        transform.Rotate(0f, h, 0f);
    }

    void Reposition() {
        if (transform.position.y < -5f)
            transform.position = startPos;

        if (transform.position.y > 1.5f) {
            transform.position = new Vector3(transform.position.x, 1.078f, transform.position.z);
            transform.rotation = Quaternion.identity;
        }
    }
    protected override void Attack() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            SoundManager.Instance.Play("player fire", 1);
            GameObject b = Instantiate(bulletPrefab, nozzle.position, transform.rotation);
            b.GetComponent<Bullet>().owner = Owner.PLAYER;
            b.GetComponent<Bullet>().dmg = 2;
        }
    }   
    public void LevelUp() {
        if (isMaxed) return;

        SoundManager.Instance.Play("level up", 0);
        level++;

        if (level >= MAX_LEVEL) {
            level = MAX_LEVEL;
            isMaxed = true;
        }

        switch (Random.Range(0, 4)) {
            case 0: maxHP += Random.Range(2, 3); break;
            case 1: movSpeed += 0.2f ; break;
            case 2: rotSpeed += Random.Range(5, 10); break;
            case 3: TakeDamage(Random.Range(1, 3)); break;
            default: break;
        }
    }
}