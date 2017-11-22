using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField] private GameObject playerShip;

    private bool gameOver;
    private UIManager uiManager;

    // Use this for initialization
    void Start () {
        gameOver = true;
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    private void Update() {
        bool gameStartPressed = Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0);

        if (gameOver && gameStartPressed) {
            StartGame();
        }
    }

    private void StartGame() {
        uiManager.HideMainMenu();
        uiManager.ResetUI();
        gameOver = false;
        SpawnPlayer();
    }

    private void SpawnPlayer() {
        Instantiate(playerShip, new Vector3(0, -3, 0), Quaternion.identity);
    }

    public void PlayerDeadSequence() {
        uiManager.ShowMainMenu();
        gameOver = true;
    }
}
