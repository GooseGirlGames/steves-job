using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Intro : MonoBehaviour {
    public Portal enterVoid;
    public TextMeshProUGUI textUI;
    private const float LETTER_DELAY = 0.03f;
    private const float SENTENCE_DELAY = 3f;
    private List<string> texts = new List<string>{
        "You're Steve, working as a janitor at an ordinary mall.",
        "After one particularly long day of work,",
        "just as you want off the main lights to the mall...",
        "...you flick the disused, old switch next to the light switch.",
        "That switch must have gone unused for decades...",
        "...but only now do you notice the yellowed printout above it:",
        "\"DO NOT TURN OFF\n                                            \nHOLDS REALITY TOGETHER\"",
        "                        \nOops..."
    };

    void Start() {
        StartCoroutine(IntroSequence());
    }
    
    IEnumerator IntroSequence() {
        foreach (string text in texts) {
            textUI.text = "";
            foreach (char c in text) {
                textUI.text += c;
                yield return new WaitForSeconds(LETTER_DELAY);
            }
            yield return new WaitForSeconds(SENTENCE_DELAY);
        }
        DialogueManager.Instance.SetInstantTrue();
        enterVoid.TriggerTeleport();
    }
}
