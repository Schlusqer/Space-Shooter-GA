using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject[] hazards;
    public GameObject boss;
    public Vector3 spawnValues;
    public int hazardCount;
    public int bossEncounter;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public GUIText scoreText;
    public GUIText restartText;
    public GUIText gameOverText;
    public GUIText highScoreText;
    public GUIText bossScoreText;

    private bool gameOver;
    private bool restart;
    public bool spawnEnemies;
    public bool bossDeath;

    private int score;
    private int highScore;

    private BossWeaponController bossWeaponController;
    private BossMover bossMover;

    void Start()
    {
        gameOver = false;
        restart = false;
        spawnEnemies = true;
        bossDeath = false;
        restartText.text = "";
        gameOverText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
        // Loads highscore from previous runs
        highScore = PlayerPrefs.GetInt("High Score");
        highScoreText.text = "High Score: \n" + highScore;
        bossEncounter = 1500;
        bossScoreText.text = "Next boss: \n" + bossEncounter;
        GameObject bossWeaponControllerObject = GameObject.FindWithTag("Boss");
        if (bossWeaponControllerObject != null)
        {
            bossWeaponController = bossWeaponControllerObject.GetComponent<BossWeaponController>();
        }
        if (bossWeaponController == null)
        {
            Debug.Log("Cannot find 'BossWeaponController' script");
        }

        GameObject bossMoverObject = GameObject.FindWithTag("Boss");
        if (bossMoverObject != null)
        {
            bossMover = bossMoverObject.GetComponent<BossMover>();
        }
        if (bossMover == null)
        {
            Debug.Log("Cannot find 'BossMover' script");
        }
    }

    void Update()
    {
        if (restart)
        {
            if(Input.GetKeyDown (KeyCode.R))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }
    // Spawns the waves
    IEnumerator SpawnWaves()
    {
        while (true)
        {
            yield return new WaitForSeconds(startWait);
            if (spawnEnemies)
            {
                for (int i = 0; i < hazardCount; i++)
                {
                    GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                    Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                    Quaternion spawnRotation = Quaternion.identity;
                    Instantiate(hazard, spawnPosition, spawnRotation);
                    yield return new WaitForSeconds(spawnWait);
                }
            if (score >= bossEncounter)
            {
                spawnEnemies = false;
            }
            }
            else
            {
                if (bossDeath)
                {
                    spawnEnemies = true;
                    bossEncounter = score + 1500;
                } 
            }
            if (checkGameEnd())
            {
                break;
            }
            yield return new WaitForSeconds(waveWait);

        }
    }

    // Checks if the game has ended
    public bool checkGameEnd()
    {
        // Allows the player to restart
        if (gameOver)
        {
            restartText.text = "Press 'R' for Restart";
            restart = true;
            return true;
        }
        return false;
    }

    // Counting score and highest score
    public void AddScore (int newScoreValue)
    {
        
        score += newScoreValue;
        UpdateScore();
        highScoreText.text = "High Score: \n" + highScore;
        
    }

    void UpdateScore ()
    {
        scoreText.text = "Score: \n" + score;

        // Updates the high score if your get a higher score
        if (score > highScore)
        {
            highScore = score;
            // Saves your new high score
            PlayerPrefs.SetInt("High Score", highScore);
        }
        
    }
    
    
    // Ends the game
    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
    }
}
