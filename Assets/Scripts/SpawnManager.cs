using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    [SerializeField] private GameObject playerShip;
    [SerializeField] private GameObject enemyShip;
    [SerializeField] private GameObject[] powerups;

    private bool playerDead;
    private UIManager uiManager;

    // Use this for initialization
    void Start() {
        playerDead = true;
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        InvokeRepeating("LaunchEnemyShip", 0.0f, 5.0f);
        InvokeRepeating("SendPowerups", 5.0f, 5.0f);
    }

    public void PlayerDeadSequence() {
        playerDead = true;
        uiManager.ShowMainMenu();
    }

    private void Update() {
        bool gameStartPressed = Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0);

        if (playerDead && gameStartPressed) {
            StartGame();
        }
    }

    private void StartGame() {
        uiManager.HideMainMenu();
        uiManager.ResetUI();
        SpawnPlayer();
    }

    private void SpawnPlayer() {
        playerDead = false;
        Vector3 position = new Vector3(0, -3, 0);
        Instantiate(playerShip, position, Quaternion.identity);
    }

    private void LaunchEnemyShip() {
        Instantiate(enemyShip, transform.position, Quaternion.identity);
    }

    private void SendPowerups() {
        GameObject randomPowerup = powerups[Random.Range(0, 3)];
        Instantiate(randomPowerup, transform.position, Quaternion.identity);
    }
}
