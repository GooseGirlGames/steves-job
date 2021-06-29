using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Personality: jimmy the cat is a Cat, he likes cat stuff and cleaning. and is bored becouse he has
                no buissnes he is happy to help ur out He loves Steve because steve is still cleanable
                
Dialog1(first Approach): 
    - Awkwarder Anfang weill ers sagt das steve schmutzig ist
    - entschuldigt sich
    - Then He offers your to clean anything your need
    - jimmy then he explains his situatuen, that everything is so clean that he is basicly without purpose
    - he dont want money because it is his live purpose
    - !!! giftes ur the bucket because he has no use for it !!!
    - "Can I do something else for your?"
    - 
    
Dialogue2*/
public class JimmyTheCatDialogue : DialogueTrigger{
    public static new JimmyTheCatDialogue Instance = null;
    public Item bucket;
    public Item bucketfull;
    public Item shirt;
    public Item clean_shirt;
    public Item dirty_shirt;
    public Item babelfish;
    
    
    private void Awake() {
        Instance = this;
    }

    public override Dialogue GetActiveDialogue(){
        return new StartDialogue();
    }

    public class StartDialogue : Dialogue {
        public StartDialogue(){
            Say("Miau");
        }
    }
}
