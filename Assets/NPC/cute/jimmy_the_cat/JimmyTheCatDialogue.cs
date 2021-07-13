using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Personality: jimmy the cat is a Cat, he likes cat stuff and cleaning. and is bored becouse he has
                no buissnes he is happy to help ur out He loves Steve because steve is still cleanable

------------

NOTE: the following info may or may not be outdated:

Dialog1(first Approach): 
    - Awkwarder Anfang weill ers sagt das steve schmutzig ist
    - entschuldigt sich
    - Then He offers your to clean anything your need
    - jimmy then he explains his situatuen, that everything is so clean that he is basicly without purpose
    - he dont want money because it is his live purpose
    - "well can I do something else for your?"
    - Itemoptions:
        - dirty shirt   -> dirty maid dress
        - shirt         -> maid dress
        - bloodbucked   -> cute bucked
        - bloody marry  -> "Sorry ... I'm not that.. Thirsty?? ... uff"
    
Dialogue2(after having the bucked):
    - ... (boring idle doialogue)  ...
    - Hallo ... Do your have something to clean for me?
    - I really need customers.. if your find someone dirty send him to me!!!
    - Itemoptions:
        see above


Dialogue3(wenn peasents dirty sind, maybe getriggert durch finalitem or something):
    - "OMG finaly some custimors, thats all I ever want in my life"
    - "my purpose is finaly fullfilled"
    - "Thank your so much janitor"
    - "here for your hard work, all my money!!"
    - "sorry it is not more i dont take money for claening ever"
    - "Ah whats happening bild fadet in schwarz und part der mall fehlt"
    
    */
public class JimmyTheCatDialogue : DialogueTrigger {
    public static JimmyTheCatDialogue t = null;
    public Item bucket;
    public Item bucketcute;
    public Item bucketfull;
    public Item bucketcutefull;
    public Item shirt;
    public Item clean_maiddress;
    public Item dirty_maiddress;
    public Item dirty_shirt;
    public Item babelfish;
    public Item bloody_mary;
    public Item finished;
    public Item _jimmycat_said_thanks;
    public Item goose;
    public Item goosebloody;
    public Item goosebow;
    public Item goosebloddybow;
    public Item switch_broken_cute;
    public Item switch_broken;
    public Item switch_broken_horror;
    public Item coin_start;
    public Item coin_horror;
    public Item coin_cute;

    
    private void Awake() {
        t = this;
    }

