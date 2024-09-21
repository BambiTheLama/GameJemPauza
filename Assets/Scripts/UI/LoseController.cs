using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseController : MonoBehaviour
{
    public GameObject loseMenu;

    void Start()
    {
       // loseMenu.SetActive(false);
    }


    public void SetVisible()
    {
        if (loseMenu)
            loseMenu.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
