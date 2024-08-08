using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/* -REQUIREMENTS-
    1. Need to have 5 different AI concepts
    2. Submit build file 
    3. Deadline -> Aug. 9, 23:59

   -AI CONCEPTS IMPLEMENTED-
    1. Complex Behaviors (Enemy Attack, Flee, Wander)
    2. FSM               (Enemy Behavior)
    3. NavMesh           (Enemy movement)
    4. Pet Follow        (XP & HP orbs)
    5. Graphs            (Moving Turrets)
*/

/* -PROJECT PROGRESS- 
   (07.30.24)
    - Planned for 2 different maps
    - Researched different top-down movement methods

   (08.02.24)
    - Added a test map (playground)
    - Implemented basic player movement & shooting
    - Added enemy movement
    - Added Player & Enemy deaths

   (08.03.24)
    - Added fighter pet

   (08.04.24)
    - impemented proper pet ranges
   
   (08.05.24)
    - Added the actual map for the game
    - Scrapped pets for following XP orbs 
    - Addded enemy variantss

   (08.06.24)
    - Added simple UI
    - Added HP orbs

   (08.07.24)
    - Added constantly rotating turrets
    - Added title screen
    - Added a bit of sounds
    - Added pause, game over panels
*/

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    [Header("Panels")]
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject pausePanel;

    [Header("UI Text")]
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI xpText, levelText;

    GameObject player;
    Player p1Script;

    int score, threshold;
    bool isPaused, canPause, gameOver;

    public GameObject GetPlayer() { return player; }

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject); // used when switching between scenes
        }
        else Destroy(gameObject);
    }
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        p1Script = player.GetComponent<Player>();
        threshold = p1Script.GetLevel() * 20;
        Time.timeScale = 1f;

        gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);

        gameOver = false;
        isPaused = false;
        canPause = true;

        UpdateUI();

        for (int i = 0; i < 3; i++) 
            SpawnManager.Instance.SpawnEnemy();
    }
    void Update() {
        if (!player.GetComponent<Player>().GetIsAlive()) {
            DoGameOver();
            gameOver = true;
            return;
        }

        UpdateUI();

        if (SpawnManager.Instance.enemies.Count < 3) {
            int randomAmt = Random.Range(4, 7);

            for (int i = 0; i < randomAmt; i++) 
                SpawnManager.Instance.SpawnEnemy();
        }

        if (Input.GetKeyDown(KeyCode.Escape)) TogglePause();
    }

    public void AddToScore(int amt) {
        if (p1Script.isMaxed) return;

        score += amt;

        if (score >= threshold) {
            p1Script.LevelUp();
            score = 0;
            threshold = p1Script.GetLevel() * 20;
        }
    }

    void UpdateUI() {
        hpText.text = "Life: " + p1Script.GetCurrHP() + "/" + p1Script.GetMaxHP();
        xpText.text = "XP: " + score + "/" + threshold;
        levelText.text = "Lv. " + p1Script.GetLevel();
    }

    void DoGameOver() {
        gameOverPanel.SetActive(true);
        canPause = false;
        Time.timeScale = 0f;
    }

    public void TogglePause() {
        if (!canPause) return;

        isPaused = !isPaused;

        if (isPaused) Time.timeScale = 0f;
        else          Time.timeScale = 1f;

        SoundManager.Instance.Play("pause", 0);
        pausePanel.SetActive(!pausePanel.activeSelf);
    }

    public void Restart() { 
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        SpawnManager.Instance.Restart();
        SoundManager.Instance.Play("select", 0);
        Awake();
        Start();
    }
    public void Quit() {
        SoundManager.Instance.Play("select", 0);
        Application.Quit(); 
    }
}