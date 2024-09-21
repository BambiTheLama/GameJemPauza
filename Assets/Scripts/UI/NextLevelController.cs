
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class NextLevelController : MonoBehaviour
{
    public GameObject nextLevelUI = null;
    public TMP_Text levelText;
    public TMP_Text timeText;

    private int currentLevel = 0;
    private float completionTime = 0.0f;

    void Start()
    {
        if (!PlayerPrefs.HasKey("CurrentLevel"))
        {
            currentLevel = 1;
        }
        else
        {
            currentLevel = PlayerPrefs.GetInt("CurrentLevel");
        }
        completionTime = EndComponent.timer;


        SetLevelText();
        SetTimeText();
    }

    private void Update()
    {
        completionTime = EndComponent.timer;
        SetTimeText();
    }

    void SetLevelText()
    {
        if (levelText)
            levelText.text = "Level " + currentLevel.ToString();
    }

    void SetTimeText()
    {
        if (timeText)
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
    public void setVisible(bool visible)
    {
        if (nextLevelUI)
            nextLevelUI.SetActive(visible);
    }
}
