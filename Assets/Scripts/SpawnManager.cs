using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    public int maximumEnemies = 100;

    private Dictionary<int, float> enemySpeedIncrementMap = new Dictionary<int, float>
    {
        { 5, 1.0f },
        { 10, 1.0f },
        { 15, 1.0f },
        { 20, 0.5f },
        { 25, 0.5f },
        { 35, 0.5f },
        { 55, 0.5f },
        { 75, 0.5f },
        { 85, 0.5f },
        { 90, 1.0f },
        { 95, 1.0f }
    };

    //public int maximumEnemies = 10;

    //private Dictionary<int, float> enemySpeedIncrementMap = new Dictionary<int, float>
    //{
    //    { 1, 1.0f },
    //    { 2, 1.0f },
    //    { 3, 1.0f },
    //    { 4, 0.5f },
    //    { 5, 0.5f },
    //    { 30, 0.5f },
    //    { 35, 0.5f },
    //    { 40, 0.5f },
    //    { 45, 0.5f },
    //    { 75, 0.5f },
    //    { 6, 0.5f },
    //    { 7, 1.0f },
    //    { 8, 1.0f }
    //};

    [SerializeField] private GameObject enemyShip;
    [SerializeField] private GameObject[] powerups;
    [SerializeField] private float initialEnemySpeed = 4.0f;

    private GameManager gameManager;
    private GameObject temporaryEnemy;
    private GameObject temporaryPowerup;

    private bool pauseSpawning = false;
    private int totalEnemiesSpawned = 0;
    private int totalEnemiesInScene = 0;
    private float enemySpeed;

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

        pauseSpawning = (totalEnemiesInScene == gameManager.totalEnemiesLeft) ||
            (totalEnemiesSpawned == maximumEnemies) ||
            (totalEnemiesInScene > 5);
    }

    public void GameStarted() {
        DestroyAllSpawnupsInScene();

        totalEnemiesSpawned = 0;
        totalEnemiesInScene = 0;

        CancelInvoke();
        InvokeRepeating("LaunchEnemyShips", 0.0f, 5.0f);
        InvokeRepeating("SendPowerups", 5.0f, 10.0f);
        enemySpeed = initialEnemySpeed;
    }

    public void EnemyKilled() {
        totalEnemiesInScene -= 1;
    }

    private void LaunchEnemyShips() {
        if (!gameManager.gameOver && !pauseSpawning) {
            CreateNewEnemy();
            totalEnemiesSpawned += 1;
            totalEnemiesInScene += 1;
        }
    }

    private void SendPowerups() {
        if (!gameManager.gameOver) {
            CreateNewPowerup();
        }
    }

    private GameObject CreateNewEnemy() {
        GameObject enemyClone = Instantiate(enemyShip, transform.position, Quaternion.identity);
        EnemyAI enemyAI = enemyClone.GetComponent<EnemyAI>();

        if (enemySpeedIncrementMap.ContainsKey(totalEnemiesSpawned)) {
            enemySpeed = enemySpeed + enemySpeedIncrementMap[totalEnemiesSpawned];
        }

        enemyAI.speed = enemySpeed;

        return enemyClone;
    }

    private GameObject CreateNewPowerup() {
        GameObject randomPowerup = powerups[Random.Range(0, 3)];
        return Instantiate(randomPowerup, transform.position, Quaternion.identity);
    }

    private void DestroyAllSpawnupsInScene() {
        GameObject[] powerupsInScene = GameObject.FindGameObjectsWithTag("Powerup");
        foreach (GameObject powerup in powerupsInScene) {
            Destroy(powerup.gameObject);
        }

        GameObject[] enemiesInScene = GameObject.FindGameObjectsWithTag("EnemyShip");
        foreach (GameObject enemy in enemiesInScene) {
            Destroy(enemy.gameObject);
        }
    }
}
