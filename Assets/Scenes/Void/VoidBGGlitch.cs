using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidBGGlitch : MonoBehaviour {
    public Sprite normal;
    public List<Sprite> glitched;
    new private SpriteRenderer renderer;
    void Start() {
        renderer = GetComponent<SpriteRenderer>();
        StartCoroutine(Glitched());
    }

    private IEnumerator Glitched() {
        while (true) {
            yield return new WaitForSeconds(Random.Range(2.6f, 4.2f));
            int frameIdx = Random.Range(0, glitched.Count - 1);
            Sprite glitch = glitched[frameIdx];
            renderer.sprite = glitch;
            yield return new WaitForSeconds(Random.Range(0.06f, 0.09f));
            renderer.sprite = normal;
        }
    }
}
