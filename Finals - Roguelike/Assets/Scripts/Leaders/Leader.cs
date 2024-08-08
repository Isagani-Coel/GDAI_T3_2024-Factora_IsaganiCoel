using UnityEngine;

public abstract class Leader : MonoBehaviour {

    [Header("Movement Settings")]
    [SerializeField] protected float movSpeed;
    [SerializeField] protected float rotSpeed;

    [Header("Stats")]
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected Transform nozzle;
    [SerializeField] protected int maxHP;

    protected bool isAlive;
    protected int currHP;

    public int GetCurrHP() { return currHP; }
    public int GetMaxHP() { return maxHP; }
    public bool GetIsAlive() { return isAlive; }

    protected virtual void Start() {
        currHP = maxHP;
        isAlive = true;
    }

    protected abstract void Attack();
    public void TakeDamage(int amt) {
        if (!isAlive) return;

        currHP -= amt;

        if (currHP <= 0){
            currHP = 0;
            isAlive = false;
        }
        if (currHP > maxHP) currHP = maxHP; // in case of gaining HP
    }
}
