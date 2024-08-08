using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    public GameObject controlPanel;

    public void SelectScene(string name) {
        SoundManager.Instance.Play("select", 0);
        SceneManager.LoadSceneAsync(name); 
    }
    public void ToggleControlPanel() {
        SoundManager.Instance.Play("select", 0);
        controlPanel.SetActive(!controlPanel.activeSelf);
    }

    public void QuitGame() {
        SoundManager.Instance.Play("select", 0);
        Application.Quit(); 
    }
}