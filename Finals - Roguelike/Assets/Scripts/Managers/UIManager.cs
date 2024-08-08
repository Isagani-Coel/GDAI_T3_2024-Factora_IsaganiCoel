using UnityEngine;

public class UIManager : MonoBehaviour {

    public static UIManager Instance;

    [SerializeField] GameObject pausePanel;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
}