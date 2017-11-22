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

    public void UpdatePlayerLives(int livesLeft) {
        playerLives.sprite = livesSprites[livesLeft];
    }

    public void ShowScore(int score) {
        scoreText.text = "Score: " + score;
    }

    public void ShowMainMenu() {
        mainMenu.SetActive(true);
    }

    public void HideMainMenu() {
        mainMenu.SetActive(false);
        ResetUI();
    }

    private void ResetUI() {
        ShowScore(0);
        UpdatePlayerLives(allLivesCode);
    }
}
