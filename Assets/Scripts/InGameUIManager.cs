using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameUIManager : MonoBehaviour
{
    public GameObject Player;
    public Text ScoreText;
    public Text GameOver;
    public float Score;

    private void Start()
    {
        GameOver.gameObject.SetActive(false);
        Score = Player.transform.position.y;
    }

    void Update()
    {
        if (Player.transform.position.y > Score)
            Score = Player.transform.position.y;
        if (Player.GetComponent<Player>().isDead)
            GameOver.gameObject.SetActive(true);

        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(0);
        }
        
        
        ScoreText.text = Mathf.CeilToInt(Score).ToString();
    }
}
