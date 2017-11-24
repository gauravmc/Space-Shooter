using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField] private GameObject playerShip;
    [SerializeField] private GameObject fireworksObject;

    public bool gameOver = true;
    public int totalEnemiesLeft;

    private UIManager uiManager;
    private SpawnManager spawnManager;
    private GameObject playerShipClone;
    private AudioSource mainGameMusic;
    private FireworksManager fireworksManager;

    void Start() {
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        spawnManager = GameObject.Find("Spawner").GetComponent<SpawnManager>();
        fireworksManager = fireworksObject.GetComponent<FireworksManager>();
        mainGameMusic = Camera.main.transform.GetComponent<AudioSource>();

        ResetScore();
    }

    private void Update() {
        bool spacePressed = Input.GetKeyDown(KeyCode.Space);

        if (gameOver && (spacePressed || Input.touchCount > 0)) {
            StartGame();
        }
    }

    public void PlayerDied() {
        Invoke("GameOverSequence", 1.0f);
    }

    public void EnemyKilled() {
        UpdateScore();
        spawnManager.EnemyKilled();
    }

    private void UpdateScore() {
        totalEnemiesLeft -= 1;
        uiManager.ShowScore(totalEnemiesLeft);

        if (totalEnemiesLeft == 0) {
            Invoke("GameWinSequence", 1.0f);
        }
    }

    private void GameOverSequence() {
        ResetScore();
        uiManager.ShowMainMenu();
        gameOver = true;
        mainGameMusic.pitch = 0.8f;
        mainGameMusic.Play();
    }

    private void GameWinSequence() {
        if (playerShipClone == null) {
            return;
        }

        Destroy(playerShipClone.gameObject);
        fireworksManager.ActivateFireworks();

        StartCoroutine(RestartGameMenu());
    }

    private void StartGame() {
        mainGameMusic.pitch = 1;
        uiManager.ResetUI(totalEnemiesLeft);
        gameOver = false;
        SpawnPlayer();
        spawnManager.GameStarted();
    }

    private IEnumerator RestartGameMenu() {
        yield return new WaitForSeconds(10);

        fireworksManager.DeactivateFireworks();
        GameOverSequence();
    }

    private void ResetScore() {
        totalEnemiesLeft = spawnManager.maximumEnemies;
        uiManager.ShowScore(totalEnemiesLeft);
    }

    private void SpawnPlayer() {
        playerShipClone = Instantiate(playerShip, new Vector3(0, -3.5f, 0), Quaternion.identity);
    }
}
