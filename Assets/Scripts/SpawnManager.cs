using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    //Entity Variables
    public GameObject enemyPrefab;
    public int enemyCount;
    public int waveNumber = 0;
    public GameObject powerupPrefab;
    public GameObject player;
    public bool isGameActive = false;
    public GameObject stage;
    public float stageX;
    public float stageY;
    public float stageZ;


    //public Enemy enemyScript;
    public GameObject enemy;

    //UI Stuff
    public GameObject titleScreen;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;

    //private variables
    private float spawnRange = 9;
    private float lowerBound = -5;
    private bool isRestart;

    // Start is called before the first frame update
    void Start()
    {
        isGameActive = false;
        //enemyScript = GameObject.Find("Enemy").GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        GameOver();

        if(isGameActive)
        {
            waveCounter();
        }
        

        if (enemyCount == 0 && isGameActive)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
        }

        if (waveNumber > 4 && enemyCount == 0)
        {
            scaleStage();
        }

        if (waveNumber % 2 == 0 && enemyCount == 0 && isGameActive)
        {
            Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        spawnRange = 9 * (stageX / 5);

        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }
    void SpawnEnemyWave(int enemiesToSpawn)
    {
        Debug.Log(enemiesToSpawn);

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }

    private void GameOver()
    {
        if(player.transform.position.y < lowerBound)
        {
            //set the game over text and button to active
            gameOverText.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);

            isGameActive = false;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        isRestart = true;
        StartGame();
    }

    
    public void StartGame()
    {
        isGameActive = true;
        waveNumber = 0;
        stageX = 5.0f;
        stageY = 5.0f;
        stageZ = 5.0f;

        SpawnEnemyWave(waveNumber);
        Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);

        player.gameObject.SetActive(true);
        titleScreen.gameObject.SetActive(false);
        if(!isRestart)
        {
            enemy.gameObject.SetActive(true);
        }
        
        
    }

    void scaleStage()
    {
        if (stageX >= 3 && enemyCount == 0)
        {
            stageX = stageX - .3f;
            stageY = stageY - .3f;
            stageZ = stageZ - .3f;
        }
        
        Vector3 local = transform.localScale;
        stage.transform.localScale = new Vector3(stageX, stageY, stageZ);
    }
    

    public void waveCounter()
    {
        waveText.text = "Wave: " + waveNumber;
    }

}
