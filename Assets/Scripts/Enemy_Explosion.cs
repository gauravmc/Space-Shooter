using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Explosion : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(DestroyExplosionObject());
	}
	
    private IEnumerator DestroyExplosionObject() {
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }
}
