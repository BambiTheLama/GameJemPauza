using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    public void PlayGame()
    {
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
