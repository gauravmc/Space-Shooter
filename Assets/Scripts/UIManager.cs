using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    private const int allLivesCode = 3;

    [SerializeField] private Sprite[] livesSprites;
    [SerializeField] private Image playerLives;
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject mainMenu;

    private int playerScore;

    public void ResetUI() {
        ResetScore(0);
        UpdatePlayerLives(allLivesCode);
    }

    public void UpdatePlayerLives(int livesLeft) {
        playerLives.sprite = livesSprites[livesLeft];
    }

    public void UpdateScore() {
        int newScore = playerScore += 10;
        ResetScore(newScore);
    }

    public void ShowMainMenu() {
        mainMenu.SetActive(true);
    }

    public void HideMainMenu() {
        mainMenu.SetActive(false);
    }

    private void ResetScore(int score) {
        playerScore = score;
        scoreText.text = "Score: " + playerScore;
    }
}
