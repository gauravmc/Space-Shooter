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

    private GameObject[] instructionObjects;

    void Start() {
        instructionObjects = GameObject.FindGameObjectsWithTag("Instruction");
    }

    public void UpdatePlayerLives(int livesLeft) {
        playerLives.sprite = livesSprites[livesLeft];
    }

    public void ShowScore(int score) {
        scoreText.text = "Enemies left: " + score;
    }

    public void ShowMainMenu() {
        mainMenu.SetActive(true);
    }

    public void ResetUI(int score) {
        mainMenu.SetActive(false);
        HideInstrcutions();
        ShowScore(score);
        UpdatePlayerLives(allLivesCode);
    }

    private void HideInstrcutions() {
        foreach (GameObject instruction in instructionObjects) {
            if (instruction.activeSelf) {
                instruction.SetActive(false);
            }
        }
    }
}
