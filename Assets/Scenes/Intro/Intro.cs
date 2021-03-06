using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Intro : MonoBehaviour {
    public Portal enterVoid;
    public TextMeshProUGUI textUI;

    public TextMeshProUGUI contentWarningUI;
    private const string contentWarning = "(features gore and strong language)";
    private const float CONTENT_WARNING_TIME = 2.0f;
    private const float LETTER_DELAY = 0.03f;
    private const float SENTENCE_DELAY = 3f;
    private List<string> texts = new List<string>{
        "You're Steve, working as a janitor at an ordinary mall.",
        "After one particularly long day of work,",
        "just as you want to turn off the main lights to the mall...",
        "...you flick the old, never-used switch next to the light switch.",
        "That switch must have gone unused for decades...",
        "...but only now do you notice the yellowed printout above it:",
        "\"DO NOT TURN OFF\n                                            \nHOLDS REALITY TOGETHER\"",
        "                    \nOops..."
    };

    void Start() {
        StartCoroutine(IntroSequence());
    }
    
    IEnumerator IntroSequence() {
        textUI.text = "";
        contentWarningUI.text = "";
        foreach (char c in contentWarning) {
            contentWarningUI.text += c;
            yield return new WaitForSeconds(LETTER_DELAY);
        }
        yield return new WaitForSeconds(CONTENT_WARNING_TIME);
        foreach (char c in contentWarning) {
            contentWarningUI.text = contentWarningUI.text.Substring(1);
            yield return new WaitForSeconds(LETTER_DELAY);
        }
        yield return new WaitForSeconds(SENTENCE_DELAY / 4.0f);

        foreach (string text in texts) {
            textUI.text = "";
            foreach (char c in text) {
                textUI.text += c;
                yield return new WaitForSeconds(LETTER_DELAY);
            }
            yield return new WaitForSeconds(SENTENCE_DELAY);
        }
        End();
    }

    private void End() {
        DialogueManager.Instance.SetInstantTrue();
        enterVoid.TriggerTeleport();
    }

    private void Update() {
        if (Input.GetKey(KeyCode.Escape)) {
            End();
        }
    }
}