    public override Dialogue GetActiveDialogue(){
        if (Inventory.Instance.HasItem(finished)){
            if (!Inventory.Instance.HasItem(_jimmycat_said_thanks)) {
                return new FinalDialogue();
            } else {
                return new ItemDialogue();
            }
        }
        else if (Inventory.Instance.HasItem(bucket)||Inventory.Instance.HasItem(bucketfull)){
            return new DefaultDialogue();
        }
        return new StartDialogue();
    }
/* ---------------------------------------------------------------------------------------------------------------- */
    public class StartDialogue : Dialogue {
        public StartDialogue(){
            Say("Oh meow... ");
            Say("...");
            Say("You're pretty rirty! Mauz!")
                .Choice(new TextOption("huh?"));
            Say("This is...");
            Say("...");
            Say("MEOW! PURRRRRRRRRRRRRFECT!!!!! UwU");
            Say("I love cleaning! *purrr* If you have anything, and I mean ANYTHING I should clean for you, just say it :3");
            Say("I won't even charge you anything, this is just my passion.");
            Say("...");
            Say("... *sad meoww* ...");
            Say("*murr* and here is my dilemma: Since this world is so clean and sparkly, nobody ever gets dirty....");
            Say("Im basically without purpose in this cute world meow");
            Say("Ah, I wish I could have some customeowrrs...");
            Say("...")
                .DoAfter(new TriggerDialogueAction<ItemDialogue>());
        }
    }
    public class ItemDialogue:Dialogue {
        public ItemDialogue(){

            Say("Thank you sooo much!  I am forever indebted to you *purrr*")
            .If(HasItem(t._jimmycat_said_thanks));

            Say("So, meeeeow can I help you out with anything else?")
                .Choice(new TextOption("Maybe later")
                    .IfChosen(new TriggerDialogueAction<Goodbye>()))
                //
                .Choice(new ItemOption(t.bucket) // -> bucketcute
                    .IfChosen(new TriggerDialogueAction<bucketDialogue>()))
                .Choice(new ItemOption(t.bucketfull)  // -> bucketcutefull
                    .IfChosen(new TriggerDialogueAction<bucketfullDialogue>()))
                //
                .Choice(new ItemOption(t.bloody_mary)
                    .IfChosen(new TriggerDialogueAction<bloodymaryDialogue>()))
                .Choice(new ItemOption(t.shirt)
                    .IfChosen(new TriggerDialogueAction<shirtToMaiddressDialogue>()))
                .Choice(new ItemOption(t.dirty_shirt)
                    .IfChosen(new TriggerDialogueAction<dirtyShirtToMaiddressDialogue>()))
                .Choice(new ItemOption(t.clean_maiddress)
                    .IfChosen(new TriggerDialogueAction<ContentWithOwnWork>()))
                .Choice(new ItemOption(t.dirty_maiddress)
                    .IfChosen(new TriggerDialogueAction<ContentWithOwnWork>()))

                //.Choice(new ItemOption(t.babelfish)  TODO cat food
                //    .IfChosen(new TriggerDialogueAction<babelDialogue>()))
                // Geese
                .Choice(new ItemOption(t.goose)
                    .IfChosen(new TriggerDialogueAction<gooseDialogue>()))
                .Choice(new ItemOption(t.goosebloody)
                    .IfChosen(new TriggerDialogueAction<goosebloodyDialogue>()))
                .Choice(new ItemOption(t.goosebow)
                    .IfChosen(new TriggerDialogueAction<ContentWithOwnWork>()))
                .Choice(new ItemOption(t.goosebloddybow)
                    .IfChosen(new TriggerDialogueAction<ContentWithOwnWork>()))
                // Switches
                .Choice(new ItemOption(t.switch_broken)
                    .IfChosen(new TriggerDialogueAction<CutifySwitch>()))
                .Choice(new ItemOption(t.switch_broken_horror)
                    .IfChosen(new TriggerDialogueAction<CutifySwitch>()))
                .Choice(new ItemOption(t.switch_broken_cute)
                    .IfChosen(new TriggerDialogueAction<ContentWithOwnWork>()))
                // Coins
                .Choice(new ItemOption(t.coin_start)
                    .IfChosen(new TriggerDialogueAction<CutifyCoin>()))
                .Choice(new ItemOption(t.coin_horror)
                    .IfChosen(new TriggerDialogueAction<CutifyCoin>()))
                .Choice(new ItemOption(t.coin_cute)
                    .IfChosen(new TriggerDialogueAction<ContentWithOwnWork>()))
                // Others
                .Choice(new OtherItemOption()
                    .IfChosen(new TriggerDialogueAction<otherItemDialogue>()));
        }
    }
    public class Goodbye : Dialogue {
        public Goodbye(){
            Say("Hmm... well I'll take this as a no, meow");
            Say("See you soon, *purrrrrr*");
        }
    }  

    public class bucketDialogue : Dialogue {
        public bucketDialogue(){
            Say("*hissss* I've seen nicer buckets before");
            Say("Here, I'll fix it for you...")
                .DoAfter(GiveItem(t.bucketcute));
            Say("....meow meow meow...")
                .DoAfter(new TriggerDialogueAction<ItemDialogue>());
        }
    }  

    public class CutifySwitch : Dialogue {
        public CutifySwitch() {
            Say("*hissss* These are some filthy electronics!");
            Say("Lemme fix them for you...")
                .DoAfter(GiveItem(t.switch_broken_cute));
            Say("....meow meow meow...")
                .DoAfter(new TriggerDialogueAction<ItemDialogue>());
        }
    } 

    public class CutifyCoin : Dialogue {
        public CutifyCoin() {
            Say("*hissss* That quarter smells weird!")
                .DoAfter(RemoveItem(DialogueManager.Instance.currentItem));
            Say("Let me dunk it into some syrup...")
                .DoAfter(GiveItem(t.coin_cute));
            Say("....meow meow meow...")
                .DoAfter(new TriggerDialogueAction<ItemDialogue>());
        }
    } 

