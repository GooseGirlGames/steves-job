using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 

 */
public class JimDialogue : DialogueTrigger{
    public Item _jim_horror_finished;
    public Item _jim_cute_finished;
    public Item _firstDialogueFinished;
    public Item _hasCleanedSwitch;
    public Item brokenSwitch;
    public Item dirtySwitch;
    public Item dirtyBrokenSwitch;
    public Item finishedSwitch;
    public Item bucket;
    public Item fullbucket;
    public Item goose;
    public Item bloodygoose;
    public Item marysPeriod;
    public static JimDialogue t;

    public override Dialogue GetActiveDialogue() {
        JimDialogue.t = this;
        if(Inventory.Instance.HasItem(_jim_cute_finished)&&Inventory.Instance.HasItem(_jim_horror_finished)){
            if (Inventory.Instance.HasItem(_hasCleanedSwitch)){
                return new IsNowUseless();
            }
            else if(Inventory.Instance.HasItem(_firstDialogueFinished)){
                return new DefaultDialogue();
            }
            return new RestoredDialogue();
        }
        else if(Inventory.Instance.HasItem(_jim_cute_finished)||Inventory.Instance.HasItem(_jim_horror_finished)){
            return new HalfExist();
        }
        return new NoneExist();
    }
    public class NoneExist : Dialogue{
        public NoneExist(){
            Say("...");
        }
    }
    public class HalfExist : Dialogue{
        public HalfExist(){
            Say("Ah I dont feel so good ... its like my atoms are not all with me...");
        }
    }
    public class RestoredDialogue : Dialogue{
        public RestoredDialogue(){
            Say("Thanks Janitor, I think I was a little muddle-headed ... I think");
            Say("...");
            Say("Anyway now im back");
            Say("Well... At least in what is left of the mall");
            Say("If I can help your with anything, just ask, My store is back in buissnes")
                .DoAfter(new TriggerDialogueAction<DefaultDialogue>());
        }
    }
    public class DefaultDialogue : Dialogue{
        public DefaultDialogue(){
            Say("Should I clean something for you? and I mean for real this time, haha")
                .Choice(new TextOption("No Thanks"))
                .Choice(new ItemOption(t.dirtyBrokenSwitch)
                    .IfChosen(new TriggerDialogueAction<DirtyBrokenSwitchDialogue>()))
                .Choice(new ItemOption(t.finishedSwitch)
                    .IfChosen(new TriggerDialogueAction<SwitchDialogue>()))
                .Choice(new ItemOption(t.brokenSwitch)
                    .IfChosen(new TriggerDialogueAction<BrokenSwitchDialogue>()))
                .Choice(new ItemOption(t.fullbucket)
                    .IfChosen(new TriggerDialogueAction<FullBucketDialogue>()))
                .Choice(new ItemOption(t.bloodygoose)
                    .IfChosen(new TriggerDialogueAction<GooseDialogue>()))
                .Choice(new ItemOption(t.marysPeriod)
                    .IfChosen(new TriggerDialogueAction<BloodyMaryDialogue>()));
        }
    }
    public class DirtyBrokenSwitchDialogue : Dialogue{
        public DirtyBrokenSwitchDialogue(){
            Say("Oh I see");
            Say("I'm pretty sure I can clean the parts for your so there down for reapir")
                .DoAfter(GiveItem(t.brokenSwitch));
            Say("But I myself am no got with repairing stuff");
        }
    }
    public class SwitchDialogue : Dialogue{
        public SwitchDialogue(){
            Say("That thing looks perfectly fine to me");
            Say("Quick put it to power and switch us out of this shithole, I miss my other laundry-machines");
            Say("i think Steve E. should be able to help with that.");
        }
    }
    public class BrokenSwitchDialogue : Dialogue{
        public BrokenSwitchDialogue(){
            Say("It is clean, sadly I can't do more");
            Say("If you find a way to talk to Handyman, he could probably help your out");
            Say("Good look, I belive in you")
                .DoAfter(new TriggerDialogueAction<DefaultDialogue>());    
        }
    }
    public class BloodyMaryDialogue : Dialogue{
        public BloodyMaryDialogue(){
            Say("Sorry but, No, no I really dont want to have something to do with that thing")
                .DoAfter(new TriggerDialogueAction<DefaultDialogue>());
        }
    }
    public class FullBucketDialogue : Dialogue{
        public FullBucketDialogue(){
            Say("Uff, yes this bucket is stinky, gimme a sec i clean it for you")
                .DoAfter(GiveItem(t.bucket));
            Say("here you go");
        }
    }
    public class GooseDialogue : Dialogue{
        public GooseDialogue(){
            Say("oh the poor thing, wait let me polish him for you");
            Say("here you go little one")
                .DoAfter(GiveItem(t.goose))
                .DoAfter(new TriggerDialogueAction<DefaultDialogue>());
        }
    }
    public class IsNowUseless : Dialogue{
        public IsNowUseless(){
            Say("I think the most important thing I already cleaned for you, but I can still do something for your if your like")
                .DoAfter(new TriggerDialogueAction<DefaultDialogue>());
        }
    }
}
