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

    // Use this for initialization
    void Start () {
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        uiManager.UpdatePlayerLives(livesLeft);
    }

    // Update is called once per frame
    void Update () {
        Movement();
        Shooting();
    }

    private void Movement() {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * speed * horizontalInput * Time.deltaTime);
        transform.Translate(Vector3.up * speed * verticalInput * Time.deltaTime);

        if (transform.position.y > 0) {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -4.5f) {
            transform.position = new Vector3(transform.position.x, -4.5f, 0);
        }

        if (transform.position.x > 9.5f) {
            transform.position = new Vector3(-9.5f, transform.position.y, 0);
        }
        else if (transform.position.x < -9.5f) {
            transform.position = new Vector3(9.5f, transform.position.y, 0);
        }
    }

    private void Shooting() {
        bool shootKeyPressed = Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0);

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
                uiManager.UpdatePlayerLives(livesLeft);
            }
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
        speed = speed * 1.5f;
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
        NotifySpawnManagerAboutDeath();
        Destroy(this.gameObject);
    }

    private void NotifySpawnManagerAboutDeath() {
        SpawnManager spawnManager = GameObject.Find("Spawner").GetComponent<SpawnManager>();
        spawnManager.PlayerDeadSequence();
    }
}
