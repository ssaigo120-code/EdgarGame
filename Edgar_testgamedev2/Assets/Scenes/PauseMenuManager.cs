using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    private bool isPaused = false;
    [SerializeField] private GameObject deathScreenUI;
    public AudioSource musicAudioSource;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {   
            if (deathScreenUI.activeSelf)
            {
                return;
            } 

            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        musicAudioSource.Pause();
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        musicAudioSource.UnPause();
        isPaused = false;
    }

    public void OnResumePressed()
    {
        ResumeGame();
    }

    public void OnMainMenuPressed()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
