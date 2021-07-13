using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Personality: Freds personality he is from Down Under

Dialog1(If Player has no shirt):
    - Hey can u fix my apron, its dirty, 
    - ur can say yes or no and apron?
    - if yes then he gives ur apron, if no he tell ur to fuck off
    - if apron? he tells ur why it is so dirty
    - your can give him the bloody bucked
    
Dialog2(If player has any kind of shirt)
    - Yo where is moi fucking shirt

Dialog3(If player has finished the dialouge, he gets Item finished):*/
    

public class FredDialogue : DialogueTrigger
{
    public Item shirt;
    public Item clean_dress;
    public Item dirty_dress;
    public Item dirty_shirt;
    public Item finished;
    public Item bucket;
    public Item bucketfull;
    public Item bucketcute;
    public Item bucketfullcute;

    public Item disgusting_cocktail;
    public static FredDialogue t = null;

    [SerializeField] private Item _fred_has_maiddress;

    /*     public void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.CompareTag("Player")) {
                Trigger();
            }
        } */

    private void Awake() {
        t = this;
    }

    public override Dialogue GetActiveDialogue(){
        if (Inventory.Instance.HasItem(shirt)
                || Inventory.Instance.HasItem(dirty_shirt)
                || Inventory.Instance.HasItem(clean_dress)
                || Inventory.Instance.HasItem(dirty_dress)
        ){
            return new FredNakedDialog();
        }
        else if (Inventory.Instance.HasItem(finished)){
            return new FredFinishedDialogue();
        }
        return new FredDefaultDialoge();
    }

/* Dialogue Classes */
/* ----------------------------------------------------------------------------------------------------------------------------------- */
    public class FredDefaultDialoge : Dialogue{
        public FredDefaultDialoge(){
            Say("Oink Mate");
            Say("Whatever its in that bloody bucket, it smells ace. anyway...")
                .If(HasItem(t.bucketfull));
            Say("Whatever its in that bloody bucket, it smells ace. anyway...")
                .If(HasItem(t.bucketfullcute));
            Say("Me bloody apron's fucked. Newly bought and already super disgusting");
            Say("Could you clean it for me, if ya know what I mean?")
            EmptySentence().DoAfter(new TriggerDialogueAction<Choose_Default>());
            Say("Bye");
        }
    }
    public class Choose_Default : Dialogue{
        public Choose_Default(){
            Say("Can ya help?")
                .Choice(
                    new TextOption("Yes")
                    .IfChosen(new TriggerDialogueAction<Yes_Default>(exitCurrent: true)))
                .Choice(
                    new TextOption("No")
                    .IfChosen(new TriggerDialogueAction<No_Default>(exitCurrent: true)))
                //.Choice(
                //    new TextOption("Apron")
                //    .IfChosen(new TriggerDialogueAction<Apron_Default>()))
                .Choice(
                    new ItemOption(t.bucketfull)
                    .IfChosen(GiveItem(t.bucket))
                    .IfChosen(new TriggerDialogueAction<Thanks_Mate>()))
                .Choice(
                    new ItemOption(t.bucketfullcute)
                    .IfChosen(GiveItem(t.bucketcute))
                    .IfChosen(new TriggerDialogueAction<Thanks_Mate>()))
                .Choice(new OtherItemOption().IfChosen(new TriggerDialogueAction<Default_Usless_Dialogue>())

                );
        }
    }

    public class Thanks_Mate : Dialogue{
        public Thanks_Mate(){
            Say("Cheers ... glug ... glug ... glug ... *hicc*");
            Say("Thanks, Mate").DoAfter(new TriggerDialogueAction<Choose_Default>());
        }
    }
    public class Yes_Default : Dialogue{
        public Yes_Default(){
            Say("Here ya go")
                .DoAfter(GiveItem(t.shirt));
            Say("Catch ya l8er, alligator");
        }
    }

    public class No_Default : Dialogue{
        public No_Default(){
            Say("Then fuck off cunt!!!");
        }
    }

    /*public class Apron_Default : Dialogue{
        public Apron_Default(){
            Say("Can't ya see, it's looks like kangaroo poo")
                .DoAfter(new TriggerDialogueAction<Choose_Default>());
        }
    }*/
    public class Default_Usless_Dialogue : Dialogue{
        public Default_Usless_Dialogue(){
            Say("This s fuckn useless")
            .DoAfter(new TriggerDialogueAction<Choose_Default>());
        }
    }

/* ----------------------------------------------------------------------------------------------------------------------------------- */

    public class FredNakedDialog : Dialogue{
        public FredNakedDialog(){
            Say("Whatever its in that bloody bucket, it smells ace. Anyway...")
                .If(HasItem(t.bucketfull));
            EmptySentence().DoAfter(new TriggerDialogueAction<FredChosenNakedDialog>());
        }
    }

