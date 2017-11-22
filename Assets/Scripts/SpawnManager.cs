using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    [SerializeField] private GameObject enemyShip;
    [SerializeField] private GameObject[] powerups;

    private GameManager gameManager;

    private GameObject temporaryEnemy;
    private GameObject temporaryPowerup;


    void Start() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (gameManager.gameOver) {
            temporaryEnemy = LaunchEnemyShip();
            temporaryPowerup = SendPowerups();            
        }
    }

    void Update() {
        if (gameManager.gameOver && temporaryPowerup == null) {
            temporaryPowerup = SendPowerups();
        }
    }

    public void SpawnAll() {
        Destroy(temporaryEnemy.gameObject);
        Destroy(temporaryPowerup.gameObject);

        InvokeRepeating("LaunchEnemyShip", 0.0f, 5.0f);
        InvokeRepeating("SendPowerups", 5.0f, 10.0f);
    }

    private GameObject LaunchEnemyShip() {
        if (gameManager.TooManyEnemiesOnScreen()) {
            return null;
        }

        gameManager.BumpEnemyCount();
        return Instantiate(enemyShip, transform.position, Quaternion.identity);
    }

    private GameObject SendPowerups() {
        GameObject randomPowerup = powerups[Random.Range(0, 3)];
        return Instantiate(randomPowerup, transform.position, Quaternion.identity);
    }
}
