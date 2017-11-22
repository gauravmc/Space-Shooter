using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Powerup : MonoBehaviour {
    private const float minX = -7.0f;
    private const float maxX = 7.0f;

    [SerializeField] private float speed = 3.0f;
    [SerializeField] private AudioClip powerupClip;

    void Start() {
        RandomizePosition();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if (transform.position.y < -7.0f) {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player player = collision.GetComponent<Player>();

            if (player != null) {
                ActivatePowerOnPlayer(player);
                AudioSource.PlayClipAtPoint(powerupClip, Camera.main.transform.position);

                Destroy(this.gameObject);
            }
        }
    }

    private void RandomizePosition() {
        float positionX = Random.Range(minX, maxX);
        transform.position = new Vector3(positionX, 7.0f, 0);
    }

    abstract public void ActivatePowerOnPlayer(Player player);
}
