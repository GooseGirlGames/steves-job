using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 

 */
public class JimDialogue : VoidNPCDiaTrigger {
    public Item _said_thanks;
    public Item _hasCleanedSwitch;
    public Item brokenSwitch;
    public Item cuteBrokenSwitch;
    public Item horrorBrokenSwitch;
    public Item dirtyBrokenSwitch;
    public Item finishedSwitch;
    public Item bucket;
    public Item bucketcute;
    public Item fullbucket;
    public Item fullbucketcute;
    public Item goose;
    public Item bloodygoose;
    public Item bowgoose;
    public Item bloodybowgoose;
    public Item marysPeriod;
    public static JimDialogue t;
    public override void UpdateStaticT() {
        t = this;
    }


    public override Dialogue NewFullRestoredDia() {
        if (!Inventory.Instance.HasItem(_said_thanks)) {
            return new Thanks();
        } else {
            if (Inventory.Instance.HasItem(_hasCleanedSwitch)) {
                return new IsNowUseless();
            }
            return new CleanItem();
        }
    }
    public override Dialogue NewHalfRestoredDia() => new HalfRestoredDia();
    public override Dialogue NewGoneDia() => new GoneDia();


    public class GoneDia : Dialogue {
        public GoneDia() {
            Say("...");
        }
    }
    public class HalfRestoredDia : Dialogue {
        public HalfRestoredDia() {
            Say("Mr. Steve, I don't feel so good... It's like not all of my atoms are with me...");
        }
    }
    public class Thanks : Dialogue {
        public Thanks(){
            Say("Thanks Janitor, I think I was a little muddle-headed... I think.");
            Say("...");
            Say("Anyway, now I'm back.");
            Say("Well... At least in what is left of the mall.");
            Say("If I can help your with anything, just ask. My store is back in business.")
            .Do(GiveItem(t._said_thanks));
        }
    }
    public class CleanItem : Dialogue {
        public CleanItem() {
            // Newlines within the same stetement, sue me
            Say("Should I clean something for you? And I mean for real this time, haha")
                .Choice(new TextOption("No Thanks"))

                .Choice(new ItemOption(t.dirtyBrokenSwitch)
                    .IfChosen(new TriggerDialogueAction<DirtyBrokenSwitchDialogue>()))
                .Choice(new ItemOption(t.cuteBrokenSwitch)
                    .IfChosen(new TriggerDialogueAction<DirtyBrokenSwitchDialogue>()))
                .Choice(new ItemOption(t.horrorBrokenSwitch)
                    .IfChosen(new TriggerDialogueAction<DirtyBrokenSwitchDialogue>()))

                .Choice(new ItemOption(t.finishedSwitch)
                    .IfChosen(new TriggerDialogueAction<SwitchDialogue>()))

                .Choice(new ItemOption(t.brokenSwitch)
                    .IfChosen(new TriggerDialogueAction<BrokenSwitchDialogue>()))

                .Choice(new ItemOption(t.fullbucket)
                    .IfChosen(new TriggerDialogueAction<FullBucketDialogue>()))
                .Choice(new ItemOption(t.fullbucketcute)
                    .IfChosen(new TriggerDialogueAction<FullBucketDialogue>()))

                .Choice(new ItemOption(t.bloodygoose)
                    .IfChosen(new TriggerDialogueAction<GooseDialogue>()))

                .Choice(new ItemOption(t.marysPeriod)
                    .IfChosen(new TriggerDialogueAction<BloodyMaryDialogue>()))
                    
                .Choice(new OtherItemOption()
                    .IfChosen(new TriggerDialogueAction<OtherItemCleaning>()));
        }
    }
    public class DirtyBrokenSwitchDialogue : Dialogue{
        public DirtyBrokenSwitchDialogue(){
            World from = DialogueManager.Instance.currentItem.originWorld;

            Say("Alright, let me see...")
            .If(() => from == World.Void);

            Say("Oof, that's one hell of a putrid smell, but...")
            .If(() => from == World.Horror);

            Say("Oh, that's a lot of sugar, but...")
            .If(() => from == World.Cute);

            Say("I'm pretty sure I can clean the parts for you, so they're ready to be repaired.");

            Say("There you go, clean parts. But I myself am no good at repairing stuff.")
                .Do(GiveItem(t.brokenSwitch))
                .Do(GiveItem(t._hasCleanedSwitch));
        }
    }
    public class SwitchDialogue : Dialogue{
        public SwitchDialogue(){
            Say("That thing looks perfectly fine to me.");
            Say(
                "Quick, put it to power and switch us out of this shithole, "
                + "I miss my other laundry machines."
            );
            Say("I think Steve E. should be able to help you with that.");
        }
    }
    public class BrokenSwitchDialogue : Dialogue{
        public BrokenSwitchDialogue(){
            Say("It's clean, sadly I can't do more.");
            Say("If you find a way to talk to Billie, they'll probably help you out.");
            Say("Good luck, I belive in you")
                .DoAfter(new TriggerDialogueAction<CleanItem>());    
        }
    }
    public class BloodyMaryDialogue : Dialogue{
        public BloodyMaryDialogue(){
            Say("Sorry but, no, no I really dont want to have something to do with that thing.")
                .DoAfter(new TriggerDialogueAction<CleanItem>());
        }
    }
    public class FullBucketDialogue : Dialogue{
        public FullBucketDialogue(){
            World origin = DialogueManager.Instance.currentItem.originWorld;
            Say("Oof, yes this bucket is stinky. Gimme a sec, I'll clean it for you.")
                .DoAfter(GiveItem(t.bucket))
                .If(() => origin != World.Cute);
            Say("Cute flowers, but oof that bucket is stinky. Gimme a sec, I'll clean it for you.")
                .DoAfter(GiveItem(t.bucketcute))
                .If(() => origin == World.Cute);
            Say("here you go");
        }
    }

    public class GooseDialogue : Dialogue {
        public GooseDialogue(){
            Say("Oh the poor thing, wait let me polish her for you.");
            Say("Here you go, little one.")
                .DoAfter(GiveItem(t.goose))
                .DoAfter(new TriggerDialogueAction<CleanItem>());
        }
    }
    public class IsNowUseless : Dialogue {
        public IsNowUseless() {
            Say(
                "I think the most important thing I already cleaned for you, "
                + "but I can still do something for your if you like."
            )
            .DoAfter(new TriggerDialogueAction<CleanItem>());
        }
    }

    public class OtherItemCleaning : Dialogue {
        public OtherItemCleaning() {
            string item = DialogueManager.Instance.currentItem.name;
            Say("Umm, I'm terribly sorry, but I really don't know how to clean a " + item + ".")
            .DoAfter(new TriggerDialogueAction<CleanItem>());
        }
    }
}
