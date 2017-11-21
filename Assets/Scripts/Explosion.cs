using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(DestroySelf());
	}
	
    private IEnumerator DestroySelf() {
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }
}
