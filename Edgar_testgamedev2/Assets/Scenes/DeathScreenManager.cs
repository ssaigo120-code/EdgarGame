using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DeathScreenManager : MonoBehaviour
{
    public GameObject deathScreenUI;
    public GameObject aliveScreenUI;
    public TMP_Text killsText;
    public TMP_Text highScoreText;

    void Start()
    {
        deathScreenUI.SetActive(false);
    }

    public void ShowDeathScreen(int kills)
    {
        deathScreenUI.SetActive(true);
        aliveScreenUI.SetActive(false);
        Time.timeScale = 0f;

        killsText.text = "Score: " + kills;

        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (kills > highScore)
        {
            highScore = kills;
            PlayerPrefs.SetInt("HighScore", highScore);
        }

        highScoreText.text = "Highscore: " + highScore;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
