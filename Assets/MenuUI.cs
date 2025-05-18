using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject controlPanel;

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene"); 
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    public void ShowControls()
    {
        controlPanel.SetActive(true);
    }

    public void HideControls()
    {
        controlPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit pressed");
    }
}