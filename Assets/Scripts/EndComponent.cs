using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndComponent : MonoBehaviour
{
    public static float timer = 0.0f;
    public static bool finishLevel = false;
    NextLevelController nextLevel = null;
    public int level = 1;
    private void Start()
    {
        PlayerPrefs.SetInt("CurrentLevel", level);
        PlayerPrefs.SetFloat("CompletionTime", timer);
        nextLevel = FindAnyObjectByType<NextLevelController>();
        timer = 0.0f;
        finishLevel = false;
    }
    private void Update()
    {
        if (!finishLevel)
            timer += Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        Player player = collision.GetComponent<Player>();
        if (!player)
            return;
        player.StopMove();
        PlayerPrefs.SetInt("CurrentLevel", level);
        PlayerPrefs.SetFloat("CompletionTime", timer);
        PlayerPrefs.Save();
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        finishLevel = true;
        if (audioManager != null)
        {
            audioManager.PlayFX(audioManager.win);
        }

        nextLevel.setVisible(true);

    }
}
