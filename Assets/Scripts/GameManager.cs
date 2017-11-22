using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField] private GameObject playerShip;

    private bool gameOver;
    private UIManager uiManager;
    private SpawnManager spawnManager;

    private int playerScore;
    private int totalEnemiesInPlay = 0;

    // Use this for initialization
    void Start () {
        gameOver = true;
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        spawnManager = GameObject.Find("Spawner").GetComponent<SpawnManager>();
    }

    public void GameOverSequence() {
        uiManager.ShowMainMenu();
        gameOver = true;
    }

    public void UpdateScore() {
        playerScore += 10;
        uiManager.ShowScore(playerScore);
    }

    public void BumpEnemyCount() {
        totalEnemiesInPlay += 1;
    }

    public void DecrementEnemyCount() {
        totalEnemiesInPlay -= 1;
    }

    public bool TooManyEnemiesOnScreen() {
        return totalEnemiesInPlay > 5;
    }

    private void Update() {
        bool gameStartPressed = Input.GetKeyDown(KeyCode.Space);

        if (gameOver && gameStartPressed) {
            StartGame();
        }
    }

    private void StartGame() {
        uiManager.HideMainMenu();
        uiManager.ResetUI();
        gameOver = false;
        SpawnPlayer();
        spawnManager.SpawnAll();
    }

    private void SpawnPlayer() {
        Instantiate(playerShip, new Vector3(0, -3.5f, 0), Quaternion.identity);
    }
}