    public class bucketfullDialogue : Dialogue {
        public bucketfullDialogue(){
            Say("Oh this thing is nasty, *hissss*");

            Say("Here, I'll fix it for you")
                .DoAfter(GiveItem(t.bucketcutefull));

            Say("meow.. Mhhh, if all this goo was on some people, I'd have some customers..");
            Say("....meow *sniff*...")
                .DoAfter(new TriggerDialogueAction<ItemDialogue>());
        }
    }
    public class bloodymaryDialogue : Dialogue {
        public bloodymaryDialogue(){
            Say("*Hiss*... meow... sorry, no. I'm really not thirsty.");
            //Say("But this fish I will take, *purrrrr*")
            //    .If(HasItem(t.babelfish));
            Say(" ... ")
                .DoAfter(new TriggerDialogueAction<ItemDialogue>());
        }
    }
    public class shirtToMaiddressDialogue : Dialogue {
        public shirtToMaiddressDialogue(){
            Say("Oh meow, this thing is filthy... just as I love it :3");
            Say("*purrr* I'll fix this for you meow.")
                .DoAfter(GiveItem(t.clean_maiddress))
                .DoAfter(new TriggerDialogueAction<ItemDialogue>());
        }
    }
    public class ContentWithOwnWork : Dialogue {
        public ContentWithOwnWork(){
            Say("*sigh* My own work, it's purrrfect...");
            Say("meow, Not much more to do here sadly.");
            Say("murr....")
                .DoAfter(new TriggerDialogueAction<ItemDialogue>());
        }
    }
    public class dirtyShirtToMaiddressDialogue : Dialogue {
        public dirtyShirtToMaiddressDialogue(){
            Say("Urgh.. meow... wow this is.. this is murrrr than disgusting meow");
            Say("A TRUE CHALLENGE!!!");
            Say("Give me one second");
            Say("here you go meow meow meow...")
                .DoAfter(GiveItem(t.dirty_maiddress))
                .DoAfter(new TriggerDialogueAction<ItemDialogue>());
        }
    }

    public class gooseDialogue : Dialogue {
        public gooseDialogue(){
            //Say("Urgh.. meow... wow this is.. this is murrrr than dirty meow");
            Say("Aww, she deserves to be sweeter than that!");
            Say("Give me one second");
            Say("Here you go meow meow meow...")
                .DoAfter(GiveItem(t.goosebow))
                .DoAfter(new TriggerDialogueAction<ItemDialogue>());
        }
    }

    public class goosebloodyDialogue : Dialogue {
        public goosebloodyDialogue(){
            Say("Yup, that is one dirty goose. Let me fix her for you :3");
            //Say("A TRUE CHALLENGE!!!");
            Say("Give me one second");
            Say("Here you go meow meow meow...")
                .DoAfter(GiveItem(t.goosebloddybow))
                .DoAfter(new TriggerDialogueAction<ItemDialogue>());
        }
    }

    public class otherItemDialogue : Dialogue {
        public otherItemDialogue(){
            World origin = DialogueManager.Instance.currentItem.originWorld;

            Say("Nah, that thing's already cute enough *purrrr*")
                .If(() => origin == World.Cute)
                .DoAfter(new TriggerDialogueAction<ItemDialogue>());

            Say("Even I can't make this cute, sorry to disappoint *sad meow*")
                .If(() => origin != World.Cute)
                .DoAfter(new TriggerDialogueAction<ItemDialogue>());
        }
    }

    /*
    public class babelDialogue : Dialogue {
        public babelDialogue(){
            Say("Meeeeow, *purrr* yes give me that");
            Say("chomp chomp chomp");
            Say("Delicious, thanks, meow.")
                .DoAfter(RemoveItem(t.babelfish))
                .DoAfter(new TriggerDialogueAction<ItemDialogue>());
        }
    }
    */

/* ---------------------------------------------------------------------------------------------------------------- */
    public class DefaultDialogue : Dialogue {
        public DefaultDialogue(){
            Say("Meow, hey again")
                .DoAfter(new TriggerDialogueAction<ItemDialogue>());
        }
    }
/* ---------------------------------------------------------------------------------------------------------------- */
    public class FinalDialogue : Dialogue {
        public FinalDialogue(){
            Say("meow Thanks Janitor");
            Say("*purrr* Now I can finally clean people again");
            Say("I finally have a purrrrrrrrrrrpuss again");
            Say("*hissss* Ah, what's happening...");
            Say(".....")
            .DoAfter(GiveItem(t._jimmycat_said_thanks));
            //Say("Anyway, here's wonderwall");
            Say("Anyway, if you have anything I can clean for you, just hit me up. *purrrrrr*");
        }
    }
/* ---------------------------------------------------------------------------------------------------------------- */

}
