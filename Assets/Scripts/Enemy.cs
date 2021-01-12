using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public SpawnManager spawnManager;
    

    private Rigidbody enemyRb;
    private float speed = 5.0f;
    private bool isTutorial;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnManager.waveNumber > 0 && spawnManager.isGameActive)
        {
            Vector3 lookDirection =
            (player.transform.position - transform.position).normalized;

            enemyRb.AddForce(lookDirection * speed);
        }

        /*
        if (spawnManager.waveNumber == 0)
        {
            isTutorial = true;
        }
        else
        {
            isTutorial = false;
        }
        */

        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }

    }
}
