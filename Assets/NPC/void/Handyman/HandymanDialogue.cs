using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandymanDialogue : VoidNPCDiaTrigger {
    private static HandymanDialogue t;
    public Item _said_thanks;
    
    // Broken dirty switches.  Handyman won't dare to touch these!
    public Item switch_broken_dirty;
    public Item switch_broken_cute;
    public Item switch_broken_horror;


    // This one is fixable
    public Item switch_broken;
    public Item switch_fixed;
    public override void UpdateStaticT() {
        t = this;
    }

    public override Dialogue NewFullRestoredDia() {
        if (!Inventory.Instance.HasItem(_said_thanks)) {
            return new Thanks();
        } else {
            return new FixItem();
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
            Say("What is happening... Why can i just feel half of myself...");
        }
    }
    public class Thanks : Dialogue {
        public Thanks() {
            Say("Uhm, well I'm not used to people fixing me... but thanks a bunch!");

            Say("But hey, I'm kind of good at fixing electrical stuff, so just let me know "
                    + "if there's anything I can fix for you.")
            .DoAfter(GiveItem(t._said_thanks))
            .DoAfter(new TriggerDialogueAction<FixItem>());
        }
    }
    public class FixItem : Dialogue {
        public FixItem() {
            Say("Anything I can fix for ya?")
            .Choice(
                new TextOption("Not for now")
            )
            .Choice(
                new ItemOption(t.switch_broken)
                .IfChosen(new TriggerDialogueAction<FixSwitch>())
            )
            .Choice(
                new ItemOption(t.switch_broken_dirty)
                .IfChosen(new TriggerDialogueAction<WontFixDirtySwitch>())
            )
            .Choice(
                new ItemOption(t.switch_broken_cute)
                .IfChosen(new TriggerDialogueAction<WontFixCuteSwitch>())
            )
            .Choice(
                new ItemOption(t.switch_broken_horror)
                .IfChosen(new TriggerDialogueAction<WontFixHorrorSwitch>())
            )
            .Choice(
                new OtherItemOption()
                .IfChosen(new TriggerDialogueAction<WontFixGeneric>())
            );
        }
    }

    public class FixSwitch : Dialogue {
        public FixSwitch() {
            Say("Ah, exactly what I was trained to do!").Do(RemoveItem(t.switch_broken));
            Say(
                "Just give me a minute and you'll be able to turn the lights back on, \n" +
                "or turn on whatever that thing's supposed to turn on."
            );
            Say(
                "I mean, you still need electricity, but that's none of my business. \n" +
                "I'm a repair person, not a power generator, haha."
            );
            Say("Huh, what an interesting mechanism, never seen one of these before.");
            Say("Anyway, here you go.")
            .Do(GiveItem(t.switch_fixed));
        }
    }

    public class WontFixDirtySwitch : Dialogue {
        public WontFixDirtySwitch() {
            Say("I'm sorry, but that thing's waayyy too rusty for me to work with.");
            Say("It needs a serious cleaning first.");
        }
    }
    public class WontFixHorrorSwitch : Dialogue {
        public WontFixHorrorSwitch() {
            Say(
                "Oh god, that goo's gonna get everywhere! "+
                "Apologies, but I wouldn't even repair that thing with a ten foot screwdriver."
            );
            Say("It really needs a serious cleaning first.");
        }
    }
    public class WontFixCuteSwitch : Dialogue {
        public WontFixCuteSwitch() {
            Say(
                "Don't make me touch that! I mean it smells nice, but nope, " +
                "I'm not getting my hands all sticky"
            );
            Say("It really needs a serious cleaning first.");
        }
    }

    public class WontFixGeneric : Dialogue {
        public WontFixGeneric() {
            string name = DialogueManager.Instance.currentItem.name;
            Say("Do I really look like a " + name + " repair person to you?");
        }
    }

}