using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    [SerializeField] private GameObject enemyShip;
    [SerializeField] private GameObject[] powerups;

    private GameManager gameManager;

    private GameObject initialEnemy;
    private GameObject initialPowerup;


    void Start() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        initialEnemy = LaunchEnemyShip();
        initialPowerup = SendPowerups();
    }

    void Update() {
        if (initialPowerup == null) {
            initialPowerup = SendPowerups();
        }
    }

    public void SpawnAll() {
        initialEnemy.transform.position = new Vector3(0, 7.0f, 0);
        Destroy(initialEnemy.gameObject);
        initialPowerup.transform.position = new Vector3(0, 7.0f, 0);
        Destroy(initialPowerup.gameObject);

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
