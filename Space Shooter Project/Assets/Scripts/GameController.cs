using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text ScoreText;
    private int score;

    public Text restartText;
    public Text gameOverText;
    public Text creditText;

    private bool gameOver;
    private bool restart;

    void Start()
    {
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());

        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        creditText.text = "";
    }

    void Update()
    {
        LoseCheck();
        WinCheck();
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(
                    -spawnValues.x, spawnValues.x), 
                    spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'Q' to Restart";
                restart = true;
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        ScoreText.text = "Points: " + score;
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over";
        creditText.text = "Made by Julia Houha";
        gameOver = true;
    }

    public void WinCheck()
    {
        if (score >= 100)
        {
            score = 100;
            UpdateScore();
            gameOverText.text = "You Win";
            creditText.text = "Made by Julia Houha";
            gameOver = true;
        }
    }

    public void LoseCheck()
    {
        if (restart == true)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SceneManager.LoadScene("Main");
            }
        }
    }
}