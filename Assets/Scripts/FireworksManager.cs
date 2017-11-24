using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworksManager : MonoBehaviour {
    private List<GameObject> fireworks = new List<GameObject>();

    [SerializeField] private AudioClip applauseClip;
    [SerializeField] private AudioClip fireworksClip;
    private AudioSource mainGameMusic;

    // Use this for initialization
    void Start() {
        foreach (Transform child in transform) {
            fireworks.Add(child.gameObject);
        }
        mainGameMusic = Camera.main.transform.GetComponent<AudioSource>();
    }

    public void ActivateFireworks() {
        gameObject.SetActive(true);
        Shuffle(fireworks);
        StartCoroutine(ActivateFirework(0));
        Invoke("PlayWinSound", 0.5f);
    }

    public void DeactivateFireworks() {
        gameObject.SetActive(false);
        foreach (GameObject firework in fireworks) {
            firework.SetActive(false);
        }
    }

    private IEnumerator ActivateFirework(int index) {
        yield return new WaitForSeconds(0.1f);
        fireworks[index].SetActive(true);

        index += 1;
        if (index < fireworks.Count) {
            StartCoroutine(ActivateFirework(index));
        }
    }

    private void PlayWinSound() {
        mainGameMusic.Stop();
        mainGameMusic.PlayOneShot(applauseClip);
        mainGameMusic.PlayOneShot(fireworksClip);
    }

    private static void Shuffle(List<GameObject> objects) {
        var count = objects.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i) {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = objects[i];
            objects[i] = objects[r];
            objects[r] = tmp;
        }
    }
}
