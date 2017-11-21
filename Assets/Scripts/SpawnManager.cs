using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    [SerializeField] private GameObject enemyShip;
    [SerializeField] private GameObject[] powerups;

    // Use this for initialization
    void Start() {
        InvokeRepeating("LaunchEnemyShip", 0.0f, 5.0f);
        InvokeRepeating("SendPowerups", 5.0f, 5.0f);
    }

    private void LaunchEnemyShip() {
        Instantiate(enemyShip, transform.position, Quaternion.identity);
    }

    private void SendPowerups() {
        GameObject randomPowerup = powerups[Random.Range(0, 3)];
        Instantiate(randomPowerup, transform.position, Quaternion.identity);
    }
}
