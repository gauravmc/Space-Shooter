using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField] private GameObject playerShip;
    [SerializeField] private GameObject fireworks;

    public bool gameOver = true;
    private UIManager uiManager;
    private SpawnManager spawnManager;
    private GameObject playerShipClone;

    private int totalEnemiesLeft = 2;
    private int totalEnemiesInPlay = 0;

    // Use this for initialization
    void Start () {
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        spawnManager = GameObject.Find("Spawner").GetComponent<SpawnManager>();
        uiManager.ShowScore(totalEnemiesLeft);
    }

    public void GameOverSequence() {
        uiManager.ShowMainMenu();
        gameOver = true;
    }

    public void UpdateScore() {
        totalEnemiesLeft -= 1;
        uiManager.ShowScore(totalEnemiesLeft);

        if (totalEnemiesLeft == 0) {
            Invoke("GameWinSequence", 0.5f);
        }
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

    private void GameWinSequence() {
        Destroy(playerShipClone.gameObject);
        gameOver = true;
        Instantiate(fireworks, Vector3.zero, Quaternion.identity);
    }

    private void Update() {
        bool gameStartPressed = Input.GetKeyDown(KeyCode.Space);

        if (gameOver && gameStartPressed) {
            StartGame();
        }
    }

    private void StartGame() {
        uiManager.ResetUI(totalEnemiesLeft);
        gameOver = false;
        SpawnPlayer();
        spawnManager.GameStarted();
    }

    private void SpawnPlayer() {
        playerShipClone = Instantiate(playerShip, new Vector3(0, -3.5f, 0), Quaternion.identity);
    }
}
