using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
Dialogue1(First):
    - The Cracked Remains of The switch,
    - Get brocken dirty switch
Dialogue2(No Power and Empty):
    - Trigger No Steve E Finished Item and broken Switch or dirty Switch or brokendirty switch or switch in inventory
    - the empty gap without switch
    - can accept switch
Dialogue3(No Power):
    - No Power here but switch(nothing happening if used)
Dialogue(Empty):
    - Dangerous, sparks are flying ect ect
    - Switch can be placed and afterwards activadet
Dialogue(Final):
    - Switch can be pressed
 */

public class SwitchDialogue : DialogueTrigger {

    public Item switch_final;
    public Item switch_broken;
    public Item switch_dirty_broken;
    public Item switch_dirty_cute_broken;
    public Item switch_dirty_horror_broken;
    public Item _powered;
    public Item _not_pickedup_with_switch;
    public static SwitchDialogue t;

    public Sprite sprite_start;
    public Sprite sprite_empty;
    public Sprite sprite_complete;
    public Sprite ava_start;
    public Sprite ava_empty;
    public Sprite ava_complete;
    new private SpriteRenderer renderer;

    public void UpdateState() {
        if(Inventory.Instance.HasItem(_not_pickedup_with_switch)) {
            avatar = ava_start;
            renderer.sprite = sprite_start;
        } else if (SteveHasAnySwitch()) {
            avatar = ava_empty;
            renderer.sprite = sprite_empty;
        } else {
            avatar = ava_complete;
            renderer.sprite = sprite_complete;
        }
    }

    private void TriggerEnding() {
        // TODO
    }

    private void Awake() {
        renderer = GetComponent<SpriteRenderer>();
        UpdateState();
    }

    public override Dialogue GetActiveDialogue() {
        UpdateState();
        SwitchDialogue.t = this;
        if (Inventory.Instance.HasItem(_not_pickedup_with_switch)){
            return new FirstDialogue();
        }

        if (SteveHasAnySwitch()) {
            if (Inventory.Instance.HasItem(_powered)){
                return new EmptyHolePower();
            }
            else {
                return new EmptyHoleNoPower();
            }
        } else {
            if (!Inventory.Instance.HasItem(_powered)){
                return new SwitchInstalledNoPower();
            } else {
                return new SwitchInstalledAndPowered();
            }
        }
    }

    public class FirstDialogue : Dialogue {
        public FirstDialogue(){
            Say("...");
            Say("......");
            Say(
                "Geez, everything that's left of that stupid switch "
                + "are some broken parts..."
            );
            Say("...with strange rust all over them.")
                .DoAfter(GiveItem(t.switch_dirty_broken));
            Say("It should probably be cleaned... And repaired...");
            Say("...And the power is out.") //, I think Steve E Wonder may have a spare generator")
                .DoAfter(RemoveItem(t._not_pickedup_with_switch));
        }
    }

    public class EmptyHoleNoPower : Dialogue{
        public EmptyHoleNoPower() {
            Say("It's not powered yet, but something might fit in here...")
            .Choice(new ItemOption(t.switch_final)
                .IfChosen(new TriggerDialogueAction<InsertSwitch>())
            )
            .Choice(new OtherItemOption()
                .IfChosen(new TriggerDialogueAction<Other>())
            );
        }

        public class Other : Dialogue {
            public Other() {
                Item item = DialogueManager.Instance.currentItem;

                Say("A " + item.name + " does not make for a good switch.")
                .If(DoesNotHaveItem(t.switch_final))
                .If(() => !t.IsBrokenSwitch(item));
                Say("You can't just throw " + item.name + " into a switch port an call it a day.")
                .If(DoesNotHaveItem(t.switch_final))
                .If(() => t.IsBrokenSwitch(item));

                Say("There must be something more fitting for here...")
                .If(HasItem(t.switch_final));
            }
        }
    }
    public class EmptyHolePower : Dialogue{
        public EmptyHolePower() {
            Say("Power's looking good... Something might fit in here...")
            .Choice(new ItemOption(t.switch_final)
                .IfChosen(new TriggerDialogueAction<InsertSwitch>())
            )
            .Choice(new OtherItemOption()
                .IfChosen(new TriggerDialogueAction<Other>())
            );
        }

        public class Other : Dialogue {
            public Other() {
                Item item = DialogueManager.Instance.currentItem;

                Say("With great power comes... Well, not a working switch, that's for sure.")
                .If(DoesNotHaveItem(t.switch_final));
                Say("And a " + item.name + " won't do the trick.")
                .If(DoesNotHaveItem(t.switch_final))
                .If(() => !t.IsBrokenSwitch(item));
                Say("And these " + item.name + " are far away from being one.")
                .If(DoesNotHaveItem(t.switch_final))
                .If(() => t.IsBrokenSwitch(item));

                Say("There must be something more fitting for here...")
                .If(HasItem(t.switch_final));
            }
        }
    }
    public class SwitchInstalledNoPower : Dialogue {
        // No switch, No power
        public SwitchInstalledNoPower(){
            Say("Power's still missing though...");
            Say("Heard something about Steve E. and a generator...");
        }
    }

    public class SwitchInstalledAndPowered : Dialogue{
        public SwitchInstalledAndPowered(){
            Say("*click*")
            .DoAfter(t.TriggerEnding);
        }
    }
    public class InsertSwitch : Dialogue {
        public InsertSwitch() {
            Say("Yup, that's it...");
            Say("Now, be careful...")
            .Do(RemoveItem(t.switch_final))
            .Do(() => t.UpdateState());

            Say("Fits like a glove.")
            .If(HasItem(t._powered))
            .DoAfter(new TriggerDialogueAction<SwitchInstalledAndPowered>());

            Say("Fits like a glove.")
            .If(DoesNotHaveItem(t._powered))
            .DoAfter(new TriggerDialogueAction<SwitchInstalledNoPower>());
        }
    }
    

    
    // utility stuff

    public bool SteveHasAnySwitch() {   
        return Inventory.Instance.HasItem(switch_broken)
            || Inventory.Instance.HasItem(switch_dirty_cute_broken)
            || Inventory.Instance.HasItem(switch_dirty_horror_broken)
            || Inventory.Instance.HasItem(switch_dirty_broken)
            || Inventory.Instance.HasItem(switch_final);
    }

    public bool IsBrokenSwitch(Item i) { // i hate this but i'm lazy
        return i == switch_dirty_cute_broken
                || i == switch_dirty_horror_broken
                || i == switch_dirty_broken
                || i == switch_broken;
    }
}