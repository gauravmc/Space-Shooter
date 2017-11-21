using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private float fireRate = 0.25f;

    private float nextFireTime = 0.0f;

	// Use this for initialization
	void Start () {
        transform.position = new Vector3(0, -3, 0);
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
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFireTime) {
            nextFireTime = Time.time + fireRate;
            Instantiate(laserPrefab, transform.position + new Vector3(0, 0.7f, 0), Quaternion.identity);
        }
    }
}
