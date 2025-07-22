using UnityEngine;
using TMPro;

public class EnemyCounter : MonoBehaviour
{
    public static EnemyCounter Instance;

    public int enemiesKilled = 0;
    public TMP_Text counterText;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start() 
    {
        UpdateUI();
    }

    public void AddKill()
    {
        enemiesKilled++;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (counterText != null)
            counterText.text = "Kills: " + enemiesKilled;
    }
}
