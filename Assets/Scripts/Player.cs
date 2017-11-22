using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private GameObject tripleLaserPrefab;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private GameObject shieldObject;
    [SerializeField] private float fireRate = 0.25f;

    private float nextFireTime = 0.0f;
    private bool canTripleShot = false;
    private bool shiledIsActive = false;
    private int livesLeft = 3;
    private UIManager uiManager;
    private List<GameObject> playerInjuries = new List<GameObject>();

    // Use this for initialization
    void Start () {
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        uiManager.UpdatePlayerLives(livesLeft);

        foreach (Transform child in transform) {
            if (child.tag == "PlayerInjury") {
                playerInjuries.Add(child.gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update () {
        Movement();
        Shooting();
    }

    private void Movement() {
        float horizontalInput = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.right * speed * horizontalInput * Time.deltaTime);

        if (transform.position.x > 9.5f) {
            transform.position = new Vector3(-9.5f, transform.position.y, 0);
        }
        else if (transform.position.x < -9.5f) {
            transform.position = new Vector3(9.5f, transform.position.y, 0);
        }
    }

    private void Shooting() {
        bool shootKeyPressed = Input.GetKeyDown(KeyCode.Space);

        if (shootKeyPressed && Time.time > nextFireTime) {
            nextFireTime = Time.time + fireRate;
            ShootLasers();
        }
    }

    public void ShootLasers() {
        if (canTripleShot) {
            Instantiate(tripleLaserPrefab, transform.position, Quaternion.identity);
        }
        else {
            Instantiate(laserPrefab, transform.position + new Vector3(0, 0.7f, 0), Quaternion.identity);
        }
    }

    public void CauseDamage() {
        if (shiledIsActive) {
            DeactivateShield();
        } else {
            if (livesLeft == 0) {
                Die();
            } else {
                livesLeft -= 1;
                ShowInjury();
                uiManager.UpdatePlayerLives(livesLeft);
            }
        }
    }

    private void ShowInjury() {
        if (livesLeft < 0) {
            return; 
        }

        int randIndex = Random.Range(0, playerInjuries.Count);
        GameObject injury = playerInjuries[randIndex];
        if (!injury.activeSelf) {
            injury.SetActive(true);
        } else {
            ShowInjury();
        }
    }

    public void ActivateTripleShot() {
        canTripleShot = true;
        StartCoroutine(EnableTripleShotPowerDown());
    }

    private IEnumerator EnableTripleShotPowerDown() {
        yield return new WaitForSeconds(3);
        canTripleShot = false;
    }

    public void ActivateSpeedBoost() {
        float originalSpeed = speed;
        speed = speed * 2f;
        StartCoroutine(EnableSpeedBoostPowerDown(originalSpeed));
    }

    private IEnumerator EnableSpeedBoostPowerDown(float originalSpeed) {
        yield return new WaitForSeconds(3);
        speed = originalSpeed;
    }

    public void ActivateShield() {
        shiledIsActive = true;
        shieldObject.SetActive(true);
    }

    public void DeactivateShield() {
        shiledIsActive = false;
        shieldObject.SetActive(false);
    }

    private void Die() {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        NotifyGameManagerAboutDeath();
        Destroy(this.gameObject);
    }

    private void NotifyGameManagerAboutDeath() {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.GameOverSequence();
    }
}
