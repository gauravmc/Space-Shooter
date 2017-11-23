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
    }

    void Update() {
        if (gameManager.gameOver) {
            if (temporaryPowerup == null) {
                temporaryPowerup = CreateNewPowerup();
            }
            if (temporaryEnemy == null) {
                temporaryEnemy = CreateNewEnemy();
            }
        }
    }

    public void GameStarted() {
        Destroy(temporaryEnemy.gameObject);
        Destroy(temporaryPowerup.gameObject);

        InvokeRepeating("LaunchEnemyShips", 0.0f, 5.0f);
        InvokeRepeating("SendPowerups", 5.0f, 10.0f);
    }

    private void LaunchEnemyShips() {
        if (!gameManager.gameOver && !gameManager.TooManyEnemiesOnScreen()) {
            gameManager.BumpEnemyCount();
            CreateNewEnemy();
        }
    }

    private void SendPowerups() {
        if (!gameManager.gameOver) {
            CreateNewPowerup();
        }
    }

    private GameObject CreateNewEnemy() {
        return Instantiate(enemyShip, transform.position, Quaternion.identity);
    }

    private GameObject CreateNewPowerup() {
        GameObject randomPowerup = powerups[Random.Range(0, 3)];
        return Instantiate(randomPowerup, transform.position, Quaternion.identity);
    }
}
