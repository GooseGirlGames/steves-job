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
    - "well can I do something else for your?"
    - Itemoptions:
        - dirty shirt   -> clean shirt
        - clean shirt   -> clean shirt "nothing to here *cries* "
        - shirt         -> clean shirt
        - babelfish     -> "a snack!" (!remove it!)
        - bloodbucked   -> bucked "i cleaned it for your, it is good to go"
        - bloody marry  -> "Sorry ... I'm not that.. Thirsty?? ... uff"
    
Dialogue2(after having the bucked):
    - ... (boring idle doialogue)  ...
    - Hallo ... Do your have something to clean for me?
    - I really need customers.. if your find someone dirty send him to me!!!
    - Itemoptions:
        - dirty shirt   -> clean shirt
        - clean shirt   -> clean shirt "nothing to here *cries* "
        - shirt         -> clean shirt
        - babelfish     -> "a snack!" (!remove it!)
        - bloodbucked   -> bucked "i cleaned it for your, it is good to go"
        - bloody marry  -> "Sorry ... I'm not that.. Thirsty?? ... uff"


Dialogue3(wenn peasents dirty sind, maybe getriggert durch finalitem or something):
    - "OMG finaly some custimors, thats all I ever want in my life"
    - "my purpose is finaly fullfilled"
    - "Thank your so much janitor"
    - "here for your hard work, all my money!!" --> gets a single coin
    - "sorry it is not more i dont take money for claening ever"
    - "Ah whats happening bild fadet in schwarz und part der mall fehlt"
    
    */
public class JimmyTheCatDialogue : DialogueTrigger{
    public static new JimmyTheCatDialogue Instance = null;
    public Item bucket;
    public Item bucketfull;
    public Item shirt;
    public Item clean_shirt;
    public Item dirty_shirt;
    public Item babelfish;
    public Item bloody_mary;
    
    
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
