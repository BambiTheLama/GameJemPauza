using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class NextLevelController : MonoBehaviour
{
    public TMP_Text levelText;
    public TMP_Text timeText;

    private int currentLevel;
    private float completionTime;

    void Start()
    {
        currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
        completionTime = PlayerPrefs.GetFloat("CompletionTime", 0f);

        SetLevelText();
        SetTimeText();
    }

    void SetLevelText()
    {
        levelText.text = "Level " + currentLevel.ToString();
    }

    void SetTimeText()
    {
        timeText.text = completionTime.ToString("F2");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToNextLevel()
    {
        int nextLevel = currentLevel + 1;
        string nextSceneName = "Level_" + nextLevel.ToString();

        if (Application.CanStreamedLevelBeLoaded(nextSceneName))
        {
            PlayerPrefs.SetInt("CurrentLevel", nextLevel);
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogError("Poziom " + nextSceneName + " nie istnieje!");
        }
    }
}
