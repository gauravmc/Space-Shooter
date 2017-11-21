using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Powerup : MonoBehaviour {
    [SerializeField] private float speed = 3.0f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player player = collision.GetComponent<Player>();

            if (player != null) {
                ActivatePowerOnPlayer(player);

                Destroy(this.gameObject);
            }
        }
    }

    abstract public void ActivatePowerOnPlayer(Player player);
}
