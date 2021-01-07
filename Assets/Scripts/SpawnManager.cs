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
    public bool isGameActive;

    //UI Stuff
    public GameObject titleScreen;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;

    //private variables
    private float spawnRange = 9;
    private float lowerBound = -5;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(waveNumber);
        Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);

        isGameActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        GameOver();
        waveCounter();

        if (enemyCount == 0 && isGameActive)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
        }

        if (waveNumber % 2 == 0 && enemyCount == 0)
        {
            Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }
    void SpawnEnemyWave(int enemiesToSpawn)
    {
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

        if (player.transform.position.y < lowerBound && isGameActive)
        {
            Destroy(player);
        }

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /**
    public void StartGame()
    {
        isGameActive = true;
        waveNumber = 1;

        SpawnEnemyWave(waveNumber);
        Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);

        titleScreen.gameObject.SetActive(false);
    }
    **/

    public void waveCounter()
    {
        waveText.text = "Wave: " + waveNumber;
    }

    public int getWaveNumber()
    {
        //returns the wave number for enemy.cs to grab in order to make
        // a tutorial level.
        return waveNumber;
    }
}
