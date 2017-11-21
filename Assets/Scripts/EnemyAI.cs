using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {
    private const float minX = -7.0f;
    private const float maxX = 7.0f;

    [SerializeField] private float speed = 5.0f;
    [SerializeField] private GameObject explosionPrefab;

    // Use this for initialization
    void Start() {
        RandomizePosition();
    }

    // Update is called once per frame
    void Update() {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if (transform.position.y < -7.0f) {
            RandomizePosition();
        }
    }

    private void RandomizePosition() {
        float positionX = Random.Range(minX, maxX);
        transform.position = new Vector3(positionX, 7.0f, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            Player player = collision.GetComponent<Player>();

            if (player != null) {
                player.CauseDamage();

                Destroy(this.gameObject);
            }
        } else if (collision.tag == "Laser") {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy() {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    }
}
