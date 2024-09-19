using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndComponent : MonoBehaviour
{
    NextLevelController nextLevel = null;
    public int level = 1;
    public float timer = 0.0f;
    private void Start()
    {
        nextLevel = FindAnyObjectByType<NextLevelController>();
        timer = 0.0f;
    }
    private void Update()
    {
        timer += Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (!player)
            return;
        Debug.Log("Wygra³eœ");
        PlayerPrefs.SetInt("CurrentLevel", level);
        PlayerPrefs.SetFloat("CompletionTime", timer);
            nextLevel.setVisible(true);

    }
}
