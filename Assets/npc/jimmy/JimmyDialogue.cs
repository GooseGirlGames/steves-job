using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JimmyDialogue : MonoBehaviour
{
    public SceneLoader sceneLoader;
    public 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Collided!");
        if (other.gameObject.CompareTag("Player")) {
            Debug.Log("with player");
            Trigger();
        }
    }

    public void EnterMiniGame() {
        sceneLoader.TriggerSceneLoad();
    }

    [SerializeField]
    public Dialogue dialogue;
    public void Trigger() {
        DialogueManager.Instance.StartDialogue(dialogue);
    }
}