    public class FredChosenNakedDialog : Dialogue{
        public FredChosenNakedDialog(){
            Say("Is the bloody apron ready? My tits freezing off!")
                .Choice(new TextOption("Maybe Later"))
                .Choice(new ItemOption(t.clean_dress)
                    .IfChosen(new TriggerDialogueAction<Naked_CleanDress_Dialogue>()))
                .Choice(new ItemOption(t.dirty_dress)
                    .IfChosen(new TriggerDialogueAction<Naked_DirtyDress_Dialogue>()))
                .Choice(new ItemOption(t.dirty_shirt)
                    .IfChosen(new TriggerDialogueAction<Naked_DirtyShirt_Dialogue>()))
                .Choice(new ItemOption(t.shirt)
                    .IfChosen(new TriggerDialogueAction<Naked_Shirt_Dialogue>(exitCurrent : true)))
                .Choice(new ItemOption(t.bucketfull)
                    .IfChosen(GiveItem(t.bucket))
                    .IfChosen(new TriggerDialogueAction<Naked_Bucked_Dialogue>()))
                .Choice(new ItemOption(t.bucketfullcute)
                    .IfChosen(GiveItem(t.bucketcute))
                    .IfChosen(new TriggerDialogueAction<Naked_Bucked_Dialogue>()))
                .Choice(new OtherItemOption().IfChosen(new TriggerDialogueAction<Naked_Usless_Dialogue>()));
        }
    }

    public class Naked_Bucked_Dialogue : Dialogue{
        public Naked_Bucked_Dialogue(){
            Say("Cheers ... glug ... glug ... glug ... *hicc*");
            Say("Thanks, Mate").DoAfter(new TriggerDialogueAction<FredChosenNakedDialog>());
        }
    }
    public class Naked_Shirt_Dialogue : Dialogue{
        public Naked_Shirt_Dialogue(){
            Say("Ey, That thinks still fucked, try harder")
            .DoAfter(new TriggerDialogueAction<FredChosenNakedDialog>());
        }
    }

    public class Naked_DirtyShirt_Dialogue : Dialogue{
        public Naked_DirtyShirt_Dialogue(){
            Say("Ah thats it, just as yucky as I love it!");
            Say("Good on ya Mate!!")
                .DoAfter(RemoveItem(t.dirty_shirt))
                .DoAfter(GiveItem(t.finished));
            Say("Alright me ‘ol cobber, for ya work here's Something from tha kitchtn");
            Say("My dishy Mary made this for ya")
                .DoAfter(GiveItem(t.disgusting_cocktail));
            Say("Goodbye krokodyle");
        }
    }

    public class Naked_DirtyDress_Dialogue : Dialogue{
        public Naked_DirtyDress_Dialogue(){
            Say("Mhh, just as disgusting as I wanted it...");
            Say("...AND it's cute. I love it!");
            Say("Good on ya Mate!!")
                .DoAfter(GiveItem(t._fred_has_maiddress))
                .DoAfter(GiveItem(t.finished))
                .DoAfter(RemoveItem(t.dirty_dress));
            Say("Alright me ‘ol cobber, for ya work here's Something from tha kitchtn");
            Say("My dishy Mary made this for ya")
                .DoAfter(GiveItem(t.disgusting_cocktail));
            Say("Goodbye krokodyle");
        }
    }

    public class Naked_CleanDress_Dialogue : Dialogue{
        public Naked_CleanDress_Dialogue(){
            Say("*UHRG* DISGUSTING! Whats wrong with ya, your fuckin wanker!");
            Say("That's gonna ruin my fucking reputation, ya cunt");
            Say("Get Stuffed!")
                .DoAfter(new TriggerDialogueAction<FredChosenNakedDialog>());
        }
    }

    public class Naked_Usless_Dialogue : Dialogue{
        public Naked_Usless_Dialogue(){
            Say("This s Fuckn useless")
                .DoAfter(new TriggerDialogueAction<FredChosenNakedDialog>());
        }
    }

/* ----------------------------------------------------------------------------------------------------------------------------------- */

    public class FinishedDialogue : Dialogue{
        public FinishedDialogue(){
            Say("Whatever its in that bloody bucket, it smelles Ace. anyway...")
                .If(HasItem(t.bucketfull));
            Say("Whatever its in that bloody bucket, it smelles Ace. anyway...")
                .If(HasItem(t.bucketfullcute));
            EmptySentence().DoAfter(new TriggerDialogueAction<FredFinishedDialogue>());
        }
    }

    public class FredFinishedDialogue : Dialogue{
        public FredFinishedDialogue(){
            Say("Oink Mate, were done, fuck off")
                .Choice(new TextOption("Ok..."))
                .Choice(new ItemOption(t.bucketfull)
                    .IfChosen(GiveItem(t.bucket))
                    .IfChosen(new TriggerDialogueAction<Bucked_Dialogue>()))
                .Choice(new ItemOption(t.bucketfullcute)
                    .IfChosen(GiveItem(t.bucketcute))
                    .IfChosen(new TriggerDialogueAction<Bucked_Dialogue>()))
                .Choice(new ItemOption(t.disgusting_cocktail)
                    .IfChosen(new TriggerDialogueAction<Cocktail_Dialogue>()))
                .Choice(new OtherItemOption().IfChosen(new TriggerDialogueAction<UselessDialogue>()));
            Say("Have a Good one!");
        }
    }

    public class UselessDialogue : Dialogue{
        public UselessDialogue(){
            Say("This is fuckn useless")
                .DoAfter(new TriggerDialogueAction<FredFinishedDialogue>());
        }
    }
    public class Bucked_Dialogue : Dialogue{
        public Bucked_Dialogue(){
            Say("Cheers... glug... glug... glug... *hicc*");
            Say("Thanks, Mate")
                .DoAfter(new TriggerDialogueAction<FredFinishedDialogue>());
        }
    }
    public class Cocktail_Dialogue : Dialogue{
        public Cocktail_Dialogue(){
            Say("Oi! Thanks but... I'll pass")
                .DoAfter(new TriggerDialogueAction<FredFinishedDialogue>());
        }
    }
}
