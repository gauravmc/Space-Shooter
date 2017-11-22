using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
        InvokeRepeating("ToggleActiveState", 0.0f, 0.2f);
	}
	
    private void ToggleActiveState() {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
