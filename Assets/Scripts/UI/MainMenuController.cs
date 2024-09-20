using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    public void PlayGame()
    {
        PlayerPrefs.SetInt("CurrentLevel", 0);
        PlayerPrefs.SetFloat("CompletionTime", 0f);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Level_0");
    }

    public void Continue()
    {
        //TODO
        //SceneManager.LoadScene("Ostatnia scena");

    }

    public void ExitGame()
    {
        Debug.Log("Zamykam grê.");
        Application.Quit();
    }
}
