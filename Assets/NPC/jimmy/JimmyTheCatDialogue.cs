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
    public Item finished;
    
    private void Awake() {
        Instance = this;
    }

    public override Dialogue GetActiveDialogue(){
        if (Inventory.Instance.HasItem(finished)){
            return new FinalDialogue();
        }
        else if (Inventory.Instance.HasItem(bucket)||Inventory.Instance.HasItem(bucketfull)){
            return new DefaultDialogue();
        }
        return new StartDialogue();
    }
/* ---------------------------------------------------------------------------------------------------------------- */
    public class StartDialogue : Dialogue {
        public StartDialogue(){
            Say("Oh meow ... ");
            Say("...");
            Say("Your pretty Dirty! mauz!")
                .Choice(new TextOption("..."))
                .Choice(new TextOption("..."))
                .Choice(new TextOption("..."))
                .Choice(new TextOption("..."));
            Say("This is ...");
            Say("...");
            Say("MEOW! PURRRRRRRRRRRRRFECT!!!!! UwU");
            Say("I love cleaning! *purrr* If your have anything, and I mean ANYTHING I should clean for you, just say it :3");
            Say("I won't even charge you anything, this is just my passion");
            Say("...");
            Say("... *sad meoww* ...");
            Say("*murr* an here is my dilemma, since this world is so clean and sparkly, nobody ever gets dirty....");
            Say("Im basically without purpose in this cute world meow");
            Say("Ah I wish I could have some customeowrrs...");
            Say("Anymeow, since I have no use for it, take this bucket *purrr*")
                .DoAfter(GiveItem(JimmyTheCatDialogue.Instance.bucket));
            Say("...")
                .DoAfter(new TriggerDialogueAction<ItemDialogue>());
        }
    }
    public class ItemDialogue:Dialogue {
        public ItemDialogue(){
            Say("So, meeeeow can I help your with anything else out?")
                .Choice(new TextOption("...")
                    .IfChosen(new TriggerDialogueAction<Goodbye>()))
                .Choice(new ItemOption(JimmyTheCatDialogue.Instance.bucketfull)
                    .IfChosen(new TriggerDialogueAction<bucketfullDialogue>()))
                .Choice(new ItemOption(JimmyTheCatDialogue.Instance.bloody_mary)
                    .IfChosen(new TriggerDialogueAction<bloodymaryDialogue>()))
                .Choice(new ItemOption(JimmyTheCatDialogue.Instance.shirt)
                    .IfChosen(new TriggerDialogueAction<shirtDialogue>()))
                .Choice(new ItemOption(JimmyTheCatDialogue.Instance.clean_shirt)
                    .IfChosen(new TriggerDialogueAction<cleanshirtDialogue>()))
                .Choice(new ItemOption(JimmyTheCatDialogue.Instance.dirty_shirt)
                    .IfChosen(new TriggerDialogueAction<dirtyDialogue>()))
                .Choice(new ItemOption(JimmyTheCatDialogue.Instance.babelfish)
                    .IfChosen(new TriggerDialogueAction<babelDialogue>()))
                .Choice(new OtherItemOption()
                    .IfChosen(new TriggerDialogueAction<otherItemDialogue>()));
        }
    }
    public class Goodbye : Dialogue {
        public Goodbye(){
            Say("Hmm... well I'll take this as a no, meow");
            Say("See your soon, *purrrrrr*");
        }
    }    
    public class bucketfullDialogue : Dialogue {
        public bucketfullDialogue(){
            Say("Oh This thing is nasty, *hissss*");
            Say("Here, I clean it for you")
                .DoAfter(GiveItem(JimmyTheCatDialogue.Instance.bucket));
            Say("meow.. if just all this goo where on some people so I have more customers..");
            Say("....meow *sniff*...")
                .DoAfter(new TriggerDialogueAction<ItemDialogue>());
        }
    }
    public class bloodymaryDialogue : Dialogue {
        public bloodymaryDialogue(){
            Say("*Hiss* ... meow.. sorry no I'm really not thirsty");
            Say("But this fish I will take, *purrrrr*")
                .If(HasItem(JimmyTheCatDialogue.Instance.babelfish));
            Say(" ... ")
                .DoAfter(new TriggerDialogueAction<ItemDialogue>());
        }
    }
    public class shirtDialogue : Dialogue {
        public shirtDialogue(){
            Say("Oh meow, this thing is filthy... just as I love it :3");
            Say("*purrr* I clean this for you meow")
                .DoAfter(GiveItem(JimmyTheCatDialogue.Instance.clean_shirt))
                .DoAfter(new TriggerDialogueAction<ItemDialogue>());
        }
    }
    public class cleanshirtDialogue : Dialogue {
        public cleanshirtDialogue(){
            Say("*sigh* my own work, is purrrfect...");
            Say("meow not much more to do here sadly");
            Say("murr ....")
                .DoAfter(new TriggerDialogueAction<ItemDialogue>());
        }
    }
    public class dirtyDialogue : Dialogue {
        public dirtyDialogue(){
            Say("Urgh.. meow... wow this is.. this is murrrr than dirty meow");
            Say("A TRUE CHALLENGE!!!");
            Say("Give me one second");
            Say("here you go meow meow meow ...")
                .DoAfter(GiveItem(JimmyTheCatDialogue.Instance.clean_shirt))
                .DoAfter(new TriggerDialogueAction<ItemDialogue>());
        }
    }
    public class otherItemDialogue : Dialogue {
        public otherItemDialogue(){
            Say("sadly I cant clean this murrrrr")
                .DoAfter(new TriggerDialogueAction<ItemDialogue>());
        }
    }

    public class babelDialogue : Dialogue {
        public babelDialogue(){
            Say("meeeeow, *purrr* yes give me that");
            Say("chomp chomp chomp");
            Say("Delicious, thanks, meow")
                .DoAfter(RemoveItem(JimmyTheCatDialogue.Instance.babelfish))
                .DoAfter(new TriggerDialogueAction<ItemDialogue>());
        }
    }

/* ---------------------------------------------------------------------------------------------------------------- */
    public class DefaultDialogue : Dialogue {
        public DefaultDialogue(){
            Say("Miau, hey again")
                .DoAfter(new TriggerDialogueAction<ItemDialogue>());
        }
    }
/* ---------------------------------------------------------------------------------------------------------------- */
    public class FinalDialogue : Dialogue {
        public FinalDialogue(){
            Say("meow Thanks Janitor");
            Say("*purrr* Now I can finally clean people again");
            Say("I finally have a purrrrrrrrrrrpuss again");
            Say("*hissss* Ah, whats happening ...");
            Say(".....");
        }
    }
/* ---------------------------------------------------------------------------------------------------------------- */

}
