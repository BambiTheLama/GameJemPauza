using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    public void PlayGame()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void OpenOptions()
    {
        Debug.Log("Opcje zosta³y otwarte.");
    }

    public void ExitGame()
    {
        Debug.Log("Zamykam grê.");
        Application.Quit();
    }
}
