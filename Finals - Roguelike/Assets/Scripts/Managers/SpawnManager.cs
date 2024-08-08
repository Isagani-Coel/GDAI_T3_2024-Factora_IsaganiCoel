using System.Collections.Generic;
using UnityEngine;

/* -SPAWN MANAGER-
    - contains all different types of enemies
    - spawns/despawns all enemies & orbs
*/

public class SpawnManager : MonoBehaviour {

    public static SpawnManager Instance;

    [Header("Prefabs")]
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] GameObject[] orbPrefabs;

    [HideInInspector]
    public List<GameObject> enemies, orbs;
    GameObject[] spawnPoints;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject); // used when switching between scenes
        }
        else Destroy(gameObject);
    }
    void Start() {
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

        enemies = new List<GameObject>();
        orbs = new List<GameObject>();
    }

    public void SpawnEnemy() { // random enemy spawning with different tiers
        GameObject e = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)],
                                   spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position,
                                   Quaternion.identity);
        enemies.Add(e);
    }
    public void SpawnOrb(GameObject enemy, int amt, int value) {
        for (int i = 0; i < amt; i++) {
            float offset = Random.Range(-1f, 1f);
            int randNum = Random.Range(0, 2);

            GameObject orb = Instantiate(orbPrefabs[randNum],
                                         new Vector3(enemy.transform.position.x + offset, 0.2f,
                                                     enemy.transform.position.z + offset),
                                         Quaternion.identity);
            orbs.Add(orb);

            if      (randNum == 0) orb.GetComponent<Orb>().value = value;
            else if (randNum == 1) orb.GetComponent<Orb>().value = 2;
        }
    }

    public void Restart() {
        Awake();
        Start();
    }
}
