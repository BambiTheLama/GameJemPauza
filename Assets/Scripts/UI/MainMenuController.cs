using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{

    public Button continueButton;

    private void Start()
    {

        int lvl = PlayerPrefs.GetInt("CurrentLevel");

        if (lvl == 0)
        {
            continueButton.interactable = false;
        }
        else
        {
            continueButton.interactable = true;
        }
    }
    public void PlayGame()
    {
        PlayerPrefs.SetInt("CurrentLevel", 0);
        PlayerPrefs.SetFloat("CompletionTime", 0f);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Level_0");
    }

    public void Continue()
    {
        int lvl = PlayerPrefs.GetInt("CurrentLevel");
        if (lvl > 0)
        {
            SceneManager.LoadScene("Level_" + lvl);
        }
        
    }

    public void ExitGame()
    {
        Debug.Log("Zamykam grê.");
        Application.Quit();
    }
}
